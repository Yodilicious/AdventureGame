using AdventureGame.Main.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using AdventureGame.Main.Items;
using AdventureGame.Main.Characters;
using Microsoft.Xna.Framework;

namespace AdventureGame.Main.Screens
{
    public class StatusBar : IDrawableComponent
    {
        private bool _initiated;
        private const int LIFE_X_POSITION = 20;
        private const int LIFE_Y_POSITION = 20;
        private bool _enabled;
        private bool _visible;

        private Player _player;
        private Life _life;
        private string[] _message;
        private Vector2[] _position;
        
        public StatusBar(
            Game game,
            Player player)
        {
            _life = new Life(game.Content.Load<Texture2D>("items/heart.fw"));
            _player = player;
            _message = new string[2];
            _position = new Vector2[2];
        }

        public void Initialize()
        {
            _initiated = true;
            _visible = true;
            _enabled = true;

            _life.show();
            _life.ImgDestination =
                new Rectangle(
                    LIFE_X_POSITION,
                    LIFE_Y_POSITION,
                    _life.ImgDestination.Width,
                    _life.ImgDestination.Height
                );
        }

        public void Update()
        {
            if (!_initiated) Initialize();

            _life.Update();
            _message[0] = $" X {_player.Life}";
            _message[1] = $"Score: {_player.Score}";
            _position[0] =
                new Vector2(
                    _life.ImgDestination.X + _life.ImgDestination.Width,
                    _life.ImgDestination.Y + (_life.ImgDestination.Center.Y - _life.ImgDestination.Y) / 2);
            _position[1] =
                new Vector2(
                    _position[0].X + Utility.HilightFont.MeasureString(_message[0]).X + Utility.HilightFont.LineSpacing, 
                    _position[0].Y);
        }

        public virtual void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            _life.Draw(spriteBatch);
            for(int i = 0; i < _message.Length; i++)
            {
                spriteBatch.DrawString(spriteFont, _message[i], _position[i], Color.White);
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {}

        public bool isEnabled()
        {
            return _enabled;
        }

        public bool isVisible()
        {
            return _visible;
        }

        public void setEnabled(bool enabled)
        {
            _enabled = enabled;
        }

        public void setVisible(bool visible)
        {
            _visible = visible;
        }

        
    }
}
