using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TileGame
{
    internal class TitleScreen
    {
        public GameState gameState;

        public TitleScreen()
        {
            // Nothing needs to be done initially.
        }

        public void Update(MouseState currentMouseState, MouseState previousMouseState, KeyboardState currentKeyState, KeyboardState previousKeyState)
        {
            if (Math.Abs(currentMouseState.X) >= 128 && Math.Abs(currentMouseState.X) <= 383
                && Math.Abs(currentMouseState.Y) >= 192 && Math.Abs(currentMouseState.Y) <= 319
                && previousMouseState.LeftButton == ButtonState.Pressed
                && currentMouseState.LeftButton == ButtonState.Released)
            {
                gameState = GameState.Game;
            }

            if (Math.Abs(currentMouseState.X) >= 128 && Math.Abs(currentMouseState.X) <= 383
                && Math.Abs(currentMouseState.Y) >= 352 && Math.Abs(currentMouseState.Y) <= 479
                && previousMouseState.LeftButton == ButtonState.Pressed
                && currentMouseState.LeftButton == ButtonState.Released)
            {
                gameState = GameState.Quit;
            }
        }

        public void Draw(SpriteBatch spriteBatch, TextureList textureList)
        {
            spriteBatch.Draw(textureList.Get("title-screen"), new Vector2(0, 0), Color.White);
        }
    }
}