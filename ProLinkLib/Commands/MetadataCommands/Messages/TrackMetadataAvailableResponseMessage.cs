using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.MetadataCommands.Messages
{
    public class TrackMetadataAvailableResponseMessage : MessageStructure
    {
        byte Unknown1 = 0x11;
        byte[] TypeRequest = { 0x00, 0x00, 0x00, 0x00 };
        byte Unknown2 = 0x11;
        public uint TrackCounter;

        public void ParseBytes(byte[] packet, bool isRekordbox)
        {
            this.FromBytes(packet);

            using (BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
            {
                if(isRekordbox)
                {
                    bin.BaseStream.Seek(0x16, SeekOrigin.Begin);
                }
                else
                {
                    bin.BaseStream.Seek(0x20, SeekOrigin.Begin);
                }
                Unknown1 = bin.ReadByte();
                TypeRequest = bin.ReadBytes(0x4);
                Unknown2 = bin.ReadByte();
                TrackCounter = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x04)), 0);
            }

        }
    }
}
