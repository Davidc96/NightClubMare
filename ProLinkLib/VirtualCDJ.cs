using Newtonsoft.Json;
using ProLinkLib.Commands.DiscoverCommands;
using ProLinkLib.Network.UDP;
using ProLinkLib.Network.UDP.DiscoverServer;
using ProLinkLib.Network.UDP.StatusServer;
using ProLinkLib.Network.UDP.SyncServer;
using static ProLinkLib.ProLinkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProLinkLib.Commands.StatusCommands;

namespace ProLinkLib
{
    public class VirtualCDJ
    {
        public byte ChannelID;
        public string DeviceName;

        public byte[] MacAddress = new byte[6];
        public byte[] IPaddress;
        public string BroadcastAddress;


        private DiscoverServer discoverServer;
        private StatusServer statusServer;
        private SyncServer syncServer;
        private DeviceSettings deviceSettings;
        private TrackMetadata trackMetadata;

        private Task cdjStatusTask;
        private Task keepAliveTask;
        private bool stopKeepAliveTask = true;
        private bool stopCDJStatusTask = true;
        public VirtualCDJ()
        {

            discoverServer = new DiscoverServer();
            statusServer = new StatusServer();
            syncServer = new SyncServer();
            deviceSettings = new DeviceSettings();

            ChannelID = VIRTUALCDJ_CHANNELID;
            trackMetadata = new TrackMetadata();
        }

        public void InitDevice()
        {
            DeviceName = VIRTUALCDJ_DEVICENAME;

            discoverServer.IP = IPaddress;
            statusServer.IP = IPaddress;
            syncServer.IP = IPaddress;

            discoverServer.BroadcastAddress = BroadcastAddress;
            statusServer.BroadcastAddress = BroadcastAddress;
            syncServer.BroadcastAddress = BroadcastAddress;


            discoverServer.initServer();
            statusServer.initServer();
            syncServer.initServer();
        }


        public DiscoverServer GetDiscoverServer()
        {
            return discoverServer;
        }

        public StatusServer GetStatusServer()
        {
            return statusServer;
        }

        public SyncServer GetSyncServer()
        {
            return syncServer;
        }

        public DeviceSettings GetDeviceSettings()
        {
            return deviceSettings;
        }

        public TrackMetadata GetTrackMetadata()
        {
            return trackMetadata;
        }

        public void ResetSettingsToDefault()
        {
            deviceSettings = new DeviceSettings();
        }

        // Setup the Virtual CDJ into the Network
        public void ConnectToTheNetwork()
        {
            DeviceInitCommand init_command = new DeviceInitCommand();
            FirstAttemptIDCommand first_command = new FirstAttemptIDCommand();
            SecondAttemptIDCommand second_command = new SecondAttemptIDCommand();
            FinalAttemptIDCommand final_command = new FinalAttemptIDCommand();

            Logger.WriteLogFile("app_client", Logger.LOG_TYPE.INFO, "Connecting VirtualCDJ to the NETWORK with\n" +
                                                                    "ChannelID: " + ChannelID + "\n" +
                                                                    "DeviceName: " + DeviceName);
            init_command.DeviceName = Utils.NameToBytes(DeviceName, 0x14);

            first_command.DeviceName = Utils.NameToBytes(DeviceName, 0x14);
            first_command.MacAddress = MacAddress;
            first_command.PacketCounter = 1;
            first_command.DeviceType = 0x01; // CDJ

            second_command.DeviceName = Utils.NameToBytes(DeviceName, 0x14);
            second_command.PacketCounter = 1;
            second_command.MacAddress = MacAddress;
            second_command.IPAddress = IPaddress;
            second_command.isAutoAssigned = 0x01;
            second_command.ChannelID = ChannelID;

            final_command.DeviceName = Utils.NameToBytes(DeviceName, 0x14);
            final_command.PacketCounter = 1;
            final_command.ChannelID = ChannelID;

            Logger.WriteLogFile("app_client", Logger.LOG_TYPE.INFO, "Sending INIT_COMMAND Packet to reclaim ChannelID");
            // Send 3 init commands as the protocol does
            discoverServer.SendPacketBroadcast(init_command);
            Thread.Sleep(200);
            discoverServer.SendPacketBroadcast(init_command);
            Thread.Sleep(200);
            discoverServer.SendPacketBroadcast(init_command);
            Thread.Sleep(500);

            Logger.WriteLogFile("app_client", Logger.LOG_TYPE.INFO, "Sending FIRST_ATTEMPT_COMMAND Packet to reclaim ChannelID");
            // Send first commands
            discoverServer.SendPacketBroadcast(first_command);
            Thread.Sleep(200);
            first_command.PacketCounter = 2;
            discoverServer.SendPacketBroadcast(first_command);
            Thread.Sleep(200);
            first_command.PacketCounter = 3;
            discoverServer.SendPacketBroadcast(first_command);
            Thread.Sleep(500);

            Logger.WriteLogFile("app_client", Logger.LOG_TYPE.INFO, "Sending SECOND_ATTEMPT_COMMAND Packet to reclaim ChannelID");
            // Send second_command
            discoverServer.SendPacketBroadcast(second_command);
            Thread.Sleep(200);
            second_command.PacketCounter = 2;
            discoverServer.SendPacketBroadcast(second_command);
            Thread.Sleep(200);
            second_command.PacketCounter = 3;
            discoverServer.SendPacketBroadcast(second_command);
            Thread.Sleep(500);

            Logger.WriteLogFile("app_client", Logger.LOG_TYPE.INFO, "Sending LAST_ATTEMPT_COMMAND Packet to reclaim ChannelID");
            // Send final command
            discoverServer.SendPacketBroadcast(final_command);
            Thread.Sleep(200);
            final_command.PacketCounter = 2;
            discoverServer.SendPacketBroadcast(final_command);
            Thread.Sleep(200);
            final_command.PacketCounter = 3;
            discoverServer.SendPacketBroadcast(final_command);
            Thread.Sleep(500);

            Logger.WriteLogFile("app_client", Logger.LOG_TYPE.INFO, "VirtualCDJ connected to the network!");
            // Set KeepAlive Task
            keepAliveTask = Task.Run(() =>
            {
                KeepAliveTask();
            });

            // Set CDJStatus Task
            cdjStatusTask = Task.Run(() =>
            {
                CDJStatusTask();
            });

        }

