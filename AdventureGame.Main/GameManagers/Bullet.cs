using AdventureGame.Main.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace AdventureGame.Main.GameManagers
{
    public enum BulletOwner
    {
        Player,
        Enemy        
    }

    public class Bullet : Item
    {

        public const int MaxBullet = 2;
        private BulletOwner _bulletOwner;

        public Bullet(Texture2D spriteSheet) : base(spriteSheet)
        {
        }

        public BulletOwner BulletOwner
        {
            get
            {
                return _bulletOwner;
            }

        }

        public Rectangle getBoundToCheckCollision()
        {
            return ImgDestination;
        }
    }
}
