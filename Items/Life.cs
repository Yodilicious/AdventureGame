using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AdventureGame.Main.Items
{
    public class Life : Item
    {    
        public Life(Texture2D spriteSheet) 
            : base(spriteSheet)
        {
            _frameSize = new Vector2(50,50);
            _score = 200;
            _tickToUpdatePerSecond = 4;
            ImgDestination = new Rectangle(200, 200, 40, 40);
            Initialize();
        }
    }
}
