using Pioneer_CLI.NFSMode.Commands;
using ProLinkLib.Database;
using ProLinkLib.NFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.NFSMode
{
    public class NFSCommandLineController
    {
        private string currentDirectory;
        private string unit;
        private NFSController nfs_controller;
        private RekordboxDB rbDB;
        private Dictionary<string, ICommand> commands;

        public NFSCommandLineController()
        {
            nfs_controller = new NFSController();
            commands = new Dictionary<string, ICommand>();
            
            currentDirectory = "";
            unit = "";

            commands.Add("cd", new CDCommand());
            commands.Add("dir", new CDCommand());
            commands.Add("ls", new LSCommand());
            commands.Add("mount", new MountCommand());

        }

        public string GetCurrentDirectory()
        {
            return currentDirectory;
        }

        public void SetUnit(string unit)
        {
            currentDirectory = ".";
            this.unit = unit;
        }
        public void SetCurrentDirectory(string new_directory)
        {
            currentDirectory = new_directory;
        }

        public void InitShell(string IP, bool isRekordbox)
        {
            bool exit = false;
            nfs_controller.Connect(IP, isRekordbox);

            while(!exit)
            {
                if(!nfs_controller.IsMounted(isRekordbox))
                {
                    Console.Write("No device mounted> ");
                }
                else
                {
                    Console.Write("readonly@" + currentDirectory + " > ");
                }

                var line = Console.ReadLine();
                var command = line.Split(' ')[0];
                var args = "";

                if(commands.ContainsKey(command))
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

                    commands[command].Run(this, nfs_controller, args, isRekordbox);
                }
                else if(command == "exit")
                {
                    exit = true;
                }
            }
        }
      
    }
}
