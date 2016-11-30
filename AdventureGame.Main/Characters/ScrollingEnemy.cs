using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using AdventureGame.Main.GameManagers;

namespace AdventureGame.Main.Characters
{
    class ScrollingEnemy : Enemy
    {
        public static int numOfEnemy;
        public ScrollingEnemy(
            Texture2D spriteSheet,
            Player p,
            int tickToMove
            ) 
            : base(spriteSheet)
        {
            numOfEnemy++;
            this.tickToMove = tickToMove;
            speed = Utility.getRandomSpeed(3,9);
            _destinationRectangle.X = (int)Utility.Stage.X;
            _destinationRectangle.Y = p._destinationRectangle.Y;
            numFrames = new int[] { 4 };
            sizeFrame = new Point(114, 85);
            base.Initialize();
        }
        
        public override void Update()
        {
            if(_enabled && ++tickCounter == tickToMove)
            {
                if(_destinationRectangle.X + _destinationRectangle.Width < 0)
                {
                    numOfEnemy--;
                    Console.WriteLine(numOfEnemy.ToString());
                    hide();
                }
                tickCounter = 0;
                idxFrame = ++idxFrame % numFrames[(int)currentDirection];
                _destinationRectangle.X -= (int)speed.X;

            }
        }

        public override void handleCollision(CollisionType collisionType, object other)
        {
            switch (collisionType)
            {
                case CollisionType.PlayerWithEnemy:
                    break;
                case CollisionType.PlayerWithEnemyBullet:
                    break;
                case CollisionType.PlayerWithItem:
                    break;
                case CollisionType.PlayerBulletWithEnemy:
                    break;
                case CollisionType.None:
                    break;
                default:
                    break;
            }
        }
    }
}
