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

        public Character CreateCharacter(CharacterName characterName)
        {
            //attention à la présence ou non des animations
            switch (characterName)
            {
                case CharacterName.MAMI:
                    return new Character(CharacterName.MAMI, new AnimatedSprite(mG.Content.Load<Texture2D>("mami_idle"), Vector2.Zero, 7, 5),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("mami_run"), Vector2.Zero, 8),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("mami_jump"), Vector2.Zero, 8, framespeed: 4),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("mami_fall"), Vector2.Zero, 3),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("mami_attack1"), Vector2.Zero, 11, framespeed: 4));
                case CharacterName.SAYAKA:
                    return new Character(CharacterName.SAYAKA, new AnimatedSprite(mG.Content.Load<Texture2D>("sayaka/sayaka_idle"), Vector2.Zero, 10),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("sayaka/sayaka_run"), Vector2.Zero, 8),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("sayaka/sayaka_jump"), Vector2.Zero, 6),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("sayaka/sayaka_fall"), Vector2.Zero, 3),
                        new AnimatedSprite(mG.Content.Load<Texture2D>("sayaka/sayaka_attack"), Vector2.Zero, 6));

                default:
                    return null;
            }


        }

    }

    public enum CharacterName
    {
        MAMI,
        SAYAKA
    }
    public enum ForegroundItemName
    {
        BARREL
    }

    //public class FactoryDTO {

    //}

}
