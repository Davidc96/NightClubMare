using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.MetadataCommands.Messages
{
    public class RenderMenuRequestMessage : MessageStructure
    {
        private byte Unknown1 = 0x11;
        public byte ChannelID;
        private byte Unknown2 = 0x01;
        public byte TrackPhysicallyLocated;
        public byte TrackType;
        private byte Unknown3 = 0x11;
        public uint Offset;
        private byte Unknown4 = 0x11;
        public uint Limit;
        private byte Unknown5 = 0x11;
        private byte[] BlankBytes1 = new byte[0x4];
        private byte Unknown6 = 0x11;
        public uint Total;
        private byte Unknown7 = 0x11;
        private byte[] BlankBytes2 = new byte[0x4];

        public void ParseBytes(byte[] packet)
        {
            this.FromBytes(packet);

            using (BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
            {
                bin.BaseStream.Seek(0x16, SeekOrigin.Begin);
                Unknown1 = bin.ReadByte();
                ChannelID = bin.ReadByte();
                Unknown2 = bin.ReadByte();
                TrackPhysicallyLocated = bin.ReadByte();
                TrackType = bin.ReadByte();
                Unknown3 = bin.ReadByte();
                Offset = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x04)), 0);
                Unknown4 = bin.ReadByte();
                Limit = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x04)), 0);
                Unknown5 = bin.ReadByte();
                BlankBytes1 = bin.ReadBytes(0x04);
                Unknown6 = bin.ReadByte();
                Total = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x04)), 0);
                Unknown7 = bin.ReadByte();
                BlankBytes2 = bin.ReadBytes(0x04);

            }
        }

        public void BuildArguments()
        {
            byte[] argument_bytes = { Unknown1, ChannelID, Unknown2, TrackPhysicallyLocated, TrackType, Unknown3 };
            byte[] offset_bytes = Utils.SwapEndianesss(BitConverter.GetBytes(Offset));
            byte[] limit_bytes = Utils.SwapEndianesss(BitConverter.GetBytes(Limit));
            byte[] total_bytes = Utils.SwapEndianesss(BitConverter.GetBytes(Total));
            MemoryStream mem = new MemoryStream();
            using(BinaryWriter bin = new BinaryWriter(mem))
            {
                bin.Write(argument_bytes);
                bin.Write(offset_bytes);
                bin.Write(Unknown4);
                bin.Write(limit_bytes);
                bin.Write(Unknown5);
                bin.Write(BlankBytes1);
                bin.Write(Unknown6);
                bin.Write(total_bytes);
                bin.Write(Unknown7);
                bin.Write(BlankBytes2);
            }


            ArgumentInformation = new byte[0xC];
            ArgumentInformation[0] = 0x06;
            ArgumentInformation[1] = 0x06;
            ArgumentInformation[2] = 0x06;
            ArgumentInformation[3] = 0x06;
            ArgumentInformation[4] = 0x06;
            ArgumentInformation[5] = 0x06;

            Arguments = mem.ToArray();

        }

    }
}
