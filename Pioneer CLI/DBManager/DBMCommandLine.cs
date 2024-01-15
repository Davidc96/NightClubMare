using Pioneer_CLI.DBManager.DBCommands;
using ProLinkLib.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.DBManager
{
    public class DBMCommandLine
    {
        DBManagerController db;
        Dictionary<string, IDBCommand> commands;
        bool offline_mode;
        bool db_imported;

        public DBMCommandLine(ProLinkController plc)
        {
            db = new DBManagerController(plc);
            commands = new Dictionary<string, IDBCommand>();
            offline_mode = true;
            db_imported = false;

            // Commands from DB
            //TODO: Add connect command and delete track command
            commands.Add("songmanager", new SongManagerCommand());
            commands.Add("track", new TrackCommand());
            commands.Add("import", new ImportCommand());
            commands.Add("disconnect", new DisconnectCommand());
        }

        public void PromptShell(CDJ device_to_connect)
        {
            bool exit = false;
            string line = "";
            string command = "";
            string args = "";

            if(device_to_connect == null)
            {
                offline_mode = true;
            }
            else
            {
                if(db.ConnectDB(device_to_connect))
                {
                    db_imported = true;
                    offline_mode = true;
                }
                else
                {
                    Console.WriteLine("[ERROR] Failed to connect to the CDJ DB, Check if the device id has an USB or SD and try again later...");
                    offline_mode = true;
                }

            }

            while (!exit)
            {
                Console.WriteLine();
                if (offline_mode != false)
                {
                    Console.Write("===:ID: " + device_to_connect.ChannelID + "@RekordboxDB > ");
                }
                //TODO: Fix while db is connected show that is not offline mode
                else if (db.IsLoaded() == false)
                {
                    Console.Write("===:NO DB IMPORTED> ");
                }
                else
                {
                    Console.Write("===:DB Locally imported > ");
                }

                line = Console.ReadLine();
                command = line.Split(' ')[0];
                if (command == "help")
                {
                    foreach (string cmd in commands.Keys)
                    {
                        Console.WriteLine(cmd);
                    }
                }
                else if(command == "exit")
                {
                    db.DisconnectDB();
                    Console.WriteLine("Disconnected from database");
                    exit = true;
                }
                else if (commands.ContainsKey(command))
                {
                    // if there command is like command arg1 arg2 arg3 get all the args
                    if (line.Split(' ').Length > 2)
                    {
                        for (int i = 1; i < line.Split(' ').Length; i++)
                        {
                            args += line.Split(' ')[i] + " ";
                        }
                    }
                    else if (line.Split(' ').Length > 1)
                    {
                        args = line.Split(' ')[1];
                    }

                    commands[command].Execute(db, args);
                    
                    if(command.Contains("songmanager"))
                    {
                        db.DisconnectDB();
                        Console.WriteLine("CLI is disconnected from DB, please reconnect it if you want to use the command line.");
                    }

                }
                args = "";
                command = "";
            }
        }
    }
}
