using Pioneer_CLI.Commands;
using Pioneer_CLI.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI
{
    public class CommandLineController
    {
        private ProLinkController plc;
        private CDJ selectedCDJ;
        private Dictionary<string, ICommand> commands;

        public CommandLineController()
        {
            plc = new ProLinkController();
            selectedCDJ = null;
            commands = new Dictionary<string, ICommand>();

            commands.Add("devices", new DevicesCommand());
            commands.Add("select", new SelectCommand());
            commands.Add("exit", new ExitCommand());
            commands.Add("info", new InfoCommand());
            commands.Add("play", new PlayCommand());
            commands.Add("pause", new PauseCommand());
            commands.Add("sync", new SyncCommand());
            commands.Add("load", new LoadTrackCommand());
            commands.Add("debug", new DebugModeCommand());

        }

        public void SelectDevice(CDJ device)
        {
            selectedCDJ = device;
        }

        public CDJ GetSelectedDevice()
        {
            return selectedCDJ;
        }

        public ProLinkController GetPLC()
        {
            return plc;
        }

        public void InitCLI()
        {
            bool exit = false;
            string line = "";
            string command = "";
            string args = "";

            // Start the VIRTUALCDJ 
            plc.GetVirtualCDJ().InitDevice();
            plc.GetVirtualCDJ().ConnectToTheNetwork();
            
            while(!exit)
            {
                Console.WriteLine();
                if (selectedCDJ != null)
                {
                    Console.Write("===:ID: " + selectedCDJ.ChannelID + "@" + selectedCDJ.DeviceName + "> ");
                }
                else
                {
                    Console.Write("===:NO DEVICE SELECTED> ");
                }

                line = Console.ReadLine();
                command = line.Split(' ')[0];
                if(command == "help")
                {
                    foreach (string cmd in commands.Keys)
                    {
                        Console.WriteLine(cmd);
                    }
                }
                else if(commands.ContainsKey(command))
                {
                    // if there command is like command arg1 arg2 arg3 get all the args
                    if (line.Split(' ').Length > 2)
                    {
                        for (int i = 1; i < line.Split(' ').Length; i++)
                        {
                            args += line.Split(' ')[i] + " ";
                        }
                    }
                    else if(line.Split(' ').Length > 1) 
                    {
                        args = line.Split(' ')[1];
                    }
                    if (args.Contains("custom"))
                    {
                        commands[command].BuildCustom(plc, this, args);
                    }
                    else
                    {
                        commands[command].Run(plc, this, args);
                    }
                }
                args = "";
                command = "";
            }
        }
    }
}
