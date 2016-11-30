using System;
using AdventureGame.Main.GameManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AdventureGame.Main.Items;
using MonogameLevel;

namespace AdventureGame.Main.Characters
{
    [Flags]
    public enum CharacterType
    {
        Jodi = 0,
        Soochang = 1
    }

    public class Player : Character
    {
        public const int MAX_LIFE = 2;

        protected Vector2 startPosition = new Vector2(100, 500);
        protected int _havingFruits;
        private Weapon weapon;

        public bool Beinghit { get; set; }
        public bool HitFlag { get; set; }
        public bool NowJumping { get; set; }
        public bool IsDroped { get; set; }
        public int Score { get; set; }
        

        public Player(CharacterType type,
            Texture2D spriteSheet) 
            : base(spriteSheet)
        {
            numFrames = new int[] { 3, 3, 1, 3, 10, 10, 10, 10 };
            speed = new Vector2(4, 2);
            previousDirection = currentDirection = MoveDirection.Right;
            _destinationRectangle.X = (int)startPosition.X;
            _destinationRectangle.Y = (int)startPosition.Y;
            sizeFrame = new Point(150, 150);
            Life = Values.StartLife;
            Initialize();
        }

        public override void Update()
        {
            Update(MoveDirection.None, Vector2.Zero);
        }

        public void jump()
        {
            if (!IsDroped && !NowJumping)
            {
                _beforeJumpHeight = _destinationRectangle.Y;
                NowJumping = true;
            }
            if (_beforeJumpHeight - _maxJumpHeight > _destinationRectangle.Y)
            {
                stopJump();
            }
        }

        public void stopJump()
        {
            NowJumping = false;
            IsDroped = true;
        }



        public void Update(MoveDirection direction, Vector2 distance)
        {
            if (_enabled && ++tickCounter >= 16 / tickToUpdate)
            {
                if (Life == 0)
                    hide();
                tickCounter = 0;

                if (_destinationRectangle.Y >= Utility.Stage.Y)
                {
                    Beinghit = false;
                    IsDroped = false;
                    currentDirection = MoveDirection.Right;
                    _destinationRectangle.X = 100;
                    _destinationRectangle.Y = 200;
                }

                if (direction == MoveDirection.None)
                {
                    if (previousDirection == MoveDirection.RunningRight)
                        direction = MoveDirection.Right;
                    else if (previousDirection == MoveDirection.RunningLeft)
                        direction = MoveDirection.Left;
                    else
                        direction = previousDirection;
                }
                

                currentDirection = direction;
                move(distance);
                idxFrame = getFrameIndex();
                previousDirection = currentDirection;
            }
        }



        public void move(Vector2 distance)
        {
            _destinationRectangle.X += (int)distance.X;
            _destinationRectangle.Y += (int)distance.Y;
        }

        public override void handleCollision(CollisionType collisionType, object other)
        {
            switch (collisionType)
            {
                case CollisionType.PlayerWithEnemy:
                    hitEnemy();
                    break;
                case CollisionType.PlayerWithEnemyBullet:
                    hitEnemy();
                    break;
                case CollisionType.PlayerWithItem:
                    getItem(other);
                    break;
                case CollisionType.PlayerBulletWithEnemy:
                    break;
                case CollisionType.None:
                    break;
                default:
                    break;
            }
        }

        private void hitEnemy()
        {
            if(!Beinghit)
                lostLife();

            if (Life == 0)
            {
                hide();
            }
            else
                Beinghit = true;
        }

        private void getLife()
        {
            Life = Math.Min(++Life, Values.MaxLife);
        }

        private void lostLife()
        {
            Life = Math.Max(--Life, 0);
        }

        private void getItem(object other)
        {
            Item item = (Item)other;

            switch (item.ItemCategory)
            {
                case ItemType.Fruit:
                    Score += Values.FruitScore;
                    if (++_havingFruits == Values.NumberOfFruitToGetLife)
                        getLife();
                    break;

                case ItemType.Key:
                    Score += Values.KeyScore;
                    break;

                case ItemType.Life:
                    Score += Values.LifeScore;
                    getLife();
                    break;

                case ItemType.Weapon:
                    Score += Values.WeaponScore;
                    weapon = (Weapon)item;
                    break;
                default:
                    break;
            }
        }
    }
}
