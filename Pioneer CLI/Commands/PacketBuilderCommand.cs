﻿using ProLinkLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.Commands
{
    public class PacketBuilderCommand : ICommand
    {
        public void BuildCustom(ProLinkController plc, CommandLineController clc, string args)
        {
            throw new NotImplementedException();
        }

        public void Run(ProLinkController plc, CommandLineController clc, string args)
        {
            PacketBuilder pb = new PacketBuilder();
            string[] args_splitted = args.Split(' ');
            string main_arg = args_splitted[0].ToLower();
            string param1 = "";

            if (args_splitted.Length > 1)
            {
                param1 = args_splitted[1].ToLower();
            }

            // This is built in case there is a path which contains folders with whitespaces
            if (args_splitted.Length > 2)
            {
                param1 = "";
                for (int i = 1; i < args_splitted.Length - 1; i++)
                {
                    param1 += args_splitted[i] + " ";
                }
                param1 = param1.Remove(param1.Length - 1);
            }

            switch(main_arg)
            {
                case "import":
                    ImportPacket(plc, plc.GetVirtualCDJ(), param1);
                    break;
                case "export":
                    pb.ExportPacket(param1);
                    break;
                default:
                    break;
            }
        }

        private void ImportPacket(ProLinkController plc, VirtualCDJ vcdj, string path)
        {
            FileStream f = new FileStream(path.Replace("\"", ""), FileMode.Open);
            BinaryReader bin = new BinaryReader(f);
            var file_data = bin.ReadBytes((int)f.Length);
            var deviceID = 0;

            Console.WriteLine("Packet imported successfully!");
            Console.WriteLine("Preview");
            Console.WriteLine(Hex.Dump(file_data));
            Console.WriteLine("");

            Console.Write("Would you like to broadcast the packet or send it to specific device? [broadcast|client|exit] Default: broadcast: ");
            var send_method = Console.ReadLine();
            

            if (send_method.ToLower() == "client")
            {
                Console.Write("Which device do you want to send it? DeviceID: ");
                deviceID = Convert.ToInt32(Console.ReadLine());
            }
            else if(send_method.ToLower() == "exit")
            {
                Console.WriteLine("Packet builder exit! Reason: User");
                return;
            }
            else
            {
                deviceID = 999;
            }

            Console.Write("Where do you want to send this packet? [Discover|Status|Sync|All] Default: All: ");
            var option = Console.ReadLine();

            if(option.ToLower() == "discover")
            {
                if (deviceID == 999)
                {
                    vcdj.GetDiscoverServer().SendPacketBroadcast(file_data);
                }
                else
                {
                    var device = plc.GetDevices()[deviceID];
                    vcdj.GetDiscoverServer().SendPacketToClient(device.GetIPAddress(), file_data);
                }
            }
            else if(option == "status")
            {
                if (deviceID == 999)
                {
                    vcdj.GetStatusServer().SendPacketBroadcast(file_data);
                }
                else
                {
                    var device = plc.GetDevices()[deviceID];
                    vcdj.GetStatusServer().SendPacketToClient(device.GetIPAddress(), file_data);
                }
            }
            else if(option == "sync")
            {
                if (deviceID == 999)
                {
                    vcdj.GetSyncServer().SendPacketBroadcast(file_data);
                }
                else
                {
                    var device = plc.GetDevices()[deviceID];
                    vcdj.GetSyncServer().SendPacketToClient(device.GetIPAddress(), file_data);
                }
            }
            else
            {
                if (deviceID == 999)
                {
                    vcdj.GetDiscoverServer().SendPacketBroadcast(file_data);
                    vcdj.GetStatusServer().SendPacketBroadcast(file_data);
                    vcdj.GetSyncServer().SendPacketBroadcast(file_data);
                }
                else
                {
                    var device = plc.GetDevices()[deviceID];
                    vcdj.GetStatusServer().SendPacketToClient(device.GetIPAddress(), file_data);
                    vcdj.GetDiscoverServer().SendPacketToClient(device.GetIPAddress(), file_data);
                    vcdj.GetSyncServer().SendPacketToClient(device.GetIPAddress(), file_data);
                }
            }

            Console.WriteLine("Packet sent!");
        }
    }
}