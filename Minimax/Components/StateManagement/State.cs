using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minimax.Components.StateManagement
{
    public interface State
    {
        void Enter();
        void Leave();
        void Update(GameTime gameTime);
    }
}
