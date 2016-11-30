using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdventureGame.Main.Screens
{
    public class StartScreen : GameScreen
    {
        protected string[] _menuItems = {"Start Game", "Best Score", "How to Play", "Help", "About", "Setting", "Exit" };
        protected MenuComponent _menu;
        protected SpriteBatch _spriteBatch;
        private Background scrolling1;
        private Background scrolling2;

        public MenuComponent Menu
        {
            get
            {
                return _menu;
            }

            set
            {
                _menu = value;
            }
        }

        public StartScreen(Game game,
            SpriteBatch spriteBatch)
            :base(game)
        {
            _menu = new MenuComponent(game, spriteBatch, game.Content.Load<SpriteFont>("fonts/regularFont"), game.Content.Load<SpriteFont>("fonts/hilightFont"), _menuItems);
            _spriteBatch = spriteBatch;
            Texture2D first = game.Content.Load<Texture2D>("backgrounds/landscape1");
            Texture2D second = game.Content.Load<Texture2D>("backgrounds/landscape");

            
            scrolling1 = new Background(first, new Rectangle(0, 0, 1024, 768));
            scrolling2 = new Background(second, new Rectangle(1024, 0, 1024, 768));
            Components.Add(_menu);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            
            //if (scrolling1.rectangle.X + scrolling1.texture.Width <= 0)
            //{
            //    scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.texture.Width;
            //}

            //if (scrolling2.rectangle.X + scrolling2.texture.Width <= 0)
            //{
            //    scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.texture.Width;
            //}

            //if (scrolling1.rectangle.X >= scrolling1.rectangle.Width)
            //{
            //    scrolling1.rectangle.X = scrolling2.rectangle.X - scrolling2.texture.Width;
            //}

            //if (scrolling2.rectangle.X >= scrolling2.rectangle.Width)
            //{
            //    scrolling2.rectangle.X = scrolling1.rectangle.X - scrolling1.texture.Width;
            //}

            //if (Keyboard.GetState().IsKeyDown(Keys.Left))
            //{
                scrolling1.Update(-1);
                scrolling2.Update(-1);
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.Right))
            //{
            //    scrolling1.Update(-1);
            //    scrolling2.Update(-1);
            //}
            base.Update(gameTime); 
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            scrolling1.Draw(_spriteBatch);
            scrolling2.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
