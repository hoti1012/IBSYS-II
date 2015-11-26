using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Planning_Tool.Data;
using Planning_Tool.Masterdata;
using Planning_Tool.Purchase;

namespace Planning_Tool
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DatabaseManager manager = new DatabaseManager();
            try
            {
                manager.initialize();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            } 
            finally
            {
                if(manager != null)
                 manager.release();
            }
        }
    }
}
