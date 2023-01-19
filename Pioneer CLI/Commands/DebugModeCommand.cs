using ProLinkLib;
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
            Logger logger = new Logger();
            System.Diagnostics.Process.Start("PCLILogger.exe", "1"); // Start PCLILogger
        }
    }
}
