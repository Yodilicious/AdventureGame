using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AdventureGame.Main.Items
{
    public enum WeaponType
    {
        Pistols,
        HeavyMachineGun
    }

    class Weapon : Item
    {
        private WeaponType _type;

        public Weapon(Texture2D spriteSheet) 
            : base(spriteSheet)
        {
            _score = 5000;
            _tickToUpdatePerSecond = 4;
        }
    }
}
