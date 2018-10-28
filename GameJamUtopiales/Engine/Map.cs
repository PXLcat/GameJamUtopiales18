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


        TmxMap mapData;
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
            mapData = new TmxMap("Content/level1.tmx");
            tileset = mainGame.Content.Load<Texture2D>(mapData.Tilesets[0].Name.ToString());

            windowWidth = pWindowWidth;
            windowHeight = pWindowHeight;

            tileWidth = mapData.Tilesets[0].TileWidth;
            tileHeight = mapData.Tilesets[0].TileHeight;

            mapWidth = mapData.Width;
            mapHeight = mapData.Height;

            maxScrollX = mapData.Width * tileWidth - windowWidth;
            maxScrollY = mapData.Height * tileHeight - windowHeight;

            tilesetColumns = tileset.Width / tileWidth;
            tilesetLines = tileset.Height / tileHeight;

            Debug.Write(mapData.Layers.Count);

            FillLayerPlayer();

        }

        private void FillLayerPlayer() {
            int line=0;
            int column=0;

            for (int i = 0; i < mapData.Layers[0].Tiles.Count; i++) //le layer Player correspond au 1
            {
                int gid = mapData.Layers[0].Tiles[i].Gid;
                TileType tileType = (TileType)gid;

                if (gid != 0)
                {
                    int tileFrame = gid - 1;
                    int tilesetColumn = tileFrame % tilesetColumns;
                    int tilesetLine = (int)Math.Floor((double)tileFrame / (double)tilesetColumns);

                    float x = column * tileWidth;
                    float y = line * tileHeight;

                    Rectangle tilesetRec = new Rectangle(tileWidth * tilesetColumn, tileHeight * tilesetLine, tileWidth, tileHeight);

                    //case TileType.NOTHING:
                    //    break;
                    //case TileType.TERRE:
                    //    layerPlayer.Add(new CollidableObject(new Vector2(x, y), tileWidth, tileWidth));
                    //    break;
                    //case TileType.EXIT:
                    //    break;
                    //case TileType.PIEDESTAL:
                    //    break;
                    //default:
                    //    break;

                    switch (tileType)
                    {
                        case TileType.NOTHING:
                            break;
                        case TileType.GRASSCORNUPRIGHT:
                            layerPlayer.Add(new TileGround(new Vector2(x, y), tileWidth, tileWidth));
                            break;
                        case TileType.GRASSCORNUPLEFT:
                            layerPlayer.Add(new TileGround(new Vector2(x, y), tileWidth, tileWidth));
                            break;
                        case TileType.GROUNDCORNUPRIGHT:
                            layerPlayer.Add(new TileGround(new Vector2(x, y), tileWidth, tileWidth));
                            break;
                        case TileType.GROUNDCORNUPLEFT:
                            layerPlayer.Add(new TileGround(new Vector2(x, y), tileWidth, tileWidth));
                            break;
                        case TileType.GHOSTPARTICLE:
                            break;
                        case TileType.GHOSTWALL:
                            break;
                        case TileType.WALLRIGHT:
                            layerPlayer.Add(new TileGround(new Vector2(x, y), tileWidth, tileWidth));
                            break;
                        case TileType.WALLLEFT:
                            layerPlayer.Add(new TileGround(new Vector2(x, y), tileWidth, tileWidth));
                            break;
                        case TileType.UNDERGROUND:
                            layerPlayer.Add(new TileGround(new Vector2(x, y), tileWidth, tileWidth));
                            break;
                        case TileType.FLOOR:
                            layerPlayer.Add(new TileGround(new Vector2(x, y), tileWidth, tileWidth));
                            break;
                        case TileType.RONCEUP:
                            break;
                        case TileType.RONCEDOWN:
                            break;
                        case TileType.GRASSCORNDOWNRIGHT:
                            layerPlayer.Add(new TileGround(new Vector2(x, y), tileWidth, tileWidth));
                            break;
                        case TileType.GRASSCORNDOWNLEFT:
                            layerPlayer.Add(new TileGround(new Vector2(x, y), tileWidth, tileWidth));
                            break;
                        case TileType.GROUNDCORNDOWNRIGHT:
                            layerPlayer.Add(new TileGround(new Vector2(x, y), tileWidth, tileWidth));
                            break;
                        case TileType.GROUNDCORNDOWNLEFT:
                            layerPlayer.Add(new TileGround(new Vector2(x, y), tileWidth, tileWidth));
                            break;
                        case TileType.ROOF:
                            layerPlayer.Add(new TileGround(new Vector2(x, y), tileWidth, tileWidth));
                            break;
                        case TileType.BLANK1:
                            break;
                        case TileType.MUTH1:
                            break;
                        case TileType.MUTH2:
                            break;
                        case TileType.MUTB1:
                            break;
                        case TileType.MUTB2:
                            break;
                        case TileType.MUTF1:
                            break;
                        case TileType.MUTF2:
                            break;
                        case TileType.MUTH3:
                            break;
                        case TileType.MUTH4:
                            break;
                        case TileType.MUTB3:
                            break;
                        case TileType.MUTB4:
                            break;
                        case TileType.MUTF3:
                            break;
                        case TileType.MUTF4:
                            break;
                        case TileType.MUTH5:
                            break;
                        case TileType.MUTH6:
                            break;
                        case TileType.MUTB5:
                            break;
                        case TileType.MUTB6:
                            break;
                        case TileType.MUTF5:
                            break;
                        case TileType.MUTF6:
                            break;
                        case TileType.EXIT1:
                            break;
                        case TileType.EXIT2:
                            break;
                        case TileType.ROCK1:
                            break;
                        case TileType.ROCK2:
                            break;
                        case TileType.ROCK3:
                            break;
                        case TileType.UNDERGROUND2:
                            break;
                        case TileType.EXIT3:
                            break;
                        case TileType.EXIT4:
                            break;
                        case TileType.ROCK4:
                            break;
                        case TileType.ROCK5:
                            break;
                        case TileType.ROCK6:
                            break;
                        case TileType.UNDERGROUND3:
                            break;
                        case TileType.EXIT5:
                            break;
                        case TileType.EXIT6:
                            break;
                        case TileType.ROCK7:
                            break;
                        case TileType.ROCK8:
                            break;
                        case TileType.ROCK9:
                            break;
                        case TileType.BLANK2:
                            break;
                        case TileType.CANNONRIGHT1:
                            break;
                        case TileType.CANNONRIGHT2:
                            break;
                        case TileType.CORNERSINGLELEFT:
                            break;
                        case TileType.CORNERSINGLERIGHT:
                            break;
                        case TileType.CANNONLEFT1:
                            break;
                        case TileType.CANNONLEFT2:
                            break;
                        case TileType.CANNONRIGHT3:
                            break;
                        case TileType.CANNONRIGHT4:
                            break;
                        case TileType.BLANK3:
                            break;
                        case TileType.BLANK4:
                            break;
                        case TileType.CANNONLEFT3:
                            break;
                        case TileType.CANNONLEFT4:
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

            int nbLayers = mapData.Layers.Count;

            int line;
            int column;

            for (int nLayer = 0; nLayer < nbLayers; nLayer++)
            {
                line = 0;
                column = 0;

                for (int i = 0; i < mapData.Layers[nLayer].Tiles.Count; i++)
                {
                    int gid = mapData.Layers[nLayer].Tiles[i].Gid;

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
        GRASSCORNUPRIGHT = 1,
        GRASSCORNUPLEFT = 2,
        GROUNDCORNUPRIGHT = 3,
        GROUNDCORNUPLEFT = 4,
        GHOSTPARTICLE = 5,
        GHOSTWALL = 6,
        WALLRIGHT = 7,
        WALLLEFT = 8,
        UNDERGROUND = 9,
        FLOOR = 10,
        RONCEUP = 11,
        RONCEDOWN = 12,
        GRASSCORNDOWNRIGHT = 13,
        GRASSCORNDOWNLEFT = 14,
        GROUNDCORNDOWNRIGHT = 15,
        GROUNDCORNDOWNLEFT = 16,
        ROOF = 17,
        BLANK1 = 18,
        MUTH1 = 19,
        MUTH2 = 20,
        MUTB1 = 21,
        MUTB2 = 22,
        MUTF1 = 23,
        MUTF2 = 24,
        MUTH3 = 25,
        MUTH4 = 26,
        MUTB3 = 27,
        MUTB4 = 28,
        MUTF3 = 29,
        MUTF4 = 30,
        MUTH5 = 31,
        MUTH6 = 32,
        MUTB5 = 33,
        MUTB6 = 34,
        MUTF5 = 35,
        MUTF6 = 36,
        EXIT1 = 37,
        EXIT2 = 38,
        ROCK1 = 39,
        ROCK2 = 40,
        ROCK3 = 41,
        UNDERGROUND2 = 42,
        EXIT3 = 43,
        EXIT4 = 44,
        ROCK4 = 45,
        ROCK5 = 46,
        ROCK6 = 47,
        UNDERGROUND3 = 48,
        EXIT5 = 49,
        EXIT6 = 50,
        ROCK7 = 51,
        ROCK8 = 52,
        ROCK9 = 53,
        BLANK2 = 54,
        CANNONRIGHT1 = 55,
        CANNONRIGHT2 = 56,
        CORNERSINGLELEFT = 57,
        CORNERSINGLERIGHT = 58,
        CANNONLEFT1 = 59,
        CANNONLEFT2 = 60,
        CANNONRIGHT3 = 61,
        CANNONRIGHT4 = 62,
        BLANK3 = 63,
        BLANK4 = 64,
        CANNONLEFT3  = 65,
        CANNONLEFT4 = 66
    }
}
