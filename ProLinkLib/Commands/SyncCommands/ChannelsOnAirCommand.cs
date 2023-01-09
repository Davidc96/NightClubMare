using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.SyncCommands
{
    public class ChannelsOnAirCommand : ICommand
    {
        public byte ID = 0x03;
        public byte[] DeviceName = new byte[0x14];
        private byte Unknown1 = 0x01;
        private byte Unknown2 = 0x00;
        public byte ChannelID = 0xF0;
        public ushort Length = 0x04;
        public byte Channel1 = 0xF1;
        public byte Channel2 = 0xF2;
        public byte Channel3 = 0xF3;
        public byte Channel4 = 0xF4;
        private byte[] BlankBytes = { 0x00, 0x00, 0x00, 0x00, 0x00 };

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
                Channel1 = bin.ReadByte();
                Channel2 = bin.ReadByte();
                Channel3 = bin.ReadByte();
                Channel4 = bin.ReadByte();
                BlankBytes = bin.ReadBytes(0x05);
            }

            RawData = packet;
        }

        public byte[] GetRawData()
        {
            return RawData;
        }

        public int GetSize()
        {
            return 0x2d;
        }

        public void PrintCommand()
        {
            Console.WriteLine(Hex.Dump(RawData));
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
                bin.Write(Channel1);
                bin.Write(Channel2);
                bin.Write(Channel3);
                bin.Write(Channel4);
                bin.Write(BlankBytes);
            }

            return stream.ToArray();
        }
    }
}
