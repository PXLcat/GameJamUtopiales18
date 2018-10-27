using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJamUtopiales
{
    public class Player
    {
        private static Player instance;
        public Character CurrentPlayerCharacter;
        public Character human;
        public Character hulk;
        public Character foetus;
        public Character spirit;


        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();

                }
                return instance;
            }
        }
        private Player() { }

        public void Load(MainGame mG)
        {
            Factory factory = Factory.Instance;
            this.human = factory.CreateCharacter(CharacterMetamorphose.HUMAN);
            this.hulk = factory.CreateCharacter(CharacterMetamorphose.HULK);
            this.foetus = factory.CreateCharacter(CharacterMetamorphose.FOETUS);
            this.spirit = factory.CreateCharacter(CharacterMetamorphose.SPIRIT);

            CurrentPlayerCharacter = human;
        }

        public void SwitchCharacter(CharacterMetamorphose newCharacter)
        {
            switch (newCharacter)
            {
                case CharacterMetamorphose.HUMAN:
                    human.CurrentPosition = CurrentPlayerCharacter.CurrentPosition;
                    human.CharacterFaces = CurrentPlayerCharacter.CharacterFaces;
                    this.CurrentPlayerCharacter = human;
                    break;
                case CharacterMetamorphose.HULK:
                    hulk.CurrentPosition = CurrentPlayerCharacter.CurrentPosition;
                    hulk.CharacterFaces = CurrentPlayerCharacter.CharacterFaces;
                    this.CurrentPlayerCharacter = hulk;
                    break;
                case CharacterMetamorphose.FOETUS:
                    foetus.CurrentPosition = CurrentPlayerCharacter.CurrentPosition;
                    foetus.CharacterFaces = CurrentPlayerCharacter.CharacterFaces;
                    this.CurrentPlayerCharacter = foetus;
                    break;
                case CharacterMetamorphose.SPIRIT:
                    spirit.CurrentPosition = CurrentPlayerCharacter.CurrentPosition;
                    spirit.CharacterFaces = CurrentPlayerCharacter.CharacterFaces;
                    this.CurrentPlayerCharacter = spirit;
                    break;
                default:
                    break;
            }
        }

    }
    public enum CharacterMetamorphose {
        HUMAN,
        HULK,
        FOETUS,
        SPIRIT
    }
}
