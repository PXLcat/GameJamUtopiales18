using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJamUtopiales
{
    public class TileObstacleFantome : ModelTile
    {
        public TileObstacleFantome(Vector2 currentPosition, int width, int height, TileType tileType, CollideType collideSides) : base(currentPosition, width, height, tileType, collideSides)
        {
            CurrentPosition = currentPosition;
            traversablePourHumain = true;
            traversablePourFantome = false;
        }
        public override void OnCollision(ICollidable other)
        {
            Console.WriteLine("TileObstacleFantome:OnCollision");
        }
    }
}
