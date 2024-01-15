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
            var table = new ConsoleTable("ID", "DeviceType", "DeviceName", "IPAddress", "MACAddress");

            foreach(var key in plc.GetDevices().Keys)
            {
                string deviceType = "Other";

                if (plc.GetDevices()[key] is CDJ)
                {
                    deviceType = "CDJ";
                }
                
                if(plc.GetDevices()[key] is Mixer)
                {
                    deviceType = "Mixer";
                }

                table.AddRow(plc.GetDevices()[key].GetChannelID(), deviceType, plc.GetDevices()[key].GetDeviceName(), plc.GetDevices()[key].GetIPAddress(), plc.GetDevices()[key].GetMacAddress());
            }

            table.Write();
            Console.WriteLine();
        }
    }
}
