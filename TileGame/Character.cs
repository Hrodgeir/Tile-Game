using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TileGame
{
    public class Character
    {
        public int pathIndex;
        public Tile[] path;
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

        public void Update(MouseState currentMouseState, MouseState previousMouseState, KeyboardState currentKeyState, KeyboardState previousKeyState, GameTime gameTime, Map map)
        {
            // Selects or deselects the character.
            if (Math.Abs(currentMouseState.X - (position.X + 15)) <= 15 
                && Math.Abs(currentMouseState.Y - (position.Y + 15)) <= 15
                && previousMouseState.LeftButton == ButtonState.Pressed
                && currentMouseState.LeftButton == ButtonState.Released)
            {
                selected = !selected;
            }

            // Deselects the character when clicking anywhere on the map.
            if (selected
                && (Math.Abs(currentMouseState.X - (position.X + 15)) > 15
                    || Math.Abs(currentMouseState.Y - (position.Y + 15)) > 15)
                && previousMouseState.LeftButton == ButtonState.Pressed
                && currentMouseState.LeftButton == ButtonState.Released)
            {
                selected = false;
            }

            // Finds the path from the current tile to the selected tile.
            if (selected
                && previousMouseState.RightButton == ButtonState.Pressed
                && currentMouseState.RightButton == ButtonState.Released)
            {
                foreach (Tile mapTile in map.tileList)
                {
                    if (Math.Abs(currentMouseState.X - (mapTile.position.X + 15)) <= 15
                        && Math.Abs(currentMouseState.Y - (mapTile.position.Y + 15)) <= 15)
                    {
                        // Clear the old path.
                        if (path != null)
                        {
                            for (int i = 1; i < path.Length; i++)
                            {
                                path[i].isPath = false;
                            }
                        }

                        // Create the new path.
                        pathIndex = 0;
                        path = map.FindPath(currentTile, mapTile).ToArray();
                        for (int i = 1; i < path.Length; i++)
                        {
                            path[i].isPath = true;
                        }
                    }
                }
            }

            // Moves the character one tile along the path when the enter key is pressed.
            if (selected && path != null && pathIndex < path.Length-1 && previousKeyState.IsKeyDown(Keys.Space) && currentKeyState.IsKeyUp(Keys.Space))
            {
                Tile nextTile = path[++pathIndex];
                currentTile = nextTile;
                position = nextTile.position;
                nextTile.isPath = false;
            }

            if (!selected)
            {
                path = null;
                foreach (Tile mapTile in map.tileList)
                {
                    mapTile.isPath = false;
                }
            }
        }
    }
}