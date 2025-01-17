﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.Commands
{
    class InfoCommand : ICommand
    {
        public void BuildCustom(ProLinkController plc, CommandLineController clc, string args)
        {
            Console.WriteLine("Cannot customize this command!");
        }

        public void Run(ProLinkController plc, CommandLineController clc, string args)
        {
            if (clc.GetSelectedDevice() != null)
            {
                var device = clc.GetSelectedDevice();
                device.PrintDeviceInfo();
            }
            else
            {
                Console.WriteLine("No device was selected! First select a device");
            }
        }

        public void HelpCommand()
        {
            Console.WriteLine("info - Shows the info from a Device (you need to first select a Device using the \"select\" command");
        }
    }
}
