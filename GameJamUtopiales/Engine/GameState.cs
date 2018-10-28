using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJamUtopiales
{
    public class Gamestate
    {
        public enum SceneType
        {
            MENU,
            TEST,
            BATTLE,
            GAMEOVER,
            FIN
        }

        protected MainGame mainGame;
        public Scene CurrentScene { get; set; }

        public Gamestate(MainGame mG)
        {
            mainGame = mG;
        }

        public void ChangeScene(SceneType sT)
        {
            if (CurrentScene != null)
            {
                CurrentScene.Unload();
                CurrentScene = null;
            }

            switch (sT)
            {
                case SceneType.MENU:
                    CurrentScene = new MenuScene(mainGame);
                    break;
                case SceneType.TEST:
                    CurrentScene = new TestScene(mainGame);
                    break;
                case SceneType.BATTLE:
                    //CurrentScene = new BattleScene(mainGame);
                    break;
                case SceneType.GAMEOVER:
                    break;
                case SceneType.FIN:
                    CurrentScene = new FinScene(mainGame);
                    break;
                default:
                    break;
            }

            CurrentScene.Load();
        }
    }
}
