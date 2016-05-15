using Minimax.Components.StateManagement;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Minimax.Components;
using Microsoft.Xna.Framework.Input;

namespace Minimax.GameLogic.GameStates
{
    public class MainMenu : State
    {
        Dictionary<string, GameObject> gameObjects;
        MouseState lastMouseState;

        Button playerVsCpu, playerVsPlayer, difficulty;

        public MainMenu(StateManager stateManager)
        {
            playerVsCpu = new Button(100, 100, "Player VS CPU");
            playerVsPlayer = new Button(100, 200, "Player VS Player");

            playerVsCpu.Click = () => {
                stateManager.ChangeState(new ChooseSymbol(stateManager, new PlayerVsCpu(null)));
            };

            playerVsPlayer.Click = () => {
            };

            gameObjects = new Dictionary<string, GameObject>() {
                { "playerVsCpu", playerVsCpu },
                { "playerVsPlayer", playerVsPlayer }
            };
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
