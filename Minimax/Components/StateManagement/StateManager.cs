using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minimax.Components.StateManagement
{
    public class StateManager
    {
        State state;

        public StateManager(Game game, State state = null)
        {
            this.state = MakeState(state);
        }

        public void Update(GameTime gameTime)
        {
            state.Update(gameTime);
        }

        public void ChangeState(State state)
        {
            this.state.Leave();

            this.state = MakeState(state);
            this.state.Enter();
        }

        public State GetState()
        {
            return this.state;
        }

        private State MakeState(State state)
        {
            if (state == null)
            {
                return new NullState();
            }

            return state;
        }
    }
}
