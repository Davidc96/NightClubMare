using ConsoleTables;
using Pioneer_CLI.Devices;
using ProLinkLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
            if(args_list.Length > 1)
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
                    if(!Directory.Exists("db"))
                    {
                        Directory.CreateDirectory("db");
                    }

                    if(argument == "")
                    {
                        Console.WriteLine("Usage \"db connect <device id>\"");
                        return;
                    }
                    ConnectCDJ(plc, clc, int.Parse(argument));
                    break;
                case "import":
                    clc.GetMetadataDB().GetElementsFromRekordboxDB(param1.Replace("\"", ""), 0x00, 0x00);
                    break;
                case "tracks":
                    PrintAllTracks(clc);
                    break;
                case "artists":
                    break;
                default:
                    Console.WriteLine("Usage db <connect|import|tracks|artists>");
                    break;
            }
        }

        public void ConnectCDJ(ProLinkController plc, CommandLineController clc, int device_id)
        {
            try
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

                    clc.GetNFSController().Connect(cdj.IpAddress, false);

                    if (cdj.UsbLocalStatus == 0x00)
                    {
                        clc.GetNFSController().MountDevice("/C/", false);
                        clc.GetNFSController().GetRekordboxDB();
                        clc.GetMetadataDB().GetElementsFromRekordboxDB("db\\database.pdb", (byte)cdj.ChannelID, (byte)0x03);
                        return;

                    }

                    if (cdj.SdLocalStatus == 0x00)
                    {
                        clc.GetNFSController().MountDevice("/U/", false);
                        clc.GetNFSController().GetRekordboxDB();
                        clc.GetMetadataDB().GetElementsFromRekordboxDB("db\\database.pdb", (byte)cdj.ChannelID, (byte)0x02);
                        return;
                    }

                    Console.WriteLine("Cannot establish a connection to the database, make sure you select the correct CDJ which contains the USB or SD Card");
                }
                else
                {
                    Console.WriteLine("Usage db connect <device_id>");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Cannot connect to the DB, make sure you select the correct CDJ");
                Logger.WriteLogFile("app_client", Logger.LOG_TYPE.ERROR, "Exception in DBCommand class:\n" + e.Message);
            }
        }

        public void PrintAllTracks(CommandLineController clc)
        {
            var tracks = clc.GetMetadataDB().GetTracks();
            ConsoleTable table = new ConsoleTable("ID", "TrackName", "Artist", "BPM");
            var id = 0;
            foreach(var track in tracks)
            {
                if(id != 0)
                    table.AddRow(id, track.TrackName, clc.GetMetadataDB().GetArtistById(track.ArtistID).ArtistName, (track.Tempo / 100.0));

                id += 1;
            }

            table.Write();
        }
    }
}
