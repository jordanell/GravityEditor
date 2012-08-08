using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GravityEditor.TileMap
{
    class Camera
    {
        private Vector2 position;
        private float   rotation;
        private float   scale;

        public Matrix   matrix;
        private Vector2 viewport;

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                updateMatrix();
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                updateMatrix();
            }
        }

        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                updateMatrix();
            }
        }

        public Camera(float width, float height)
        {
            position = Vector2.Zero;
            rotation = 0;
            scale = 1.0f;
            viewport = new Vector2(width, height);
            updateMatrix();
        }

        void updateMatrix()
        {
            matrix = Matrix.CreateTranslation(-position.X, -position.Y, 0.0f) *
                     Matrix.CreateRotationZ(rotation) *
                     Matrix.CreateScale(scale) *
                     Matrix.CreateTranslation(viewport.X / 2, viewport.Y / 2, 0.0f);
        }

        public void updateViewport(float width, float height)
        {
            viewport.X = width;
            viewport.Y = height;
            updateMatrix();
        }
    }
}
