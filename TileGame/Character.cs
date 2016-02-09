using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TileGame
{
    public class Character
    {
        public Vector2 speed;
        public Vector2 position;
        public Vector2 minPosition;
        public Vector2 maxPosition;
        public Tile currentTile;
        public bool selected;

        public Character(Viewport viewport, Tile tile)
        {
            speed = Vector2.Zero;
            position = tile.position;
            minPosition = Vector2.Zero;
            maxPosition = new Vector2(viewport.Width, viewport.Height);
            currentTile = tile;
            selected = false;
        }

        public void Draw(SpriteBatch spriteBatch, TextureList textureList)
        {
            if (selected)
            {
                spriteBatch.Draw(textureList.Get("character-selected"), position, Color.White);
            }
            else
            {
                spriteBatch.Draw(textureList.Get("character"), position, Color.White);
            }
        }

        public void Update(MouseState currentMouseState, MouseState previousMouseState, GameTime gameTime, Map map)
        {
            List<Tile> path = new List<Tile>();

            // Selects or deselects the character.
            if (Math.Abs(currentMouseState.X - (position.X + 16)) <= 16 
                && Math.Abs(currentMouseState.Y - (position.Y + 16)) <= 16
                && previousMouseState.LeftButton == ButtonState.Pressed
                && currentMouseState.LeftButton == ButtonState.Released)
            {
                selected = !selected;
            }

            // Moves the selected character.
            if (selected
                && previousMouseState.RightButton == ButtonState.Pressed
                && currentMouseState.RightButton == ButtonState.Released)
            {
                foreach (Tile tile in map.tileList)
                {
                    if (Math.Abs(currentMouseState.X - (tile.position.X + 16)) <= 16
                        && Math.Abs(currentMouseState.Y - (tile.position.Y + 16)) <= 16)
                    {
                        path = map.FindPath(currentTile, tile);
                    }
                }
            }

            foreach (Tile tile in path)
            {
                KeyboardState keyState = new KeyboardState();
                if (keyState.IsKeyDown(Keys.A))
                    position = tile.position;
            }
        }
    }
}