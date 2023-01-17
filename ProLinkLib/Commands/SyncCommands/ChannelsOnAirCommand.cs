using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.SyncCommands
{
    public class ChannelsOnAirCommand : ICommand
    {
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ID = 0x03;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] DeviceName = Enumerable.Repeat((byte)0xB0, 0x14).ToArray();
        [JsonConverter(typeof(HexJsonConverter))]
        private byte Unknown1 = 0x01;
        [JsonConverter(typeof(HexJsonConverter))]
        private byte SubCategory = 0x00;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ChannelID = 0xA0;
        [JsonConverter(typeof(HexJsonConverter))]
        public ushort Length = 0x09;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte Channel1 = 0xA1;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte Channel2 = 0xA2;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte Channel3 = 0xA3;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte Channel4 = 0xA4;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] BlankBytes = { 0x00, 0x00, 0x00, 0x00, 0x00 };

        public byte[] RawData;
        public void FromBytes(byte[] packet)
        {
            using (BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
            {
                bin.BaseStream.Seek(0x0B, SeekOrigin.Begin);
                DeviceName = bin.ReadBytes(0x14);
                Unknown1 = bin.ReadByte();
                SubCategory = bin.ReadByte();
                ChannelID = bin.ReadByte();
                Length = BitConverter.ToUInt16(Utils.SwapEndianesss(bin.ReadBytes(0x02)), 0);
                Channel1 = bin.ReadByte();
                Channel2 = bin.ReadByte();
                Channel3 = bin.ReadByte();
                Channel4 = bin.ReadByte();
                BlankBytes = bin.ReadBytes(0x05);
            }

            RawData = packet;
        }

        public byte[] GetRawData()
        {
            return RawData;
        }

        public int GetSize()
        {
            return 0x2d;
        }

        public void PrintCommand()
        {
            Console.WriteLine(Hex.Dump(RawData));
        }

        public byte[] ToBytes()
        {
            MemoryStream stream = new MemoryStream();
            using (BinaryWriter bin = new BinaryWriter(stream))
            {
                bin.Write(ID);
                bin.Write(DeviceName);
                bin.Write(Unknown1);
                bin.Write(SubCategory);
                bin.Write(ChannelID);
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(Length)));
                bin.Write(Channel1);
                bin.Write(Channel2);
                bin.Write(Channel3);
                bin.Write(Channel4);
                bin.Write(BlankBytes);
            }

            return stream.ToArray();
        }
    }
}
