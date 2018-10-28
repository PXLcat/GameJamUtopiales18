using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace GameJamUtopiales
{
    public class Map
    {
        int maxScrollX;
        int maxScrollY;

        TmxMap map;
        public static Texture2D tileset;
        int tileWidth;
        int tileHeight;
        int mapWidth;
        int mapHeight;
        int tilesetLines;
        int tilesetColumns;

        int windowWidth;
        int windowHeight;

        public int ScrollX { get; private set; }
        public int ScrollY { get; private set; }

        public List<ModelTile> layerPlayer = new List<ModelTile>(); //(ce qui est au même niveau que les Character avec lesquels y'a des collisions quoi)

        public Map()
        {

        }

        public void Load(MainGame mainGame, int pWindowWidth, int pWindowHeight)
        {
            map = new TmxMap("Content/tiled.tmx");
            tileset = mainGame.Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());

            windowWidth = pWindowWidth;
            windowHeight = pWindowHeight;

            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;

            mapWidth = map.Width;
            mapHeight = map.Height;

            maxScrollX = map.Width * tileWidth - windowWidth;
            maxScrollY = map.Height * tileHeight - windowHeight;

            tilesetColumns = tileset.Width / tileWidth;
            tilesetLines = tileset.Height / tileHeight;

            Debug.Write(map.Layers.Count);

            FillLayerPlayer();

        }

        private void FillLayerPlayer() {
            int line=0;
            int column=0;

            for (int i = 0; i < map.Layers[1].Tiles.Count; i++) //le layer Player correspond au 1
            {
                int gid = map.Layers[1].Tiles[i].Gid;
                TileType tileType = (TileType)gid;

                if (gid != 0)
                {
                    int tileFrame = gid - 1;
                    int tilesetColumn = tileFrame % tilesetColumns;
                    int tilesetLine = (int)Math.Floor((double)tileFrame / (double)tilesetColumns);

                    float x = column * tileWidth;
                    float y = line * tileHeight;

                    Rectangle tilesetRec = new Rectangle(tileWidth * tilesetColumn, tileHeight * tilesetLine, tileWidth, tileHeight);

                    switch (tileType)
                    {
                        case TileType.NOTHING:
                            break;
                        case TileType.TERRE:
                            layerPlayer.Add(new TileGround(new Vector2(x, y), tileWidth, tileWidth, tilesetRec));
                            break;
                        case TileType.EXIT:
                            break;
                        case TileType.PIEDESTAL:
                            break;
                        default:
                            break;
                    }

                    
                }

                column++;
                if (column == mapWidth)
                {
                    column = 0;
                    line++;
                }
            }
        }

        public void Update(Character player)
        {
            Vector2 currentMamiPosition = player.CurrentPosition;

            // X Direction Scroll
            ScrollX -= (int)(windowWidth * 0.5f - currentMamiPosition.X);

            if (ScrollX < 0)
            {
                Console.WriteLine("Limit Scroll X LEFT");
                ScrollX = 0;
            }
            else if (ScrollX > maxScrollX)
            {
                Console.WriteLine("Limit Scroll X RIGHT");
                ScrollX = maxScrollX;
            }
            else
                currentMamiPosition.X = windowWidth * 0.5f;

            // Y Direction Scroll
            ScrollY -= (int)(windowHeight * 0.5f - currentMamiPosition.Y);

            // On teste si on arrive aux bords de la map
            if (ScrollY < 0)
                ScrollY = 0;
            else if (ScrollY > maxScrollY)
                ScrollY = maxScrollY;
            else
                currentMamiPosition.Y = windowHeight * 0.5f;

            // On teste si le personnage touche les bords de l'écran
            if (currentMamiPosition.X + player.HitBox.Width * 0.5f >= windowWidth)
                currentMamiPosition.X = (int)(windowWidth - player.HitBox.Width * 0.5f);
            else if (currentMamiPosition.X - player.HitBox.Width * 0.5f <= 0)
                currentMamiPosition.X = (int)(player.HitBox.Width * 0.5f);

            player.CurrentPosition = currentMamiPosition;
        }

        public void Draw(SpriteBatch sb)
        {
            //foreach (var item in layerPlayer)
            //{
            //    item.Draw(sb);
            //}

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

                        float x = column * tileWidth - ScrollX;
                        float y = line * tileHeight - ScrollY;

                        Rectangle tilesetRec = new Rectangle(tileWidth * tilesetColumn, tileHeight * tilesetLine, tileWidth, tileHeight);

                        sb.Draw(tileset, new Vector2(x, y), tilesetRec, Color.White);
                    }
                    column++;
                    if (column == mapWidth)
                    {
                        column = 0;
                        line++;
                    }
                }
            }
        }
    }
    public class Tile {

    }
    public enum TileType
    {
        NOTHING = 0,
        TERRE = 1,
        EXIT = 2,
        PIEDESTAL = 3

    }
}
