using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJamUtopiales
{
    public class TileTransform : ModelTile
    {
        CharacterMetamorphose typeMetamorphose;

        public TileTransform(Vector2 currentPosition, Rectangle sourceRectangle, int width, int height, CharacterMetamorphose typeMetamorphose)
            : base(currentPosition, sourceRectangle, width, height)
        {
            CurrentPosition = currentPosition;
            traversablePourHumain = true;
            traversablePourFantome = true;
            this.typeMetamorphose = typeMetamorphose;
        }
        public override void OnCollision(ICollidable other)
        {
            switch (typeMetamorphose)
            {
                case CharacterMetamorphose.HUMAN:
                    Player.Instance.SwitchCharacter(CharacterMetamorphose.HUMAN);
                    break;
                case CharacterMetamorphose.HULK:
                    Player.Instance.SwitchCharacter(CharacterMetamorphose.HULK);
                    break;
                case CharacterMetamorphose.FOETUS:
                    Player.Instance.SwitchCharacter(CharacterMetamorphose.FOETUS);
                    break;
                case CharacterMetamorphose.SPIRIT:
                    Player.Instance.SwitchCharacter(CharacterMetamorphose.SPIRIT);
                    break;
                default:
                    break;
            }
        }

    }
}
