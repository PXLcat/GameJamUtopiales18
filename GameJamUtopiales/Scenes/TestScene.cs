using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using TiledSharp;
using Microsoft.Xna.Framework.Media;

namespace GameJamUtopiales
{
    class TestScene : Scene
    {
        private DrawableImage fond;
        public Map tiledMap;

        public TestScene(MainGame mG) : base(mG)
        {
            
        }

        public override void Load()
        {
            tiledMap = new Map("Content/level3.tmx");
            base.Load();
            fond = new DrawableImage(mainGame.Content.Load<Texture2D>("FOND_EXPORT"), new Vector2(960, 540));
            tiledMap.Load(mainGame, windowWidth, windowHeight);
            // TODO: use this.Content to load your game content here


            Debug.WriteLine("Load TestScene");

            player.Load(mainGame);
            player.PlayMusique();

            player.CurrentPlayerCharacter.CurrentPosition = new Vector2(200, 600);


            base.Load();
            
        }

        public override void Unload()
        {
            Debug.WriteLine("Unload TestScene");
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            List<InputType> playerInputs = Input.DefineInputs(ref oldKbState);
            

            player.CurrentPlayerCharacter.Update(playerInputs, tiledMap);
            tiledMap.Update(player.CurrentPlayerCharacter);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame.spriteBatch.Begin();
            //grid.DrawTiled(mainGame.spriteBatch, windowWidth / grid.Texture.Width +1, windowHeight / grid.Texture.Height+1);
            fond.Draw(mainGame.spriteBatch);
            tiledMap.Draw(mainGame.spriteBatch);

            player.CurrentPlayerCharacter.Draw(mainGame.spriteBatch);

            mainGame.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
