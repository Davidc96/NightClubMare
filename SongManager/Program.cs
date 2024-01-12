using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SongManager
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if(!System.IO.File.Exists("rekordbox_track.db"))
            {
                MessageBox.Show("No database found! Exiting...");
                return;
            }
            Application.Run(new MainForm());
        }
    }
}
