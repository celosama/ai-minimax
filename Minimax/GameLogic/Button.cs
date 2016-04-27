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
            this.text = text;
            position = new Vector2(x, y);
        }

        public void CalculateArea()
        {
            area = new Rectangle(position.ToPoint(), ComponentLocator.FindFont("arial").MeasureString(text).ToPoint());
            Console.WriteLine(area);
        }

        public Rectangle GetArea()
        {
            return area;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(ComponentLocator.FindFont("arial"), text, position, Color.Black);
        }

        public void SetText(string text)
        {
            this.text = text;
        }

        public Action Click = () => { };
    }
}
