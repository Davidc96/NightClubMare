using ConsoleTables;
using ProLinkLib.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.Commands
{
    public class DevicesCommand : ICommand
    {
        public void BuildCustom(ProLinkController plc, CommandLineController clc, string args)
        {
            Console.WriteLine("Cannot customize this command");
        }

        public void Run(ProLinkController plc, CommandLineController clc, string args)
        { 
            // TODO: Add If there is a database connected or not
            var table = new ConsoleTable("ID", "DeviceType", "DeviceName", "IPAddress", "MACAddress", "DatabaseAvailable?");
            bool databaseAvailable = false;

            foreach(var key in plc.GetDevices().Keys)
            {
                var device = plc.GetDevices()[key];
                string deviceType = "Other";

                if (device is CDJ)
                {
                    deviceType = "CDJ";

                    CDJ cdj = (CDJ)device;
                    databaseAvailable = (cdj.UsbLocalStatus == 0x00 | cdj.SdLocalStatus == 0x00);
                }
                
                if(device is Mixer)
                {
                    deviceType = "Mixer";
                }

                table.AddRow(plc.GetDevices()[key].GetChannelID(), deviceType, plc.GetDevices()[key].GetDeviceName(), plc.GetDevices()[key].GetIPAddress(), plc.GetDevices()[key].GetMacAddress(), databaseAvailable);
            }

            table.Write();
            Console.WriteLine();
        }

        public void HelpCommand()
        {
            Console.WriteLine("devices - Shows the devices connected to the network");
        }
    }
}
