using ProLinkLib;
using ProLinkLib.Commands.DiscoverCommands;
using ProLinkLib.Commands.StatusCommands;
using ProLinkLib.Network.UDP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI
{
    class CLIMain
    {
        static void Main(string[] args)
        {
            PrintBanner();
            CommandLineController clc = new CommandLineController();
            NetworkManagementController nwc = new NetworkManagementController();
            /* CDJStatusCommand status = new CDJStatusCommand();
            FileStream f = new FileStream("CDJStatus4.hex.bin", FileMode.Open);
            BinaryReader bin = new BinaryReader(f);
            byte[] test = bin.ReadBytes((int)f.Length);

            status.FromBytes(test);
            clc.GetPLC().StatusServerOnRecvPacket(0x0A, status);*/
            Console.WriteLine();
            nwc.InitCLI(clc.GetPLC());
            clc.InitCLI();
            Console.ReadLine();
        }

        public static void PrintBanner()
        {
             string banner = @"
             ___  _  ___  _ _  ___  ___  ___   ___  ___  ___  _    _  _ _  _ __  ___  _    _ 
            | . \| || . || \ || __>| __>| . \ | . \| . \| . || |  | || \ || / / |  _>| |  | |
            |  _/| || | ||   || _> | _> |   / |  _/|   /| | || |_ | ||   ||  \  | <__| |_ | |
            |_|  |_|`___'|_\_||___>|___>|_\_\ |_|  |_\_\`___'|___||_||_\_||_\_\ `___/|___||_|
                                                                                             ";
            Console.WriteLine("*************************************************************************************************");
            Console.WriteLine(banner);
            Console.WriteLine("                         Developed by @Davidc96 https://github.com/Davidc96");
            Console.WriteLine("");
            Console.WriteLine("*************************************************************************************************");
            Console.WriteLine();
        }


    }
}
