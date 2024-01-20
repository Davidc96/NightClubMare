using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.Commands
{
    public interface ICommand
    {
       void Run(ProLinkController plc, CommandLineController clc, string args);

        void BuildCustom(ProLinkController plc, CommandLineController clc, string args);

        void HelpCommand();
    
    }
}
