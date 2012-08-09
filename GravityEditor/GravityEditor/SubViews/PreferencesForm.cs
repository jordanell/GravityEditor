using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GravityEditor.SubViews
{
    public partial class PreferencesForm : Form
    {
        public PreferencesForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Preferences.Instance.Export("preferences.xml");
            Close();
        }

        private void PreferencesForm_Load(object sender, EventArgs e)
        {
            Preferences.Instance.Export("preferences.xml");
            propertyGrid1.SelectedObject = Preferences.Instance;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
