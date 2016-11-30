using AdventureGame.Main.Characters;
using AdventureGame.Main.GameManagers;
using AdventureGame.Main.Interfaces;
using AdventureGame.Main.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using MonogameLevel;
using MonogameLevel.Helpers;

namespace AdventureGame.Main.Screens
{
    public class ActionScreen : GameScreen
    {
        private LevelManager levelManager;
        private Dictionary<Vector2, Tile> level;

        protected bool _initated;

        protected Player _player;
        protected SpriteBatch _spriteBatch;
        protected SpriteFont _spriteFont;

        protected StatusBar _status;
        protected List<Enemy> _enemies;
        protected List<Bullet> _bullets;
        protected List<Item> _items;
        protected CollisionManager _collisionManager;
        protected PositionManager _positionManager;
        protected KeyboardState oldKey;
        private Background scrolling1;
        private Background scrolling2;
        //protected InputManager _inputManager =
        //    new InputManager()
        //    {
        //        KeyDown = Keys.Down,
        //        KeyLeft = Keys.Left,
        //        KeyRight = Keys.Right,
        //        KeyUp = Keys.Up,
        //        Shoot = Keys.Space
        //    };

        public ActionScreen(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            _spriteBatch = spriteBatch;
            _enemies = new List<Enemy>();
            _bullets = new List<Bullet>();
            _items = new List<Item>();
        }

        public override void Initialize()
        {
            _initated = true;
            _player = new Player(CharacterType.Soochang, Game.Content.Load<Texture2D>("characters/soochang.fw"));
            _items.Add(new Fruit(Game.Content.Load<Texture2D>("items/apple.fw"), FruitType.Apple));
            _items.Add(new Life(Game.Content.Load<Texture2D>("items/heart.fw")));
            _enemies.Add(new ScrollingEnemy(Game.Content.Load<Texture2D>("enemies/enemy1"), _player, 2));
            _collisionManager = new CollisionManager(_player, _enemies, _items, _bullets);
            _status = new StatusBar(Game, _player);
            _spriteFont = Game.Content.Load<SpriteFont>("fonts/hilightFont");
            levelManager = new LevelManager(Game.Content.Load<Texture2D>("levels/game_sprite_level"));
            level = levelManager.LoadLevel("Levels/SecondLevel.lvl");
            _positionManager = new PositionManager(level, _player);
            Texture2D first = Game.Content.Load<Texture2D>("backgrounds/level_background");
            Texture2D second = Game.Content.Load<Texture2D>("backgrounds/level_background");
            scrolling1 = new Background(first, new Rectangle(0, 0, 1024, 768));
            scrolling2 = new Background(second, new Rectangle(1024, 0, 1024, 768));
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {            
            if (Enabled)
            {
                if (!_initated) Initialize();

                //user input management
                KeyboardState ks = Keyboard.GetState();

                MoveDirection tempDirection = MoveDirection.None;

                if (ks.IsKeyUp(Keys.Right) && oldKey.IsKeyDown(Keys.Right))
                {
                    tempDirection = MoveDirection.Right;
                }
                else if (ks.IsKeyDown(Keys.Right))
                {
                    tempDirection = MoveDirection.RunningRight;
                }

                if (ks.IsKeyUp(Keys.Left) && oldKey.IsKeyDown(Keys.Left))
                {
                    tempDirection = MoveDirection.Left;
                }
                else if (ks.IsKeyDown(Keys.Left))
                {
                    tempDirection = MoveDirection.RunningLeft;
                }


                
                if (ks.IsKeyDown(Keys.Space))
                {
                    // to shoot
                }

                if (ks.IsKeyUp(Keys.Up) && oldKey.IsKeyDown(Keys.Up))
                {
                    _player.stopJump();
                }
                else if (ks.IsKeyDown(Keys.Up))
                {
                    _player.jump();
                }
                
                _player.Update(tempDirection, _positionManager.getPosition(tempDirection));

                if (_player.Beinghit)
                {
                    tempDirection = MoveDirection.Down;
                }

                oldKey = ks;

                //Only when stage 1

                generateScrollingEnemy();

                _collisionManager.checkAllCollision();
                _status.Update();
                UpdateList(_enemies);
                UpdateList(_items);
                UpdateList(_bullets);
                scrolling1.Update(-1);
                scrolling2.Update(-1);
                base.Update(gameTime);
            }
        }
        
        private void generateScrollingEnemy()
        {
            if(ScrollingEnemy.numOfEnemy < Values.MaxScrollingEnemy && 
                Utility.Random.Next(250) == 1)
            {
                _enemies.Add(new ScrollingEnemy(Game.Content.Load<Texture2D>("enemies/enemy1"), _player, 2));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                _spriteBatch.Begin();
                scrolling1.Draw(_spriteBatch);
                scrolling2.Draw(_spriteBatch);
                _player.Draw(_spriteBatch);
                DrawList(_enemies);
                DrawList(_bullets);
                DrawList(_items);
                _status.Draw(_spriteBatch, _spriteFont);
                foreach (var tile in level)
                {
                    tile.Value.Draw(_spriteBatch);
                }
                _spriteBatch.End();
                base.Draw(gameTime);
            }
        }

        public void UpdateList<T> (List<T> listItems)
        {
            IDrawableComponent item;
            for(int i = 0; i < listItems.Count; i++)
            {
                if(listItems[i] is IDrawableComponent)
                {
                    item = (IDrawableComponent)listItems[i];

                    if(!item.isEnabled() && !item.isVisible())
                    {
                        listItems.Remove(listItems[i]);
                    }
                    else
                    {
                        UpdateIDrawableComponent(item);
                    }
                }
            }
        }                

        private void UpdateIDrawableComponent(IDrawableComponent component)
        {
            if (component.isEnabled())
            {
                component.Update();
            }
        }       
        
        private void DrawIDrawableComponent(IDrawableComponent component)
        {
            if (component.isVisible())
            {
                component.Draw(_spriteBatch);
            }
        }

        public void DrawList<T>(List<T> listItems)
        {
            foreach (T listItem in listItems)
            {
                if (listItem is IDrawableComponent)
                {
                    DrawIDrawableComponent((IDrawableComponent)listItem);
                }
            }
        }

        
    }
}
