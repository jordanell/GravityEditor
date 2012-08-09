using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GravityEditor.TileMap;
using GravityEditor.SubViews;

namespace GravityEditor
{
    public partial class MainWindow : Form
    {
        public static MainWindow Instance;

        public bool dirtyFlag;
        private String mapFileName;

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
    }
}
