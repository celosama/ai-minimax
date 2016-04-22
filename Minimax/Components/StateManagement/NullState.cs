using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Minimax.Components.StateManagement
{
    public class NullState : State
    {
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
