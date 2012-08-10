using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Forms = System.Windows.Forms;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace GravityEditor.TileMap
{
    public class TileMap : IUndoable
    {
        public class EditorVars
        {
            public int NextItemNumber;
            public Vector2 CameraPosition;
            public string Version;
        }


        public String Name;
        public bool Visible;
        public List<TileLayer> Layers;

        [XmlIgnore()]
        public string selectedLayers;
        [XmlIgnore()]
        public string selectedObjects;
        [XmlIgnore()]
        public Forms.TreeNode treeNode;

        EditorVars editorrelated = new EditorVars();

        [Browsable(false)]
        public EditorVars EditorRelated
        {
            get
            {
                return editorrelated;
            }
            set
            {
                editorrelated = value;
            }
        }


        public TileMap()
        {
            Visible = true;
            Layers = new List<TileLayer>();
        }

        public string getNextItemNumber()
        {
            return (++EditorRelated.NextItemNumber).ToString("0000");
        }

        public Tile getTileByName(string name)
        {
            foreach (TileLayer layer in Layers)
            {
                foreach (Tile tile in layer.Tiles)
                {
                    if (tile.Name == name)
                        return tile;
                }
            }
            return null;
        }

        public TileLayer getLayerByName(string name)
        {
            foreach (TileLayer layer in Layers)
            {
                if (layer.Name == name)
                    return layer;
            }
            return null;
        }

        public string RelativePath(string relativeTo, string pathToTranslate)
        {
            string[] absoluteDirectories = relativeTo.Split('\\');
            string[] relativeDirectories = pathToTranslate.Split('\\');

            //Get the shortest of the two paths
            int length = absoluteDirectories.Length < relativeDirectories.Length ? absoluteDirectories.Length : relativeDirectories.Length;

            //Use to determine where in the loop we exited
            int lastCommonRoot = -1;
            int index;

            //Find common root
            for (index = 0; index < length; index++)
                if (absoluteDirectories[index] == relativeDirectories[index])
                    lastCommonRoot = index;
                else
                    break;

            //If we didn't find a common prefix then throw
            if (lastCommonRoot == -1)
                // throw new ArgumentException("Paths do not have a common base");
                return pathToTranslate;

            //Build up the relative path
            StringBuilder relativePath = new StringBuilder();

            //Add on the ..
            for (index = lastCommonRoot + 1; index < absoluteDirectories.Length; index++)
                if (absoluteDirectories[index].Length > 0) relativePath.Append("..\\");

            //Add on the folders
            for (index = lastCommonRoot + 1; index < relativeDirectories.Length - 1; index++)
                relativePath.Append(relativeDirectories[index] + "\\");

            relativePath.Append(relativeDirectories[relativeDirectories.Length - 1]);

            return relativePath.ToString();
        }

        public IUndoable cloneForUndo()
        {
            selectedObjects = "";
            foreach (Tile i in Editor.Instance.SelectedTiles) selectedObjects += i.Name + ";";
            if (Editor.Instance.SelectedLayer != null) selectedObjects = Editor.Instance.SelectedLayer.Name;


            TileMap result = (TileMap)this.MemberwiseClone();
            result.Layers = new List<TileLayer>(Layers);
            for (int i = 0; i < result.Layers.Count; i++)
            {
                result.Layers[i] = result.Layers[i].clone();
                result.Layers[i].map = result;
            }
            return (IUndoable)result;
        }

        public void makeLike(IUndoable other)
        {
            TileMap l2 = (TileMap)other;
            Layers = l2.Layers;
            treeNode.Nodes.Clear();
            foreach (TileLayer l in Layers)
            {
                Editor.Instance.addLayer(l);
                //TODO add all items (this is not our comment)
            }
        }

        public void export(string filename)
        {
            foreach (TileLayer l in Layers)
            {
                foreach (Tile i in l.Tiles)
                {
                        i.texture_filename = RelativePath(Preferences.Instance.DefaultContentRootFolder, i.texture_fullpath);
                }
            }

            XmlTextWriter writer = new XmlTextWriter(filename, null);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;

            XmlSerializer serializer = new XmlSerializer(typeof(TileMap));
            serializer.Serialize(writer, this);

            writer.Close();
        }

        public static TileMap FromFile(string filename, ContentManager cm)
        {
            FileStream stream = File.Open(filename, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(TileMap));
            TileMap map = (TileMap)serializer.Deserialize(stream);
            stream.Close();

            return map;
        }
    }
}
