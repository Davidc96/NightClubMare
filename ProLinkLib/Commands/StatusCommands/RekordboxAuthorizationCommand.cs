using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.StatusCommands
{
    public class RekordboxAuthorizationCommand : ICommand
    {
        public byte ID = 0x4C;
        public byte[] DeviceName = Enumerable.Repeat((byte)0xB0, 0x14).ToArray();
        private byte UnknownByte = 0x01;
        public byte SubCategory = 0x00;
        public byte ChannelID = 0xA0;
        public ushort Length = 0x04;
        public byte OwnChannelID = 0xA1;        
        public byte DeviceType = 0xA2;          // 0x04 -> Rekordbox
        private byte[] BlankBytes = new byte[0x02];

        public byte[] RawData;

        public void FromBytes(byte[] packet)
        {
            using (BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
            {
                bin.BaseStream.Seek(0x0B, SeekOrigin.Begin);
                DeviceName = bin.ReadBytes(0x14);
                UnknownByte = bin.ReadByte();
                SubCategory = bin.ReadByte();
                ChannelID = bin.ReadByte();
                Length = BitConverter.ToUInt16(Utils.SwapEndianesss(bin.ReadBytes(0x02)), 0);
                OwnChannelID = bin.ReadByte();
                DeviceType = bin.ReadByte();
                BlankBytes = bin.ReadBytes(0x02);
            }

            RawData = packet;
        }

        public byte[] GetRawData()
        {
            return RawData;
        }

        public int GetSize()
        {
            return 0x27;
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
                bin.Write(UnknownByte);
                bin.Write(SubCategory);
                bin.Write(ChannelID);
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(Length)));
                bin.Write(OwnChannelID);
                bin.Write(DeviceType);
                bin.Write(BlankBytes);
            }

            return stream.ToArray();
        }
    }
}
