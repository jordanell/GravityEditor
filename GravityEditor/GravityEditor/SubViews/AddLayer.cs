using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GravityEditor.TileMap;

namespace GravityEditor.SubViews
{
    public partial class AddLayer : Form
    {
        public AddLayer()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            TreeNode[] nodes = MainWindow.Instance.mapTree.Nodes.Find(textBox1.Text, true);
            if (nodes.Length > 0)
            {
                MessageBox.Show("A layer or item with the name \"" + textBox1.Text + "\" already exists in the level. Please use another name!");
                return;
            }
            TileLayer l = new TileLayer(textBox1.Text);
            Editor.Instance.beginCommand("Add Layer \"" + l.Name + "\"");
            Editor.Instance.addLayer(l);
            Editor.Instance.endCommand();
            Editor.Instance.SelectedLayer = l;
            Editor.Instance.updateTreeView();
            this.Close();
        }
    }
}
