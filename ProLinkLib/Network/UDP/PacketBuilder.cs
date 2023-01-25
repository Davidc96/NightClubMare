using ProLinkLib.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Network.UDP
{
    public class PacketBuilder
    {

        public static byte[] PACKET_HEADER = { 0x51, 0x73, 0x70, 0x74, 0x31, 0x57, 0x6d, 0x4a, 0x4f, 0x4c }; // Constant packet

        public byte[] BuildPacket(ICommand command)
        {
            byte[] builded_packet = PACKET_HEADER.Concat(command.ToBytes()).ToArray();

            Logger.WriteLogFile("app_client", Logger.LOG_TYPE.DEBUG, "Packet Builded\n\n" + Hex.Dump(builded_packet));
            return builded_packet;

        }
    
    }
}
