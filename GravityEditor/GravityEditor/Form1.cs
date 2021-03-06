﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml;
using GravityEditor.TileMap;
using GravityEditor.SubViews;

namespace GravityEditor
{
    public partial class MainWindow : Form
    {
        public static MainWindow Instance;

        public bool dirtyFlag;
        private String mapFileName;
        private Cursor dragcursor;

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
        }

        public IntPtr getHandle()
        {
            return drawingBox.Handle;
        }

        public static Image getThumbNail(Bitmap bmp, int imgWidth, int imgHeight)
        {
            Bitmap retBmp = new Bitmap(imgWidth, imgHeight, System.Drawing.Imaging.PixelFormat.Format64bppPArgb);
            Graphics grp = Graphics.FromImage(retBmp);
            int tnWidth = imgWidth, tnHeight = imgHeight;
            if (bmp.Width > bmp.Height)
                tnHeight = (int)(((float)bmp.Height / (float)bmp.Width) * tnWidth);
            else if (bmp.Width < bmp.Height)
                tnWidth = (int)(((float)bmp.Width / (float)bmp.Height) * tnHeight);
            int iLeft = (imgWidth / 2) - (tnWidth / 2);
            int iTop = (imgHeight / 2) - (tnHeight / 2);
            grp.DrawImage(bmp, iLeft, iTop, tnWidth, tnHeight);
            retBmp.Tag = bmp;
            return retBmp;
        }

        public void loadFolder(string path)
        {
            imageList48.Images.Clear();
            imageList64.Images.Clear();
            imageList96.Images.Clear();
            imageList128.Images.Clear();
            imageList256.Images.Clear();

            listViewTexture.Clear();

            Image img = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + "\\..\\..\\Content\\folder.png");
            imageList48.Images.Add(img);
            imageList64.Images.Add(img);
            imageList96.Images.Add(img);
            imageList128.Images.Add(img);
            imageList256.Images.Add(img);

