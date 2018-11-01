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

namespace GameJamUtopiales
{
    public class Level1 : Scene
    {
        private DrawableImage fond;
        public Map tiledMap;

        public Level1(MainGame mG) : base(mG)
        {
        }

        public override void Load()
        {
            tiledMap = new Map("Content/level1.tmx");
            base.Load();
            fond = new DrawableImage(mainGame.Content.Load<Texture2D>("FOND_EXPORT"), new Vector2(960, 540));
            tiledMap.Load(mainGame, windowWidth, windowHeight);
            // TODO: use this.Content to load your game content here

            Debug.WriteLine("Load TestScene");

            player.Load(mainGame);
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
            fond.Draw(mainGame.spriteBatch);
            tiledMap.Draw(mainGame.spriteBatch);

            player.CurrentPlayerCharacter.Draw(mainGame.spriteBatch);

            mainGame.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
