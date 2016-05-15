using Minimax.Components.StateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Minimax.Components;
using Microsoft.Xna.Framework.Input;

namespace Minimax.GameLogic.GameStates
{
    public class PlayerVsCpu : State
    {
        StateManager stateManager;
        Point screenCenter;
        Dictionary<Point, Rectangle> boundingBoxes;

        char human, cpu;
        char currentTurn;

        Board board;

        Rectangle reset;

        MouseState lastMouseState;

        public PlayerVsCpu(StateManager stateManager)
        {
            this.stateManager = stateManager;
            human = GameSettings.Player1;
            cpu = GameSettings.Player2;

            board = new Board(human);
            boundingBoxes = new Dictionary<Point, Rectangle>();
            lastMouseState = Mouse.GetState();
            currentTurn = board.CurrentPlayer();
        }

        private string GetPlayerTurnText()
        {
            return (currentTurn == GameSettings.Player1) ? "Player" : "Computer";
        }

        public void Enter()
        {
            GraphicsDeviceManager graphics = ComponentLocator.FindGraphicsDeviceManager();
            screenCenter = new Point(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);

            reset = new Rectangle(screenCenter.X * 2 - 128, screenCenter.Y * 2 - 128, 64, 64);
        }

        public void Leave()
        {
        }

        public void Update(GameTime gameTime)
        {
            List<Move> moves = board.GetMoves();
            boundingBoxes.Clear();

            foreach (Move move in moves)
            {
                Rectangle value;
                bool found = boundingBoxes.TryGetValue(move.GetPosition(), out value);

                if (!found)
                {
                    Point location = new Point(move.GetPosition().X, move.GetPosition().Y);

                    Vector2 margin = new Vector2(screenCenter.X - (64 * 3 / 2), screenCenter.Y - (64 * 3 / 2));
                    Vector2 position = new Vector2(64 * location.Y, 64 * location.X);
                    Vector2 finalPosition = position + margin;

                    boundingBoxes.Add(location, new Rectangle(finalPosition.ToPoint(), new Point(64, 64)));
                }
            }

            checkMoves(Mouse.GetState());
            checkReset(Mouse.GetState());

            lastMouseState = Mouse.GetState();
        }

        private void checkReset(MouseState mouseState)
        {
            MouseState currentMouseState = mouseState;

            if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
            {
                if (reset.Contains(mouseState.X, mouseState.Y))
                {
                    board.Reset();
                }
            }

        }

        private void checkMoves(MouseState mouseState)
        {
            MouseState currentMouseState = mouseState;
            Point mousePosition = new Point(currentMouseState.X, currentMouseState.Y);

            if (board.CurrentPlayer() == human)
            {
                if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
                {
                    foreach (KeyValuePair<Point, Rectangle> boundingBox in boundingBoxes)
                    {
                        if (boundingBox.Value.Contains(mousePosition))
                        {
                            board.MakeMove(new Move(boundingBox.Key), human);
                        }
                    }
                }
            }
            else
            {
                MinimaxResult result = Minimax.Do(board, cpu, 8, 0);

                Move move = result.GetMove();

                board.MakeMove(move, cpu);
            }

            currentTurn = board.CurrentPlayer();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!board.IsGameOver())
                spriteBatch.DrawString(ComponentLocator.FindFont("arial"), "Current Turn: " + GetPlayerTurnText(), new Vector2(10, 10), Color.White);
            else
            {
                string text = "";

                if (board.GetGameState() == Board.State.Draw)
                    text = "Draw";
                else if (board.GetGameState() == Board.State.PlayerXWins)
                    text = "Player X Wins";
                else if (board.GetGameState() == Board.State.PlayerOWins)
                    text = "Player O Wins";

                Point textSize = ComponentLocator.FindFont("arial").MeasureString(text).ToPoint();
                Point position = new Point(screenCenter.X, screenCenter.Y) - textSize / new Point(2, 2) + new Point(0, -150);
                spriteBatch.DrawString(ComponentLocator.FindFont("arial"), text, position.ToVector2(), Color.White);
            }

            drawLines(spriteBatch);
            drawBoard(spriteBatch, board);

            spriteBatch.Draw(ComponentLocator.FindTexture("Reset"), reset, Color.White);
            //drawDebug(spriteBatch);
        }

        private void drawDebug(SpriteBatch spriteBatch)
        {
            foreach (KeyValuePair<Point, Rectangle> boundingBox in boundingBoxes)
            {
                spriteBatch.Draw(ComponentLocator.FindTexture("Reset"), boundingBox.Value, Color.Red);
            }
        }

        private void drawBoard(SpriteBatch spriteBatch, Board board)
        {
            char[,] nodes = board.GetBoard();

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
                new Vector2(screenCenter.X - 32, screenCenter.Y - 96),
                new Vector2(screenCenter.X - 32, screenCenter.Y + 96)
            );

            drawSingleLine(
                spriteBatch,
                new Vector2(screenCenter.X + 32, screenCenter.Y - 96),
                new Vector2(screenCenter.X + 32, screenCenter.Y + 96)
            );

            drawSingleLine(
                spriteBatch,
                new Vector2(screenCenter.X - 96, screenCenter.Y - 32),
                new Vector2(screenCenter.X + 96, screenCenter.Y - 32)
            );

            drawSingleLine(
                spriteBatch,
                new Vector2(screenCenter.X - 96, screenCenter.Y + 32),
                new Vector2(screenCenter.X + 96, screenCenter.Y + 32)
            );
        }

        private void drawSingleLine(SpriteBatch spriteBatch, Vector2 lineStart, Vector2 lineEnd)
        {
            Vector2 edge = lineEnd - lineStart;
            float angle = (float)Math.Atan2(edge.Y, edge.X);

            spriteBatch.Draw(
                ComponentLocator.FindTexture("Line"),
                new Rectangle((int)lineStart.X, (int)lineStart.Y, (int)edge.Length(), 2),
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
