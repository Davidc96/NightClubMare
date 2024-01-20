using ProLinkLib.Devices;
using ProLinkLib;
using ProLinkLib.Commands.SyncCommands;
using ProLinkLib.Network.UDP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.Commands
{
    public class PlayCommand : ICommand
    {
        public void BuildCustom(ProLinkController plc, CommandLineController clc, string args)
        {
            VirtualCDJ vcdj = plc.GetVirtualCDJ();
            CDJControlCommand cd_command = new CDJControlCommand();

            string[] args_splitted = args.Split(' ');
            byte[] file_data;

            if (args_splitted.Length < 2)
            {
                Console.WriteLine("Usage play custom <import <path>|export>");
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

            // Init command with dummy values
            cd_command.DeviceName = Utils.NameToBytes("AAAAAAAAAAAAAAAAAAAA", 0x14);
            cd_command.Length = 0x4;
            cd_command.ChannelID = 0xF0;
            cd_command.CommandID1 = 0xF1;
            cd_command.CommandID2 = 0xF2;
            cd_command.CommandID3 = 0xF3;
            cd_command.CommandID4 = 0xF4;

            if (main_arg == "export")
            {
                File.WriteAllBytes("command_cmd_template.bin", PacketBuilder.PACKET_HEADER.Concat(cd_command.ToBytes()).ToArray());
                Console.WriteLine("SYNC Packet template saved as \"command_cmd_template.bin\"!\nEdit the packet with an external tool such as HxD and import the custom packet using \"play custom import <path of customed packet>\"");
                return;
            }

            if (main_arg == "import")
            {
                FileStream f = new FileStream(param1.Replace("\"", ""), FileMode.Open);
                BinaryReader bin = new BinaryReader(f);
                file_data = bin.ReadBytes((int)f.Length);
                cd_command.FromBytes(file_data);
                Console.WriteLine("COMMAND Packet imported successfully!");
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
                vcdj.GetSyncServer().SendPacketToClient(ip_address, cd_command);
            }
            else
            {
                vcdj.GetSyncServer().SendPacketBroadcast(cd_command);
            }

            Console.WriteLine("Custom payload Sent!");
        }

        public void Run(ProLinkController plc, CommandLineController clc, string args)
        {
            if (clc.GetSelectedDevice() != null && clc.GetSelectedDevice() is CDJ)
            {
                VirtualCDJ vcdj = plc.GetVirtualCDJ();
                var cdj = (CDJ)clc.GetSelectedDevice();
                int id = cdj.ChannelID;
                CDJControlCommand ctrl_command = new CDJControlCommand();

                ctrl_command.ChannelID = vcdj.ChannelID;
                ctrl_command.DeviceName = Utils.NameToBytes(vcdj.DeviceName, 0x14);
                ctrl_command.Length = 0x4;
                ctrl_command.CommandID1 = 0x02; // Do Nothing
                ctrl_command.CommandID2 = 0x02; // Do Nothing
                ctrl_command.CommandID3 = 0x02; // Do Nothing
                ctrl_command.CommandID4 = 0x02; // Do Nothing

                switch (id)
                {
                    case 1:
                        ctrl_command.CommandID1 = 0x00;
                        break;
                    case 2:
                        ctrl_command.CommandID2 = 0x00;
                        break;
                    case 3:
                        ctrl_command.CommandID3 = 0x00;
                        break;
                    case 4:
                        ctrl_command.CommandID4 = 0x00;
                        break;
                }
                Logger.WriteMessage(Encoding.UTF8.GetBytes("PLAY CDJControl PAYLOAD"), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.STRING);
                Logger.WriteMessage(PacketBuilder.PACKET_HEADER.Concat(ctrl_command.ToBytes()).ToArray(), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.HEX);

                Logger.WriteLogFile("app_client", Logger.LOG_TYPE.INFO, $"User sent PLAY_COMMAND to CDJId: {id}");
                vcdj.GetSyncServer().SendPacketBroadcast(ctrl_command);
                Console.WriteLine("CDJ " + id + " started playing!");
            }
            else
            {
                Console.WriteLine("No device or mixer was selected! First select a CDJ device");
            }

        }

        public void HelpCommand()
        {
            Console.WriteLine("play - Plays the music (needs a CDJ selected)");
        }
    }
}
