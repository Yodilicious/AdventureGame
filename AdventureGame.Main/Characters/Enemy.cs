using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdventureGame.Main.Characters
{
    public class Enemy : Character
    {
        protected int score;
        protected bool isFoundPlayer = false;
        protected int tickToMove = 20;
        private int counterToMove;
        
        public Enemy(
            Texture2D spriteSheet
            ) 
            : base(spriteSheet)
        {
            this.speed = Vector2.Zero;
        }

        protected virtual void death()
        {
            hide();
        }
    }
}
