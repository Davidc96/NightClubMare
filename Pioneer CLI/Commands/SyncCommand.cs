using ProLinkLib;
using ProLinkLib.Commands.SyncCommands;
using ProLinkLib.Network.UDP;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.Commands
{
    public class SyncCommand : ICommand
    {
        public void BuildCustom(ProLinkController plc, CommandLineController clc, string args)
        {
            VirtualCDJ vcdj = plc.GetVirtualCDJ();
            SyncModeCommand sync_command = new SyncModeCommand();
            sync_command.DeviceName = Utils.NameToBytes(vcdj.DeviceName, 0x14);
            sync_command.ChannelID = vcdj.ChannelID;
            sync_command.ChannelID2 = vcdj.ChannelID;
            sync_command.Length = 0x08;

            Console.WriteLine("Payload customization");
            Console.Write("Sync mode status (HEX Value) (ON: 10, OFF: 20): ");
            byte sync_cmd = byte.Parse(Console.ReadLine(), NumberStyles.HexNumber);
            Console.Write("CDJ IP Address you want to load the track: ");
            string ip_address = Console.ReadLine();

            sync_command.SyncCommand = sync_cmd;
            
            Console.WriteLine();
            Console.WriteLine("Sending custom payload!");

            Console.WriteLine(Hex.Dump(PacketBuilder.PACKET_HEADER.Concat(sync_command.ToBytes()).ToArray()));

            vcdj.GetSyncServer().SendPacketToClient(ip_address, sync_command);

        }

        public void Run(ProLinkController plc, CommandLineController clc, string args)
        {
            if(clc.GetSelectedDevice() != null)
            {
                VirtualCDJ vcdj = plc.GetVirtualCDJ();
                SyncModeCommand sync_command = new SyncModeCommand();
                string mode = args.ToLower();

                sync_command.DeviceName = Utils.NameToBytes(vcdj.DeviceName, 0x14);
                sync_command.ChannelID = vcdj.ChannelID;
                sync_command.ChannelID2 = vcdj.ChannelID;
                sync_command.Length = 0x08;

                if(mode == "on")
                {
                    sync_command.SyncCommand = 0x10;
                }
                else if(mode == "off")
                {
                    sync_command.SyncCommand = 0x20;
                }
                else
                {
                    Console.WriteLine("Unknown mode: Usage sync <on|off>");
                    return;
                }
                vcdj.GetSyncServer().SendPacketToClient(clc.GetSelectedDevice().IpAddress, sync_command);
                Console.WriteLine("Sync Mode: " + mode);

                Logger.WriteMessage(Encoding.UTF8.GetBytes("SYNC MODE PAYLOAD"), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.STRING);
                Logger.WriteMessage(PacketBuilder.PACKET_HEADER.Concat(sync_command.ToBytes()).ToArray(), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.HEX); ;
            }
            else
            {
                Console.WriteLine("No device was selected! First select a device");
            }
        }
    }
}
