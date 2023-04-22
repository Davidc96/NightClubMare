using ConsoleTables;
using Pioneer_CLI.DBManager;
using Pioneer_CLI.Devices;
using ProLinkLib;
using System;
using System.IO;
using System.Linq;

namespace Pioneer_CLI.Commands
{
    public class DBCommand : ICommand
    {
        public void BuildCustom(ProLinkController plc, CommandLineController clc, string args)
        {
            throw new NotImplementedException();
        }

        public void Run(ProLinkController plc, CommandLineController clc, string args)
        {
            string[] args_list = args.Split(' ');
            string command = args_list[0];
            string argument = "";
            var param1 = "";
            DBMCommandLine cli = new DBMCommandLine(plc);

            if (args_list.Length > 1)
            {
                argument = args_list[1];
            }

            // This is built in case there is a path which contains folders with whitespaces
            if (args_list.Length > 2)
            {
                param1 = "";
                for (int i = 1; i < args_list.Length - 1; i++)
                {
                    param1 += args_list[i] + " ";
                }
                param1 = param1.Remove(param1.Length - 1);
            }

            switch (command)
            {
                case "connect":
                    // Create the directory if it doesn't exist
                    if (!Directory.Exists("db"))
                    {
                        Directory.CreateDirectory("db");
                    }

                    if (argument == "")
                    {
                        Console.WriteLine("Usage \"db connect <device id>\"");
                        return;
                    }
                    ConnectCDJ(plc, cli, int.Parse(argument));
                    break;
                case "offline":
                    cli.PromptShell(null);
                    break;
                default:
                    Console.WriteLine("Usage db <connect|import|tracks|artists>");
                    break;
            }
        }

        public void ConnectCDJ(ProLinkController plc, DBMCommandLine cli, int device_id)
        {

            if (plc.GetDevices().Keys.Contains(device_id))
            {
                var device = plc.GetDevices()[device_id];
                if ((device is CDJ) == false)
                {
                    Console.WriteLine("You cannot connect to db to that device because is not a CDJ. If it is a Rekordbox device, use music command instead!");
                    return;
                }
                var cdj = (CDJ)device;
                cli.PromptShell(cdj);

            }
            else
            {
                Console.WriteLine("Cannot connect to the CDJ please check if the CDJ is connected");
            }        
        }      
    }
}
