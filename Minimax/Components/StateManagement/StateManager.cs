using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Minimax.Components.StateManagement
{
    public class StateManager
    {
        State state;

        public StateManager(Game game, State state = null)
        {
            this.state = MakeState(state);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            state.Draw(spriteBatch, gameTime);
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
            return state;
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
