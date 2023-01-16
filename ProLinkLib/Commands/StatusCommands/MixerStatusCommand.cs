using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.StatusCommands
{
    public class MixerStatusCommand : ICommand
    {
        public byte ID = 0x29;
        public byte[] DeviceName = new byte[0x14];
        private byte Unknown = 0x01;
        private byte SubCategory = 0x00;
        public byte ChannelID1 = 0xA0;
        public ushort Length = 0x14;
        public byte ChannelID2 = 0xA1;
        private byte[] BlankBytes = { 0x00, 0x00 };
        public byte FlagStatus = 0xA2;
        public byte[] Pitch = { 0xA3, 0xA3, 0xA3, 0xA3 };
        private byte[] UnknownBytes = { 0x80, 0x00 };
        public byte[] BPM = { 0xA4, 0xA4 };
        private byte[] UnknownBytes2 = { 0x00, 0x10, 0x00, 0x00, 0x00, 0x09 };
        public byte MasterHandoff = 0xA5;
        public byte BeatCount = 0xA6;

        public byte[] RawData;
        public void FromBytes(byte[] packet)
        {
            using (BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
            {
                bin.BaseStream.Seek(0x0B, SeekOrigin.Begin);
                DeviceName = bin.ReadBytes(0x14);
                Unknown = bin.ReadByte();
                SubCategory = bin.ReadByte();
                ChannelID1 = bin.ReadByte();
                Length = BitConverter.ToUInt16(Utils.SwapEndianesss(bin.ReadBytes(2)), 0);
                ChannelID2 = bin.ReadByte();
                BlankBytes = bin.ReadBytes(0x02);
                FlagStatus = bin.ReadByte();
                Pitch = bin.ReadBytes(0x4);
                UnknownBytes = bin.ReadBytes(0x02);
                BPM = bin.ReadBytes(0x02);
                UnknownBytes2 = bin.ReadBytes(0x06);
                MasterHandoff = bin.ReadByte();
                BeatCount = bin.ReadByte();
            }

            RawData = packet;
        }

        public byte[] ToBytes()
        {
            MemoryStream stream = new MemoryStream();
            using (BinaryWriter bin = new BinaryWriter(stream))
            {
                bin.Write(ID);
                bin.Write(DeviceName);
                bin.Write(Unknown);
                bin.Write(SubCategory);
                bin.Write(ChannelID1);
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(Length)));
                bin.Write(ChannelID2);
                bin.Write(BlankBytes);
                bin.Write(FlagStatus);
                bin.Write(Pitch);
                bin.Write(UnknownBytes);
                bin.Write(BPM);
                bin.Write(UnknownBytes2);
                bin.Write(MasterHandoff);
                bin.Write(BeatCount);
            }

            return stream.ToArray();
        }

        public void PrintCommand()
        {
            throw new NotImplementedException();
        }

        public int GetSize()
        {
            return 0x38;
        }

        public byte[] GetRawData()
        {
            return RawData;
        }
    }
}
