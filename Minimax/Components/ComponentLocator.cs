using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Minimax.Components
{
    public static class ComponentLocator
    {
        static Dictionary<string, GameObject> gameObjects;
        static Dictionary<string, Texture2D> textures;
        static Dictionary<string, SpriteFont> fonts;
        static GraphicsDeviceManager graphicsDeviceManager;

        public static void RegisterGameObjects(Dictionary<string, GameObject> gameObjects)
        {
            ComponentLocator.gameObjects = gameObjects;
        }

        public static void RegisterTextures(Dictionary<string, Texture2D> textures)
        {
            ComponentLocator.textures = textures;
        }

        public static void RegisterSpriteFonts(Dictionary<string, SpriteFont> fonts)
        {
            ComponentLocator.fonts = fonts;
        }

        public static void RegisterGraphicsDeviceManager(GraphicsDeviceManager graphics)
        {
            ComponentLocator.graphicsDeviceManager = graphics;
        }

        internal static GraphicsDeviceManager FindGraphicsDeviceManager()
        {
            return graphicsDeviceManager;
        }

        public static Texture2D FindTexture(string identifier)
        {
            Texture2D texture;

            textures.TryGetValue(identifier, out texture);

            return texture;
        }

        public static GameObject FindGameObject(string identifier)
        {
            GameObject gameObject;

            gameObjects.TryGetValue(identifier, out gameObject);

            return gameObject;
        }

        public static SpriteFont FindFont(string identifier)
        {
            SpriteFont font;

            fonts.TryGetValue(identifier, out font);

            return font;
        }
    }
}
