using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdventureGame.Main.Screens
{
    public class StartScreen : GameScreen
    {
        protected string[] _menuItems = {"Start Game", "Best Score", "How to Play", "Help", "About", "Setting", "Exit" };
        protected MenuComponent _menu;
        protected SpriteBatch _spriteBatch;
        

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
            : base(game)
        {
            _menu = new MenuComponent(game, spriteBatch, game.Content.Load<SpriteFont>("fonts/regularFont"), game.Content.Load<SpriteFont>("fonts/hilightFont"), _menuItems);
            _spriteBatch = spriteBatch;
            Texture2D first = game.Content.Load<Texture2D>("backgrounds/landscape1");
            Texture2D second = game.Content.Load<Texture2D>("backgrounds/landscape");
            
            
            Components.Add(_menu);
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime); 
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
           
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
