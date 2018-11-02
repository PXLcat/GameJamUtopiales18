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
        public static CollideType CheckCollision(Player actor1, ModelTile actor2) // Toujours gérer la collision après tous les mouvements
        {
            CollideType result = new CollideType();
            Rectangle playerHitbox = actor1.CurrentPlayerCharacter.HitBox;

            if (playerHitbox.Intersects(actor2.HitBox))
            {
                actor2.OnCollision(actor1.CurrentPlayerCharacter);

                // On calcule les rapports de distance pour chaque point du player et le centre du tile en X et en Y
                int distanceTopY = actor2.HitBox.Center.Y - playerHitbox.Top;
                int distanceLeftX = actor2.HitBox.Center.X - playerHitbox.Left;
                int distanceRightX = actor2.HitBox.Center.X - playerHitbox.Right;
                int distanceBottomY = actor2.HitBox.Center.Y - playerHitbox.Bottom;

                // On calcule les distances des vecteurs entre chaque point à partir des calculs du dessus
                int distanceVectorTopLeft = (int)(Math.Pow(distanceLeftX, 2) + Math.Pow(distanceTopY, 2));
                int distanceVectorTopRight = (int)(Math.Pow(distanceRightX, 2) + Math.Pow(distanceTopY, 2));
                int distanceVectorBottomLeft = (int)(Math.Pow(distanceLeftX, 2) + Math.Pow(distanceBottomY, 2));
                int distanceVectorBottomRight = (int)(Math.Pow(distanceRightX, 2) + Math.Pow(distanceBottomY, 2));

                // On sélectionne celle qui est la plus proche du centre de la tuile touchée
                int closerDistance = Math.Min(distanceVectorTopLeft, distanceVectorTopRight);
                closerDistance = Math.Min(closerDistance, distanceVectorBottomLeft);
                closerDistance = Math.Min(closerDistance, distanceVectorBottomRight);

                if (closerDistance == distanceVectorTopLeft)
                {
                    // Si la distance absolue en X du point en haut à gauche est
                    // plus proche du centre de la tuile que sa distance en Y
                    // alors le player touche par son côté haut sinon, il touche par son côté gauche
                    if (Math.Abs(distanceLeftX) < Math.Abs(distanceTopY))
                    {
                        result.collideTop = true;
                        result.topCollisionPosition = actor2.HitBox.Top;
                    }
                    else
                    {
                        result.collideLeft = true;
                        result.leftCollisionPosition = actor2.HitBox.Left;
                    }
                }
                else if (closerDistance == distanceVectorTopRight)
                {
                    if (Math.Abs(distanceRightX) < Math.Abs(distanceTopY))
                    {
                        result.collideTop = true;
                        result.topCollisionPosition = actor2.HitBox.Top;
                    }
                    else
                    {
                        result.collideRight = true;
                        result.rightCollisionPosition = actor2.HitBox.Right;
                    }
                }
                else if (closerDistance == distanceVectorBottomLeft)
                {
                    if (Math.Abs(distanceLeftX) < Math.Abs(distanceBottomY))
                    {
                        result.collideBottom = true;
                        result.bottomCollisionPosition = actor2.HitBox.Bottom;
                    }
                    else
                    {
                        result.collideLeft = true;
                        result.leftCollisionPosition = actor2.HitBox.Left;
                    }
                }
                else if (closerDistance == distanceVectorBottomRight)
                {
                    if (Math.Abs(distanceRightX) < Math.Abs(distanceBottomY))
                    {
                        result.collideBottom = true;
                        result.bottomCollisionPosition = actor2.HitBox.Bottom;
                    }
                    else
                    {
                        result.collideRight = true;
                        result.rightCollisionPosition = actor2.HitBox.Right;
                    }
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

        public float leftCollisionPosition = float.NegativeInfinity;
        public float rightCollisionPosition = float.PositiveInfinity;
        public float topCollisionPosition = float.NegativeInfinity;
        public float bottomCollisionPosition = float.PositiveInfinity;

        public CollideType(bool collideLeft = false, bool collideRight = false, bool collideTop = false, bool collideBottom = false)
        {
            this.collideLeft = collideLeft;
            this.collideRight = collideRight;
            this.collideTop = collideTop;
            this.collideBottom = collideBottom;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (collideLeft||collideRight||collideTop||collideBottom)
            {
                sb.Append("Collisions: ");
                if (collideLeft)
                {
                    sb.Append("left ");
                }
                if (collideRight)
                {
                    sb.Append("right ");
                }
                if (collideTop)
                {
                    sb.Append("top ");
                }
                if (collideBottom)
                {
                    sb.Append("bottom");
                }
            }
            else
            {
                sb.Append("No collision");
            }
            return sb.ToString();
        }

    }
}
