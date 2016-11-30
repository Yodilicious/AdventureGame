using System.Collections.Generic;
using AdventureGame.Main.Characters;
using AdventureGame.Main.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameLevel;

namespace AdventureGame.Main
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class AdventureGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        StartScreen startScreen;
        ActionScreen actionScreen;

        private Background background;
        
        public AdventureGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Window.Title = "Jodi and Soochang's Adventure";

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            IsFixedTimeStep = true;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Utility.Initialize(graphics);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            startScreen = new StartScreen(this, spriteBatch);
            Components.Add(startScreen);
            actionScreen = new ActionScreen(this, spriteBatch);
            Components.Add(actionScreen);

            Utility.HilightFont = Content.Load<SpriteFont>("fonts/hilightFont");

            var backgroundImage = Content.Load<Texture2D>("backgrounds/main_menu_background");
            background = new Background(backgroundImage, new Rectangle(0, 0, 1024, 768));

            HideAllScreens();
            startScreen.show();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            if (startScreen.Enabled && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                HideAllScreens();
                switch (startScreen.Menu.SelectedItem)
                {
                    case 0:
                        actionScreen.show();
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        Exit();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    HideAllScreens();
                    startScreen.show();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            background.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void HideAllScreens()
        {
            foreach (var component in Components)
            {
                if (component is GameScreen)
                {
                    ((GameScreen)component).hide();
                }
            }
        }

        private void ShowAllScreens()
        {
            foreach (var component in Components)
            {
                if (component is GameScreen)
                {
                    ((GameScreen)component).hide();
                }
            }
        }
    }
}
