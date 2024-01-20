using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.Commands
{
    class ExitCommand : ICommand
    {
        public void BuildCustom(ProLinkController plc, CommandLineController clc, string args)
        {
            Console.WriteLine("Cannot customize this command!");
        }

        public void Run(ProLinkController plc, CommandLineController clc, string args)
        {
            if(clc.GetSelectedDevice() != null)
            {
                clc.SelectDevice(null);
            }
            else
            {
                System.Environment.Exit(0);
            }
        }

        public void HelpCommand()
        {
            Console.WriteLine("exit - Close the program");
        }
    }
}
