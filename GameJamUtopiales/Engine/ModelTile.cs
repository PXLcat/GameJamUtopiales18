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
        public Rectangle HitBox
        {
            get => new Rectangle((int)CurrentPosition.X, (int)CurrentPosition.Y, Texture.Width, Texture.Height);
        }

        public Vector2 CurrentPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Texture2D Texture { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public virtual void OnCollision(ICollidable other)
        {
            
        }

        public void Draw(SpriteBatch sb)
        {
            throw new NotImplementedException();
        }

        public ModelTile(Vector2 currentPosition, int width, int height)
        {
            CurrentPosition = currentPosition;
        }
    }
}
