using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJamUtopiales
{
    public class Factory //attention pas thread safe
    {
        private static Factory instance = null;
        public MainGame mG;

        public static Factory Instance {
            get
            {
                if (instance == null)
                {
                    instance = new Factory();
                }
                return instance;
            }
        }

        public void SetMainGame(MainGame mG) {
            this.mG = mG;
        }


        private Factory()
        { 
        }

        public Character CreateCharacter(CharacterMetamorphose characterState)
        {
            //attention à la présence ou non des animations
            switch (characterState)
            {
                case CharacterMetamorphose.HUMAN:
                    return new Character(CharacterMetamorphose.HUMAN, new AnimatedSprite(mG.Content.Load<Texture2D>("animation/HUMAN_IDLE"), Vector2.Zero, 1),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("animation/HUMAN_RUN"), Vector2.Zero, 12, framespeed: 3),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("animation/HUMAN_JUMP"), Vector2.Zero, 1),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("animation/HUMAN_JUMP"), Vector2.Zero, 1),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("human_idle"), Vector2.Zero, 1));
                case CharacterMetamorphose.HULK:
                    return new Character(CharacterMetamorphose.HULK, new AnimatedSprite(mG.Content.Load<Texture2D>("animation/HULK_IDLE"), Vector2.Zero, 1),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("animation/HULK_RUN"), Vector2.Zero, 4, framespeed: 4),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("animation/HULK_JUMP"), Vector2.Zero, 1),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("animation/HULK_JUMP"), Vector2.Zero, 1),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("human_idle"), Vector2.Zero, 1));
                case CharacterMetamorphose.FOETUS:
                    return new Character(CharacterMetamorphose.HULK, new AnimatedSprite(mG.Content.Load<Texture2D>("animation/FOETUS_IDLE"), Vector2.Zero, 1),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("animation/FOETUS_RUN"), Vector2.Zero, 12, framespeed: 4),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("human_idle"), Vector2.Zero, 1),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("human_idle"), Vector2.Zero, 1),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("human_idle"), Vector2.Zero, 1));
                case CharacterMetamorphose.SPIRIT:
                    return new Character(CharacterMetamorphose.HULK, new AnimatedSprite(mG.Content.Load<Texture2D>("animation/SPIRIT_IDLE"), Vector2.Zero, 4),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("animation/SPIRIT_IDLE"), Vector2.Zero, 4),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("animation/SPIRIT_IDLE"), Vector2.Zero, 4),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("animation/SPIRIT_IDLE"), Vector2.Zero, 4),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("animation/SPIRIT_IDLE"), Vector2.Zero, 4));

                default:
                    return null;
            }


        }

    }

    public enum CharacterName
    {
        HUMAN,
        HULK,
        FOETUS,
        SPIRIT
    }
    public enum ForegroundItemName
    {
        BARREL
    }

    //public class FactoryDTO {

    //}

}
