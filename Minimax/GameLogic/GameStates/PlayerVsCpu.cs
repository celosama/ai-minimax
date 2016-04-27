using Minimax.Components.StateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Minimax.Components;

namespace Minimax.GameLogic.GameStates
{
    public class PlayerVsCpu : State
    {
        StateManager stateManager;
        Point screenCenter;

        char human = GameSettings.Player1, cpu = GameSettings.Player2;
        char currentTurn = GameSettings.FirstPlayer;

        Board board;

        public PlayerVsCpu(StateManager stateManager)
        {
            this.stateManager = stateManager;

            board = new Board();
        }

        private string GetPlayerTurnText()
        {
            return (currentTurn == GameSettings.Player1) ? "Player" : "Computer";
        }

        public void Enter()
        {
            GraphicsDeviceManager graphics = ComponentLocator.FindGraphicsDeviceManager();
            screenCenter = new Point(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            board.MakeMove(new Move(new Point(0, 0)), human);
            board.MakeMove(new Move(new Point(1, 1)), human);
            board.MakeMove(new Move(new Point(2, 2)), human);
            board.MakeMove(new Move(new Point(1, 2)), cpu);
        }

        public void Leave()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(ComponentLocator.FindFont("arial"), "Current Turn: " + GetPlayerTurnText(), new Vector2(10, 10), Color.White);
            //drawLines(spriteBatch);
            drawBoard(spriteBatch, board);
        }

        private void drawBoard(SpriteBatch spriteBatch, Board board)
        {
            char[,] nodes = board.GetBoard();
            //Vector2 margin = new Vector2(304, 204);
            Vector2 margin = new Vector2(screenCenter.X - (64 * 3 / 2), screenCenter.Y - (64 * 3 / 2));
            Vector2 padding = new Vector2(2, 2);

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    char player = nodes[x, y];
                    Vector2 position = new Vector2(64 * y, 64 * x);
                    Vector2 finalPosition = position + margin;

                    if (player != ' ')
                        spriteBatch.Draw(ComponentLocator.FindTexture(player.ToString()), new Rectangle(finalPosition.ToPoint(), new Point(64, 64)), Color.White);
                }
            }
        }

        private void drawLines(SpriteBatch spriteBatch)
        {
            drawSingleLine(
                spriteBatch,
                new Vector2(screenCenter.X - 100, screenCenter.Y - 245),
                new Vector2(screenCenter.X - 100, screenCenter.Y + 245)
            );

            drawSingleLine(
                spriteBatch,
                new Vector2(screenCenter.X + 100, screenCenter.Y - 245),
                new Vector2(screenCenter.X + 100, screenCenter.Y + 245)
            );

            drawSingleLine(
                spriteBatch,
                new Vector2(screenCenter.X - 300, screenCenter.Y - 95),
                new Vector2(screenCenter.X + 300, screenCenter.Y - 95)
            );

            drawSingleLine(
                spriteBatch,
                new Vector2(screenCenter.X - 300, screenCenter.Y + 95),
                new Vector2(screenCenter.X + 300, screenCenter.Y + 95)
            );
        }

        private void drawSingleLine(SpriteBatch spriteBatch, Vector2 lineStart, Vector2 lineEnd)
        {
            Vector2 edge = lineEnd - lineStart;
            float angle = (float)Math.Atan2(edge.Y, edge.X);

            spriteBatch.Draw(
                ComponentLocator.FindTexture("Line"),
                new Rectangle((int)lineStart.X, (int)lineStart.Y, (int)edge.Length(), 5),
                null,
                Color.Red,
                angle,
                new Vector2(0, 0),
                SpriteEffects.None,
                0
            );
        }
    }
}
