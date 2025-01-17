﻿using ProLinkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.Commands
{
    public class DebugModeCommand : ICommand
    {
        public void BuildCustom(ProLinkController plc, CommandLineController clc, string args)
        {
            Console.WriteLine("Cannot customize this command!");
        }

        public void Run(ProLinkController plc, CommandLineController clc, string args)
        {
            Logger.PRINT_DEBUG = true;
        }

        public void HelpCommand()
        {
            Console.WriteLine("debug - Adds more verbosity into logs");
        }
    }
}
