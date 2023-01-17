using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.StatusCommands
{
    public class MediaResponseCommand : ICommand
    {
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ID = 0x06;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] DeviceName = Enumerable.Repeat((byte)0xB0, 0x14).ToArray();
        [JsonConverter(typeof(HexJsonConverter))]
        public byte Unknown1 = 0x00;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte SubCategory = 0x00;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ChannelID = 0xA0;
        [JsonConverter(typeof(HexJsonConverter))]
        public ushort Length = 0x9C;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] BlankBytes = new byte[0x3];
        [JsonConverter(typeof(HexJsonConverter))]
        public byte DeviceTrackListLocatedID = 0xA1;                   // CDJ ChannelID where is located the tracklist
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] BlankBytes2 = new byte[0x3];
        [JsonConverter(typeof(HexJsonConverter))]
        public byte DeviceTracklistLocation = 0xA2;                    // 0x01 -> CD-Drive, 0x02 -> SD Slot, 0x03 -> USB Slot, 0x04 -> Laptop
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] MediaName = Enumerable.Repeat((byte)0xA3, 0x40).ToArray();               // UTF-16 Media Name
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] CreationDate = Enumerable.Repeat((byte)0xA4, 0x18).ToArray();            // UTF-16 Creation Date
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] UnknownData = new byte[0x22];             // UTF-16 Unknown Data
        [JsonConverter(typeof(HexJsonConverter))]
        public ushort TotalTracks = 0xA6;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte UIColor = 0xA7;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte BlankByte = 0x00;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte TracksType = 0xA8;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte SettingsAvailable = 0xA9;                        // 0x00 -> No 0x01 -> Yes
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] BlankBytes3 = new byte[0x02];
        [JsonConverter(typeof(HexJsonConverter))]
        public ushort TotalPlaylist = 0xAA;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] TotalSpace = Enumerable.Repeat((byte)0xAB, 0x40).ToArray();
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] FreeSpace = Enumerable.Repeat((byte)0xAC, 0x40).ToArray();

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
                DeviceTrackListLocatedID = bin.ReadByte();
                BlankBytes2 = bin.ReadBytes(0x03);
                DeviceTracklistLocation = bin.ReadByte();
                MediaName = bin.ReadBytes(0x5F);
                CreationDate = bin.ReadBytes(0x17);
                UnknownData = bin.ReadBytes(0x21);
                TotalTracks = BitConverter.ToUInt16(Utils.SwapEndianesss(bin.ReadBytes(0x02)), 0);
                UIColor = bin.ReadByte();
                BlankByte = bin.ReadByte();
                TracksType = bin.ReadByte();
                SettingsAvailable = bin.ReadByte();
                BlankBytes3 = bin.ReadBytes(0x02);
                TotalPlaylist = BitConverter.ToUInt16(Utils.SwapEndianesss(bin.ReadBytes(0x02)), 0);
                TotalSpace = bin.ReadBytes(0x08);
                FreeSpace = bin.ReadBytes(0x08);

            }

            RawData = packet;
        }

        public int GetSize()
        {
            return 0xC0;
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
                bin.Write(DeviceTrackListLocatedID);
                bin.Write(BlankBytes2);
                bin.Write(DeviceTracklistLocation);
                bin.Write(MediaName);
                bin.Write(CreationDate);
                bin.Write(UnknownData);
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(TotalTracks)));
                bin.Write(UIColor);
                bin.Write(BlankByte);
                bin.Write(TracksType);
                bin.Write(SettingsAvailable);
                bin.Write(BlankBytes3);
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(TotalPlaylist)));
                bin.Write(TotalSpace);
                bin.Write(FreeSpace);

            }

            return stream.ToArray();
        }
        public byte[] GetRawData()
        {
            return RawData;
        }
    }
}
