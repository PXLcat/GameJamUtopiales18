using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GameJamUtopiales
{
    public class Character : IDrawable, ICollidable
    {

        public CharacterName CharacterName;
        protected AnimatedSprite spriteIdle, spriteRun, spriteJump, spriteFall, spriteAttack1;

        private Vector2 movement; //new Vector2(déplacement horizontal, déplacement vertical)

        private float jumpHeight;

        public State CharacterState;
        public Facing CharacterFaces;
        private Rectangle hitBox;

        private InputMethod inputMethod;

        public InputMethod InputMethod
        {
            get { return inputMethod; }
            set { inputMethod = value; }
        }

        private bool isGrounded;
        public int groundedHeight;


        public AnimatedSprite CurrentSprite
        {
            get
            {
                switch (CharacterState)
                {
                    case State.IDLE:
                        return spriteIdle;
                    case State.RUNNING:
                        return spriteRun;
                    case State.JUMPING:
                        return spriteJump;
                    case State.FALLING:
                        return spriteFall;
                    case State.ATTACKING1: // TODO overwrite pour player pour avoir plus d'attaques
                        return spriteAttack1;
                    default:
                        throw new System.Exception();
                }
            }
        }

        private float runSpeed;
        private int jumpsDone; //remettre à private + tard
        private float jumpInitPosY;
        private int maxJumps;

        private float velocity;
        private float maxVelocity;
        private float gravity = 0.6f;

        public int MaxJumps
        {
            get { return maxJumps; }
            set { maxJumps = value; }
        }
        public float RunSpeed
        {
            get { return runSpeed; }
            set { runSpeed = value; }
        }

        public Vector2 Movement
        {
            get { return movement; }
            set { movement = value; }
        }
        public float JumpHeight
        {
            get { return jumpHeight; }
            set { jumpHeight = value; }
        }


        public float JumpInitPosY
        {
            get { return jumpInitPosY; }
            set { jumpInitPosY = value; }
        }

        public Rectangle HitBox
        {
            get { return new Rectangle((int)CurrentPosition.X, (int)CurrentPosition.Y, CurrentSprite.FrameWidth, CurrentSprite.FrameHeight); } //TODO prendre en compte les Offset plus tard?
            set { hitBox = value; }
        }

        public Texture2D Texture { get => CurrentSprite.Texture; set => throw new Exception("La texture d'un sprite ne peut être modifiée directement"); }
        public Vector2 CurrentPosition { get; set; }



        #region Methods
        public Character(CharacterName characterName, AnimatedSprite spriteIdle, AnimatedSprite spriteRun, AnimatedSprite spriteJump, AnimatedSprite spriteFall, AnimatedSprite spriteAttack1, int jumpHeight = 120, int maxJumps = 2, float runSpeed = 2)
        {
            this.spriteIdle = spriteIdle;
            this.spriteRun = spriteRun;
            this.spriteJump = spriteJump;
            this.spriteFall = spriteFall;
            this.spriteAttack1 = spriteAttack1;

            this.jumpHeight = jumpHeight;
            this.runSpeed = runSpeed;

            this.CharacterName = characterName;
            this.CharacterState = State.IDLE;
            this.maxJumps = maxJumps;

            this.maxVelocity = 20;

            //this.InputMethod = InputMethod.FILE; //TODO !!
        }

        public void Load()
        {
            //utile?
        }
        public void Update(List<InputType> inputs, List<CollidableObject> collidableItems)
        {
            Movement = Vector2.Zero;
            if (inputs.Count > 0)
            {
                SortAndExecuteInput(inputs);
            }
            else if (CharacterState == State.RUNNING)
            {
                ResetPose();
            }
            ContinueActions();
            ApplyGravity();
            CheckCollisions(collidableItems);

            CurrentPosition += Movement;
            if (isGrounded)
            {
                CurrentPosition = new Vector2(CurrentPosition.X, groundedHeight);
            }
            CurrentSprite.CurrentPosition = CurrentPosition; //TODO redondant avec la ligne précédente; faire un eventHandler pour qu'à chaque
            CurrentSprite.Update();                             //changement de sprite, la position se màj automatiquement par rapport à CharaPosition?
        }


        private void ContinueActions()
        {
            if (CharacterState == State.ATTACKING1)
            {
                if (spriteAttack1.FirstLoopDone)
                {
                    ResetPose();
                    spriteAttack1.FirstLoopDone = false;
                }
            }
        }

        private void SortAndExecuteInput(List<InputType> inputs)
        {

            if (inputs.Contains(InputType.JUMP))
            {
                Debug.Write("Jumps done: " + jumpsDone);

                if (jumpsDone < maxJumps)
                {
                    Jump();
                }


            }
            if (inputs.Contains(InputType.MOVE_LEFT) && (inputs.Contains(InputType.MOVE_RIGHT)))
            {
                ResetPose();
            }
            else if (inputs.Contains(InputType.MOVE_LEFT) && (!inputs.Contains(InputType.MOVE_RIGHT)))
            {
                MoveLeft();
            }
            else if (inputs.Contains(InputType.MOVE_RIGHT) && (!inputs.Contains(InputType.MOVE_LEFT)))
            {
                MoveRight();
            }
            else if (inputs.Contains(InputType.ATTACK1))
            {
                if ((CharacterState != State.JUMPING) && (CharacterState != State.FALLING))
                {
                    Attack1();
                }

            }
        }



        private void Attack1() //attaque au sol //TODO comment externaliser les attaques?
        {
            CharacterState = State.ATTACKING1;

        }

        private void Jump() {
            this.JumpInitPosY = this.CurrentPosition.Y;

            jumpsDone++;

            CharacterState = State.JUMPING;
            isGrounded = false;
            spriteJump.CurrentFrame = 0;


            velocity = -12; //TODO externaliser
        }

        private void MoveRight()
        {
            if (CharacterState != State.ATTACKING1)
            {
                if (CharacterState == State.IDLE)
                {
                    CharacterState = State.RUNNING;
                }
                CharacterFaces = Facing.RIGHT;

                this.Movement = new Vector2(this.Movement.X + runSpeed, this.Movement.Y);
            }
        }
        private void MoveLeft()
        {
            if (CharacterState != State.ATTACKING1)
            {
                if (CharacterState == State.IDLE)
                {
                    CharacterState = State.RUNNING;
                }
                CharacterFaces = Facing.LEFT;
                this.Movement = new Vector2(this.Movement.X - runSpeed, this.Movement.Y);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            CurrentSprite.Draw(sb, (CharacterFaces == Facing.LEFT));
        }

        public void ApplyGravity()
        {
            //if(CharacterState == State.JUMPING || CharacterState == S)
            if (!isGrounded)
            {
                if ((velocity + gravity) > maxVelocity)
                {
                    velocity = maxVelocity;
                }
                else
                {
                    velocity += gravity;
                }
            }


            if (velocity > 0)
            {
                CharacterState = State.FALLING;
            }
            //Vector2 nextPosition = new Vector2(this.CurrentPosition.X + this.Movement.X, (this.CurrentPosition.Y + this.Movement.Y + velocity));
            Debug.WriteLine("velocity : " + velocity);

            this.Movement = new Vector2(this.Movement.X, (this.Movement.Y + velocity));



        }

        public void CheckCollisions(List<CollidableObject> collidableItems)
        {

            Rectangle nextPosition = new Rectangle((int)(CurrentPosition.X + Movement.X), (int)(CurrentPosition.Y + Movement.Y),
                CurrentSprite.Texture.Width, CurrentSprite.Texture.Height); //TODO attention au check qui varie selon la taille du sprite!

            //bool doReset = false;
            CollideType collision = new CollideType();
            foreach (CollidableObject cObject in collidableItems)
            {
                //est ce qu'on est au sol

                collision = Utilities.CheckCollision(nextPosition, cObject.HitBox);
                if (collision.collideBottom)
                    groundedHeight = cObject.HitBox.Top;


                //if (collision.collideLeft || collision.collideRight) //on devrait "coller" à l'objet qu'on percute
                //    //Movement = new Vector2(0, Movement.Y);
                //if (collision.collideTop || collision.collideBottom) 
                //    Movement = new Vector2(Movement.X, 0);
                //if ((CharacterState == State.FALLING) && collision.collideBottom)
                //{
                //    doReset = true;
                //    resetHeight = cObject.HitBox.Top - CurrentSprite.Texture.Height / 2;
                //}


            }
            //if (doReset)
            //{
            //    ResetPose();
            //    CurrentPosition = new Vector2((int)CurrentPosition.X, resetHeight );
            //}
            if (collision.collideBottom)
            {
                if (CharacterState == State.FALLING)
                {
                    ResetPose();
                    Debug.WriteLine("collision sol");
                    Movement = new Vector2(Movement.X, 0); //(Movement.X, 0+CurrentSprite.FrameHeight- collision.bottomCollisionDepth);
                    isGrounded = true;
                }

            }
            else
            {
                isGrounded = false;
            }

        }




        public void ResetPose()
        {
            //TODO eventarg pour qu'à chaque changement de currentSprite, la position se mette à jour toute seule?
            CharacterState = State.IDLE;
            jumpsDone = 0;
            velocity = 0;
        }

        public void OnCollision(ICollidable other)
        {
            Debug.Write("Collision entre " + this.CharacterName + " et " + other.ToString());
        }


        #endregion



        public enum State
        {
            IDLE,
            RUNNING,
            JUMPING,
            FALLING,
            ATTACKING1


        }
        public enum Facing
        {
            LEFT,
            RIGHT
        }

    }
}
