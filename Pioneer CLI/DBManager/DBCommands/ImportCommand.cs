using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.DBManager.DBCommands
{
    public class ImportCommand : IDBCommand
    {
        public void Execute(DBManagerController db_manager, string args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Usage \"import <db_path>\"");
                return;
            }

            db_manager.LoadDatabaseLocally(args.Replace("\"", ""));
            db_manager.SetLoaded(true);
        }
    }
}
