using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Forms = System.Windows.Forms;
using CustomUITypeEditors;
using System.Windows.Forms;
using System.IO;

namespace GravityEditor.TileMap
{
    public class Tile
    {
        public String Name;
        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;
        public bool Visible;
        public Color TintColor;
        public Vector2 Origin;
        public bool Physics;

        public String texture_filename;
        private Texture2D texture;

        private Color[] coldata;
        private Matrix transform;
        private Rectangle boundingrectangle;
        private Vector2[] polygon;

        protected bool hovering;

        [XmlIgnore()]
        public TileLayer layer;

        [XmlIgnore()]
        public string texture_fullpath;


        [XmlIgnore()]
        public Vector2 pPosition
        {
            get
            {
                return Position;
            }
            set
            {
                Position = value;
                OnTransformed();
            }
        }

        [XmlIgnore()]
        public Vector2 pOrigin
        {
            get
            {
                return Origin;
            }
            set
            {
                Origin = value;
                OnTransformed();
            }
        }

        [XmlIgnore()]
        public float pRotation
        {
            get
            {
                return Rotation;
            }
            set
            {
                Rotation = value;
                OnTransformed();
            }
        }

        [XmlIgnore()]
        public Vector2 pScale
        {
            get
            {
                return Scale;
            }
            set
            {
                Scale = value;
                OnTransformed();
            }
        }

        [XmlIgnore()]
        public Color pTintColor
        {
            get
            {
                return TintColor;
            }
            set
            {
                TintColor = value;
            }
        }

        [XmlIgnore()]
        public bool pPhysics
        {
            get
            {
                return Physics;
            }
            set
            {
                Physics = value;
            }
        }

        public Tile()
        {
            Visible = true;
        }

        public Tile(String fullpath, Vector2 position)
        {
            this.texture_fullpath = fullpath;
            this.Visible = true;
            this.Rotation = 0;
            this.Scale = Vector2.One;
            this.TintColor = Microsoft.Xna.Framework.Color.White;
            loadIntoEditor();
            this.pPosition = position;

            this.Origin = getTextureOrigin(texture);

            Vector2 center = new Vector2(texture.Width / 2, texture.Height / 2);
            this.pPosition -= (center - Origin);
        }

        public bool loadIntoEditor()
        {
            if (layer != null) 
                this.texture_fullpath = System.IO.Path.Combine(layer.map.ContentRootFolder + "\\", texture_filename);

            if (!File.Exists(texture_fullpath))
            {
                DialogResult dr = Forms.MessageBox.Show("The file \"" + texture_fullpath + "\" doesn't exist!\n"
                    + "The texture path is a combination of the Level's ContentRootFolder and the TextureItem's relative path.\n"
                    + "Please adjust the XML file before trying to load this level again.\n"
                    + "For now, a dummy texture will be used. Continue loading the level?", "Error loading texture file",
                    MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Error);
                if (dr == DialogResult.No)
                    return false;
                texture = Editor.Instance.dummytexture;
            }
            else
            {
                texture = TextureLoader.Instance.FromFile(GravityEditor.Instance.GraphicsDevice, texture_fullpath);
            }

            coldata = new Color[texture.Width * texture.Height];
            texture.GetData(coldata);

            polygon = new Vector2[4];

            OnTransformed();
            return true;
        }

        public Tile clone()
        {
            Tile result = (Tile)this.MemberwiseClone();
            result.polygon = (Vector2[])polygon.Clone();
            result.hovering = false;
            return result;
        }

        public Vector2 getTextureOrigin(Texture2D texture)
        {
            switch (Preferences.Instance.DefaultTextureOriginMethod)
            {
                case TextureOriginMethodEnum.TextureCenter:
                    return new Vector2(texture.Width / 2, texture.Height / 2);
                case TextureOriginMethodEnum.Centroid:
                    uint[] data = new uint[texture.Width * texture.Height];
                    texture.GetData(data);
                    Vertices verts = Vertices.CreatePolygon(data, texture.Width, texture.Height);
                    return verts.GetCentroid();
                case TextureOriginMethodEnum.TopLeft:
                    return new Vector2(0, 0);
                case TextureOriginMethodEnum.TopRight:
                    return new Vector2(texture.Width, 0);
                case TextureOriginMethodEnum.BottomLeft:
                    return new Vector2(0, texture.Height);
                case TextureOriginMethodEnum.BottomRight:
                    return new Vector2(texture.Width, texture.Height);
            }
            return Vector2.Zero;
        }

