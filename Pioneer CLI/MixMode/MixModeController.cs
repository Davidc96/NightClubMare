using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

namespace Pioneer_CLI.MixMode
{
    public class MixModeController
    {
        // The screen height and width are in number of tiles
        private static int mainScreenWidth = 100;
        private static int mainScreenHeight = 70;
        private static RLRootConsole mainConsole;

        // The map console takes up most of the screen and is where the map will be drawn
        private static readonly int decksConsoleWidth = 80;
        private static readonly int decksConsoleHeight = 48;
        private static RLConsole decksConsole;

        // Below the map console is the message console which displays attack rolls and other information
        private static readonly int CLIConsoleWidth = 80;
        private static readonly int CLIConsoleHeight = 11;
        private static RLConsole CLIConsole;


        public MixModeController()
        {
            mainConsole = new RLRootConsole("terminal8x8.png", mainScreenWidth, mainScreenHeight, 8, 8, 1, "DJ MIX MODE by @Davidc96");
            decksConsole = new RLConsole(decksConsoleWidth, decksConsoleHeight);
            CLIConsole = new RLConsole(CLIConsoleWidth, CLIConsoleHeight);

            mainConsole.Update += OnMainConsoleUpdate;
            mainConsole.Render += OnMainConsoleRender;

            mainConsole.Run();
        }

        private static void OnMainConsoleUpdate(object sender, UpdateEventArgs e)
        {
            decksConsole.SetBackColor(0, 0, RLColor.Blue);
            decksConsole.Print(1,1, "DECKS CONSOLE", RLColor.White);

            CLIConsole.SetBackColor(0, 0, RLColor.Green);
            CLIConsole.Print(1, 1, "CLI CONSOLE", RLColor.White);
        }

        private static void OnMainConsoleRender(object sender, UpdateEventArgs e)
        {
            RLConsole.Blit(decksConsole, 0, 0, decksConsoleWidth, decksConsoleHeight, mainConsole, 0, decksConsoleHeight);
            RLConsole.Blit(CLIConsole, 0, 0, CLIConsoleWidth, CLIConsoleHeight, mainConsole, decksConsoleWidth, 0);

            mainConsole.Draw();
        }
    }
}
