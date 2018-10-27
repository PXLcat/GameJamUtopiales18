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
    class TestScene : Scene
    {
        private SpriteFont consolas;
        private DrawableImage grid;
        private DrawableImage barrel;
        private Character mami;
        private List<CollidableObject> listCollidable = new List<CollidableObject>();

        TmxMap map;
        Texture2D tileset;
        int tileWidth;
        int tileHeight;
        int mapWidth;
        int mapHeight;
        int tilesetLines;
        int tilesetColumns;

        public TestScene(MainGame mG) : base(mG)
        {
            
        }

        public override void Load()
        {
            base.Load();

            // TODO: use this.Content to load your game content here
            map = new TmxMap("Content/tiled.tmx");
            tileset = mainGame.Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());

            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;

            mapWidth = map.Width;
            mapHeight = map.Height;

            tilesetColumns = tileset.Width / tileWidth;
            tilesetLines = tileset.Height / tileHeight;

            Debug.WriteLine("Load TestScene");
            consolas = mainGame.Content.Load<SpriteFont>("Consolas");

            mami = factory.CreateCharacter(CharacterName.MAMI);
            mami.CurrentPosition = new Vector2(100, 100);
            grid = new DrawableImage(mainGame.Content.Load<Texture2D>("grid"), Vector2.Zero);
            listCollidable.Add(new CollidableObject(mainGame.Content.Load<Texture2D>("tileproto1"), new Vector2(100, 400)));

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
            mami.Update(playerInputs, listCollidable);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame.spriteBatch.Begin();
            grid.DrawTiled(mainGame.spriteBatch, windowWidth / grid.Texture.Width +1, windowHeight / grid.Texture.Height+1);
            mainGame.spriteBatch.DrawString(consolas, "test police", Vector2.Zero, Color.White);

            //tiledDraw
            int nbLayers = map.Layers.Count;

            int line;
            int column;

            for (int nLayer = 0; nLayer < nbLayers; nLayer++)
            {
                line = 0;
                column = 0;

                for (int i = 0; i < map.Layers[nLayer].Tiles.Count; i++)
                {
                    int gid = map.Layers[nLayer].Tiles[i].Gid;

                    if (gid != 0)
                    {
                        int tileFrame = gid - 1;
                        int tilesetColumn = tileFrame % tilesetColumns;
                        int tilesetLine = (int)Math.Floor((double)tileFrame / (double)tilesetColumns);

                        float x = column * tileWidth;
                        float y = line * tileHeight;

                        Rectangle tilesetRec = new Rectangle(tileWidth * tilesetColumn, tileHeight * tilesetLine, tileWidth, tileHeight);

                        mainGame.spriteBatch.Draw(tileset, new Vector2(x, y), tilesetRec, Color.White);
                    }
                    column++;
                    if (column == mapWidth)
                    {
                        column = 0;
                        line++;
                    }
                }
            }

            foreach (CollidableObject cObject in listCollidable)
            {
                cObject.Draw(mainGame.spriteBatch);
            }
            barrel.Draw(mainGame.spriteBatch);
            mami.Draw(mainGame.spriteBatch);
            mainGame.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
