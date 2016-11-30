using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdventureGame.Main
{
    public static class Utility 
    {
        public const int TICK_PER_SECOND = 32;
        public static Vector2 Stage { get; set; }
        public static SpriteFont HilightFont { get; set; }
        public static Random Random { get; set; } = new Random();

        public static void Initialize(GraphicsDeviceManager graphic)
        {
            Stage = new Vector2(graphic.PreferredBackBufferWidth, graphic.PreferredBackBufferHeight);
        }

        public static Vector2 GetCenter()
        {
            return new Vector2(Stage.X / 2, Stage.Y / 2);
        }

        public static Vector2 getRandomSpeed(int min, int max, bool randomDirection = false)
        {
            if (randomDirection) { }
            return new Vector2(Random.Next(min, max), Random.Next(min, max));
        }
    }
}
