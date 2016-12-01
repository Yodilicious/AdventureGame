using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using AdventureGame.Main.Interfaces;
using AdventureGame.Main.GameManagers;

namespace AdventureGame.Main.Characters
{
    [Flags]
    public enum MoveDirection
    {
        Down,
        Left,
        Up,
        Right,
        ClimbingDown,
        RunningLeft,
        ClimbingUp,
        RunningRight,
        None
    }
    public class Character : IGetBound, ICollisionHandler, IDrawableComponent
    {        
        protected int[] numFrames;
        protected List<Rectangle>[] moveFrames;
        public Rectangle _destinationRectangle;
        protected Texture2D spriteSheet;
        public Vector2 speed;
        protected Vector2 size;
        protected bool isMoving = false;
        protected int idxFrame;
        protected Point sizeFrame;
        protected int tickCounter;
        protected int tickToUpdate = 4;
        protected bool _enabled;
        protected bool _visible;
        protected int _beforeJumpHeight;
        protected int _maxJumpHeight = 100;

        public int Life { get; set; }
        

        protected MoveDirection currentDirection;
        public MoveDirection CurrentDirection
        {
            get
            {
                return currentDirection;
            }
        }
        protected MoveDirection previousDirection;


        /// <summary>
        /// Base class for all class which has animation
        /// </summary>
        /// <param name="spriteSheet"> 2D Spritesheet for all kind of animation </param>
        public Character(Texture2D spriteSheet)
        {
            this.spriteSheet = spriteSheet;
            moveFrames = new List<Rectangle>[8];
            size = new Vector2(40, 40);
            _destinationRectangle = 
                new Rectangle(
                    Point.Zero, 
                    size.ToPoint()
                    );
            show();
        }

        public virtual void Initialize()
        {
            for (int i = 0; i < numFrames.Length; i++)
            {
                moveFrames[i] = new List<Rectangle>();
                AddFrames(i, moveFrames[i]);
            }
        }

        protected virtual void AddFrames(int direction, List<Rectangle> moveFrames)
        {
            for(int i = 0; i < numFrames[direction]; i++)
            {
                moveFrames.Add(
                    new Rectangle(
                        sizeFrame.X * i, 
                        sizeFrame.Y * direction + 1, 
                        sizeFrame.X, 
                        sizeFrame.Y));
            }
        }

        public virtual void Update()
        {
            if(_enabled == true)
            {
                if (++tickCounter >= 16 / tickToUpdate)
                {
                    tickCounter = 0;
                    idxFrame = ++idxFrame % numFrames[(int)currentDirection];
                }
            }            
        }
        public void setPosition(Vector2 pos)
        {
            _destinationRectangle.X = (int)pos.X;
            _destinationRectangle.Y = (int)pos.Y;
        }

        protected int getFrameIndex()
        {
            return (previousDirection != currentDirection)? 
                0 : ++idxFrame % numFrames[(int)currentDirection];
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(_visible == true)
            {
                spriteBatch.Draw(
                    spriteSheet, 
                    _destinationRectangle, 
                    moveFrames[(int)currentDirection][idxFrame],
                    Color.White
                    );
            }            
        }

        public virtual Rectangle getBoundToCheckCollision()
        {
            return new Rectangle(
                _destinationRectangle.X + 5,
                _destinationRectangle.Y + 5,
                _destinationRectangle.Width - 13,
                _destinationRectangle.Height - 10
                );
        }

        public virtual void handleCollision(CollisionType collisionType, object other)
        {
            throw new NotImplementedException();
        }

        public void hide()
        {
            setEnabled(false);
            setVisible(false);
        }

        public void show()
        {
            setEnabled(true);
            setVisible(true);
        }


        public bool isEnabled()
        {
            return _enabled; 
        }

        public void setEnabled(bool enabled)
        {
            _enabled = enabled;
        }

        public bool isVisible()
        {
            return _visible;
        }

        public void setVisible(bool visible)
        {
            _visible = visible;
        }

    }
}
