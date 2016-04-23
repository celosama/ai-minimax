using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Minimax.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minimax.GameLogic
{
    public class Button : GameObject
    {
        Vector2 position;
        Rectangle area;
        string text;

        public Button(int x, int y, string text)
        {
            position = new Vector2(x, y);
            area = new Rectangle(position.ToPoint(), new Point(100, 50));

            this.text = text;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(ComponentLocator.FindFont("arial"), text, position, Color.Black);
        }
    }
}
