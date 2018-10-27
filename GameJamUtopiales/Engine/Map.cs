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
        TmxMap map;
        Texture2D tileset;
        int tileWidth;
        int tileHeight;
        int mapWidth;
        int mapHeight;
        int tilesetLines;
        int tilesetColumns;

        public List<CollidableObject> layerPlayer = new List<CollidableObject>(); //(ce qui est au même niveau que les Character avec lesquels y'a des collisions quoi)

        public Map()
        {

        }

        public void Load(MainGame mainGame) {
            map = new TmxMap("Content/tiled.tmx");
            tileset = mainGame.Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());

            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;

            mapWidth = map.Width;
            mapHeight = map.Height;

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
                            layerPlayer.Add(new CollidableObject(tileset, new Vector2(x, y), tilesetRec));
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

        public void Draw(SpriteBatch sb) {
            foreach (var item in layerPlayer)
            {
                item.Draw(sb);
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
