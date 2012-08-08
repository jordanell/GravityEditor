using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Forms = System.Windows.Forms;
using GravityEditor.TileMap;

namespace GravityEditor
{
    class Editor
    {
        public static Editor Instance;

        // Tile Map
        public Camera camera;

        public Editor()
        {
            Instance = this;

            Logger.Instance.log("Loading preferences.");
            Preferences.Instance.Import("preferences.xml");
            Logger.Instance.log("Preferences loaded.");
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GravityEditor.Instance.GraphicsDevice.Clear(Preferences.Instance.ColorBackground);



            if (Preferences.Instance.ShowGrid)
            {
                spriteBatch.Begin();
                int max = Preferences.Instance.GridNumberOfGridLines / 2;
                for (int x = 0; x <= max; x++)
                {
                    Vector2 start = Vector2.Transform(new Vector2(x, -max) * Preferences.Instance.GridSpacing.X, camera.matrix);
                    Vector2 end = Vector2.Transform(new Vector2(x, max) * Preferences.Instance.GridSpacing.X, camera.matrix);
                    Primitives.Instance.drawLine(spriteBatch, start, end, Preferences.Instance.GridColor, Preferences.Instance.GridLineThickness);
                    start = Vector2.Transform(new Vector2(-x, -max) * Preferences.Instance.GridSpacing.X, camera.matrix);
                    end = Vector2.Transform(new Vector2(-x, max) * Preferences.Instance.GridSpacing.X, camera.matrix);
                    Primitives.Instance.drawLine(spriteBatch, start, end, Preferences.Instance.GridColor, Preferences.Instance.GridLineThickness);
                }
                for (int y = 0; y <= max; y++)
                {
                    Vector2 start = Vector2.Transform(new Vector2(-max, y) * Preferences.Instance.GridSpacing.Y, camera.matrix);
                    Vector2 end = Vector2.Transform(new Vector2(max, y) * Preferences.Instance.GridSpacing.Y, camera.matrix);
                    Primitives.Instance.drawLine(spriteBatch, start, end, Preferences.Instance.GridColor, Preferences.Instance.GridLineThickness);
                    start = Vector2.Transform(new Vector2(-max, -y) * Preferences.Instance.GridSpacing.Y, camera.matrix);
                    end = Vector2.Transform(new Vector2(max, -y) * Preferences.Instance.GridSpacing.Y, camera.matrix);
                    Primitives.Instance.drawLine(spriteBatch, start, end, Preferences.Instance.GridColor, Preferences.Instance.GridLineThickness);
                }
                spriteBatch.End();
            }

            if (Preferences.Instance.ShowWorldOrigin)
            {
                spriteBatch.Begin();
                Vector2 worldOrigin = Vector2.Transform(Vector2.Zero, camera.matrix);
                Primitives.Instance.drawLine(spriteBatch, worldOrigin + new Vector2(-20, 0), worldOrigin + new Vector2(+20, 0), Preferences.Instance.WorldOriginColor, Preferences.Instance.WorldOriginLineThickness);
                Primitives.Instance.drawLine(spriteBatch, worldOrigin + new Vector2(0, -20), worldOrigin + new Vector2(0, 20), Preferences.Instance.WorldOriginColor, Preferences.Instance.WorldOriginLineThickness);
                spriteBatch.End();
            }
        }
    }
}
