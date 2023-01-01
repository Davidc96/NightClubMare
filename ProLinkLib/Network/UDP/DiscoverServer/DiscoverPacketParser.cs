using ProLinkLib.Commands;
using ProLinkLib.Commands.DiscoverCommands;
using static ProLinkLib.Commands.DiscoverCommands.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Network.UDP.DiscoverServer
{
    public class DiscoverPacketParser
    {
        private Dictionary<int, ICommand> packetCommands;
        public DiscoverPacketParser()
        {
            packetCommands = new Dictionary<int, ICommand>();

            // Discover Server
            packetCommands.Add(FIRST_ATTEMPT_ID_COMMAND, new FirstAttemptIDCommand());
            packetCommands.Add(MIXER_ID_ATTEMPT_COMMAND, new MixerIDAttemptCommand());
            packetCommands.Add(SECOND_ATTEMPT_ID_COMMAND, new SecondAttemptIDCommand());
            packetCommands.Add(MIXER_ID_ASSIGN_COMMAND, new MixerIDAssignCommand());
            packetCommands.Add(FINAL_ATTEMPT_ID_COMMAND, new FinalAttemptIDCommand());
            packetCommands.Add(FINAL_MIXER_ID_ASSIGN_COMMAND, new FinalMixerIDAssignCommand());
            packetCommands.Add(KEEP_ALIVE_COMMAND, new KeepAliveCommand());
            packetCommands.Add(CONFLICTID_COMMAND, new ConflictIDCommand());
            packetCommands.Add(DEVICEINIT_COMMAND, new DeviceInitCommand());

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
                //Console.WriteLine("[Discover Server] Unknown packet " + packet_id);
            }
        }
    
    }
}
