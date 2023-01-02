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
    }
}
