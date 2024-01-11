using ProLinkLib.Devices;
using ProLinkLib;
using ProLinkLib.Commands;
using ProLinkLib.Commands.StatusCommands;
using static ProLinkLib.Commands.DiscoverCommands.Commands;
using static ProLinkLib.Commands.StatusCommands.Commands;
using static ProLinkLib.Commands.SyncCommands.Commands;
using static ProLinkLib.ProLinkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProLinkLib.Commands.DiscoverCommands;
using ProLinkLib.Network.UDP;
using ProLinkLib.Commands.SyncCommands;

namespace Pioneer_CLI
{
    public class ProLinkController
    {
        private VirtualCDJ virtualCDJ;
        private Dictionary<int, IDevice> CDJList;

        public ProLinkController()
        {
            virtualCDJ = new VirtualCDJ();
            virtualCDJ.GetDiscoverServer().OnRecvPacketFunc += DiscoverServerOnRecvPacket;
            virtualCDJ.GetStatusServer().OnRecvPacketFunc += StatusServerOnRecvPacket;
            virtualCDJ.GetSyncServer().OnRecvPacketFunc += SyncServerOnRecvPacket;

            CDJList = new Dictionary<int, IDevice>();

            var task = Task.Run(() =>
            {
                TimeOutTask();
            });
        }

        public Dictionary<int, IDevice> GetDevices()
        {
            return CDJList;
        }

        public IDevice GetDeviceById(int id)
        {
            try
            {
                return CDJList[id];
            }
            catch(Exception)
            {
                return null;
            }

        }

        public VirtualCDJ GetVirtualCDJ()
        {
            return virtualCDJ;
        }
        
        // Task to remove innactive devices
        public async void TimeOutTask()
        {
            while(true)
            {
                try
                {
                    foreach (int device_id in CDJList.Keys)
                    {
                        var device = CDJList[device_id];
                        var current_timeout = device.GetTimeOut() - 1;

                        // If device is not connected let's remove it from the list
                        if (current_timeout <= 0)
                        {
                            CDJList.Remove(device_id);
                        }
                        else
                        {
                            device.SetTimeOut(current_timeout);
                        }
                    }
                }
                catch(Exception)
                {
                    // Exception because we are modificating the dict inside the loop TODO: Remove try exception
                }
                await Task.Delay(1000);
            }
        }

        public bool DiscoverServerOnRecvPacket(int packet_id, ICommand command)
        {

            Logger.WriteMessage(Encoding.UTF8.GetBytes("RECV FROM DISCOVER SERVER!!"), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.STRING);
            Logger.WriteMessage(PacketBuilder.PACKET_HEADER.Concat(command.GetRawData()).ToArray(), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.HEX);
            
            // If there is a new KEEP_ALIVE command, it seems there is a new device connected
            if (packet_id == KEEP_ALIVE_COMMAND)
            {
                KeepAliveCommand ka_command = (KeepAliveCommand)command;
                if(!CDJList.ContainsKey(ka_command.ChannelID))
                {
                    // Check if is not ourselves
                    if(Utils.BytesToIPString(virtualCDJ.IPaddress) != Utils.BytesToIPString(ka_command.IPAddress))
                    {
                        if (Encoding.UTF8.GetString(ka_command.DeviceName).Contains("CDJ"))
                        {
                            CDJ new_device = new CDJ();
                            new_device.ChannelID = ka_command.ChannelID;
                            new_device.DeviceName = Encoding.UTF8.GetString(ka_command.DeviceName);
                            new_device.IpAddress = Utils.BytesToIPString(ka_command.IPAddress);
                            new_device.MacAddress = $"{ka_command.MacAddress[0]:X}:{ka_command.MacAddress[1]:X}" +
                                $":{ka_command.MacAddress[2]:X}:{ka_command.MacAddress[3]:X}:" +
                                $"{ka_command.MacAddress[4]:X}:{ka_command.MacAddress[5]:X}";
                            new_device.SetTimeOut(5);

                            CDJList.Add(ka_command.ChannelID, new_device);
                        }

                        if (Encoding.UTF8.GetString(ka_command.DeviceName).Contains("DJM") || Encoding.UTF8.GetString(ka_command.DeviceName).Contains("rekordbox") || ka_command.DeviceType == 0x02)
                        {
                            Mixer new_device = new Mixer();
                            new_device.ChannelID = ka_command.ChannelID;
                            new_device.DeviceName = Encoding.UTF8.GetString(ka_command.DeviceName);
                            new_device.IpAddress = Utils.BytesToIPString(ka_command.IPAddress);
                            new_device.MacAddress = $"{ka_command.MacAddress[0]:X}:{ka_command.MacAddress[1]:X}" +
                                $":{ka_command.MacAddress[2]:X}:{ka_command.MacAddress[3]:X}:" +
                                $"{ka_command.MacAddress[4]:X}:{ka_command.MacAddress[5]:X}";
                            new_device.SetTimeOut(5);

                            CDJList.Add(ka_command.ChannelID, new_device);
                        }
                    }
                }
                else
                {
                    CDJList[ka_command.ChannelID].SetTimeOut(5); // Let's reset the counter
                }
            }
            else
            {
                //Console.WriteLine("[DEBUG] Packet with id " + packet_id + " received!");
            }
            return true;
        }

