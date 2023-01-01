using ConsoleTables;
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
            var table = new ConsoleTable("ID", "DeviceName", "IPAddress", "MACAddress");

            foreach(var key in plc.GetDevices().Keys)
            {
                table.AddRow(plc.GetDevices()[key].ChannelID, plc.GetDevices()[key].DeviceName, plc.GetDevices()[key].IpAddress, plc.GetDevices()[key].MacAddress);
            }

            table.Write();
            Console.WriteLine();
        }
    }
}
