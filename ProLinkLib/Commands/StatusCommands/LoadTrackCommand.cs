using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.StatusCommands
{
    public class LoadTrackCommand : ICommand
    {
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ID = 0x19;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] DeviceName = Enumerable.Repeat((byte)0xB0, 0x14).ToArray();
        [JsonConverter(typeof(HexJsonConverter))]
        public byte Unknown1 = 0x01;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte SubCategory = 0x00;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ChannelID = 0xA0;
        [JsonConverter(typeof(HexJsonConverter))]
        public ushort Length = 0x34;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ChannelID2 = 0xA1;
        private byte[] BlankBytes = new byte[0x03];
        [JsonConverter(typeof(HexJsonConverter))]
        public byte DeviceTrackListLocatedID = 0xA2;                   // CDJ ChannelID where is located the tracklist
        [JsonConverter(typeof(HexJsonConverter))]
        public byte DeviceTracklistLocation = 0xA3;                    // 0x01 -> CD-Drive, 0x02 -> SD Slot, 0x03 -> USB Slot, 0x04 -> Laptop
        [JsonConverter(typeof(HexJsonConverter))]
        public byte TrackType = 0xA4;
        [JsonConverter(typeof(HexJsonConverter))]
        private byte BlankByte = 0x00;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] TrackID = { 0xA5, 0xA5, 0xA5, 0xA5 };
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] BlankBytes2 = new byte[0xF];
        [JsonConverter(typeof(HexJsonConverter))]
        public byte DeviceToLoad = 0xA6;                                //CDJ ID to load  -1
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] BlankBytes3 = new byte[0x17];

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
                Length = BitConverter.ToUInt16(Utils.SwapEndianesss(bin.ReadBytes(0x02)),0);
                ChannelID2 = bin.ReadByte();
                BlankBytes = bin.ReadBytes(0x03);
                DeviceTrackListLocatedID = bin.ReadByte();
                DeviceTracklistLocation = bin.ReadByte();
                TrackType = bin.ReadByte();
                BlankByte = bin.ReadByte();
                TrackID = bin.ReadBytes(0x04);
                BlankBytes2 = bin.ReadBytes(0x16);
                DeviceToLoad = bin.ReadByte();
                BlankBytes3 = bin.ReadBytes(0x23);

            }

            RawData = packet;

        }

        public int GetSize()
        {
            return 0x58;
        }

        public void PrintCommand()
        {
            throw new NotImplementedException();
        }

        public byte[] ToBytes()
        {
            MemoryStream stream = new MemoryStream();
            BlankBytes2[3] = 0x32;
            using (BinaryWriter bin = new BinaryWriter(stream))
            {
                bin.Write(ID);
                bin.Write(DeviceName);
                bin.Write(Unknown1);
                bin.Write(SubCategory);
                bin.Write(ChannelID);
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(Length)));
                bin.Write(ChannelID2);
                bin.Write(BlankBytes);
                bin.Write(DeviceTrackListLocatedID);
                bin.Write(DeviceTracklistLocation);
                bin.Write(TrackType);
                bin.Write(BlankByte);
                bin.Write(TrackID);
                bin.Write(BlankBytes2);
                bin.Write(DeviceToLoad);
                bin.Write(BlankBytes3);
            }

            return stream.ToArray();
        }
        public byte[] GetRawData()
        {
            return RawData;
        }
    }
}
