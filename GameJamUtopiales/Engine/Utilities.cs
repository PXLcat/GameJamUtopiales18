using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJamUtopiales
{
    public class Utilities
    {

        public static CollideType CheckCollision(Player actor1, ModelTile actor2) //TODO la gravité devrait passer par là aussi ?
        {
            Rectangle nextPosition = actor1.CurrentPlayerCharacter.HitBox;
            CollideType result = new CollideType();

            if (actor1.CurrentPlayerCharacter.HitBox.Intersects(actor2.HitBox))
            {
                if (actor1.CurrentPlayerCharacter.HitBox.Left > actor2.HitBox.Right)
                {
                    //collision par la gauche
                    result.collideLeft = true;
                    //result.leftCollisionDepth = actor1.Left - actor1.Right;
                }
                if (actor1.CurrentPlayerCharacter.HitBox.Right > actor2.HitBox.Left)
                {
                    //collision par la droite
                    result.collideRight = true;
                    //result.rightCollisionDepth = actor1.Right - actor1.Left;
                }
                if (actor1.CurrentPlayerCharacter.HitBox.Top > actor2.HitBox.Bottom)
                {
                    //collision par le haut
                    result.collideTop = true;
                    //result.topCollisionDepth = actor1.Top - actor1.Bottom;
                }
                if (actor1.CurrentPlayerCharacter.HitBox.Bottom > actor2.HitBox.Top)
                {
                    //collision par le bas
                    result.collideBottom = true;
                    //result.bottomCollisionDepth = actor1.Bottom - actor1.Top;
                }

            }

            return result;
        }
    }
    public class CollideType
    {
        public bool collideLeft;
        public bool collideRight;
        public bool collideTop;
        public bool collideBottom;

        public int leftCollisionDepth;
        public int rightCollisionDepth;
        public int topCollisionDepth;
        public int bottomCollisionDepth;

        public CollideType(bool collideLeft = false, bool collideRight = false, bool collideUp = false, bool collideDown = false)
        {
            this.collideLeft = collideLeft;
            this.collideRight = collideRight;
            this.collideTop = collideUp;
            this.collideBottom = collideDown;
        }



    }
}
