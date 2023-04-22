using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.DBManager.DBCommands
{
    public interface IDBCommand
    {
        void Execute(DBManagerController db_manager, string args);
    }
}
