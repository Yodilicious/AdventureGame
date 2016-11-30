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
        
        public Enemy(Texture2D spriteSheet) 
            : base(spriteSheet)
        {
            this.speed = Vector2.Zero;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update()
        {
            if(Enabled && ++counterToMove == tickToMove)
            {
                bool isGoingLeft = (Utility.Random.Next() % 2 == 0);
                if (isGoingLeft)
                {
                    currentDirection = MoveDirection.RunningLeft;
                }
                else
                {
                    currentDirection = MoveDirection.RunningRight;
                }
                counterToMove = 0;
            }

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public bool findPlayer(Player p)
        {
            return true;
        }
    }
}
