using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Main.Items
{
    public class Item
    {
        public bool Enabled { get; set; }
        public bool Visible { get; set; }
        public Rectangle ImgDestination { get; set; }
        
        protected Vector2 _frameSize;
        protected List<Rectangle> _sourceRectangles;
        protected Texture2D _spriteSheet;
        protected int _idxFrame;
        protected int _score;

        protected int _tickToUpdatePerSecond;
        protected int _tickCounter = 0;

        public Item(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
        }

        public virtual void Initialize()
        {
            if(_sourceRectangles == null)
            {
                _sourceRectangles = new List<Rectangle>();
                if(_frameSize != null && _frameSize!= Vector2.Zero)
                {
                    createSourceRectangle();
                }
                
            }
        }

        protected virtual void createSourceRectangle()
        {
            int x = 0;
            int y = 0;
            while (x < _spriteSheet.Width)
            {
                _sourceRectangles.Add(new Rectangle(new Point(x, y), _frameSize.ToPoint()));
                x += (int)_frameSize.X;
            }
        }


        public virtual void Update()
        {
            if(Enabled && ++_tickCounter > Utility.TICK_PER_SECOND / _tickToUpdatePerSecond)
            {
                _tickCounter = 0;
                _idxFrame = ++_idxFrame % _sourceRectangles.Count;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(Visible)
            {
                spriteBatch.Draw(_spriteSheet, ImgDestination, _sourceRectangles[_idxFrame], Color.White);
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

    public interface IItem
    {
        void createSourceRectangle();
    }
}
