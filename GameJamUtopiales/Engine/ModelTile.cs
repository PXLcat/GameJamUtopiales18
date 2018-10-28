using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJamUtopiales
{
    abstract public class ModelTile : ICollidable, IDrawable
    {
        private Rectangle sourceRectangle;

        public Rectangle HitBox
        {
            get => new Rectangle((int)CurrentPosition.X, (int)CurrentPosition.Y, 100, 100);//TODO
        }

        public Vector2 CurrentPosition { get; set; }
        public Texture2D Texture { get; set; }

        public virtual void OnCollision(ICollidable other)
        {
            
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Map.tileset, new Vector2(CurrentPosition.X, CurrentPosition.Y), sourceRectangle, Color.White);
        }

        public ModelTile(Vector2 currentPosition, int width, int height, Rectangle sourceRectangle)
        {
            CurrentPosition = currentPosition;
            this.sourceRectangle = sourceRectangle;
        }
    }
}
