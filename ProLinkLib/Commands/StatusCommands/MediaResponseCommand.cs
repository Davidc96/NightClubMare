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
        public byte ID = 0x06;
        public byte[] DeviceName = new byte[0x14];
        public byte Unknown1 = 0x00;
        public byte Unknown2 = 0x00;
        public byte ChannelID;
        public ushort Length;
        private byte[] BlankBytes = new byte[0x3];
        public byte DeviceTrackListLocatedID;                   // CDJ ChannelID where is located the tracklist
        private byte[] BlankBytes2 = new byte[0x3];
        public byte DeviceTracklistLocation;                    // 0x01 -> CD-Drive, 0x02 -> SD Slot, 0x03 -> USB Slot, 0x04 -> Laptop
        public byte[] MediaName = new byte[0x40];               // UTF-16 Media Name
        public byte[] CreationDate = new byte[0x18];            // UTF-16 Creation Date
        public byte[] UnknownData = new byte[0x22];             // UTF-16 Unknown Data
        public ushort TotalTracks;
        public byte UIColor;
        public byte BlankByte = 0x00;
        public byte TracksType;
        public byte SettingsAvailable;                        // 0x00 -> No 0x01 -> Yes
        public byte[] BlankBytes3 = new byte[0x02];
        public ushort TotalPlaylist;
        public byte[] TotalSpace = new byte[0x08];
        public byte[] FreeSpace = new byte[0x08];

        public byte[] RawData;
        public void FromBytes(byte[] packet)
        {
            using (BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
            {
                bin.BaseStream.Seek(0x0B, SeekOrigin.Begin);
                DeviceName = bin.ReadBytes(0x14);
                Unknown1 = bin.ReadByte();
                Unknown2 = bin.ReadByte();
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
                bin.Write(Unknown2);
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
