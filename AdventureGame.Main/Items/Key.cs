using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AdventureGame.Main.Items
{
    public enum Keytype
    {
        CageKey,
        DoorKey
    }
    public class Key : Item
    {
        protected Keytype _type;
        public Key(Texture2D spriteSheet,
            Keytype type)
            : base(spriteSheet)
        {
            _type = type;
            _itemCategory = ItemType.Key;
            _tickToUpdatePerSecond = 4;
        }
    }
}
