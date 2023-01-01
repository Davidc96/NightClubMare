using ProLinkLib;
using ProLinkLib.Commands.SyncCommands;
using ProLinkLib.Network.UDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.Commands
{
    public class PauseCommand : ICommand
    {
        public void BuildCustom(ProLinkController plc, CommandLineController clc, string args)
        {
            VirtualCDJ vcdj = plc.GetVirtualCDJ();
            CDJControlCommand ctrl_command = new CDJControlCommand();
            ctrl_command.ChannelID = vcdj.ChannelID;
            ctrl_command.DeviceName = Utils.NameToBytes(vcdj.DeviceName, 0x14);
            ctrl_command.Length = 0x4;
            Console.WriteLine("Payload customization");
            Console.Write("CDJ ID: 1 Command (PLAY: 0 STOP: 1 DONOTHING: 2): ");
            byte command1 = Convert.ToByte(Console.ReadLine());
            Console.Write("CDJ ID: 2 Command (PLAY: 0 STOP: 1 DONOTHING: 2): ");
            byte command2 = Convert.ToByte(Console.ReadLine());
            Console.Write("CDJ ID: 3 Command (PLAY: 0 STOP: 1 DONOTHING: 2): ");
            byte command3 = Convert.ToByte(Console.ReadLine());
            Console.Write("CDJ ID: 4 Command (PLAY: 0 STOP: 1 DONOTHING: 2): ");
            byte command4 = Convert.ToByte(Console.ReadLine());

            ctrl_command.CommandID1 = command1;
            ctrl_command.CommandID2 = command2;
            ctrl_command.CommandID3 = command3;
            ctrl_command.CommandID4 = command4;

            Console.WriteLine();
            Console.WriteLine("Sending custom payload!");

            Logger.WriteMessage(Encoding.UTF8.GetBytes("PAUSE CDJControl PAYLOAD"), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.STRING);
            Logger.WriteMessage(PacketBuilder.PACKET_HEADER.Concat(ctrl_command.ToBytes()).ToArray(), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.HEX);

            Console.WriteLine(Hex.Dump(PacketBuilder.PACKET_HEADER.Concat(ctrl_command.ToBytes()).ToArray()));

            vcdj.GetSyncServer().SendPacketBroadcast(ctrl_command);

        }

        public void Run(ProLinkController plc, CommandLineController clc, string args)
        {
            if (clc.GetSelectedDevice() != null)
            {
                VirtualCDJ vcdj = plc.GetVirtualCDJ();
                int id = clc.GetSelectedDevice().ChannelID;
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
                        ctrl_command.CommandID1 = 0x01;
                        break;
                    case 2:
                        ctrl_command.CommandID2 = 0x01;
                        break;
                    case 3:
                        ctrl_command.CommandID3 = 0x01;
                        break;
                    case 4:
                        ctrl_command.CommandID4 = 0x01;
                        break;
                }
                Logger.WriteMessage(Encoding.UTF8.GetBytes("PAUSE CDJControl PAYLOAD"), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.STRING);
                Logger.WriteMessage(PacketBuilder.PACKET_HEADER.Concat(ctrl_command.ToBytes()).ToArray(), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.HEX);

                vcdj.GetSyncServer().SendPacketBroadcast(ctrl_command);
                Console.WriteLine("CDJ " + id + " stopped!");

            }
            else
            {
                Console.WriteLine("No device was selected! First select a device");
            }

        }
    }
}
