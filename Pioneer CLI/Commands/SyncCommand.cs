using Pioneer_CLI.Devices;
using ProLinkLib;
using ProLinkLib.Commands.SyncCommands;
using ProLinkLib.Network.UDP;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

            string[] args_splitted = args.Split(' ');
            byte[] file_data;

            if (args_splitted.Length < 2)
            {
                Console.WriteLine("Usage sync custom <import <path> |export>");
                return;
            }

            string main_arg = args_splitted[1].ToLower();
            string param1 = "";

            if (args_splitted.Length > 2)
            {
                param1 = args_splitted[2].ToLower();
            }

            // This is built in case there is a path which contains folders with whitespaces
            if (args_splitted.Length > 3)
            {
                param1 = "";
                for (int i = 2; i < args_splitted.Length - 1; i++)
                {
                    param1 += args_splitted[i] + " ";
                }
                param1 = param1.Remove(param1.Length - 1);
            }

            // Init sync command with dummy values
            sync_command.DeviceName = Utils.NameToBytes("AAAAAAAAAAAAAAAAAAAA", 0x14);
            sync_command.ChannelID = 0XF0;
            sync_command.ChannelID2 = 0xF1;
            sync_command.Length = 0x08;
            sync_command.SyncCommand = 0xF2;

            
            if (main_arg == "export")
            {
                File.WriteAllBytes("sync_cmd_template.bin", PacketBuilder.PACKET_HEADER.Concat(sync_command.ToBytes()).ToArray());
                Console.WriteLine("SYNC Packet template saved as \"sync_cmd_template.bin\"!\nEdit the packet with an external tool such as HxD and import the custom packet using \"sync custom import <path of customed packet>\"");
                return;
            }

            if (main_arg == "import")
            {
                FileStream f = new FileStream(param1.Replace("\"", ""), FileMode.Open);
                BinaryReader bin = new BinaryReader(f);
                file_data = bin.ReadBytes((int)f.Length);
                sync_command.FromBytes(file_data);
                Console.WriteLine("SYNC Packet imported successfully!");
                Console.WriteLine("Preview:");
                Console.WriteLine(Hex.Dump(file_data));
                Console.WriteLine();
            }

            Console.Write("Do you want to send it into specific target or broadcast it? [target|broadcast]: ");
            string option = Console.ReadLine().ToLower();
            string ip_address = "";
            if (option == "target")
            {
                Console.Write("CDJ IP Address you want to send the packet: ");
                ip_address = Console.ReadLine();
                vcdj.GetSyncServer().SendPacketToClient(ip_address, sync_command);
            }
            else
            {
                vcdj.GetSyncServer().SendPacketBroadcast(sync_command);
            }

            Console.WriteLine("Custom payload Sent!");
            /*
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
            */

        }

        public void Run(ProLinkController plc, CommandLineController clc, string args)
        {
            if(clc.GetSelectedDevice() != null && clc.GetSelectedDevice() is CDJ)
            {
                VirtualCDJ vcdj = plc.GetVirtualCDJ();
                SyncModeCommand sync_command = new SyncModeCommand();
                string mode = args.ToLower();
                var cdj = (CDJ)clc.GetSelectedDevice();

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
                Logger.WriteLogFile("app_client", Logger.LOG_TYPE.INFO, "User sent SYNC_COMMAND to CDJId: " + cdj.ChannelID);
                vcdj.GetSyncServer().SendPacketToClient(cdj.IpAddress, sync_command);
                Console.WriteLine("Sync Mode: " + mode);

                Logger.WriteMessage(Encoding.UTF8.GetBytes("SYNC MODE PAYLOAD"), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.STRING);
                Logger.WriteMessage(PacketBuilder.PACKET_HEADER.Concat(sync_command.ToBytes()).ToArray(), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.HEX); ;
            }
            else
            {
                Console.WriteLine("No device or mixer was selected! First select a CDJ device");
            }
        }
    }
}
