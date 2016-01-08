using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Timeravel
{
    public class Tile
    {
        public Vector2 size;
        public Vector2 position;
        public TextureList textureList;
        public Texture2D texture;
        public bool hover;
        public bool restricted;
        public bool isPath;

        public Tile(Texture2D setTexture, Vector2 setPosition)
        {
            size = new Vector2(32, 32);
            texture = setTexture;
            position = setPosition;
        }
    }
}