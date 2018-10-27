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
    class MenuScene : Scene
    {
        private DrawableImage fond;

        public MenuScene(MainGame mG) : base(mG) 
        {

        }

        public override void Load()
        {
            base.Load();
            fond = new DrawableImage(mainGame.Content.Load<Texture2D>("grid"), Vector2.Zero);
        }

        public override void Unload()
        {
            Debug.WriteLine("Unload Menu");
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame.spriteBatch.Begin();

            
            mainGame.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
