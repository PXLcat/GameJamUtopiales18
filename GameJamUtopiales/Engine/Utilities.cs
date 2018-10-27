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

        public static CollideType CheckCollision(Rectangle actor1, Rectangle actor2) //TODO la gravité devrait passer par là aussi ?
        {
            CollideType result = new CollideType();
            if ((actor1.Intersects(actor2)) && actor1.Left > actor1.Right)
            {
                //collision par la gauche
                result.collideLeft = true;
                result.leftCollisionDepth = actor1.Left - actor1.Right;
            }
            if ((actor1.Intersects(actor2)) && actor1.Right > actor2.Left)
            {
                //collision par la droite
                result.collideRight = true;
                result.rightCollisionDepth = actor1.Right - actor1.Left;
            }
            if ((actor1.Intersects(actor2)) && actor1.Top > actor2.Bottom)
            {
                //collision par le haut
                result.collideTop = true;
                result.topCollisionDepth = actor1.Top - actor1.Bottom;
            }
            if ((actor1.Intersects(actor2)) && actor1.Bottom > actor2.Top)
            {
                //collision par le bas
                result.collideBottom = true;
                result.bottomCollisionDepth = actor1.Bottom - actor1.Top;
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
