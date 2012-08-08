using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forms = System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GravityEditor
{
    class GravityEditor : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager   graphics;
        public  SpriteBatch             spriteBatch;
        private KeyboardState           keyboardState, oldKeyboardState;
        public  Forms.Form              winform;
        private IntPtr                  drawSurface;

        public static GravityEditor     Instance;

        public GravityEditor(IntPtr drawSurface)
        {
            Instance = this;

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            Content.RootDirectory = "Content";

            this.drawSurface = drawSurface;
            graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
            winform = (Forms.Form)Forms.Form.FromHandle(Window.Handle);
            winform.VisibleChanged += new EventHandler(GravityEditor_VisibleChanged);
            winform.Size = new System.Drawing.Size(10, 10);
            Mouse.WindowHandle = drawSurface;
            resizeBackBuffer(MainWindow.Instance.drawingBox.Width, MainWindow.Instance.drawingBox.Height);
            winform.Hide();
        }

        protected override void Initialize()
        {
            new Editor();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (!MainWindow.Instance.drawingBox.ContainsFocus)
                return;

            keyboardState = Keyboard.GetState();
            Editor.Instance.Update(gameTime);
            oldKeyboardState = keyboardState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Editor.Instance.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = drawSurface;
        }

        private void GravityEditor_VisibleChanged(object sender, EventArgs e)
        {
            winform.Hide();
            winform.Size = new System.Drawing.Size(10, 10);
            winform.Visible = false;
        }

        public void resizeBackBuffer(int width, int height)
        {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.ApplyChanges();
        }
    }
}
