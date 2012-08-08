using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace GravityEditor
{
    class TextureLoader
    {
        private static TextureLoader instance;
        Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public static TextureLoader Instance
        {
            get
            {
                if (instance == null)
                    instance = new TextureLoader();
                return instance;
            }
        }

        public Texture2D FromFile(GraphicsDevice gd, string fileName)
        {
            if (!textures.ContainsKey(fileName))
            {
                using (FileStream stream = File.OpenRead(fileName))
                {
                    Texture2D tex = Texture2D.FromStream(gd, stream);
                    stream.Close();
                    textures[fileName] = tex;
                }
            }
            return textures[fileName];
        }

        public void Clear()
        {
            textures.Clear();
        }
    }
}
