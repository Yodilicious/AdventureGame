using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdventureGame.Main.Characters
{
    [Flags]
    public enum CharacterType
    {
        Jodi = 0,
        Soochang = 1
    }

    public class Player : Character
    {
        public const int MAX_LIFE = 2;
        protected KeyboardState oldKeyState;
        protected KeyboardState curKeyState;
        protected Vector2 startPosition = new Vector2(100, 500);
        private string name;
        private int maxJumpHeight = 100;
        private int beforeJumpHeight;
        public bool NowJumping { get; set; }
        public bool IsDroped { get; set; }


        public Player(CharacterType type,
            Texture2D spriteSheet) 
            : base(spriteSheet)
        {
            numFrames = new int[] { 3, 3, 1, 3, 10, 10, 10, 10 };
            speed = new Vector2(1, 1);
            previousDirection = currentDirection = MoveDirection.Right;
            _destinationRectangle.X = (int)startPosition.X;
            _destinationRectangle.Y = (int)startPosition.Y;
            Initialize();
        }

        public override void Update()
        {
            if(Enabled && ++tickCounter >= 16 / tickToUpdate)
            {
                tickCounter = 0;
                getDirection();
                getPosition();
                idxFrame = getFrameIndex();
                resetDirectionAndKeystate();
            }
        }

        private void getPosition()
        {
            if (IsDroped)
            {
                NowJumping = false;
                _destinationRectangle.Y += (int)speed.Y * 8;
            }

            switch (currentDirection)
            {
                case MoveDirection.Down:
                    break;
                case MoveDirection.Left:
                    break;
                case MoveDirection.Up:

                    break;
                case MoveDirection.Right:
                    break;
                case MoveDirection.RunningDown:
                    break;
                case MoveDirection.RunningLeft:
                    _destinationRectangle.X -= (NowJumping) ? (int)(speed.X * 1.5) : (int)speed.X;
                    break;
                case MoveDirection.RunningUp:
                    break;
                case MoveDirection.RunningRight:
                    _destinationRectangle.X += (NowJumping) ? (int)(speed.X * 1.5) : (int)speed.X;
                    break;
                case MoveDirection.None:
                    break;
                default:
                    break;
            }

            if (NowJumping)
                _destinationRectangle.Y -= (int)speed.Y * 8;
        }

        public void resetDirectionAndKeystate()
        {
            previousDirection = currentDirection;
            oldKeyState = curKeyState;
        }

        private void getDirection()
        {
            curKeyState = Keyboard.GetState();

            if(curKeyState.IsKeyUp(Keys.Right) && oldKeyState.IsKeyDown(Keys.Right))
            {
                currentDirection =  MoveDirection.Right;
            }
            else if (curKeyState.IsKeyDown(Keys.Right))
            {
                currentDirection = MoveDirection.RunningRight;
            }

            if (curKeyState.IsKeyUp(Keys.Left) && oldKeyState.IsKeyDown(Keys.Left))
            {
                currentDirection = MoveDirection.Left;
            }
            else if (curKeyState.IsKeyDown(Keys.Left))
            {
                currentDirection = MoveDirection.RunningLeft;
            }
            if (curKeyState.IsKeyDown(Keys.Up) && oldKeyState.IsKeyDown(Keys.Up))
            {
                if (IsDroped == false && NowJumping == false)
                {
                    beforeJumpHeight = _destinationRectangle.Y;
                    NowJumping = true;
                }

                if (beforeJumpHeight - maxJumpHeight <= _destinationRectangle.Y)
                {
                    NowJumping = true;
                }
                else
                {
                    NowJumping = false;
                    IsDroped = true;
                }
            }
            if (!IsDroped && NowJumping && oldKeyState.IsKeyUp(Keys.Up))
            {
                IsDroped = true;
            }            
        }
    }
}
