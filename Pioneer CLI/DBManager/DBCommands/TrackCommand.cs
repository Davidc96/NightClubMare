using ConsoleTables;
using ProLinkLib;
using ProLinkLib.Commands.StatusCommands;
using ProLinkLib.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.DBManager.DBCommands
{
    public class TrackCommand : IDBCommand
    {
        public void Execute(DBManagerController db_manager, string args)
        {
            string[] arg_list = args.Split(' ');
            
            try
            {
                int track_id = int.Parse(arg_list[0]);
                string command = arg_list[1];
                
                switch(command)
                {
                    case "info":
                        PrintTrackInfo(db_manager, track_id);
                        break;
                    case "download":
                        DownloadTrack(db_manager, track_id);
                        break;
                    case "load":
                        if(arg_list.Length < 3)
                        {
                            Console.WriteLine("Usage \"track <track_id> load <cdj_id>\"");
                            return;
                        }
                        LoadTrackCommand(db_manager, track_id, int.Parse(arg_list[2]));
                        break;
                    default:
                        Console.WriteLine("Usage \"track <id> <info|download|load>\"");
                        break;
                }
                
            }
            catch (Exception ex)
            {
                Logger.WriteLogFile("app_client.txt", Logger.LOG_TYPE.ERROR, ex.ToString());
                Console.WriteLine("Usage track <id> <info|download>");
            }
        }

        public void PrintTrackInfo(DBManagerController db, int track_id)
        {
            var track = db.GetMetadataDB().GetTrackById(track_id);
            if(track != null)
            {
                Console.WriteLine("Track Name: " + track.TrackName);
                Console.WriteLine($"Rekordbox ID: 0x{track.RekordboxID:X}");
                Console.WriteLine("Artist: " + db.GetMetadataDB().GetArtistById(track.ArtistID).ArtistName);
                Console.WriteLine("BPM: " + track.Tempo / 100.0);
            }
            else
            {
                Console.WriteLine("Unable to find a track with ID: " + track_id);
            }
        }

        public void DownloadTrack(DBManagerController db, int track_id)
        {
            if(db.GetConnectedDevice() != null)
            {
                var track = db.GetMetadataDB().GetTrackById(track_id);
                if(track != null)
                {
                    db.GetMetadataDB().DownloadTrack(track);
                }
                else
                {
                    Console.WriteLine("Unable to find a track with ID: " + track_id);
                }
            }
            else
            {
                Console.WriteLine("Sorry but you imported the db locally and download is not available on this mode, " +
                    "please connect to a CDJ to enable all features");
            }
        }

        public void LoadTrackCommand(DBManagerController db, int track_id, int device_to_load)
        {
            LoadTrackCommand ld_command = new LoadTrackCommand();
            var vcdj = db.GetProLinkController().GetVirtualCDJ();
            var track = db.GetMetadataDB().GetTrackById(track_id);
            CDJ device = (CDJ)db.GetProLinkController().GetDeviceById(device_to_load);

            if(track == null)
            {
                Console.WriteLine("Cannot find the track with the specified id");
                return;
            }
            if(device == null)
            {
                Console.WriteLine("Cannot find the specified CDJ. This is the current CDJs available on the network");
                ConsoleTable table = new ConsoleTable("ID", "Device Name");
                
                foreach(IDevice dev in db.GetProLinkController().GetDevices().Values)
                {
                    if(dev is CDJ)
                    {
                        table.AddRow(dev.GetChannelID(), dev.GetDeviceName());
                    }
                }

                table.Write();
                return;
            }

            ld_command.ChannelID = vcdj.ChannelID;
            ld_command.ChannelID2 = vcdj.ChannelID;
            ld_command.DeviceName = Utils.NameToBytes(vcdj.DeviceName, 0x14);
            ld_command.DeviceToLoad = (byte)device_to_load;
            ld_command.TrackID = Utils.SwapEndianesss(BitConverter.GetBytes(track.RekordboxID));
            ld_command.TrackType = 0x01;
            ld_command.DeviceTrackListLocatedID = (byte)track.TrackChannelID;
            ld_command.DeviceTracklistLocation = (byte)track.TrackPhysicallyLocated;
            ld_command.Length = 0x34;

            Logger.WriteLogFile("app_client", Logger.LOG_TYPE.INFO, $"User load {track.TrackName}(0x{track.RekordboxID:X}) to DeviceID: {device.ChannelID}");
            Logger.WriteLogFile("app_client", Logger.LOG_TYPE.DEBUG, "User sent LOAD_TRACKCOMMAND with this properties:\n" +
                                $"TrackID: 0x{track.RekordboxID:X}\n" +
                                $"DeviceToLoad: {ld_command.DeviceToLoad}\n" +
                                $"DeviceTrackListLocatedID: {ld_command.DeviceTrackListLocatedID}\n" +
                                $"DeviceTrackListLocation: {ld_command.DeviceTracklistLocation}\n");
            vcdj.GetStatusServer().SendPacketToClient(device.IpAddress, ld_command);


            Console.WriteLine($"Track {track.TrackName} loaded!");


        }
    }
}