        private async void KeepAliveTask()
        {
            KeepAliveCommand keep_command = new KeepAliveCommand();
            keep_command.DeviceName = Utils.NameToBytes(DeviceName, 0x14);
            keep_command.ChannelID = ChannelID;
            keep_command.DeviceType = 0x01;
            keep_command.IPAddress = IPaddress;
            keep_command.MacAddress = MacAddress;

            while (stopKeepAliveTask)
            {
                //Console.WriteLine("[Discover Server] Sending Keep Alive packet!");
                Logger.WriteLogFile("app_client", Logger.LOG_TYPE.INFO, "Sending KEEP_ALIVE Packet");
                discoverServer.SendPacketBroadcast(keep_command);
                await Task.Delay(1500);
            }
        }

        private async void CDJStatusTask()
        {
            byte[] default_pitch = { 0x00, 0x0F, 0xC6, 0xA7 }; // Default pitch value while no track is loaded
            byte[] default_bpmcontrol = { 0x7F, 0xFF };
            byte[] default_bpmtrack = { 0xFF, 0xFF };
            byte[] default_cuedistance = { 0x01, 0xFF };
            CDJStatusCommand st = new CDJStatusCommand();
            st.DeviceName = Utils.NameToBytes(DeviceName, 0x14);
            st.ChannelID1 = ChannelID;
            st.ChannelID2 = ChannelID;
            st.ActivityFlag = 0x00;
            st.TrackDeviceLocatedID = 0x00;
            st.TrackPhysicallyLocated = 0x00;
            st.TrackType = 0x00;
            st.RekordboxTrackID = new byte[4];
            st.RekordBoxTrackListPos = new byte[2];
            st.NumberTracksInPlaylist = new byte[2];
            st.USBActivity = 0X04;
            st.SDActivity = 0x04;
            st.USBLocalStatus = 0x04;
            st.SDLocalStatus = 0x04;
            st.LinkAvailable = 0x01;
            st.PlayerStatus = 0x00;
            st.FirmwareVersion = Encoding.ASCII.GetBytes("1.85");
            st.SyncCounter = new byte[0x04];
            st.FlagStatus = 0x8C;
            st.PlayerStatus2 = 0xFE;
            st.Pitch1 = default_pitch;
            st.BPMControl = default_bpmcontrol;
            st.BPMTrack = default_bpmtrack;
            st.Pitch2 = default_pitch;
            st.PlayerModeStatus = 0x00;
            st.MasterHandoff = 0xFF;
            st.Beat = new byte[0x4];
            st.CueDistance = default_cuedistance;
            st.BeatCounter = 0x00;
            st.MediaPresence = 0x00;
            st.USBPresence = 0x00;
            st.SDPresence = 0x00;
            st.EmergencyLoop = 0x00;
            st.Pitch3 = default_pitch;
            st.Pitch4 = default_pitch;
            st.PacketCounter = new byte[4];
            st.IsNexus = 0x1F;
            st.PreviewTrackSupport = 0x00;
            st.WaveColor = 0x00;
            st.WavePosition = 0x01;

            while (stopCDJStatusTask)
            {
                uint packet_counter = BitConverter.ToUInt32(st.PacketCounter, 0);
                packet_counter = packet_counter + 1;
                st.PacketCounter = BitConverter.GetBytes(packet_counter);

                Logger.WriteLogFile("app_client", Logger.LOG_TYPE.INFO, "Sending Virtual CDJ_STATUS");
                Logger.WriteLogFile("app_client", Logger.LOG_TYPE.DEBUG, "CDJ_STATUS Packet information:\n" + Hex.Dump(st.ToBytes()));
                statusServer.SendPacketBroadcast(st);
                await Task.Delay(200);
            }
        }

        public void LoadSettingsFromDisk(string path)
        {

            deviceSettings = JsonConvert.DeserializeObject<DeviceSettings>(System.IO.File.ReadAllText(path.Replace("\"", "")));
        }

        public void SaveSettingsToDisk(string file_name)
        {
            string json = JsonConvert.SerializeObject(deviceSettings);
            System.IO.File.WriteAllText(file_name + ".json", json);
        }

        public void StopTasks()
        {
            stopKeepAliveTask = false;
            stopCDJStatusTask = false;
        }
    }
}
