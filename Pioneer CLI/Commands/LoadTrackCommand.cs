using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if(clc.GetSelectedDevice() != null)
            {
                ProLinkLib.Commands.StatusCommands.LoadTrackCommand ld_command = new ProLinkLib.Commands.StatusCommands.LoadTrackCommand();
                uint trackID = UInt32.Parse(args, NumberStyles.HexNumber);
                ld_command.ChannelID = plc.GetVirtualCDJ().ChannelID;
                ld_command.ChannelID2 = plc.GetVirtualCDJ().ChannelID;
                ld_command.DeviceName = Utils.NameToBytes(plc.GetVirtualCDJ().DeviceName, 0x14);
                ld_command.DeviceToLoad = Convert.ToByte(clc.GetSelectedDevice().ChannelID);
                ld_command.DeviceTrackListLocatedID = clc.GetSelectedDevice().TrackDeviceLocatedID;
                ld_command.DeviceTracklistLocation = clc.GetSelectedDevice().TrackPhysicallyLocated;
                ld_command.TrackType = clc.GetSelectedDevice().TrackType;
                ld_command.TrackID = Utils.SwapEndianesss(BitConverter.GetBytes(trackID));
                ld_command.Length = 0x34;

                plc.GetVirtualCDJ().GetStatusServer().SendPacketToClient(clc.GetSelectedDevice().IpAddress, ld_command);
                Console.WriteLine($"Track ID: {trackID:X} loaded!");

            }
            else
            {
                Console.WriteLine("No device was selected! First select a device");
            }
        }
    }
}
