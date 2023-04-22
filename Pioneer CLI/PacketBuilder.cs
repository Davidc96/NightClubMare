using Newtonsoft.Json;
using ProLinkLib.Commands;
using ProLinkLib.Commands.DiscoverCommands;
using ProLinkLib.Commands.StatusCommands;
using ProLinkLib.Commands.SyncCommands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI
{
    public class PacketBuilder
    {
        public static byte[] PACKET_HEADER = { 0x51, 0x73, 0x70, 0x74, 0x31, 0x57, 0x6d, 0x4a, 0x4f, 0x4c };

        private Dictionary<string, ICommand> commands;

        public PacketBuilder()
        {
            commands = new Dictionary<string, ICommand>();

            // Discover Commands
            commands.Add("conflict_id", new ConflictIDCommand());
            commands.Add("device_init", new DeviceInitCommand());
            commands.Add("final_attempt_id", new FinalAttemptIDCommand());
            commands.Add("final_mixer_id", new FinalMixerIDAssignCommand());
            commands.Add("first_attempt_id", new FirstAttemptIDCommand());
            commands.Add("keep_alive", new KeepAliveCommand());
            commands.Add("mixer_id_assign", new MixerIDAssignCommand());
            commands.Add("mixer_id_attempt", new MixerIDAttemptCommand());
            commands.Add("second_attempt_id", new SecondAttemptIDCommand());

            // Status Commands
            commands.Add("cdj_status", new CDJStatusCommand());
            commands.Add("load_settings", new LoadSettingsCommand());
            commands.Add("load_tracks", new LoadTrackCommand());
            commands.Add("load_track_ack", new LoadTrackAckCommand());
            commands.Add("media_query", new MediaQueryCommand());
            commands.Add("media_response", new MediaResponseCommand());
            commands.Add("mixer_status", new MixerStatusCommand());

            // Sync Commands
            commands.Add("cdj_control", new CDJControlCommand());
            commands.Add("channels_onair", new ChannelsOnAirCommand());
            commands.Add("sync_mode", new SyncModeCommand());
            commands.Add("beat_packet", new BeatCommand());
        
        }

        public void ExportPacket(string packet_name)
        {
            if(commands.ContainsKey(packet_name))
            {
                var cmd = commands[packet_name];
                var bytes = PACKET_HEADER.Concat(commands[packet_name].ToBytes()).ToArray();
                string json = JsonConvert.SerializeObject(cmd);
                if(!Directory.Exists("exported_packets"))
                {
                    Directory.CreateDirectory("exported_packets");
                }
                System.IO.File.WriteAllText("exported_packets\\"+ packet_name + "_template.json", json);
                System.IO.File.WriteAllBytes("exported_packets\\" + packet_name + "_binary.bin", bytes);

                Console.WriteLine("Packet save as: exported_packets\\" + packet_name + "_binary.bin");
                Console.WriteLine("Template saved as: exported_packets\\" + packet_name + "_template.json");

            }
            else
            {
                Console.WriteLine("Unknown command: " + packet_name + ". Use \"pb list\" to list all the available commands");
            }
        }

        public void ListAllCommands()
        {
            Console.WriteLine("Available commands");
            Console.WriteLine("------------------");
            foreach(string cmd in commands.Keys)
            {
                Console.WriteLine(cmd);
            }
        }
    }
}
