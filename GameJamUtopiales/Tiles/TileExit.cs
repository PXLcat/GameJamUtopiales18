using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJamUtopiales
{
    public class TileExit : ModelTile
    {

        MainGame mG;

        public TileExit(Vector2 currentPosition, Rectangle sourceRectangle, int width, int height, MainGame mG)
        : base(currentPosition, sourceRectangle, width, height)
        {
            CurrentPosition = currentPosition;
            traversablePourHumain = true;
            this.mG = mG;
        }
        public override void OnCollision(ICollidable other)
        {
            mG.gameState.ChangeScene(Gamestate.SceneType.FIN);
        }


    }
}
