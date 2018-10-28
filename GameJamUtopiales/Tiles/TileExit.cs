using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJamUtopiales
{
    public class TileExit
    {
        public class TileTransform : ModelTile
        {
            CharacterMetamorphose typeMetamorphose;

            public TileTransform(Vector2 currentPosition, int width, int height, CharacterMetamorphose typeMetamorphose) : base(currentPosition, width, height)
            {
                CurrentPosition = currentPosition;
                traversable = true;
                this.typeMetamorphose = typeMetamorphose;
            }
            public override void OnCollision(ICollidable other)
            {

            }

        }
    }
}
