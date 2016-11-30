using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdventureGame.Main
{
    class ScrollingBackground : Background
    {
        private int _speed;
        public ScrollingBackground(
            Texture2D newTexture, 
            Rectangle newRectangle,
            int speed) 
            : base(newTexture, newRectangle)
        {
            _speed = speed;
        }

        public void Update()
        {
            if (rectangle.X <= -rectangle.Width)
                rectangle.X = rectangle.Width;
            rectangle.X += _speed;
        }
    }
}
