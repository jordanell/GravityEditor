using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GravityEditor
{
    public partial class MainWindow : Form
    {
        public static MainWindow Instance;

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
        }

        public IntPtr getHandle()
        {
            return drawingBox.Handle;
        }

        private void newMapButton_Click(object sender, EventArgs e)
        {

        }

        private void loadButton_Click(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {

        }

        private void undoButton_Click(object sender, EventArgs e)
        {

        }

        private void redoButton_Click(object sender, EventArgs e)
        {

        }

        private void zoomcombo_TextChanged(object sender, EventArgs e)
        {

        }

        private void ShowGridButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void SnapToGridButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ShowWorldOriginButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void preferencesButton_Click(object sender, EventArgs e)
        {

        }
    }
}
