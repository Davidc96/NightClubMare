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
            packetCommands.Add(MIXER_STATUS_COMMAND, new MixerStatusCommand());


            //
        }
        public void readPacket(byte[] packet, Func<int, ICommand, bool> callback)
        {
            BinaryReader bin = new BinaryReader(new MemoryStream(packet));
            byte[] header = bin.ReadBytes(0x0A);
            byte packet_id = bin.ReadByte();
            try
            {
                Logger.WriteLogFile("Status_Server - logs", Logger.LOG_TYPE.INFO, $"Received packet {Utils.GetStatusCommandMessageByID(packet_id)}");
                Logger.WriteLogFile("Status_Server - logs", Logger.LOG_TYPE.DEBUG, "Packet Preview\n\n" + Hex.Dump(packet));

                packetCommands[packet_id].FromBytes(packet);
                callback(packet_id, packetCommands[packet_id]);
            }
            catch(Exception e)
            {
                //Console.WriteLine("[Status Server] Unknown packet " + packet_id);
                Logger.WriteLogFile("Status_Server - logs", Logger.LOG_TYPE.ERROR, $"Exception error with packet 0x{packet_id:X}" +
                     "\nException message: " + e.Message + "Exception: " + e.GetType().ToString());
                File.WriteAllBytes($"logs\\failed_dumped_packets\\status_server - packet - 0x{packet_id:X} - " + DateTime.Now.ToString().Replace("/","").Replace(":","-") + ".bin", packet);
            }
        }
    }
}
