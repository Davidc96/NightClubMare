using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.MetadataCommands.Messages
{
    public class DisconnectMessage : MessageStructure
    {
        public void ParseBytes(byte[] packet)
        {
            this.FromBytes(packet);
        }
    }
}
