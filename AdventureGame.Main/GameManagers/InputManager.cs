using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Main
{
    public class InputManager
    {
        public KeyboardState CurrentKeyState { get; set; }
        public KeyboardState OldKeyState { get; set; }
        public Keys KeyUp { get; set; }
        public Keys KeyDown { get; set; }
        public Keys KeyRight { get; set; }
        public Keys KeyLeft { get; set; }
        public Keys Shoot { get; set; }

        public bool isSameDirection()
        {
            return CurrentKeyState == OldKeyState;
        }
    }
}
