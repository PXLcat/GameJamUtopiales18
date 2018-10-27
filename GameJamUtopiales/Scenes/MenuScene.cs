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
        public MenuScene(MainGame mG) : base(mG) 
        {

        }

        public override void Load()
        {
            Debug.WriteLine("Load Menu");
            base.Load();
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
