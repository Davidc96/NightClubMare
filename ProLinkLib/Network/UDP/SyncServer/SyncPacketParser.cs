﻿using ProLinkLib.Commands;
using static ProLinkLib.Commands.SyncCommands.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProLinkLib.Commands.SyncCommands;

namespace ProLinkLib.Network.UDP.SyncServer
{
    public class SyncPacketParser
    {
        private Dictionary<int, ICommand> packetCommands;

        public SyncPacketParser()
        {
            packetCommands = new Dictionary<int, ICommand>();

            // Sync Server
            packetCommands.Add(CDJ_CONTROL_COMMAND, new CDJControlCommand());
            packetCommands.Add(SYNC_MODE_COMMAND, new SyncModeCommand());
            packetCommands.Add(CHANNELSONAIR_COMMAND, new ChannelsOnAirCommand());
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
            catch(Exception e)
            {
                //Console.WriteLine("[Sync Server] Unknown packet " + packet_id);
                Logger.WriteLogFile("Sync_Server - logs", Logger.LOG_TYPE.WARNING, $"Exception error with packet 0x{packet_id : X}" +
                                    "\nException message: " + e.Message);
                File.WriteAllBytes($"logs\\failed_dumped_packets\\sync_server - packet - {packet_id: X} - " + DateTime.Now.ToString().Replace("/","").Replace(":","-") + ".bin", packet);
            }
        }
    }
}