        public bool StatusServerOnRecvPacket(int packet_id, ICommand command)
        {
            Logger.WriteMessage(Encoding.UTF8.GetBytes("RECV FROM STATUS SERVER!!"), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.STRING);
            Logger.WriteMessage(PacketBuilder.PACKET_HEADER.Concat(command.GetRawData()).ToArray(), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.HEX);
            if (packet_id == CDJ_STATUS_COMMAND)
            {
                CDJStatusCommand st_command = (CDJStatusCommand)command;
                if(CDJList.ContainsKey(st_command.ChannelID1))
                {
                    CDJ cdj = (CDJ)CDJList[st_command.ChannelID1];

                    cdj.RekordboxTrackID = st_command.RekordboxTrackID;
                    cdj.UsbLocalStatus = st_command.USBLocalStatus;
                    cdj.SdLocalStatus = st_command.SDLocalStatus;
                    cdj.PlayerStatus = st_command.PlayerStatus;
                    cdj.TrackBPM = Utils.CalculateBPM(st_command.BPMTrack);
                    cdj.FlagStatus.BPMSync = Utils.GetBitFromByte(st_command.FlagStatus, 1);
                    cdj.FlagStatus.OnAir = Utils.GetBitFromByte(st_command.FlagStatus, 3);
                    cdj.FlagStatus.SyncMode = Utils.GetBitFromByte(st_command.FlagStatus, 4);
                    cdj.FlagStatus.Master = Utils.GetBitFromByte(st_command.FlagStatus, 5);
                    cdj.FlagStatus.Play = Utils.GetBitFromByte(st_command.FlagStatus, 6);
                    cdj.TrackDeviceLocatedID = st_command.TrackDeviceLocatedID;
                    cdj.TrackPhysicallyLocated = st_command.TrackPhysicallyLocated;
                    cdj.TrackType = st_command.TrackType;

                    // CDJList[st_command.ChannelID1].PrintCDJInfo();
                }
            }
            if(packet_id == MIXER_STATUS_COMMAND)
            {
                MixerStatusCommand mx_command = (MixerStatusCommand)command;
                if(CDJList.ContainsKey(mx_command.ChannelID1))
                {
                    Mixer mixer = (Mixer)CDJList[mx_command.ChannelID1];

                    mixer.BPM = Utils.CalculateBPM(mx_command.BPM);
                    mixer.Pitch = mx_command.Pitch;
                    mixer.Beat = mx_command.BeatCount;
                }
            }

            return true;
        }

        public bool SyncServerOnRecvPacket(int packet_id, ICommand command)
        {
            Logger.WriteMessage(Encoding.UTF8.GetBytes("RECV FROM SYNC SERVER!!"), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.STRING);
            Logger.WriteMessage(PacketBuilder.PACKET_HEADER.Concat(command.GetRawData()).ToArray(), Logger.LOG_TYPE.INFO, Logger.PRINT_MODE.HEX);
            if (packet_id == CHANNELSONAIR_COMMAND)
            {
                ChannelsOnAirCommand ch_command = (ChannelsOnAirCommand)command;
                byte[] ch_array = { ch_command.Channel1, ch_command.Channel2, ch_command.Channel3, ch_command.Channel4 };
                
                for (int i = 0; i < MAX_CHANNELS; i++) // 4 is the number of max channels
                {
                    if (CDJList.ContainsKey(i + 1))
                    {
                        var cdj = (CDJ)CDJList[i + 1];
                       cdj.OnAirChannel = ch_array[i] == 0x01;
                    }
                }
            }

            if(packet_id == BEAT_COMMAND)
            {
                BeatCommand b_command = (BeatCommand)command;
                if(CDJList.ContainsKey(b_command.ChannelID))
                {
                    if (CDJList[b_command.ChannelID] is CDJ)
                    {
                        var cdj = (CDJ)CDJList[b_command.ChannelID];
                        cdj.Beat = b_command.BeatCount;
                    }
                }
            }
            return true;
        }
    
    }
}
