﻿using ProLinkLib.Database.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib
{
    public static class Utils
    {
        public static byte[] SwapEndianesss(byte[] data)
        {
            Array.Reverse(data);
            return data;
        }

        public static PhysicalAddress GetMacAddress(string networkInterfaceName)
        {
            var networkInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(nw => nw.Name == networkInterfaceName);

            return networkInterface.GetPhysicalAddress();
        }

        public static Dictionary<int, NetworkInterface> GetNetworkInterfaces()
        {
            Dictionary<int, NetworkInterface> nw_int = new Dictionary<int, NetworkInterface>();
            int id = 1;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Let's filter all the Virtual and VPN network interfaces
                if((nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet || nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211) 
                    && !nic.Description.Contains("Virtual") && !nic.Description.Contains("VPN"))
                {
                    nw_int.Add(id, nic);
                    id = id + 1;
                }

            }

            return nw_int;
        }

        public static bool SetIP(string networkInterfaceName, string ipAddress, string subnetMask, string gateway = null)
        {
            var networkInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(nw => nw.Name == networkInterfaceName);
            var ipProperties = networkInterface.GetIPProperties();
            var ipInfo = ipProperties.UnicastAddresses.FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork);
            var currentIPaddress = ipInfo.Address.ToString();
            var currentSubnetMask = ipInfo.IPv4Mask.ToString();
            var isDHCPenabled = ipProperties.GetIPv4Properties().IsDhcpEnabled;

            if (!isDHCPenabled && currentIPaddress == ipAddress && currentSubnetMask == subnetMask)
                return true;    // no change necessary

            var process = new Process
            {
                StartInfo = new ProcessStartInfo("netsh", $"interface ip set address \"{networkInterfaceName}\" static {ipAddress} {subnetMask}" + (string.IsNullOrWhiteSpace(gateway) ? "" : $"{gateway} 1")) { Verb = "runas" }
            };
            process.Start();
            //var successful = process.ExitCode == 0;
            process.Dispose();
            return true;
        }

        public static IPAddress GetBroadcastAddress(IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] | (subnetMaskBytes[i] ^ 255));
            }
            return new IPAddress(broadcastAddress);
        }

        // Prepare the name to be used into UDP packets
        public static byte[] NameToBytes(string deviceName, int name_length)
        {
            byte[] name_b = Encoding.UTF8.GetBytes(deviceName);
            byte[] result = new byte[name_length];

            for (int i = 0; i < name_b.Length; i++)
            {
                result[i] = name_b[i];
            }

            return result;
        }

        public static string BytesToName(byte[] name)
        {
            string nme = "";
            for(int i = 0; i < name.Length; i++)
            {
                char nm = (char)name[i];
                if(nm != 0x00)
                {
                    nme += nm;
                }
            }

            return nme;
        }
        public static bool IsTrackDuplicated(List<Track> tracks, string track_name)
        {
            foreach(Track track in tracks)
            {
                if(track.TrackName == track_name)
                {
                    return true;
                }
            }

            return false;
        }
        public static string BytesToIPString(byte[] ip_bytes)
        {
            return new IPAddress(ip_bytes).ToString();
        }

        public static float CalculateBPM(byte[] bpm)
        {
            return (bpm[0] * 256 + bpm[1]) / 100;
        }

        public static string GetDeviceStatus(byte deviceStatus)
        {
            switch(deviceStatus)
            {
                case 0x00:
                    return "Loaded";
                case 0x04:
                    return "Not Loaded";
                case 0x03:
                    return "Stopped";
                default:
                    return "Unknown";
            }
        }

        public static bool GetBitFromByte(byte b, int bitNumber)
        {
            return ((b >> bitNumber) & 1) != 0;
        }

        public static string GetPlayerStatus(byte playerStatus)
        {
            switch(playerStatus)
            {
                case 0x00:
                    return "No track";
                case 0x02:
                    return "Track Loading";
                case 0x03:
                    return "Playing normal";
                case 0x04:
                    return "Playing a Loop";
                case 0x05:
                    return "Paused in another point";
                case 0x06:
                    return "Paused in Cue Point";
                case 0x07:
                    return "Cue Play in progress";
                case 0x08:
                    return "Cue Scratch in Progress";
                case 0x09:
                    return "Player searching";
                case 0x0e:
                    return "Audio CD Shutdown";
                case 0x11:
                    return "End of the track";
                default:
                    return "Unknown";
            }
        }

        public static byte GetLanguage(string language)
        {
            switch(language)
            {
                case "english":
                    return 0x81;
                case "french":
                    return 0x82;
                case "german":
                    return 0x83;
                case "italian":
                    return 0x84;
                case "dutch":
                    return 0x85;
                case "spanish":
                    return 0x86;
                case "russian":
                    return 0x87;
                case "korean":
                    return 0x88;
                case "chinese-simp":
                    return 0x89;
                case "chinese-trad":
                    return 0x8A;
                case "japanese":
                    return 0x8B;
                case "portuguese":
                    return 0x8C;
                case "swedish":
                    return 0x8D;
                case "czech":
                    return 0x8E;
                case "magyar":
                    return 0x8F;
                case "danish":
                    return 0x90;
                case "greek":
                    return 0x91;
                case "turkish":
                    return 0x92;
                default:
                    return 0x81; // English
            }
        }

        public static byte GetAutoCueLevel(string acl)
        {
            switch(acl)
            {
                case "-36":
                    return 0x80;
                case "-42":
                    return 0x81;
                case "-48":
                    return 0x82;
                case "-54":
                    return 0x83;
                case "-60":
                    return 0x84;
                case "-66":
                    return 0x85;
                case "-72":
                    return 0x86;
                case "-78":
                    return 0x87;
                case "memory":
                    return 0x88;
                default:
                    return 0x88; // Memory mode
            }
        }

        public static byte GetRangeTempo(string rt)
        {
            switch(rt)
            {
                case "+-6":
                    return 0x80;
                case "+-10":
                    return 0x81;
                case "+-16":
                    return 0x82;
                case "wide":
                    return 0x83;
                default:
                    return 0x83; // Wide
            }
        }

        public static byte GetVinylSpeedAdjust(string vsa)
        {
            switch(vsa)
            {
                case "touch_release":
                    return 0x80;
                case "touch":
                    return 0x81;
                case "release":
                    return 0x82;
                default:
                    return 0x80;
            }
        }

        public static byte GetJogLCDContent(string lcd_content)
        {
            switch(lcd_content)
            {
                case "auto":
                    return 0x80;
                case "simple":
                    return 0x81;
                case "artwork":
                    return 0x82;
                default:
                    return 0x80;
            }
        }

        public static byte GetDiscSlotBrightnessMode(string ds_br)
        {
            switch(ds_br)
            {
                case "off":
                    return 0x80;
                case "min":
                    return 0x81;
                case "max":
                    return 0x82;
                default:
                    return 0x80; // Off
            }
        }
        public static string GetAutoCueLevel(byte al)
        {
            string[] autocue_level = { "-36", "-42", "-48", "-54", "-60", "-66", "-72", "-78", "MEMORY" };
            
            return autocue_level[al - 0x80];
        }

        public static string GetLanguage(byte lang)
        {
            string[] languages = { "English", "French", "German", "Italian", 
                "Dutch", "Spanish", "Russian", "Korean", "Chinese Simpl.", "Chinese Trad.", 
                "Japenese", "Portuguese", "Swedish", "Czech", "Magyar", "Danish", "Turkish" };
            return languages[lang - 0x81];
        }

        public static string GetDiscSlotBrightnessMode(byte ds_br)
        {
            string[] brightness_mode = { "OFF", "Min", "Max" };
            return brightness_mode[ds_br - 0x80];
        }

        public static string GetRangeTempo(byte rt)
        {
            string[] range_tempo = { "+-6", "+-10", "+-16", "+-16", "WIDE" };
            return range_tempo[rt - 0x80];
        }

        public static string GetVinylSpeedAdjust(byte vsa)
        {
            string[] vinylspeed_adjust = { "Touch & Release", "Touch", "Release" };
            return vinylspeed_adjust[vsa - 0x80];
        }

        public static string GetJogLCDContent(byte lcd_content)
        {
            string[] joglcd_content = { "Auto", "Simple", "Artwork" };
            return joglcd_content[lcd_content - 0x80];
        }

        public static string GetDiscoverCommandMessageByID(byte packet_id)
        {
            switch(packet_id)
            {
                case 0x08:
                    return "CONFLICTID_COMMAND";
                case 0x0A:
                    return "DISCOVER_COMMAND";
                case 0x04:
                    return "FINAL_ATTEMPT_ID_COMMAND";
                case 0x05:
                    return "FINAL_MIXER_ID_ASSIGN_COMMAND";
                case 0x00:
                    return "FIRST_ATTEMPT_ID_COMMAND";
                case 0x06:
                    return "KEEP_ALIVE_COMMAND";
                case 0x03:
                    return "MIXER_ID_ASSIGN_COMMAND";
                case 0x01:
                    return "MIXER_ID_ATTEMPT_COMMAND";
                case 0x02:
                    return "SECOND_ATTEMPT_ID_COMMAND";
                default:
                    return "UNKNOWN_PACKET";
            }
        }

        public static string GetStatusCommandMessageByID(byte packet_id)
        {
            switch(packet_id)
            {
                case 0x05:
                    return "MEDIA_QUERY_COMMAND";
                case 0x06:
                    return "MEDIA_RESPONSE_COMMAND";
                case 0x0A:
                    return "CDJ_STATUS_COMMAND";
                case 0x29:
                    return "MIXER_STATUS_COMMAND";
                case 0x1A:
                    return "LOAD_TRACK_ACK_COMMAND";
                default:
                    return "UNKNOWN_PACKET";

            }
        }

        public static string GetSyncCommandMessageByID(byte packet_id)
        {
            switch(packet_id)
            {
                case 0x02:
                    return "CDJ_CONTROL_COMMAND";
                case 0x2A:
                    return "SYNC_MODE_COMMAND";
                case 0x03:
                    return "CHANNELSONAIR_COMMAND";
                default:
                    return "UNKNOWN_PACKET";
            }
        }
    }
}
