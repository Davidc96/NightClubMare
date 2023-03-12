using ProLinkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.Devices
{
    public class Mixer : IDevice
    {
        private string deviceName;
        private string ipAddress;
        private string macAddress;
        private int channelID;
        public flagStatus FlagStatus = new flagStatus();
        private byte[] pitch;
        private float bpm;
        private int beat;
        private int TimeOutCounter;
        public string DeviceName { get => deviceName; set => deviceName = value; }
        public string IpAddress { get => ipAddress; set => ipAddress = value; }
        public string MacAddress { get => macAddress; set => macAddress = value; }
        public int ChannelID { get => channelID; set => channelID = value; }
        public byte[] Pitch { get => pitch; set => pitch = value; }
        public float BPM { get => bpm; set => bpm = value; }
        public int Beat { get => beat; set => beat = value; }

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

        /*
         * 
 ------------------------
|    .    .    .    .    |  DeviceName: DEVICENAME    DeviceID: 33
|    .    .    .    .    |  Beat: |////|====|====|====|
|    .    .    .    .    |  BPM: 128.10 
|    .    .    .    .    |  Status Flags: M: 1 BPM: 1 Sync: 1
|    |    |    |    |    |                OAir: 1     Play: 1
|        ---|---         |   
 ------------------------   IPAddress: 255.255.255.255 MAC: 00:01:02:03:04:05
         */
        public void PrintDeviceInfo()
        {
            Console.WriteLine("  ------------------------");
            Console.Write(" |    .    .    .    .    |");
            Console.Write("  DeviceName: " + deviceName);
            Console.WriteLine("     DeviceID: " + channelID);
            Console.Write(" |    .    .    .    .    |");
            Console.Write("  Beat: ");
            PrintBeat();
            Console.Write(" |    .    .    .    .    |");
            Console.WriteLine("  BPM: " + bpm);
            Console.Write(" |    .    .    .    .    |");
            Console.WriteLine("  Status Flags: M: " + FlagStatus.Master + " BPM: " + FlagStatus.BPMSync + " Sync: " + FlagStatus.SyncMode);
            Console.Write(" |    |    |    |    |    |");
            Console.WriteLine("                  OAir: " + FlagStatus.OnAir + " Play: " + FlagStatus.Play);
            Console.WriteLine(" |        ---|---         |");
            Console.Write("  ------------------------ ");
            Console.WriteLine("                 IP: " + IpAddress + " MAC: " + MacAddress);

        }

        public string GetDeviceInfo()
        {
            string device_info = "";
            device_info += " ------------------------ \n";
            device_info += "|    4    1    2    3    |\n";
            device_info += "|    .    .    .    .    |\n";
            device_info += "|    .    .    .    .    |\n";
            device_info += "|    .    .    .    .    |\n";
            device_info += "|    |    |    |    |    |\n";
            device_info += "|        ---|---         |\n";
            device_info += " ------------------------ \n";
            device_info += "      BPM: " + bpm + "    \n";
            return device_info;
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
            switch(beat)
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

            Console.WriteLine(beat_form);
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
