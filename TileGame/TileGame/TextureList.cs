using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Timeravel
{
    public class TextureList
    {
        public ContentManager content;
        private Dictionary<string, Texture2D> textures;

        public TextureList(ContentManager Content)
        {
            content = Content;
            textures = LoadTextures();
        }

        private Dictionary<string, Texture2D> LoadTextures()
        {
            Dictionary<string, Texture2D> loadedTextures = new Dictionary<string, Texture2D>();
            foreach (string fileName in Directory.GetFiles(content.RootDirectory))
            {
                string actualFileName = fileName.Split('\\', '.')[1];
                Texture2D texture = content.Load<Texture2D>(actualFileName);
                loadedTextures.Add(actualFileName, texture);
            }
            return loadedTextures;
        }

        public Texture2D Get(string textureName)
        {
            return textures[textureName];
        }
    }
}
