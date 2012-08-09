using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GravityEditor.Drawing
{
    class EditorBrush
    {
        public String fullPath;
        public Texture2D texture;

        public EditorBrush(String fullPath)
        {
            this.fullPath = fullPath;
            this.texture = TextureLoader.Instance.FromFile(GravityEditor.Instance.GraphicsDevice, fullPath);
        }
    }
}