        public void OnTransformed()
        {
            transform =
                Matrix.CreateTranslation(new Vector3(-Origin.X, -Origin.Y, 0.0f)) *
                Matrix.CreateScale(Scale.X, Scale.Y, 1) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateTranslation(new Vector3(Position, 0.0f));

            Vector2 leftTop = new Vector2(0, 0);
            Vector2 rightTop = new Vector2(texture.Width, 0);
            Vector2 leftBottom = new Vector2(0, texture.Height);
            Vector2 rightBottom = new Vector2(texture.Width, texture.Height);

            // Transform all four corners into work space
            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

            polygon[0] = leftTop;
            polygon[1] = rightTop;
            polygon[3] = leftBottom;
            polygon[2] = rightBottom;

            // Find the minimum and maximum extents of the rectangle in world space
            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                                      Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                                      Vector2.Max(leftBottom, rightBottom));

            // Return as a rectangle
            boundingrectangle = new Rectangle((int)min.X, (int)min.Y,
                                 (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        public bool contains(Vector2 worldpos)
        {
            if (boundingrectangle.Contains((int)worldpos.X, (int)worldpos.Y))
            {
                return intersectpixels(worldpos);
            }
            return false;
        }

        public bool intersectpixels(Vector2 worldpos)
        {
            Vector2 positionInB = Vector2.Transform(worldpos, Matrix.Invert(transform));
            int xB = (int)Math.Round(positionInB.X);
            int yB = (int)Math.Round(positionInB.Y);

            // If the pixel lies within the bounds of B
            if (0 <= xB && xB < texture.Width && 0 <= yB && yB < texture.Height)
            {
                Color colorB = coldata[xB + yB * texture.Width];
                if (colorB.A != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public string getNamePrefix()
        {
            return "Tile_";
        }

        public void drawInEditor(SpriteBatch sb)
        {
            if (!Visible) 
                return;

            SpriteEffects se = SpriteEffects.None;
            Color c = TintColor;
            if (hovering && Preferences.Instance.EnableHighlightOnMouseOver) 
                c = Preferences.Instance.ColorHighlight;
            sb.Draw(texture, Position, null, c, Rotation, Origin, Scale, se, 0);
        }

        public void drawSelectionFrame(SpriteBatch sb, Matrix matrix, Color color)
        {
            Vector2[] poly = new Vector2[4];
            Vector2.Transform(polygon, ref matrix, poly);

            Primitives.Instance.drawPolygon(sb, poly, color, 2);
            foreach (Vector2 p in poly)
            {
                Primitives.Instance.drawCircleFilled(sb, p, 4, color);
            }
            Vector2 origin = Vector2.Transform(Position, matrix);
            Primitives.Instance.drawBoxFilled(sb, origin.X - 5, origin.Y - 5, 10, 10, color);
        }

        public void draw(SpriteBatch sb)
        {
            if (!Visible)
                return;
            SpriteEffects effects = SpriteEffects.None;
            sb.Draw(texture, Position, null, TintColor, Rotation, Origin, Scale, effects, 0);
        }

        public void onMouseOver(Vector2 mouseworldpos)
        {
            hovering = true;
        }

        public void onMouseOut()
        {
            hovering = false;
            MainWindow.Instance.drawingBox.Cursor = Cursors.Default;
        }

        public void onMouseButtonDown(Vector2 mouseworldpos)
        {
            hovering = false;
            MainWindow.Instance.drawingBox.Cursor = Cursors.SizeAll;
        }

        public void onMouseButtonUp(Vector2 mouseworldpos)
        {

        }
    }
}
