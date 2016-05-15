using Minimax.Components.StateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Minimax.Components;

namespace Minimax.GameLogic.GameStates
{
    public class ChooseSymbol : State
    {
        State gameMode;

        Button symbolX, symbolY, player1Symbol, play, firstPlayer, player, cpu;
        Dictionary<string, GameObject> gameObjects;
        MouseState lastMouseState;

        public ChooseSymbol(StateManager stateManager, State nextState)
        {
            gameMode = nextState;

            player1Symbol = new Button(100, 100, GetPlayer1Text());
            symbolX = new Button(100, 150, "X");
            symbolY = new Button(150, 150, "O");

            firstPlayer = new Button(100, 200, WhoGoesFirst());
            player = new Button(100, 250, "Player");
            cpu = new Button(200, 250, "CPU");

            play = new Button(100, 300, "Play");

            symbolX.Click = () =>
            {
                GameSettings.Player1 = 'X';
                GameSettings.Player2 = 'O';
            };

            symbolY.Click = () =>
            {
                GameSettings.Player1 = 'O';
                GameSettings.Player2 = 'X';
            };

            player.Click = () =>
            {
                GameSettings.FirstPlayer = "Player";
            };

            cpu.Click = () =>
            {
                GameSettings.FirstPlayer = "CPU";
            };

            play.Click = () =>
            {
                State actualNextState = null;

                if (nextState.GetType() == typeof(PlayerVsCpu))
                    actualNextState = new PlayerVsCpu(stateManager);

                stateManager.ChangeState(actualNextState);
            };

            gameObjects = new Dictionary<string, GameObject>() {
                { "symbolX", symbolX },
                { "symbolY", symbolY },
                { "p1Label", player1Symbol },
                { "firstPlayer", firstPlayer },
                { "player", player },
                { "cpu", cpu },
                { "play", play }
            };
        }

        private string WhoGoesFirst()
        {
            return "Playing First: " + GameSettings.FirstPlayer.ToString();
        }

        private string GetPlayer1Text()
        {
            return "Player 1: " + GameSettings.Player1.ToString();
        }

        public void Enter()
        {
            lastMouseState = Mouse.GetState();

            foreach (KeyValuePair<string, GameObject> button in gameObjects)
            {
                var b = button.Value;

                if (b.GetType() == typeof(Button))
                {
                    Button reference = (Button)b;
                    reference.CalculateArea();
                }
            }
        }

        public void Leave()
        {
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (KeyValuePair<string, GameObject> entry in gameObjects)
            {
                entry.Value.Draw(spriteBatch, gameTime);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<string, GameObject> entry in gameObjects)
            {
                entry.Value.Update(gameTime);
            }

            player1Symbol.SetText(GetPlayer1Text());
            firstPlayer.SetText(WhoGoesFirst());

            HandleButtons(Mouse.GetState());
        }

        private void HandleButtons(MouseState mouseState)
        {
            MouseState currentMouseState = mouseState;

            foreach (KeyValuePair<string, GameObject> gameObject in gameObjects)
            {
                var gameObj = gameObject.Value;

                if (gameObj.GetType() == typeof(Button))
                {
                    if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        Button button = (Button)gameObj;

                        if (button.GetArea().Contains(new Point(currentMouseState.X, currentMouseState.Y)))
                            button.Click();
                    }
                }
            }

            lastMouseState = currentMouseState;
        }
    }
}
