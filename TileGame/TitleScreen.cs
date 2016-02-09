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
            if (Math.Abs(currentMouseState.X) >= 118 && Math.Abs(currentMouseState.X) <= 404
                && Math.Abs(currentMouseState.Y) >= 153 && Math.Abs(currentMouseState.Y) <= 287
                && previousMouseState.LeftButton == ButtonState.Pressed
                && currentMouseState.LeftButton == ButtonState.Released)
            {
                gameState = GameState.Game;
            }

            if (Math.Abs(currentMouseState.X) >= 118 && Math.Abs(currentMouseState.X) <= 404
                && Math.Abs(currentMouseState.Y) >= 332 && Math.Abs(currentMouseState.Y) <= 466
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