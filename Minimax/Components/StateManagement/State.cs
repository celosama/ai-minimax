using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Minimax.Components.StateManagement
{
    public interface State
    {
        void Enter();
        void Leave();
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        void Update(GameTime gameTime);
    }
}
