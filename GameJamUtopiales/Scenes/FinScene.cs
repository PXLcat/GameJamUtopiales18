using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJamUtopiales
{
    class FinScene : Scene
    {
        private DrawableImage fond;

        public FinScene(MainGame mG) : base(mG) 
        {

        }

        public override void Load()
        {
            base.Load();
            fond = new DrawableImage(mainGame.Content.Load<Texture2D>("fin_v0"), new Vector2(960,540));
        }

        public override void Unload()
        {
            Debug.WriteLine("Unload Menu");
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            List<InputType> playerInputs = Input.DefineInputs(ref oldKbState);

            if (playerInputs.Contains(InputType.START))
            {
                mainGame.gameState.ChangeScene(Gamestate.SceneType.TEST);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame.spriteBatch.Begin();
            fond.Draw(mainGame.spriteBatch);

            mainGame.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
