using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pioneer_CLI.Devices;
using ProLinkLib;
using ProLinkLib.Network.UDP;

namespace Pioneer_CLI.Commands
{
    public class LoadTrackCommand : ICommand
    {
        public void BuildCustom(ProLinkController plc, CommandLineController clc, string args)
        {
            ProLinkLib.Commands.StatusCommands.LoadTrackCommand ld_command = new ProLinkLib.Commands.StatusCommands.LoadTrackCommand();
            ld_command.ChannelID = plc.GetVirtualCDJ().ChannelID;
            ld_command.ChannelID2 = plc.GetVirtualCDJ().ChannelID;
            ld_command.DeviceName = Utils.NameToBytes(plc.GetVirtualCDJ().DeviceName, 0x14);
            ld_command.TrackType = 0x01;
            ld_command.Length = 0x34;
            Console.WriteLine("Payload customization");
            Console.Write("Where is the music located (CD: 1, SD: 2, USB: 3, Laptop: 4): ");
            byte tl_id = Convert.ToByte(Console.ReadLine());
            Console.Write("CDJ where the USB/SD/CD/Laptop is mounted: ");
            byte id_cdj = Convert.ToByte(Console.ReadLine());
            Console.Write("Rekordbox TrackID: ");
            uint trackID = UInt32.Parse(Console.ReadLine(), NumberStyles.HexNumber);
            Console.Write("CDJ ID you want to load the track: ");
            byte id_tocdj = Convert.ToByte(Console.ReadLine());
            Console.Write("CDJ IP Address you want to load the track: ");
            string ip_address = Console.ReadLine();

            ld_command.DeviceToLoad = id_tocdj;
            ld_command.DeviceTrackListLocatedID = id_cdj;
            ld_command.DeviceTracklistLocation = tl_id;
            ld_command.TrackID = Utils.SwapEndianesss(BitConverter.GetBytes(trackID));

            Console.WriteLine();
            Console.WriteLine("Sending custom payload!");

            Console.WriteLine(Hex.Dump(PacketBuilder.PACKET_HEADER.Concat(ld_command.ToBytes()).ToArray()));
            plc.GetVirtualCDJ().GetStatusServer().SendPacketToClient(ip_address, ld_command);



        }

        public void Run(ProLinkController plc, CommandLineController clc, string args)
        {
            try
            {
                if (clc.GetSelectedDevice() != null && clc.GetSelectedDevice() is CDJ)
                {
                    ProLinkLib.Commands.StatusCommands.LoadTrackCommand ld_command = new ProLinkLib.Commands.StatusCommands.LoadTrackCommand();
                    var cdj = (CDJ)clc.GetSelectedDevice();
                    int trackID = Int32.Parse(args);
                    var track_metadata = plc.GetVirtualCDJ().GetTrackMetadata().GetTrackByID(trackID);
                    var track_nfs = clc.GetRekordboxDB().GetTrackById(trackID);
                    byte trackChannelID = 0x00;
                    byte trackPhysicallyLocated = 0x00;
                    byte RekordboxID = 0x00;
                    string trackName = "";
                    if(track_metadata != null)
                    {
                        trackChannelID = (byte)track_metadata.TrackChannelID;
                        trackPhysicallyLocated = (byte)track_metadata.TrackPhysicallyLocated;
                        RekordboxID = (byte)track_metadata.RekordboxID;
                        trackName = track_metadata.TitleName;
                    }
                    else if(track_nfs != null)
                    {
                        trackChannelID = (byte)track_nfs.TrackChannelID;
                        trackPhysicallyLocated = (byte)track_nfs.TrackPhysicallyLocated;
                        RekordboxID = (byte)track_nfs.RekordboxID;
                        trackName = track_nfs.TrackName;
                    }
                    else
                    {
                        Console.WriteLine("Song was not found, please use \"music\" command or \"db connect <device id>\" command to retrieve music from CDJ or Rekordbox");
                        return;
                    }

                    ld_command.ChannelID = plc.GetVirtualCDJ().ChannelID;
                    ld_command.ChannelID2 = plc.GetVirtualCDJ().ChannelID;
                    ld_command.DeviceName = Utils.NameToBytes(plc.GetVirtualCDJ().DeviceName, 0x14);
                    ld_command.DeviceToLoad = Convert.ToByte(cdj.ChannelID);
                    ld_command.DeviceTrackListLocatedID = trackChannelID;
                    ld_command.DeviceTracklistLocation = trackPhysicallyLocated;
                    ld_command.TrackType = cdj.TrackType;
                    ld_command.TrackID = Utils.SwapEndianesss(BitConverter.GetBytes(RekordboxID));
                    ld_command.Length = 0x34;

                    Logger.WriteLogFile("app_client", Logger.LOG_TYPE.INFO, "User sent LOAD_TRACKCOMMAND with this properties:\n" +
                                        $"TrackID: 0x{RekordboxID:X}\n" +
                                        $"DeviceToLoad: {ld_command.DeviceToLoad}\n" +
                                        $"DeviceTrackListLocatedID: {ld_command.DeviceTrackListLocatedID}\n" +
                                        $"DeviceTrackListLocation: {ld_command.DeviceTracklistLocation}\n");
                    plc.GetVirtualCDJ().GetStatusServer().SendPacketToClient(cdj.IpAddress, ld_command);


                    Console.WriteLine($"Track {trackName} loaded!");

                }
                else
                {
                    Console.WriteLine("No device was selected! First select a CDJ device");
                }
            }
            catch(Exception)
            {
                Console.WriteLine("ID does not exist or tracklist is empty, use \"music\" command to retrieve tracks from the network");
            }
        }
    }
}
