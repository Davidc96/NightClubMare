using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.MetadataCommands.Messages
{
    class QuerySetupResponseMessage : MessageStructure
    {
        private byte UnknownByte = 0x11;
        private byte[] BlankBytes = new byte[0x4];
        private byte UnknownByte2 = 0x11;
        public byte[] ChannelID = new byte[0x4];
        public void ParseBytes(byte[] packet)
        {
            this.FromBytes(packet);

            using (BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
            {
                bin.BaseStream.Seek(0x16, SeekOrigin.Begin);
                UnknownByte = bin.ReadByte();
                BlankBytes = bin.ReadBytes(0x4);
                UnknownByte2 = bin.ReadByte();
                ChannelID = bin.ReadBytes(0x4);
            }

        }
    }
}
