using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJamUtopiales
{
    public class CollidableObject : DrawableImage
    {
        Rectangle sourceRectangle;
        bool isCroppedTexture = false; 

        /// <summary>
        /// Constructeur à utiliser lorsque la texture est la surface entière à dessinner
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="currentPosition"></param>
        public CollidableObject(Texture2D texture, Vector2 currentPosition) : base(texture, currentPosition)
        {

        }

        /// <summary>
        /// Constructeur à utiliser lorsque la surface à dessinner n'est qu'une partie de la texture
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="currentPosition"></param>
        public CollidableObject(Texture2D texture, Vector2 currentPosition, Rectangle sourceRectangle) : base(texture, currentPosition)
        {
            isCroppedTexture = true;
            this.sourceRectangle = sourceRectangle;
        }

        public Rectangle HitBox { get => new Rectangle((int)CurrentPosition.X- Texture.Width/2, (int)CurrentPosition.Y- Texture.Height/2,
            Texture.Width, Texture.Height); }

        public void OnCollision(ICollidable other)
        {
            Debug.Write("Collision entre " + this.Texture.Name + " et " + other.ToString());
        }
        public override void Draw(SpriteBatch sb, bool horizontalFlip = false) {
            if(isCroppedTexture)
                sb.Draw(Texture, new Vector2(CurrentPosition.X, CurrentPosition.Y), sourceRectangle, Color.White);
            else
                base.Draw(sb, horizontalFlip);
//#if DEBUG
//            Texture2D hitboxTexture = new Texture2D(sb.GraphicsDevice, 1, 1);
//            hitboxTexture.SetData(new[] { Color.Red });
//            sb.Draw(hitboxTexture, CurrentPosition, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White * 0.5f, 0,
//                    new Vector2(Texture.Width / 2, Texture.Height / 2), 1, SpriteEffects.None, LayerDepth); //TODO attention à layerDepth  
//#endif
            
        }

    }
}
