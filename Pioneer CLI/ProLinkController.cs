using Pioneer_CLI.Devices;
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
        private Dictionary<int, CDJ> CDJList;

        public ProLinkController()
        {
            virtualCDJ = new VirtualCDJ();
            virtualCDJ.GetDiscoverServer().OnRecvPacketFunc += DiscoverServerOnRecvPacket;
            virtualCDJ.GetStatusServer().OnRecvPacketFunc += StatusServerOnRecvPacket;
            virtualCDJ.GetSyncServer().OnRecvPacketFunc += SyncServerOnRecvPacket;

            CDJList = new Dictionary<int, CDJ>();          
        }

        public Dictionary<int, CDJ> GetDevices()
        {
            return CDJList;
        }

        public VirtualCDJ GetVirtualCDJ()
        {
            return virtualCDJ;
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
                        CDJ new_device = new CDJ();
                        new_device.ChannelID = ka_command.ChannelID;
                        new_device.DeviceName = Encoding.UTF8.GetString(ka_command.DeviceName);
                        new_device.IpAddress = Utils.BytesToIPString(ka_command.IPAddress);
                        new_device.MacAddress = $"{ka_command.MacAddress[0]:X}:{ka_command.MacAddress[1]:X}" +
                            $":{ka_command.MacAddress[2]:X}:{ka_command.MacAddress[3]:X}:" +
                            $"{ka_command.MacAddress[4]:X}:{ka_command.MacAddress[5]:X}";

                        CDJList.Add(ka_command.ChannelID, new_device);
                    }
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
                    CDJList[st_command.ChannelID1].RekordboxTrackID = st_command.RekordboxTrackID;
                    CDJList[st_command.ChannelID1].UsbLocalStatus = st_command.USBLocalStatus;
                    CDJList[st_command.ChannelID1].SdLocalStatus = st_command.SDLocalStatus;
                    CDJList[st_command.ChannelID1].PlayerStatus = st_command.PlayerStatus;
                    CDJList[st_command.ChannelID1].TrackBPM = Utils.CalculateBPM(st_command.BPMTrack);
                    CDJList[st_command.ChannelID1].FlagStatus.BPMSync = Utils.GetBitFromByte(st_command.FlagStatus, 1);
                    CDJList[st_command.ChannelID1].FlagStatus.OnAir = Utils.GetBitFromByte(st_command.FlagStatus, 3);
                    CDJList[st_command.ChannelID1].FlagStatus.SyncMode = Utils.GetBitFromByte(st_command.FlagStatus, 4);
                    CDJList[st_command.ChannelID1].FlagStatus.Master = Utils.GetBitFromByte(st_command.FlagStatus, 5);
                    CDJList[st_command.ChannelID1].FlagStatus.Play = Utils.GetBitFromByte(st_command.FlagStatus, 6);
                    CDJList[st_command.ChannelID1].TrackDeviceLocatedID = st_command.TrackDeviceLocatedID;
                    CDJList[st_command.ChannelID1].TrackPhysicallyLocated = st_command.TrackPhysicallyLocated;
                    CDJList[st_command.ChannelID1].TrackType = st_command.TrackType;

                    // CDJList[st_command.ChannelID1].PrintCDJInfo();
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
                        CDJList[i + 1].OnAirChannel = ch_array[i] == 0x01;
                    }
                }
            }
            return true;
        }
    
    }
}
