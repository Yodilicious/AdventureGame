using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AdventureGame.Main.Items
{
    public enum FruitType
    {
        Apple,
        Banana,
        Pinapple
    }
    public class Fruit : Item
    {
        protected FruitType _type;

        public Fruit(Texture2D spriteSheet,
            FruitType type) 
            : base(spriteSheet)
        {
            _type = type;
            _score = 500 + 100 * (int)_type;
            _tickToUpdatePerSecond = 2;
            SetFrameSize(_type);
            ImgDestination = new Rectangle(new Point(20, 20), _frameSize.ToPoint());
            Initialize();
        }


        public void SetFrameSize(FruitType type)
        {
            switch(type)
            {
                case FruitType.Apple:
                    _frameSize = new Vector2(40, 40);
                    break;
                case FruitType.Banana:
                    _frameSize = new Vector2(30, 51);
                    break;
                default:
                    break;
            }
        }        
    }
}
