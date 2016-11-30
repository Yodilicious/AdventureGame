using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AdventureGame.Main.Characters
{
    [Flags]
    public enum MoveDirection
    {
        Down,
        Left,
        Up,
        Right,
        RunningDown,
        RunningLeft,
        RunningUp,
        RunningRight,
        None
    }
    public class Character
    {
        protected int[] numFrames;
        protected List<Rectangle>[] moveFrames;
        public Rectangle _destinationRectangle;
        protected Texture2D spriteSheet;
        protected Vector2 speed;
        protected Vector2 size;
        protected bool isMoving = false;
        protected int idxFrame;

        protected int tickCounter;
        protected int tickToUpdate = 4;

        
        protected bool Enabled { get; set; }
        protected bool Visible { get; set; }

        protected MoveDirection currentDirection;
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
            _destinationRectangle = new Rectangle(Point.Zero, size.ToPoint());
        }

        public virtual void Initialize()
        {
            for (int i = 0; i < 8; i++)
            {
                moveFrames[i] = new List<Rectangle>();
                AddFrames(i, moveFrames[i]);
            }
        }

        protected virtual void AddFrames(int direction, List<Rectangle> moveFrames)
        {
            int x = 0;
            int y = 0;
            int width = 150;
            int height = 150;
            
            for(int i = 0; i < numFrames[direction]; i++)
            {
                moveFrames.Add(new Rectangle(x, y + height * direction + 1, width, height));
                x += width;
            }
        }

        public virtual void Update()
        {
            if(Enabled == true)
            {
                if (++tickCounter >= 16 / tickToUpdate)
                {
                    tickCounter = 0;
                    idxFrame = ++idxFrame % numFrames[(int)currentDirection];
                }
            }            
        }

        protected int getFrameIndex()
        {
            return (previousDirection != currentDirection)? 
                0 : ++idxFrame % numFrames[(int)currentDirection];
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(Visible == true)
            {
                spriteBatch.Draw(spriteSheet, 
                    _destinationRectangle, 
                    moveFrames[(int)currentDirection][idxFrame],
                    Color.White);
            }            
        }
        
        public void hide()
        {
            Enabled = false;
            Visible = false;
        }

        public void show()
        {
            Enabled = true;
            Visible = true;
        }
    }
}
