using ProLinkLib.Commands;
using ProLinkLib.Commands.MetadataCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Network.TCP
{
    class PacketBuilder
    {
        private byte[] HEADER = { 0x11, 0x87, 0x23, 0x49, 0xAE, 0x11 };

        public byte[] BuildPacketWithoutHeader(ICommand command)
        {
            return command.ToBytes();
        }

        public byte[] BuildMessage(uint TXID, MessageStructure command)
        {
            command.TXID = TXID;

            return HEADER.Concat(command.ToBytes()).ToArray();

            
        }
            
    }
}
