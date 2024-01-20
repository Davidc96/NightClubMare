using ProLinkLib;
using ProLinkLib.Commands.StatusCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.Commands
{
    public class AuthCommand : ICommand
    {
        public void BuildCustom(ProLinkController plc, CommandLineController clc, string args)
        {
            throw new NotImplementedException();
        }

        public void HelpCommand()
        {
            //
            //throw new NotImplementedException();
        }

        public void Run(ProLinkController plc, CommandLineController clc, string args)
        {
            var devices = plc.GetDevices();
            
            foreach(int id in devices.Keys)
            {
                if(devices[id].GetDeviceName().Contains("rekordbox"))
                {
                    var vcdj = plc.GetVirtualCDJ();
                    RekordboxAuthorizationCommand rb_command = new RekordboxAuthorizationCommand();
                    rb_command.ChannelID = vcdj.ChannelID;
                    rb_command.DeviceName = Utils.NameToBytes(vcdj.DeviceName, 0x14);
                    rb_command.OwnChannelID = vcdj.ChannelID;
                    rb_command.DeviceType = 0x04; // Laptop

                    Logger.WriteLogFile("app_client", Logger.LOG_TYPE.INFO, "User sent AUTH_COMMAND to Rekordbox");
                    plc.GetVirtualCDJ().GetStatusServer().SendPacketToRekordbox(devices[id].GetIPAddress(), rb_command);

                    Logger.WriteMessage(Encoding.UTF8.GetBytes("AUTH MODE PAYLOAD"), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.STRING);
                    Logger.WriteMessage(PacketBuilder.PACKET_HEADER.Concat(rb_command.ToBytes()).ToArray(), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.HEX);
                    Console.WriteLine("Authorization sent to Rekordbox!");
                    return;
                }
            }

            Console.WriteLine("No Rekordbox found!");
        }
    }
}
