﻿using System;
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
    class TestScene : Scene
    {
        private SpriteFont consolas;
        private DrawableImage grid;
        private DrawableImage barrel;
        private List<CollidableObject> listCollidable = new List<CollidableObject>();

        public Map tiledMap = new Map();

        public TestScene(MainGame mG) : base(mG)
        {
            
        }

        public override void Load()
        {
            base.Load();
            tiledMap.Load(mainGame, windowWidth, windowHeight);
            // TODO: use this.Content to load your game content here

            Debug.WriteLine("Load TestScene");
            consolas = mainGame.Content.Load<SpriteFont>("Consolas");

            player.Load(mainGame);
            player.CurrentPlayerCharacter.CurrentPosition = new Vector2(300, 300);
            grid = new DrawableImage(mainGame.Content.Load<Texture2D>("grid"), Vector2.Zero);

            //listCollidable.Add(new CollidableObject(mainGame.Content.Load<Texture2D>("tileproto1"), new Vector2(100, 400)));

            barrel = new DrawableImage(mainGame.Content.Load<Texture2D>("barrel"), new Vector2(200, 200));
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

            tiledMap.Draw(mainGame.spriteBatch);

            //foreach (CollidableObject cObject in listCollidable)
            //{
            //    cObject.Draw(mainGame.spriteBatch);
            //}
            barrel.Draw(mainGame.spriteBatch);
            player.CurrentPlayerCharacter.Draw(mainGame.spriteBatch);

            mainGame.spriteBatch.DrawString(consolas, "test police", Vector2.Zero, Color.White);
            mainGame.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
