using ProLinkLib.Commands;
using ProLinkLib.Commands.StatusCommands;
using static ProLinkLib.Commands.StatusCommands.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Network.UDP.StatusServer
{
    class StatusPacketParser
    {
        private Dictionary<int, ICommand> packetCommands;

        public StatusPacketParser()
        {
            packetCommands = new Dictionary<int, ICommand>();

            // Status Server
            packetCommands.Add(MEDIA_QUERY_COMMAND, new MediaQueryCommand());
            packetCommands.Add(MEDIA_RESPONSE_COMMAND, new MediaResponseCommand());
            packetCommands.Add(CDJ_STATUS_COMMAND, new CDJStatusCommand());
            packetCommands.Add(LOAD_TRACK_ACK_COMMAND, new LoadTrackAckCommand());


            //
        }
        public void readPacket(byte[] packet, Func<int, ICommand, bool> callback)
        {
            BinaryReader bin = new BinaryReader(new MemoryStream(packet));
            byte[] header = bin.ReadBytes(0x0A);
            byte packet_id = bin.ReadByte();
            try
            {
                packetCommands[packet_id].FromBytes(packet);
                callback(packet_id, packetCommands[packet_id]);
            }
            catch
            {
                //Console.WriteLine("[Status Server] Unknown packet " + packet_id);
            }
        }
    }
}
