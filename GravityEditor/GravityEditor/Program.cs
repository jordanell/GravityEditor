using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GravityEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainWindow form = new MainWindow();
            form.Show();

            Logger.Instance.log("Creating XNA Game object (GravityEditor).");

            using (GravityEditor game = new GravityEditor(form.getHandle()))
                game.Run();
        }
    }
}
