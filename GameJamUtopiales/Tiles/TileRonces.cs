using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJamUtopiales
{
    public class TileRonces : ModelTile
    {
        public TileRonces(Vector2 currentPosition, Rectangle sourceRectangle, int width, int height)
            : base(currentPosition, sourceRectangle, width, height)
        {
            CurrentPosition = currentPosition;
            traversablePourHumain = false;
        }
        public override void OnCollision(ICollidable other)
        {
            Player.Instance.SwitchCharacter(CharacterMetamorphose.SPIRIT);
        }
    }
}
