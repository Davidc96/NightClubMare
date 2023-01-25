using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.DiscoverCommands
{
    public class KeepAliveCommand : ICommand
    {
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ID = 0x06;                                 // 0x0A
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ToMixer = 0x00;                            // 0x0B
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] DeviceName = Enumerable.Repeat((byte)0xB0, 0x14).ToArray();            // 0x0C - 0x1F
        [JsonConverter(typeof(HexJsonConverter))]
        public byte Unknown = 0x01;                            // 0x20
        [JsonConverter(typeof(HexJsonConverter))]
        public byte SubCategory = 0x02;                           // 0x21
        [JsonConverter(typeof(HexJsonConverter))]
        public ushort Length = 54;                             // 0x22 - 0x23
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ChannelID = 0xA0;                                 // 0x24
        [JsonConverter(typeof(HexJsonConverter))]
        public byte DeviceType = 0xA1;                                // 0x25   0x01 -> CDJ 0x02 -> Mixer
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] MacAddress = {0xA2, 0xA2, 0xA2, 0xA2, 0xA2, 0xA2};                // 0x26 - 0x2B
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] IPAddress = { 0xA3, 0xA3, 0xA3, 0xA3 };                 // 0x2C - 0x2F
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] Payload = { 0x01, 0x00, 0x00, 0x00, 0x01, 0x20};                   // 0x30 - 0x35

        public byte[] RawData;
        public void FromBytes(byte[] packet)
        {
            using (BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
            {
                bin.BaseStream.Seek(0x0B, SeekOrigin.Begin);
                ToMixer = bin.ReadByte();
                DeviceName = bin.ReadBytes(0x14);
                Unknown = bin.ReadByte();
                SubCategory = bin.ReadByte();
                Length = BitConverter.ToUInt16(Utils.SwapEndianesss(bin.ReadBytes(2)), 0);
                ChannelID = bin.ReadByte();
                DeviceType = bin.ReadByte();
                MacAddress = bin.ReadBytes(6);
                IPAddress = bin.ReadBytes(4);
                Payload = bin.ReadBytes(5);
            }

            RawData = packet;
        }

        public void PrintCommand()
        {
            Console.WriteLine("Packet Payload");
            Console.WriteLine("ToMixer: " + $"0x{ToMixer:X}");
            Console.WriteLine("DeviceName: " + Encoding.UTF8.GetString(DeviceName));
            Console.WriteLine("Unknown: " + $"0x{Unknown:X}");
            Console.WriteLine("SubCategory: " + $"0x{SubCategory:X}");
            Console.WriteLine("Length: " + Length);
            Console.WriteLine("ChannelID: " + ChannelID);
            Console.WriteLine("MacAddress: " + $"{MacAddress[0]:X}:{MacAddress[1]:X}:{MacAddress[2]:X}:{MacAddress[3]:X}:{MacAddress[4]:X}:{MacAddress[5]:X}");
            Console.WriteLine("IPAddress: " + new System.Net.IPAddress(IPAddress).ToString());
            Console.WriteLine("Payload: " + $"0x{Payload:X}");
        }

        public byte[] ToBytes()
        {
            MemoryStream stream = new MemoryStream();
            using (BinaryWriter bin = new BinaryWriter(stream))
            {
                bin.Write(ID);
                bin.Write(ToMixer);
                bin.Write(DeviceName);
                bin.Write(Unknown);
                bin.Write(SubCategory);
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(Length)));
                bin.Write(ChannelID);
                bin.Write(DeviceType);
                bin.Write(MacAddress);
                bin.Write(IPAddress);
                bin.Write(Payload);
            }

            return stream.ToArray();
        }
        public int GetSize()
        {
            return Length;
        }
        public byte[] GetRawData()
        {
            return RawData;
        }
    }
}
