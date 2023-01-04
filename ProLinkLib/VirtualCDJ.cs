using Newtonsoft.Json;
using ProLinkLib.Commands.DiscoverCommands;
using ProLinkLib.Network.UDP;
using ProLinkLib.Network.UDP.DiscoverServer;
using ProLinkLib.Network.UDP.StatusServer;
using ProLinkLib.Network.UDP.SyncServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        public VirtualCDJ()
        {

            discoverServer = new DiscoverServer();
            statusServer = new StatusServer();
            syncServer = new SyncServer();
            deviceSettings = new DeviceSettings();

            ChannelID = 25;
        }

        public void InitDevice()
        {
            DeviceName = "CDJ-2000NXS2 DAVID";
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

        // Setup the Virtual CDJ into the Network
        public void ConnectToTheNetwork()
        {
            DeviceInitCommand init_command = new DeviceInitCommand();
            FirstAttemptIDCommand first_command = new FirstAttemptIDCommand();
            SecondAttemptIDCommand second_command = new SecondAttemptIDCommand();
            FinalAttemptIDCommand final_command = new FinalAttemptIDCommand();

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
            
            // Send 3 init commands as the protocol does
            discoverServer.SendPacketBroadcast(init_command);
            Thread.Sleep(200);
            discoverServer.SendPacketBroadcast(init_command);
            Thread.Sleep(200);
            discoverServer.SendPacketBroadcast(init_command);
            Thread.Sleep(500);

            // Send first commands
            discoverServer.SendPacketBroadcast(first_command);
            Thread.Sleep(200);
            first_command.PacketCounter = 2;
            discoverServer.SendPacketBroadcast(first_command);
            Thread.Sleep(200);
            first_command.PacketCounter = 3;
            discoverServer.SendPacketBroadcast(first_command);
            Thread.Sleep(500);

            // Send second_command
            discoverServer.SendPacketBroadcast(second_command);
            Thread.Sleep(200);
            second_command.PacketCounter = 2;
            discoverServer.SendPacketBroadcast(second_command);
            Thread.Sleep(200);
            second_command.PacketCounter = 3;
            discoverServer.SendPacketBroadcast(second_command);
            Thread.Sleep(500);

            // Send final command
            discoverServer.SendPacketBroadcast(final_command);
            Thread.Sleep(200);
            final_command.PacketCounter = 2;
            discoverServer.SendPacketBroadcast(final_command);
            Thread.Sleep(200);
            final_command.PacketCounter = 3;
            discoverServer.SendPacketBroadcast(final_command);
            Thread.Sleep(500);

            // Set KeepAlive Task
            var keepAliveTask = Task.Run(() =>
            {
                KeepAliveTask();
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

            while (true)
            {
                //Console.WriteLine("[Discover Server] Sending Keep Alive packet!");
                discoverServer.SendPacketBroadcast(keep_command);
                await Task.Delay(1500);
            }
        }

        public void LoadSettingsFromDisk(string path)
        {

            deviceSettings = (DeviceSettings)JsonConvert.DeserializeObject(System.IO.File.ReadAllText(path));
        }

        public void SaveSettingsToDisk(string file_name)
        {
            string json = JsonConvert.SerializeObject(deviceSettings);
            System.IO.File.WriteAllText(file_name, json);
        }
    }
}
