using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.StatusCommands
{
    public class MediaQueryCommand : ICommand
    {
        public byte ID = 0x05;
        public byte[] DeviceName = new byte[0x14];
        public byte Unknown1 = 0x00;
        public byte Unknown2 = 0x00;
        public byte ChannelID;
        public ushort Length;
        public byte[] IPAddress = new byte[0x4];
        private byte[] BlankBytes = new byte[0x3];
        public byte DeviceTrackListLocatedID;                   // CDJ ChannelID where is located the tracklist
        private byte[] BlankBytes2 = new byte[0x3];         
        public byte DeviceTracklistLocation;                    // 0x01 -> CD-Drive, 0x02 -> SD Slot, 0x03 -> USB Slot, 0x04 -> Laptop

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
                IPAddress = bin.ReadBytes(0x04);
                BlankBytes = bin.ReadBytes(0x03);
                DeviceTrackListLocatedID = bin.ReadByte();
                BlankBytes2 = bin.ReadBytes(0x03);
                DeviceTracklistLocation = bin.ReadByte();
            }

            RawData = packet;
        }

        public int GetSize()
        {
            return 0x30;
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
                bin.Write(IPAddress);
                bin.Write(BlankBytes);
                bin.Write(DeviceTrackListLocatedID);
                bin.Write(BlankBytes2);
                bin.Write(DeviceTracklistLocation);

            }

            return stream.ToArray();
        }
        public byte[] GetRawData()
        {
            return RawData;
        }
    }
}
