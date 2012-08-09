using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace GravityEditor.TileMap
{
    public class TileLayer
    {
        public String Name;
        public bool Visible;
        public List<Tile> Tiles;
        public Vector2 ScrollSpeed;

        [XmlIgnore()]
        public TileMap map;

        [XmlIgnore]
        public Vector2 pScrollSpeed
        {
            get
            {
                return ScrollSpeed;
            }
            set
            {
                ScrollSpeed = value;
            }
        }

        public TileLayer()
        {
            Tiles = new List<Tile>();
            ScrollSpeed = Vector2.One;
        }

        public TileLayer(String name)
        {
            this.Name = name;
            this.Visible = true;
            ScrollSpeed = Vector2.One;
            Tiles = new List<Tile>();
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;
            foreach (Tile tile in Tiles)
                tile.draw(spriteBatch);
        }

        public void drawInEditor(SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;
            foreach (Tile tile in Tiles)
                tile.drawInEditor(spriteBatch);
        }

        public TileLayer clone()
        {
            TileLayer result = (TileLayer)this.MemberwiseClone();
            result.Tiles = new List<Tile>(Tiles);
            for (int i = 0; i < result.Tiles.Count; i++)
            {
                result.Tiles[i] = result.Tiles[i].clone();
                result.Tiles[i].layer = result;
            }
            return result;
        }

        public Tile getItemAtPos(Vector2 mouseworldpos)
        {
            for (int i = Tiles.Count - 1; i >= 0; i--)
            {
                if (Tiles[i].contains(mouseworldpos) && Tiles[i].Visible) 
                    return Tiles[i];
            }
            return null;
        }
    }
}
