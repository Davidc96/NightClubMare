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
        public byte ID = 0x19;
        public byte[] DeviceName = new byte[0x14];
        public byte Unknown1 = 0x01;
        public byte Unknown2 = 0x00;
        public byte ChannelID;
        public ushort Length;
        public byte ChannelID2;
        private byte[] BlankBytes = new byte[0x03];
        public byte DeviceTrackListLocatedID;                   // CDJ ChannelID where is located the tracklist
        public byte DeviceTracklistLocation;                    // 0x01 -> CD-Drive, 0x02 -> SD Slot, 0x03 -> USB Slot, 0x04 -> Laptop
        public byte TrackType;
        private byte BlankByte = 0x00;
        public byte[] TrackID = new byte[0x04];
        private byte[] BlankBytes2 = new byte[0xF];
        public byte DeviceToLoad;                           //CDJ ID to load  -1
        private byte[] BlankBytes3 = new byte[0x17];

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
                bin.Write(Unknown2);
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
