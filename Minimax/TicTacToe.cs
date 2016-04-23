using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Minimax.Components;
using Minimax.GameLogic;
using System.Collections.Generic;

namespace Minimax
{
    public class TicTacToe : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texX, texO, texReset;
        Button button;
        SpriteFont font;

        Dictionary<string, GameObject> gameObjects;
        Dictionary<string, Texture2D> textures;

        StateManager stateManager;

        public TicTacToe()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            Content.RootDirectory = "Content";

            button = new Button(0, 0, "Button1");

            gameObjects = new Dictionary<string, GameObject>()
            {
                { "Button1", button }
            };

            ComponentLocator.RegisterGameObjects(gameObjects);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("arial");

            texX = Content.Load<Texture2D>("X");
            texO = Content.Load<Texture2D>("O");
            texReset = Content.Load<Texture2D>("Reset");

            textures = new Dictionary<string, Texture2D>()
            {
                { "X", texX },
                { "O", texO },
                { "Reset", texReset },
                { "Button", texReset }
            };

            ComponentLocator.RegisterTextures(textures);
            ComponentLocator.RegisterSpriteFonts(new Dictionary<string, SpriteFont>() { { "arial", font } });
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (KeyValuePair<string, GameObject> entry in gameObjects)
            {
                entry.Value.Draw(spriteBatch, gameTime);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
