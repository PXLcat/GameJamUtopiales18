using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJamUtopiales
{
    public class TileGround : ModelTile
    {
        public TileGround(Vector2 currentPosition, int width, int height, Rectangle sourceRectangle) : base(currentPosition, width, height, sourceRectangle)
        {
            CurrentPosition = currentPosition;
            traversable = false;
        }
        public override void OnCollision(ICollidable other)
        {
            
        }
    }
}
