using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.SyncCommands
{
    public class BeatCommand : ICommand
    {
        public byte ID = 0x28;
        public byte[] DeviceName = Enumerable.Repeat((byte)0xB0, 0x14).ToArray();
        private byte Unknown1 = 0x01;
        private byte SubCategory = 0x00;
        public byte ChannelID = 0xA0;
        public ushort Length = 0x3c;
        public uint NextBeat = 0xA1A1A1A1;
        public uint SecondBeat = 0xA2A2A2A2;
        public uint NextBar = 0xA3A3A3A3;
        public uint FourthBeat = 0xA4A4A4A4;
        public uint SecondBar = 0xA5A5A5A5;
        public uint EightBar = 0xA6A6A6A6;
        private byte[] FBytes = Enumerable.Repeat((byte)0xFF, 24).ToArray();
        public uint Pitch = 0xA7A7A7A7;
        private byte[] BlankBytes = { 0x00, 0x00 };
        public byte[] BPM = { 0xA8, 0xA8 };
        public byte BeatCount = 0xA9;
        private byte[] BlankBytes2 = { 0x00, 0x00 };
        public byte ChannelID2 = 0xAA;

        private byte[] RawData;
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
                NextBeat = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x04)), 0);
                SecondBeat = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x04)), 0);
                NextBar = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x04)), 0);
                FourthBeat = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x04)), 0);
                SecondBar = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x04)), 0);
                EightBar = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x04)), 0);
                FBytes = bin.ReadBytes(24);
                Pitch = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x04)), 0);
                BlankBytes = bin.ReadBytes(0x02);
                BPM = bin.ReadBytes(0x02);
                BeatCount = bin.ReadByte();
                BlankBytes2 = bin.ReadBytes(0x02);
                ChannelID2 = bin.ReadByte();

            }

            RawData = packet;
        }

        public byte[] GetRawData()
        {
            return RawData;
        }

        public int GetSize()
        {
            return 0x60;
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
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(NextBeat)));
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(SecondBeat)));
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(NextBar)));
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(FourthBeat)));
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(SecondBar)));
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(EightBar)));
                bin.Write(FBytes);
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(Pitch)));
                bin.Write(BlankBytes);
                bin.Write(BPM);
                bin.Write(BeatCount);
                bin.Write(BlankBytes2);
                bin.Write(ChannelID2);
            }

            return stream.ToArray();
        }
    }
}
