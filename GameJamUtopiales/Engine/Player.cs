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
            this.human = factory.CreateCharacter(CharacterName.MAMI);
            this.hulk = factory.CreateCharacter(CharacterName.SAYAKA);
            //this.foetus = factory.CreateCharacter(CharacterName.MAMI);
            //this.spirit = factory.CreateCharacter(CharacterName.MAMI);

            CurrentPlayerCharacter = human;
        }

        public void SwitchCharacter(CharacterStates newCharacter)
        {
            switch (newCharacter)
            {
                case CharacterStates.HUMAN:
                    human.CurrentPosition = CurrentPlayerCharacter.CurrentPosition;
                    human.CharacterFaces = CurrentPlayerCharacter.CharacterFaces;
                    this.CurrentPlayerCharacter = human;
                    break;
                case CharacterStates.HULK:
                    hulk.CurrentPosition = CurrentPlayerCharacter.CurrentPosition;
                    hulk.CharacterFaces = CurrentPlayerCharacter.CharacterFaces;
                    this.CurrentPlayerCharacter = hulk;
                    break;
                case CharacterStates.FOETUS:
                    foetus.CurrentPosition = CurrentPlayerCharacter.CurrentPosition;
                    foetus.CharacterFaces = CurrentPlayerCharacter.CharacterFaces;
                    this.CurrentPlayerCharacter = foetus;
                    break;
                case CharacterStates.SPIRIT:
                    spirit.CurrentPosition = CurrentPlayerCharacter.CurrentPosition;
                    spirit.CharacterFaces = CurrentPlayerCharacter.CharacterFaces;
                    this.CurrentPlayerCharacter = spirit;
                    break;
                default:
                    break;
            }
        }

    }
    public enum CharacterStates {
        HUMAN,
        HULK,
        FOETUS,
        SPIRIT
    }
}
