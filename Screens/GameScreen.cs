using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AdventureGame.Main.Screens
{
    public class GameScreen : DrawableGameComponent
    {
        protected List<GameComponent> _components = new List<GameComponent>();
        
        public List<GameComponent> Components
        {
            get
            {
                return _components;
            }

            set
            {
                _components = value;
            }
        }

        public GameScreen(Game game)
            : base(game)
        {}

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in _components)
            {
                if (component.Enabled)
                    component.Update(gameTime);
            }
            base.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent temp;
            foreach (GameComponent component in _components)
            {
                if (component is DrawableGameComponent)
                {
                    temp = (DrawableGameComponent)component;
                    if (temp.Visible)
                        temp.Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }

        public void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        public void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }
    }
}
