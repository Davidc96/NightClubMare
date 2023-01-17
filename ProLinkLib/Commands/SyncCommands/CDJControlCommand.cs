using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.SyncCommands
{
    public class CDJControlCommand : ICommand
    {
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ID = 0x02;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] DeviceName = Enumerable.Repeat((byte)0xB0, 0x14).ToArray();
        [JsonConverter(typeof(HexJsonConverter))]
        public byte Unknown1 = 0x01;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte SubCategory = 0x00;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ChannelID = 0xA0;
        [JsonConverter(typeof(HexJsonConverter))]
        public ushort Length = 0x04;                       // Only 4 Bytes build in payload
        [JsonConverter(typeof(HexJsonConverter))]
        public byte CommandID1 = 0xA1;                     // 0x00 -> Play if stopped 0x01 -> Stop CDJ if Play and return to begin 0x02 -> Do Nothing
        [JsonConverter(typeof(HexJsonConverter))]
        public byte CommandID2 = 0xA2;                     // 0x00 -> Play if stopped 0x01 -> Stop CDJ if Play and return to begin 0x02 -> Do Nothing
        [JsonConverter(typeof(HexJsonConverter))]
        public byte CommandID3 = 0xA3;                     // 0x00 -> Play if stopped 0x01 -> Stop CDJ if Play and return to begin 0x02 -> Do Nothing
        [JsonConverter(typeof(HexJsonConverter))]
        public byte CommandID4 = 0xA4;                     // 0x00 -> Play if stopped 0x01 -> Stop CDJ if Play and return to begin 0x02 -> Do Nothing

        public byte[] RawData;
        public void FromBytes(byte[] packet)
        {
            using(BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
            {
                bin.BaseStream.Seek(0x0B, SeekOrigin.Begin);
                DeviceName = bin.ReadBytes(0x14);
                Unknown1 = bin.ReadByte();
                SubCategory = bin.ReadByte();
                ChannelID = bin.ReadByte();
                Length = BitConverter.ToUInt16(Utils.SwapEndianesss(bin.ReadBytes(0x2)), 0);
                CommandID1 = bin.ReadByte();
                CommandID2 = bin.ReadByte();
                CommandID3 = bin.ReadByte();
                CommandID4 = bin.ReadByte();

            }

            RawData = packet;
        }

        public int GetSize()
        {
            return 0x28; //HEADER + DEVICENAME + BYTE1 + BYTE2 + LENGTH(0x4) + PAYLOAD
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
                bin.Write(CommandID1);
                bin.Write(CommandID2);
                bin.Write(CommandID3);
                bin.Write(CommandID4);
            }

            return stream.ToArray();
        }
        public byte[] GetRawData()
        {
            return RawData;
        }
    }
}
