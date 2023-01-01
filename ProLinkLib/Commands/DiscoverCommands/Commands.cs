using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.DiscoverCommands
{
    public static class Commands
    {
        public static int CONFLICTID_COMMAND = 0x08;
        public static int DEVICEINIT_COMMAND = 0x0A;
        public static int FINAL_ATTEMPT_ID_COMMAND = 0x04;
        public static int FINAL_MIXER_ID_ASSIGN_COMMAND = 0x05;
        public static int FIRST_ATTEMPT_ID_COMMAND = 0x00;
        public static int KEEP_ALIVE_COMMAND = 0x06;
        public static int MIXER_ID_ASSIGN_COMMAND = 0x03;
        public static int MIXER_ID_ATTEMPT_COMMAND = 0x01;
        public static int SECOND_ATTEMPT_ID_COMMAND = 0x02;
    
    }
}
