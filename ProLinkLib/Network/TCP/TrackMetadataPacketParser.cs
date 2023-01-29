using ProLinkLib.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Network.TCP
{
    public class TrackMetadataPacketParser
    {
        private Dictionary<int, ICommand> commands;

        public TrackMetadataPacketParser()
        {
            commands = new Dictionary<int, ICommand>();
        }

        public void ReadPacket(byte[] packet)
        {

        }
    
    }
}
