﻿using Microsoft.Xna.Framework;
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

        private float PreviousPositionX, PreviousPositionY;
        //public Vector2 Movement { get; set; }
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
            PreviousPositionX = CurrentPosition.X;
            PreviousPositionY = CurrentPosition.Y;

            SortAndExecuteInput(inputs);
            ApplyGravity();

            CheckCollisions(currentMap);

            if(!isGrounded)
            {
                CharacterState = State.JUMPING;
            }

            if (PreviousPositionX != CurrentPosition.X)
            {
                if (CharacterState != State.JUMPING)
                    CharacterState = State.RUNNING;

                if (PreviousPositionX > CurrentPosition.X && CharacterFaces != Facing.LEFT)
                    CharacterFaces = Facing.LEFT;
                else if (PreviousPositionX < CurrentPosition.X && CharacterFaces != Facing.RIGHT)
                    CharacterFaces = Facing.RIGHT;
            }
            else
                CharacterState = State.IDLE;

            CurrentSprite.CurrentPosition = CurrentPosition; //TODO redondant avec la ligne précédente; faire un eventHandler pour qu'à chaque
            CurrentSprite.Update();                             //changement de sprite, la position se màj automatiquement par rapport à CharaPosition?
        }

        private void SortAndExecuteInput(List<InputType> inputs)
        {
            if (inputs.Count == 0)
                return;

            if (inputs.Contains(InputType.JUMP))
            {
                Debug.Write("Jumps done: " + jumpsDone);
                Jump();
            }

            if (inputs.Contains(InputType.MOVE_LEFT))
                MoveLeft();

            if (inputs.Contains(InputType.MOVE_RIGHT))
                MoveRight();

            if (inputs.Contains(InputType.FLYUP))
                FlyUp();

            if (inputs.Contains(InputType.FLYDOWN))
                FlyDown();
            
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
            CurrentPosition = new Vector2(CurrentPosition.X, CurrentPosition.Y - RunSpeed);
        }

        private void FlyDown()
        {
            CurrentPosition = new Vector2(CurrentPosition.X, CurrentPosition.Y + RunSpeed);
        }

        private void Attack1() //attaque au sol //TODO comment externaliser les attaques?
        {
            CharacterState = State.ATTACKING1;
        }

        private void Jump()
        {
            if (characterMetamorphose == CharacterMetamorphose.FOETUS || jumpsDone >= MaxJumps)
                return;

            JumpInitPosY = CurrentPosition.Y;

            jumpsDone++;

            isGrounded = false;
            spriteJump.CurrentFrame = 0;

            velocityY = -12; //TODO externaliser
        }

        private void MoveRight()
        {
            CurrentPosition = new Vector2(CurrentPosition.X + RunSpeed, CurrentPosition.Y);
        }

        private void MoveLeft()
        {
            CurrentPosition = new Vector2(CurrentPosition.X - RunSpeed, CurrentPosition.Y);
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
            if (characterMetamorphose == CharacterMetamorphose.SPIRIT)
                velocityY = 0;
            else
            {
                if (velocityY < maxVelocity)
                    velocityY += gravity;
                else
                    velocityY = maxVelocity;
            }

            Debug.WriteLine("velocity : " + velocityY);

            CurrentPosition = new Vector2(CurrentPosition.X, CurrentPosition.Y + velocityY);
        }

        public void CheckCollisions(Map currentMap)
        {
            List<ModelTile> collidableItems = currentMap.layerPlayer;

            CollideType PlayerCollision = new CollideType();

            foreach (ModelTile cObject in collidableItems)
            {
                //est ce qu'on est au sol
                CollideType byObjectCollision = Utilities.CheckCollision(Player.Instance, cObject);

                if (cObject.traversablePourHumain && characterMetamorphose != CharacterMetamorphose.SPIRIT ||
                    cObject.traversablePourFantome && characterMetamorphose == CharacterMetamorphose.SPIRIT)
                    continue;

                if (byObjectCollision.collideLeft)
                {
                    PlayerCollision.collideLeft = true;
                    PlayerCollision.leftCollisionPosition = Math.Max(PlayerCollision.leftCollisionPosition, byObjectCollision.leftCollisionPosition);
                }
                
                if (byObjectCollision.collideRight)
                {
                    PlayerCollision.collideRight = true;
                    PlayerCollision.rightCollisionPosition = Math.Max(PlayerCollision.rightCollisionPosition, byObjectCollision.rightCollisionPosition);
                }

                if (byObjectCollision.collideBottom)
                {
                    PlayerCollision.collideBottom = true;
                    PlayerCollision.bottomCollisionPosition = Math.Max(PlayerCollision.bottomCollisionPosition, byObjectCollision.bottomCollisionPosition);
                }

                if (byObjectCollision.collideTop)
                {
                    PlayerCollision.collideTop = true;
                    PlayerCollision.topCollisionPosition = Math.Max(PlayerCollision.topCollisionPosition, byObjectCollision.topCollisionPosition);
                }
            }

            Debug.WriteLine(PlayerCollision.ToString());

            if(PlayerCollision.collideTop)
            {
                velocityY = 0;
                CurrentPosition = new Vector2(CurrentPosition.X, PreviousPositionY);
            }

            if (PlayerCollision.collideBottom)
            {
                velocityY = 0;
                jumpsDone = 0;
                isGrounded = true;
                CurrentPosition = new Vector2(CurrentPosition.X, PreviousPositionY);
            }

            if (PlayerCollision.collideLeft || PlayerCollision.collideRight)
                CurrentPosition = new Vector2(PreviousPositionX, CurrentPosition.Y);
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