            DirectoryInfo di = new DirectoryInfo(path);
            textBoxFolder.Text = di.FullName;
            DirectoryInfo[] folders = di.GetDirectories();
            foreach (DirectoryInfo folder in folders)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = folder.Name;
                lvi.ToolTipText = folder.Name;
                lvi.ImageIndex = 0;
                lvi.Tag = "folder";
                lvi.Name = folder.FullName;
                listViewTexture.Items.Add(lvi);
            }

            string filters = "*.jpg;*.png;*.bmp;";
            List<FileInfo> fileList = new List<FileInfo>();
            string[] extensions = filters.Split(';');
            foreach (string filter in extensions)
                fileList.AddRange(di.GetFiles(filter));
            FileInfo[] files = fileList.ToArray();

            foreach (FileInfo file in files)
            {
                Bitmap bmp = new Bitmap(file.FullName);
                imageList48.Images.Add(file.FullName, getThumbNail(bmp, 48, 48));
                imageList64.Images.Add(file.FullName, getThumbNail(bmp, 64, 64));
                imageList96.Images.Add(file.FullName, getThumbNail(bmp, 96, 96));
                imageList128.Images.Add(file.FullName, getThumbNail(bmp, 128, 128));
                imageList256.Images.Add(file.FullName, getThumbNail(bmp, 256, 256));

                ListViewItem lvi = new ListViewItem();
                lvi.Name = file.FullName;
                lvi.Text = file.Name;
                lvi.ImageKey = file.FullName;
                lvi.Tag = "file";
                lvi.ToolTipText = file.Name + " (" + bmp.Width.ToString() + " x " + bmp.Height.ToString() + ")";

                listViewTexture.Items.Add(lvi);
            }
        }

        public void saveMap(String filename)
        {
            Editor.Instance.saveMap(filename);
            mapFileName = filename;
            dirtyFlag = false;
        }

        public DialogResult checkCurrentLevelAndSaveEventually()
        {
            if (dirtyFlag)
            {
                DialogResult dr = MessageBox.Show("The current level has not been saved. Do you want to save now?", "Save?",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (mapFileName == "untitled")
                    {
                        SaveFileDialog dialog = new SaveFileDialog();
                        dialog.Filter = "XML Files (*.xml)|*.xml";
                        if (dialog.ShowDialog() == DialogResult.OK)
                            saveMap(dialog.FileName);
                        else
                            return DialogResult.Cancel;
                    }
                    else
                    {
                        saveMap(mapFileName);
                    }
                }
                if (dr == DialogResult.Cancel)
                    return DialogResult.Cancel;
            }
            return DialogResult.OK;
        }

        public void loadMap(String filename)
        {
            TileMap.TileMap map = TileMap.TileMap.FromFile(filename, GravityEditor.Instance.Content);

            Editor.Instance.loadMap(map);
            mapFileName = filename;
            dirtyFlag = false;
        }

        public void newMap()
        {
            Application.DoEvents();
            TileMap.TileMap map = new TileMap.TileMap();
            Editor.Instance.loadMap(map);
            mapFileName = "untitled";
            dirtyFlag = false;
        }

        private void ActionCenterView(object sender, EventArgs e)
        {
            if (mapTree.SelectedNode.Tag is TileMap.TileMap)
            {
                Editor.Instance.camera.Position = Microsoft.Xna.Framework.Vector2.Zero;
            }
            if (mapTree.SelectedNode.Tag is Tile)
            {
                Tile i = (Tile)mapTree.SelectedNode.Tag;
                Editor.Instance.camera.Position = i.pPosition;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Preferences.Instance.Export("preferences.xml");
            GravityEditor.Instance.Exit();
            Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapFileName == "untitled")
                saveAsToolStripMenuItem_Click(sender, e);
            else
                saveMap(mapFileName);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "XML Files (*.xml)|*.xml";
            if (dialog.ShowDialog() == DialogResult.OK)
                saveMap(dialog.FileName);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkCurrentLevelAndSaveEventually() == DialogResult.Cancel)
                return;
            OpenFileDialog opendialog = new OpenFileDialog();
            opendialog.Filter = "XML Files (*.xml)|*.xml";
            if (opendialog.ShowDialog() == DialogResult.OK)
                loadMap(opendialog.FileName);
        }

        private void undoMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Instance.undo();
        }

        private void redoMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Instance.redo();
        }

        private void undoButton_Click_1(object sender, EventArgs e)
        {
            Editor.Instance.undo();
        }

        private void redoButton_Click_1(object sender, EventArgs e)
        {
            Editor.Instance.redo();
        }

        private void newMapButton_Click_1(object sender, EventArgs e)
        {
            newMap();
        }

        private void loadMapButton_Click(object sender, EventArgs e)
        {
            if (checkCurrentLevelAndSaveEventually() == DialogResult.Cancel)
                return;
            OpenFileDialog opendialog = new OpenFileDialog();
            opendialog.Filter = "XML Files (*.xml)|*.xml";
            if (opendialog.ShowDialog() == DialogResult.OK)
                loadMap(opendialog.FileName);
        }

        private void saveButton_Click_1(object sender, EventArgs e)
        {
            if (mapFileName == "untitled")
                saveAsToolStripMenuItem_Click(sender, e);
            else
                saveMap(mapFileName);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (mapTree.SelectedNode == null) return;
            if (mapTree.SelectedNode.Tag is TileLayer)
            {
                TileLayer l = (TileLayer)mapTree.SelectedNode.Tag;
                Editor.Instance.deleteLayer(l);
            }
            else if (mapTree.SelectedNode.Tag is Tile)
            {
                Editor.Instance.deleteSelectedMapObjects();
            }
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            if (mapTree.SelectedNode.Tag is TileLayer)
            {
                TileLayer l = (TileLayer)mapTree.SelectedNode.Tag;
                if (l.map.Layers.IndexOf(l) > 0)
                {
                    Editor.Instance.beginCommand("Move Up Layer \"" + l.Name + "\"");
                    Editor.Instance.moveLayerUp(l);
                    Editor.Instance.endCommand();
                    Editor.Instance.updateTreeView();
                }
            }
            if (mapTree.SelectedNode.Tag is Tile)
            {
                Tile i = (Tile)mapTree.SelectedNode.Tag;
                if (i.layer.Tiles.IndexOf(i) > 0)
                {
                    Editor.Instance.beginCommand("Move Up Item \"" + i.Name + "\"");
                    Editor.Instance.moveTileUp(i);
                    Editor.Instance.endCommand();
                    Editor.Instance.updateTreeView();
                }
            }
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            if (mapTree.SelectedNode.Tag is TileLayer)
            {
                TileLayer l = (TileLayer)mapTree.SelectedNode.Tag;
                if (l.map.Layers.IndexOf(l) < l.map.Layers.Count - 1)
                {
                    Editor.Instance.beginCommand("Move Down Layer \"" + l.Name + "\"");
                    Editor.Instance.moveLayerDown(l);
                    Editor.Instance.endCommand();
                    Editor.Instance.updateTreeView();
                }
            }
            if (mapTree.SelectedNode.Tag is Tile)
            {
                Tile i = (Tile)mapTree.SelectedNode.Tag;
                if (i.layer.Tiles.IndexOf(i) < i.layer.Tiles.Count - 1)
                {
                    Editor.Instance.beginCommand("Move Down Item \"" + i.Name + "\"");
                    Editor.Instance.moveTileDown(i);
                    Editor.Instance.endCommand();
                    Editor.Instance.updateTreeView();
                }
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            drawingBox.AllowDrop = true;

            //fill zoom combobox
            for (int i = 25; i <= 200; i += 25)
            {
                zoomCombo.Items.Add(i.ToString() + "%");
            }
        }

        private void newLayerButton_Click(object sender, EventArgs e)
        {
            AddLayer f = new AddLayer();
            f.ShowDialog();
        }

        private void preferencesButton_Click_1(object sender, EventArgs e)
        {
            PreferencesForm f = new PreferencesForm();
            f.ShowDialog();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreferencesForm f = new PreferencesForm();
            f.ShowDialog();
        }

        private void showGridButton_Click(object sender, EventArgs e)
        {
            Preferences.Instance.ShowGrid = showGridButton.Checked;
        }

        private void alignToGridButton_Click(object sender, EventArgs e)
        {
            Preferences.Instance.SnapToGrid = alignToGridButton.Checked;
        }

        private void originButton_Click(object sender, EventArgs e)
        {
            Preferences.Instance.ShowWorldOrigin = originButton.Checked;
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Preferences.Instance.Export("preferences.xml");
            GravityEditor.Instance.Exit();
        }

        private void buttonFolderUp_Click(object sender, EventArgs e)
        {
            if (textBoxFolder.Text != "")
            {
                DirectoryInfo di = Directory.GetParent(textBoxFolder.Text);
                if (di == null) return;
                loadFolder(di.FullName);
            }
        }

        private void chooseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog d = new FolderBrowserDialog();
            d.SelectedPath = textBoxFolder.Text;
            if (d.ShowDialog() == DialogResult.OK)
                loadFolder(d.SelectedPath);
        }

        [DllImport("User32.dll")]
        private static extern int SendMessage(int Handle, int wMsg, int wParam, int lParam);
        public static void SetListViewSpacing(ListView lst, int x, int y)
        {
            SendMessage((int)lst.Handle, 0x1000 + 53, 0, y * 65536 + x);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    listViewTexture.LargeImageList = imageList48;
                    SetListViewSpacing(listViewTexture, 48 + 8, 48 + 32);
                    break;
                case 1:
                    listViewTexture.LargeImageList = imageList64;
                    SetListViewSpacing(listViewTexture, 64 + 8, 64 + 32);
                    break;
                case 2:
                    listViewTexture.LargeImageList = imageList96;
                    SetListViewSpacing(listViewTexture, 96 + 8, 96 + 32);
                    break;
                case 3:
                    listViewTexture.LargeImageList = imageList128;
                    SetListViewSpacing(listViewTexture, 128 + 8, 128 + 32);
                    break;
                case 4:
                    listViewTexture.LargeImageList = imageList256;
                    SetListViewSpacing(listViewTexture, 256 + 8, 256 + 32);
                    break;
            }
        }

        private void listViewTexture_Click(object sender, EventArgs e)
        {

        }

        private void listViewTexture_DragDrop(object sender, DragEventArgs e)
        {
            listViewTexture.Cursor = Cursors.Default;
            drawingBox.Cursor = Cursors.Default;
        }

        private void listViewTexture_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listViewTexture_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                e.UseDefaultCursors = false;
                listViewTexture.Cursor = dragcursor;
                drawingBox.Cursor = Cursors.Default;
            }
            else
            {
                e.UseDefaultCursors = true;
                listViewTexture.Cursor = Cursors.Default;
                drawingBox.Cursor = Cursors.Default;
            }
        }

        private void listViewTexture_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListViewItem lvi = (ListViewItem)e.Item;
            if (lvi.Tag.ToString() == "folder") return;
            Bitmap bmp = new Bitmap(listViewTexture.LargeImageList.Images[lvi.ImageKey]);
            dragcursor = new Cursor(bmp.GetHicon());
            listViewTexture.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void listViewTexture_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string itemtype = listViewTexture.FocusedItem.Tag.ToString();
            if (itemtype == "folder")
            {
                loadFolder(listViewTexture.FocusedItem.Name);
            }
            if (itemtype == "file")
            {
                Editor.Instance.createTextureBrush(listViewTexture.FocusedItem.Name);
            }
        }

        private void drawingBox_DragDrop(object sender, DragEventArgs e)
        {
            ListViewItem lvi = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            Editor.Instance.paintTextureBrush(false);
            listViewTexture.Cursor = Cursors.Default;
            drawingBox.Cursor = Cursors.Default;
        }

        private void drawingBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            ListViewItem lvi = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            Editor.Instance.createTextureBrush(lvi.Name);
        }

        private void drawingBox_DragLeave(object sender, EventArgs e)
        {
            Editor.Instance.destroyTextureBrush();
            Editor.Instance.Draw(GravityEditor.Instance.spriteBatch);
            GravityEditor.Instance.GraphicsDevice.Present();
        }

        private void drawingBox_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            Point p = drawingBox.PointToClient(new Point(e.X, e.Y));
            Editor.Instance.setmousepos(p.X, p.Y);
            Editor.Instance.Draw(GravityEditor.Instance.spriteBatch);
            GravityEditor.Instance.GraphicsDevice.Present();
        }

        private void drawingBox_MouseEnter(object sender, EventArgs e)
        {
            drawingBox.Select();
        }

        private void drawingBox_MouseLeave(object sender, EventArgs e)
        {
            menuStrip1.Select();
        }

        private void drawingBox_Resize(object sender, EventArgs e)
        {
            Logger.Instance.log("pictureBox1_Resize().");
            if (GravityEditor.Instance != null) GravityEditor.Instance.resizeBackBuffer(drawingBox.Width, drawingBox.Height);
            if (Editor.Instance != null && Editor.Instance.camera != null) Editor.Instance.camera.updateViewport(drawingBox.Width, drawingBox.Height);
        }

        private void mapTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is TileMap.TileMap)
            {
                Editor.Instance.map.Visible = e.Node.Checked;
            }
            if (e.Node.Tag is TileLayer)
            {
                TileLayer l = (TileLayer)e.Node.Tag;
                l.Visible = e.Node.Checked;
            }
            if (e.Node.Tag is Tile)
            {
                Tile i = (Tile)e.Node.Tag;
                i.Visible = e.Node.Checked;
            }
        }

        private void mapTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null) return;

            TreeNode[] nodes = mapTree.Nodes.Find(e.Label, true);
            if (nodes.Length > 0)
            {
                MessageBox.Show("A layer or item with the name \"" + e.Label + "\" already exists in the level. Please use another name!");
                e.CancelEdit = true;
                return;
            }
            if (e.Node.Tag is TileMap.TileMap)
            {
                TileMap.TileMap l = (TileMap.TileMap)e.Node.Tag;
                Editor.Instance.beginCommand("Rename Level (\"" + l.Name + "\" -> \"" + e.Label + "\")");
                l.Name = e.Label;
                e.Node.Name = e.Label;
                Editor.Instance.endCommand();
            }
            if (e.Node.Tag is TileLayer)
            {
                TileLayer l = (TileLayer)e.Node.Tag;
                Editor.Instance.beginCommand("Rename Layer (\"" + l.Name + "\" -> \"" + e.Label + "\")");
                l.Name = e.Label;
                e.Node.Name = e.Label;
                Editor.Instance.endCommand();
            }
            if (e.Node.Tag is Tile)
            {
                Tile i = (Tile)e.Node.Tag;
                Editor.Instance.beginCommand("Rename Item (\"" + i.Name + "\" -> \"" + e.Label + "\")");
                i.Name = e.Label;
                e.Node.Name = e.Label;
                Editor.Instance.endCommand();
            }
            propertyGrid1.Refresh();
            drawingBox.Select();
        }

        private void mapTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is TileMap.TileMap)
            {
                Editor.Instance.selectMap();
            }
            if (e.Node.Tag is TileLayer)
            {
                TileLayer l = (TileLayer)e.Node.Tag;
                Editor.Instance.selectLayer(l);
            }
            if (e.Node.Tag is Tile)
            {
                Tile i = (Tile)e.Node.Tag;
                Editor.Instance.selectTile(i);
            }
        }

        private void mapTree_DragDrop(object sender, DragEventArgs e)
        {
            Editor.Instance.endCommand();
        }

        private void mapTree_DragOver(object sender, DragEventArgs e)
        {
            //get source node
            TreeNode sourcenode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            if (sourcenode == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            else e.Effect = DragDropEffects.Move;

            //get destination node and select it
            Point p = mapTree.PointToClient(new Point(e.X, e.Y));
            TreeNode destnode = mapTree.GetNodeAt(p);
            if (destnode.Tag is TileMap.TileMap) return;
            mapTree.SelectedNode = destnode;

            if (destnode != sourcenode)
            {
                Tile i1 = (Tile)sourcenode.Tag;
                if (destnode.Tag is Tile)
                {
                    Tile i2 = (Tile)destnode.Tag;
                    Editor.Instance.moveTileToLayer(i1, i2.layer, i2);
                    int delta = 0;
                    if (destnode.Index > sourcenode.Index && i1.layer == i2.layer) delta = 1;
                    sourcenode.Remove();
                    destnode.Parent.Nodes.Insert(destnode.Index + delta, sourcenode);
                }
                if (destnode.Tag is TileLayer)
                {
                    TileLayer l2 = (TileLayer)destnode.Tag;
                    Editor.Instance.moveTileToLayer(i1, l2, null);
                    sourcenode.Remove();
                    destnode.Nodes.Insert(0, sourcenode);
                }
                Editor.Instance.selectTile(i1);
                Editor.Instance.Draw(GravityEditor.Instance.spriteBatch);
                GravityEditor.Instance.GraphicsDevice.Present();
                Application.DoEvents();
            }
        }

        private void mapTree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (((TreeNode)e.Item).Tag is TileLayer) return;
            if (((TreeNode)e.Item).Tag is TileMap.TileMap) return;
            Editor.Instance.beginCommand("Drag Item");
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void mapTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.N) newLayerButton_Click(sender, e);
            if (e.KeyCode == Keys.Delete) deleteButton_Click(sender, e);
            if (e.KeyCode == Keys.F7) moveUpButton_Click(sender, e);
            if (e.KeyCode == Keys.F8) moveDownButton_Click(sender, e);
            if (e.KeyCode == Keys.F4) ActionCenterView(sender, e);
            if (e.KeyCode == Keys.F2) mapTree.SelectedNode.BeginEdit();
        }

        private void mapTree_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mapTree.SelectedNode = mapTree.GetNodeAt(e.X, e.Y);
            }
        }

        private void zoomCombo_TextChanged(object sender, EventArgs e)
        {
            if (Editor.Instance.camera != null)
            {
                if (zoomCombo.Text.Length > 0 && Editor.Instance != null)
                {
                    float zoom = float.Parse(zoomCombo.Text.Substring(0, zoomCombo.Text.Length - 1));
                    Editor.Instance.camera.Scale = zoom / 100;
                }
            }
            else
                MessageBox.Show("The camera has not been initialized yet. Create or load a level to initialize the camera.");
        }
    }
}
