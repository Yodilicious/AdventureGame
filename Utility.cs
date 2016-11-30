using System;
using Microsoft.Xna.Framework;

namespace AdventureGame.Main
{
    public static class Utility 
    {
        public const int TICK_PER_SECOND = 32;
        public static Vector2 Stage { get; set; }
        public static Random Random { get; set; } = new Random();

        public static void Initialize(GraphicsDeviceManager graphic)
        {
            Stage = new Vector2(graphic.PreferredBackBufferWidth, graphic.PreferredBackBufferHeight);
        }

        public static Vector2 GetCenter()
        {
            return new Vector2(Stage.X / 2, Stage.Y / 2);
        }
    }
}
