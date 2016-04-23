using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Minimax.Components.StateManagement
{
    public class NullState : State
    {
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // do nothing
        }

        public void Enter()
        {
            // do nothing
        }

        public void Leave()
        {
            // do nothing
        }

        public void Update(GameTime gameTime)
        {
            // do nothing
        }
    }
}
