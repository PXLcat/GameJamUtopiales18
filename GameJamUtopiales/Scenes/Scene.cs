using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameJamUtopiales.Gamestate;

namespace GameJamUtopiales
{
    abstract public class Scene
    {
        protected MainGame mainGame;
        public KeyboardState oldKbState;
        public int windowWidth;
        public int windowHeight;
        public Factory factory;
        public Player player;

        public Scene(MainGame mG)
        {
            mainGame = mG;
        }

        public virtual void Load()
        {
            //windowWidth = mainGame.GraphicsDevice.DisplayMode.Width; //Attention, c'est la taille de l'écran, pas de la fenêtre
            //windowHeight = mainGame.GraphicsDevice.DisplayMode.Height;

            factory = Factory.Instance;
            factory.SetMainGame(mainGame);
            player = Player.Instance;

            windowWidth = mainGame.GraphicsDevice.Viewport.Bounds.Width; 
            windowHeight = mainGame.GraphicsDevice.Viewport.Bounds.Height;
            Debug.WriteLine("Window width = "+ windowWidth + ", window height = "+ windowHeight);
        }

        public virtual void Unload()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime)
        {

        }
        public void goToScene(MainGame mG,SceneType sT)
        {
            mG.gameState.ChangeScene(sT);
        }
    }
}
