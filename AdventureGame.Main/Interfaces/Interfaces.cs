using AdventureGame.Main.GameManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Main.Interfaces
{
    public interface IDrawableComponent
    {
        bool isEnabled();
        void setEnabled(bool enabled);
        bool isVisible();
        void setVisible(bool visible);

        void Initialize();
        void Update();
        void Draw(SpriteBatch spriteBatch);

    }

    public interface IGetBound
    {
        Rectangle getBoundToCheckCollision();
    }

    public interface ICollisionHandler
    { 
        void handleCollision(CollisionType collisionType, object other);
    }
}
