using ConsoleTables;
using Pioneer_CLI.Commands;
using Pioneer_CLI.Devices;
using ProLinkLib;
using ProLinkLib.Database;
using ProLinkLib.NFS;
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
        private IDevice selectedCDJ;
        private Dictionary<string, ICommand> commands;
        private MetadataDB metadata;
        private NFSController nfs;

        public CommandLineController()
        {
            plc = new ProLinkController();
            selectedCDJ = null;
            commands = new Dictionary<string, ICommand>();
            nfs = new NFSController();
            metadata = new MetadataDB();


            commands.Add("devices", new DevicesCommand());
            commands.Add("select", new SelectCommand());
            commands.Add("exit", new ExitCommand());
            commands.Add("info", new InfoCommand());
            commands.Add("play", new PlayCommand());
            commands.Add("pause", new PauseCommand());
            commands.Add("stop", new PauseCommand());
            commands.Add("sync", new SyncCommand());
            commands.Add("load", new LoadTrackCommand());
            commands.Add("debug", new DebugModeCommand());
            commands.Add("settings", new SettingsCommand());
            commands.Add("packetbuilder", new PacketBuilderCommand());
            commands.Add("pb", new PacketBuilderCommand());
            commands.Add("music", new MusicCommand());
            commands.Add("nfs", new NFSCommand());
            commands.Add("auth", new AuthCommand());
            commands.Add("db", new DBCommand());

        }

        public void SelectDevice(IDevice device)
        {
            selectedCDJ = device;
        }

        public IDevice GetSelectedDevice()
        {
            return selectedCDJ;
        }

        public ProLinkController GetPLC()
        {
            return plc;
        }

        public MetadataDB GetMetadataDB()
        {
            return metadata;
        }

        public NFSController GetNFSController()
        {
            return nfs;
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

            while (!exit)
            {
                Console.WriteLine();
                if (selectedCDJ != null)
                {
                    Console.Write("===:ID: " + selectedCDJ.GetChannelID() + "@" + selectedCDJ.GetDeviceName() + "> ");
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
