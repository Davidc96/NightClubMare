using ConsoleTables;
using ProLinkLib;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.DBManager.DBCommands
{
    public class SongManagerCommand : IDBCommand
    {
        public void Execute(DBManagerController db_manager, string args)
        {
            var db_list = Convert.ToBase64String(Encoding.UTF8.GetBytes(db_manager.GetMetadataDB().GetTrackListJSON()));
            File.WriteAllText("rekordbox_track.db", db_list);

            db_manager.DisconnectDB(); // Let's disconnect from the CLI to avoid problems while managing tracks using SongManager (if the db comes from a CDJ)

            Process.Start("SongManager.exe"); // Open Songmanager
        }
    }
}
