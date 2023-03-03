using ProLinkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.Devices
{
    public class CDJ : IDevice
    {
        private string deviceName;
        private string ipAddress;
        private string macAddress;
        private int channelID;
        private byte[] rekordboxTrackID = new byte[0x4];
        private byte usbLocalStatus;
        private byte sdLocalStatus;
        private byte playerStatus;
        private int beat;
        private int TimeOutCounter;
        public struct flagStatus
        {
            private bool bpmSync;
            private bool onAir;
            private bool syncMode;
            private bool master;
            private bool play;

            public bool BPMSync { get => bpmSync; set => bpmSync = value; }
            public bool OnAir { get => onAir; set => onAir = value; }
            public bool SyncMode { get => syncMode; set => syncMode = value; }
            public bool Master { get => master; set => master = value; }
            public bool Play { get => play; set => play = value; }
        }
        public flagStatus FlagStatus = new flagStatus();

        private float bpmTrack;
        private byte trackDeviceLocatedID;                       // ChannelID if is in local, CDJ ChannelID where is located the track
        private byte trackPhysicallyLocated;                     // 0x00 -> NO TRACK, 0x01 -> CD-Drive, 0x02 -> SD Slot, 0x03 -> USB Slot, 0x04 -> Laptop
        private byte trackType;

        private bool onAirChannel = false;

        public string DeviceName { get => deviceName; set => deviceName = value; }
        public string IpAddress { get => ipAddress; set => ipAddress = value; }
        public string MacAddress { get => macAddress; set => macAddress = value; }
        public int ChannelID { get => channelID; set => channelID = value; }
        public byte[] RekordboxTrackID { get => rekordboxTrackID; set => rekordboxTrackID = value; }
        public byte UsbLocalStatus { get => usbLocalStatus; set => usbLocalStatus = value; }
        public byte SdLocalStatus { get => sdLocalStatus; set => sdLocalStatus = value; }
        public byte PlayerStatus { get => playerStatus; set => playerStatus = value; }
        public float TrackBPM{ get => bpmTrack; set => bpmTrack = value; }
        public byte TrackDeviceLocatedID { get => trackDeviceLocatedID; set => trackDeviceLocatedID = value; }
        public byte TrackPhysicallyLocated { get => trackPhysicallyLocated; set => trackPhysicallyLocated = value; }
        public byte TrackType { get => trackType; set => trackType = value; }
        public bool OnAirChannel { get => onAirChannel; set => onAirChannel = value; }
        public int Beat { get => beat; set => beat = value; }

        /*
         *  ------------
           | |________| |
           |            |
           |    ----    |
           |   |    |   |
           |    ----    |
           |	        |
            ------------
         */
        public void PrintDeviceInfo()
        {
            Console.WriteLine("  ------------");
            Console.Write(" | |________| |");
            Console.Write("  DeviceName: " + deviceName);
            Console.Write("     DeviceID: " + channelID);
            PrintOnAirChannelUI();
            Console.Write(" |            |  ");
            PrintBeat();
            Console.Write("  BPM Track: " + bpmTrack + " BPM");
            Console.WriteLine("    RekordBoxTrackID: " + $"0x{BitConverter.ToUInt32(rekordboxTrackID, 0):X}");
            Console.WriteLine(" |    ----    |");
            Console.Write(" |   |    |   |");
            Console.Write("  USB: " + Utils.GetDeviceStatus(usbLocalStatus) + "(" + $"0x{usbLocalStatus:X}" + ")");
            Console.Write(" SD: " + Utils.GetDeviceStatus(sdLocalStatus) + "(" + $"0x{sdLocalStatus:X}" + ")");
            Console.WriteLine("  Player: " + Utils.GetPlayerStatus(playerStatus) + "(" + $"0x{playerStatus:X}" + ")");
            Console.WriteLine(" |    ----    |");
            Console.Write(" |	      |");
            Console.WriteLine("  Status Flags: M: " + FlagStatus.Master + " BPM: " + FlagStatus.BPMSync + " Sync: " + FlagStatus.SyncMode);
            Console.Write("  -----------");
            Console.WriteLine("                  OAir: " + FlagStatus.OnAir + " Play: " + FlagStatus.Play);
            Console.WriteLine("");
            Console.WriteLine("                 IP: " + IpAddress + " MAC: " + MacAddress);
        }

        public override string ToString()
        {
            string device_info = @"";
            // TODO
            return device_info;
        }

        private void PrintOnAirChannelUI()
        {
            if(onAirChannel == true)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("       |ON AIR|");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("       |ON AIR|");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public string GetDeviceName()
        {
            return deviceName;
        }

        public int GetChannelID()
        {
            return channelID;
        }

        public string GetIPAddress()
        {
            return ipAddress;
        }

        public string GetMacAddress()
        {
            return macAddress;
        }

        private void PrintBeat()
        {
            string beat_form = "|====|====|====|====|";
            switch (beat)
            {
                case 1:
                    beat_form = "|////|====|====|====|";
                    break;
                case 2:
                    beat_form = "|====|////|====|====|";
                    break;
                case 3:
                    beat_form = "|====|====|////|====|";
                    break;
                case 4:
                    beat_form = "|====|====|====|////|";
                    break;
            }

            Console.Write(beat_form);
        }

        public int GetTimeOut()
        {
            return TimeOutCounter;
        }

        public void SetTimeOut(int timeout)
        {
            TimeOutCounter = timeout;
        }
    }
}
