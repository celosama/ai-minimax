using Minimax.Components.StateManagement;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Minimax.Components;

namespace Minimax.GameLogic.GameStates
{
    public class MainMenu : State
    {
        Dictionary<string, GameObject> gameObjects;

        Button playerVsCpu, playerVsPlayer, difficulty;

        public MainMenu()
        {
            playerVsCpu = new Button(100, 100, "Player VS CPU");
            playerVsPlayer = new Button(100, 200, "Player VS Player");
            difficulty = new Button(100, 300, "Difficulty");

            gameObjects = new Dictionary<string, GameObject>() {
                { "playerVsCpu", playerVsCpu },
                { "playerVsPlayer", playerVsPlayer },
                { "difficulty", difficulty }
            };
        }

        public void Enter()
        {
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

        }
    }
}
