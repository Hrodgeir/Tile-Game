using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Timeravel
{
    public class Camera
    {
        public Vector2 Position;
        public Vector2 Origin;
        public float Zoom;
        public float Rotation;

        public Camera(Viewport viewport)
        {
            Zoom = 1.0f;
            Origin = new Vector2(viewport.Width / 2, viewport.Height / 2);
        }

        public Matrix GetViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-Position * parallax, 0.0f))
                * Matrix.CreateTranslation(new Vector3(-Origin, 0.0f))
                * Matrix.CreateRotationZ(Rotation)
                * Matrix.CreateScale(Zoom, Zoom, 1)
                * Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        public void Update(MouseState currentMouseState, MouseState previousMouseState)
        {
            if (currentMouseState.ScrollWheelValue > previousMouseState.ScrollWheelValue)
            {
                Zoom += 0.25f;
            }
            if (currentMouseState.ScrollWheelValue < previousMouseState.ScrollWheelValue)
            {
                Zoom -= 0.25f;
            }
        }
    }
}