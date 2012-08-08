using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GravityEditor
{
    class Logger
    {
        private static Logger instance;
        string logFileName = System.Windows.Forms.Application.StartupPath + "\\log.txt";
        StreamWriter sw;

        public static Logger Instance
        {
            get
            {
                if (instance == null)
                    instance = new Logger();
                return instance;
            }
        }

        public Logger()
        {
            sw = new StreamWriter(logFileName, false);
            sw.WriteLine(box("Log file creadet."));
            sw.Close();
        }

        public void log(string message)
        {
            sw = new StreamWriter(logFileName, true);
            sw.WriteLine(box(message));
            sw.Close();
        }

        string box(string message)
        {
            return DateTime.Now + "." + DateTime.Now.Millisecond.ToString("000") + " - " + message;
        }
    }
}
