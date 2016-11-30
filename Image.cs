using System;
using AdventureGame.Main.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdventureGame.Main
{
    [Flags]
    public enum ImageTypes
    {
        Grass = 0,
        Sky = 1,
        Street = 2,
        Building = 4
    }

    public class Image : IMovePosition
    {
        public static int count = 0;
        
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public Rectangle SourceRectangle { get; set; }
        
        public Image(Vector2 position,
            Texture2D texture,
            Rectangle sourceRectangle)
        {
            this.Position = position;
            this.Texture = texture;
            this.SourceRectangle = sourceRectangle;
            count++;
        }
        
        public void Move(Vector2 distance)
        {
            Position += distance;
        }
    }
}
