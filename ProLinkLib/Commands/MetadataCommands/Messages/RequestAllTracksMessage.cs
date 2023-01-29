using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.MetadataCommands.Messages
{
    public class RequestAllTracksMessage : MessageStructure
    {
        private byte Unknown1 = 0x11;
        public byte ChannelID;
        private byte Unknown2 = 0x01;
        public byte TrackPhysicallyLocated;
        public byte TrackType;
        private byte Unknown3 = 0x11;
        public byte[] Sort = { 0x00, 0x00, 0x00, 0x00 };

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
                Sort = bin.ReadBytes(0x4);
            }
        }

        public void BuildArguments()
        {
            byte[] argument_bytes = { Unknown1, ChannelID, Unknown2, TrackPhysicallyLocated, TrackType, Unknown3 };
            
            argument_bytes = argument_bytes.Concat(Sort).ToArray();

            ArgumentInformation = new byte[0xC];
            ArgumentInformation[0] = 0x06;
            ArgumentInformation[1] = 0x06;
            
            Arguments = argument_bytes;

        }


    }
}
