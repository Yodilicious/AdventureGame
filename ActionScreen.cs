using AdventureGame.Main.Characters;
using AdventureGame.Main.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AdventureGame.Main.Screens
{
    public class ActionScreen : GameScreen
    {
        protected Player player;
        protected List<Enemy> enemies;
        protected SpriteBatch _spriteBatch;
        protected Fruit fruit;
        protected Life life;
        
        public ActionScreen(Game game, SpriteBatch spriteBatch) 
            : base(game)
        {
            _spriteBatch = spriteBatch;
            player = new Player(CharacterType.Soochang, game.Content.Load<Texture2D>("characters/soochang.fw"));
            enemies = new List<Enemy>();
            fruit = new Fruit(game.Content.Load<Texture2D>("items/apple.fw"), FruitType.Apple);
            fruit.show();
            life = new Life(game.Content.Load<Texture2D>("items/heart.fw"));

            player.show();
            life.show();
            hide();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            player.Update();
            foreach(Enemy e in enemies)
            {
                e.Update();
            }
            fruit.Update();
            life.Update();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            player.Draw(_spriteBatch);
            foreach(Enemy e in enemies)
            {
                e.Draw(_spriteBatch);
            }
            fruit.Draw(_spriteBatch);
            life.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
