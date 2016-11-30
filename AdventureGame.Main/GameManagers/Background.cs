using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdventureGame.Main
{
    public class Background
    {
        public Texture2D texture;
        public Rectangle rectangle;

        public Background(Texture2D newTexture, Rectangle newRectangle)
        {
            this.texture = newTexture;
            this.rectangle = newRectangle;
        }

        public virtual void Update(int move)
        {
            if (rectangle.X <= -rectangle.Width)
                rectangle.X = rectangle.Width;
            rectangle.X += move;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
