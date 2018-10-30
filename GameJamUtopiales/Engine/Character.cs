using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameJamUtopiales
{
    public class Character : IDrawable, ICollidable
    {

        public CharacterMetamorphose characterMetamorphose;
        protected AnimatedSprite spriteIdle, spriteRun, spriteJump, spriteFall, spriteAttack1;

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

        private int jumpsDone; //remettre à private + tard

        private float velocityY;
        private float maxVelocity;
        private float gravity = 0.6f;

        public int MaxJumps { get; set; }
        public float RunSpeed { get; set; }

        public Vector2 Movement { get; set; }
        public float JumpHeight { get; set; }


        public float JumpInitPosY { get; set; }

        public Rectangle HitBox
        {
            get
            {
                return new Rectangle((int)CurrentPosition.X - spriteIdle.FrameWidth / 2, (int)CurrentPosition.Y - spriteIdle.FrameHeight,
              spriteIdle.FrameWidth, spriteIdle.FrameHeight);
            } //TODO prendre en compte les Offset plus tard?
        }

        public Texture2D Texture { get => CurrentSprite.Texture; set => throw new Exception("La texture d'un sprite ne peut être modifiée directement"); }
        public Vector2 CurrentPosition { get; set; }



        #region Methods
        public Character(CharacterMetamorphose characterName, AnimatedSprite spriteIdle, AnimatedSprite spriteRun, AnimatedSprite spriteJump, AnimatedSprite spriteFall, AnimatedSprite spriteAttack1, int jumpHeight = 120, int maxJumps = 2, float runSpeed = 5)
        {
            this.spriteIdle = spriteIdle;
            this.spriteRun = spriteRun;
            this.spriteJump = spriteJump;
            this.spriteFall = spriteFall;
            this.spriteAttack1 = spriteAttack1;

            JumpHeight = jumpHeight;
            RunSpeed = runSpeed;

            this.characterMetamorphose = characterName;
            this.CharacterState = State.IDLE;

            MaxJumps = maxJumps;

            this.maxVelocity = 15;

            //this.InputMethod = InputMethod.FILE; //TODO !!
        }

        public void Load()
        {
            //utile?
        }
        public void Update(List<InputType> inputs, Map currentMap)
        {
            Movement = Vector2.Zero;
            if (inputs.Count > 0)
            {
                SortAndExecuteInput(inputs);
            }
            else if (CharacterState == State.RUNNING)
            {
                CharacterState = State.IDLE;
            }
            ContinueActions();

            ApplyGravity();

            CheckCollisions(currentMap);

            CurrentPosition += Movement;

            CurrentSprite.CurrentPosition = CurrentPosition; //TODO redondant avec la ligne précédente; faire un eventHandler pour qu'à chaque
            CurrentSprite.Update();                             //changement de sprite, la position se màj automatiquement par rapport à CharaPosition?
        }


        private void ContinueActions()
        {
            //if (CharacterState == State.ATTACKING1)
            //{
            //    if (spriteAttack1.FirstLoopDone)
            //    {
            //        ResetPose();
            //        spriteAttack1.FirstLoopDone = false;
            //    }
            //}
        }

        private void SortAndExecuteInput(List<InputType> inputs)
        {

            if (inputs.Contains(InputType.JUMP))
            {
                Debug.Write("Jumps done: " + jumpsDone);

                if (jumpsDone < MaxJumps)
                {
                    Jump();
                }


            }
            if (inputs.Contains(InputType.MOVE_LEFT) && (inputs.Contains(InputType.MOVE_RIGHT)))
            {
                //ResetPose();
            }
            else if (inputs.Contains(InputType.MOVE_LEFT) && (!inputs.Contains(InputType.MOVE_RIGHT)))
            {
                MoveLeft();
            }
            else if (inputs.Contains(InputType.MOVE_RIGHT) && (!inputs.Contains(InputType.MOVE_LEFT)))
            {
                MoveRight();
            }
            if (inputs.Contains(InputType.FLYUP))
            {
                FlyUp();
            }
            else if (inputs.Contains(InputType.FLYDOWN))
            {
                FlyDown();
            }
            //else if (inputs.Contains(InputType.ATTACK1))
            //{
            //    if ((CharacterState != State.JUMPING) && (CharacterState != State.FALLING))
            //    {
            //        Attack1();
            //    }

            //}
        }

        private void FlyUp()
        {
            this.Movement = new Vector2(this.Movement.X, this.Movement.Y - RunSpeed);
        }
        private void FlyDown()
        {
            this.Movement = new Vector2(this.Movement.X, this.Movement.Y  +RunSpeed);
        }

        private void Attack1() //attaque au sol //TODO comment externaliser les attaques?
        {
            CharacterState = State.ATTACKING1;

        }

        private void Jump()
        {

            if ((characterMetamorphose != CharacterMetamorphose.FOETUS)
                && (characterMetamorphose != CharacterMetamorphose.FOETUS))
            {
                this.JumpInitPosY = this.CurrentPosition.Y;

                jumpsDone++;

                CharacterState = State.JUMPING;
                isGrounded = false;
                spriteJump.CurrentFrame = 0;

                velocityY = -12; //TODO externaliser
            }


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

                this.Movement = new Vector2(this.Movement.X + RunSpeed, this.Movement.Y);
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
                this.Movement = new Vector2(this.Movement.X - RunSpeed, this.Movement.Y);
            }
        }

        public void Draw(SpriteBatch sb)
        {
#if DEBUG
            Rectangle sourceRectangle = new Rectangle(CurrentSprite.FrameWidth, 0, CurrentSprite.FrameWidth, CurrentSprite.FrameHeight);

            Texture2D hitboxTexture = new Texture2D(sb.GraphicsDevice, 1, 1);
            hitboxTexture.SetData(new[] { Color.Red });
            sb.Draw(hitboxTexture, CurrentPosition, sourceRectangle, Color.White * 0.5f, 0, CurrentSprite.center, 1, SpriteEffects.FlipHorizontally, 0);
#endif
            CurrentSprite.Draw(sb, (CharacterFaces == Facing.LEFT));

        }

        public void ApplyGravity()
        {
            if (characterMetamorphose != CharacterMetamorphose.SPIRIT)
            {
                //CharacterMetamorphose.
                if ((velocityY + gravity) > maxVelocity)
                {
                    velocityY = maxVelocity;
                }
                else
                {
                    velocityY += gravity;
                }

                //if (velocityY > 1.2f) //!TODO
                //{
                //    CharacterState = State.FALLING;
                //}

            }
            else
            {
                if ((velocityY + gravity) > maxVelocity)
                {
                    velocityY = maxVelocity;
                }
            }



            Debug.WriteLine("velocity : " + velocityY);

            this.Movement = new Vector2(this.Movement.X, (this.Movement.Y + velocityY));
            if (characterMetamorphose == CharacterMetamorphose.SPIRIT)
            {
                velocityY = 0;
            }


        }

        public void CheckCollisions(Map currentMap)
        {
            List<ModelTile> collidableItems = currentMap.layerPlayer;

            CollideType PlayerCollision = new CollideType();

            int HitBoxTopPlusOne = 100; //!
            foreach (ModelTile cObject in collidableItems)
            {
                //est ce qu'on est au sol

                CollideType byObjectCollision = Utilities.CheckCollision(Player.Instance, cObject);

                if (byObjectCollision.collideLeft)
                {
                    if (!cObject.traversablePourHumain)
                    {
                        PlayerCollision.collideLeft = true;
                        //this.CurrentPosition = new Vector2(cObject.HitBox.Right,this.CurrentPosition.X);
                    }
                    if (!cObject.traversablePourFantome && characterMetamorphose==CharacterMetamorphose.SPIRIT )
                    {
                        PlayerCollision.collideLeft = true;
                    }

                }
                if (byObjectCollision.collideRight)
                {
                    if (!cObject.traversablePourHumain)
                    {
                        PlayerCollision.collideRight = true;
                    }
                    if (!cObject.traversablePourFantome && characterMetamorphose == CharacterMetamorphose.SPIRIT)
                    {
                        PlayerCollision.collideRight = true;
                    }

                }
                if (byObjectCollision.collideBottom && !cObject.traversablePourHumain)
                {
                    PlayerCollision.collideBottom = true;
                    PlayerCollision.bottomCollisionDepth = byObjectCollision.bottomCollisionDepth;
                    HitBoxTopPlusOne = cObject.HitBox.Top + 1;



                }


            }

            Debug.WriteLine(PlayerCollision.ToString());

            if (PlayerCollision.collideBottom)
            {
                if (this.Movement.Y > 0)
                {
                    this.Movement = new Vector2(this.Movement.X, 0);
                }

                if (PlayerCollision.bottomCollisionDepth > 1)
                {
                    //if (CharacterState == State.FALLING) //
                    //{
                    //    ResetPose();
                    //    jumpsDone = 0;
                    //}
                    ResetPose();
                    this.CurrentPosition = new Vector2(this.CurrentPosition.X + this.Movement.X, HitBoxTopPlusOne);
                    
                }
                else if (PlayerCollision.bottomCollisionDepth == 1)
                {
                    //ResetPose();
                }
            }
            else
            {
                CharacterState = State.FALLING;
            }

            if (PlayerCollision.collideLeft)
            {
                if (Movement.X<0)
                {
                    Movement = new Vector2(0, Movement.Y);

                }
                

            }
            if (PlayerCollision.collideRight)
            {
                if (Movement.X > 0)
                {
                    Movement = new Vector2(0, Movement.Y);
                }
            }


        }




        public void ResetPose()
        {
            //TODO eventarg pour qu'à chaque changement de currentSprite, la position se mette à jour toute seule?
            CharacterState = State.IDLE;
            jumpsDone = 0;

        }

        public void OnCollision(ICollidable other)
        {
            Debug.Write("Collision entre " + this.characterMetamorphose + " et " + other.ToString());
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
