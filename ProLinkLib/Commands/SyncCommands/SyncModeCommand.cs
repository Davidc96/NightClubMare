using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.SyncCommands
{
    public class SyncModeCommand : ICommand
    {
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ID = 0x2A;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] DeviceName = Enumerable.Repeat((byte)0xB0, 0x14).ToArray();
        [JsonConverter(typeof(HexJsonConverter))]
        public byte Unknown1 = 0x01;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte SubCategory = 0x00;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ChannelID = 0xA0;
        [JsonConverter(typeof(HexJsonConverter))]
        public ushort Length = 0x08;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] BlankBytes = new byte[0x3];
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ChannelID2 = 0xA1;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] BlankBytes2 = new byte[0x3];
        [JsonConverter(typeof(HexJsonConverter))]
        public byte SyncCommand = 0xA2;                    // 0x10 Turn On Sync 0x20 Turn Off Sync Mode

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
                BlankBytes = bin.ReadBytes(0x03);
                ChannelID2 = bin.ReadByte();
                BlankBytes2 = bin.ReadBytes(0x03);
                SyncCommand = bin.ReadByte();
            }

            RawData = packet;
        }

        public int GetSize()
        {
            return 0x2C;
        }
        public void PrintCommand()
        {
            throw new NotImplementedException();
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
                bin.Write(BlankBytes);
                bin.Write(ChannelID2);
                bin.Write(BlankBytes2);
                bin.Write(SyncCommand);
            }

            return stream.ToArray();
        }
        public byte[] GetRawData()
        {
            return RawData;
        }
    }
}
