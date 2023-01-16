using Pioneer_CLI.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.Commands
{
    public class SelectCommand : ICommand
    {
        public void BuildCustom(ProLinkController plc, CommandLineController clc, string args)
        {
            Console.WriteLine("Cannot customize this command");
        }

        public void Run(ProLinkController plc, CommandLineController clc, string args)
        {
            try
            {
                InfoCommand info_cmd = new InfoCommand();
                int device_id = Convert.ToInt32(args);
                var device_list = plc.GetDevices();
                if(!device_list.ContainsKey(device_id))
                {
                    Console.WriteLine("ID not found! Use devices command to see the current devices on network");
                    return;
                }

                clc.SelectDevice(device_list[device_id]);
                Console.WriteLine("Device " + device_id + " selected!");
                
                // Run info command automatically to show the current state of CDJ
                info_cmd.Run(plc, clc, null);
            }
            catch
            {
                Console.WriteLine("Error while processing command. Usage select <id>");
            }
        }
    }
}
