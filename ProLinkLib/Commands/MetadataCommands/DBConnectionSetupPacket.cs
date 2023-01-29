using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.MetadataCommands
{
    public class DBConnectionSetupPacket : ICommand
    {
        byte Unknown1 = 0x11;
        byte[] UnknownBytes = { 0x00, 0x00, 0x00, 0x01 };

        byte[] RawBytes;

        public void FromBytes(byte[] packet)
        {
            using (BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
            {
                Unknown1 = bin.ReadByte();
                UnknownBytes = bin.ReadBytes(0x04);
            }

            RawBytes = packet;
        }

        public byte[] GetRawData()
        {
            return RawBytes;
        }

        public int GetSize()
        {
            return 0x05;
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
                bin.Write(Unknown1);
                bin.Write(UnknownBytes);
            }

            return stream.ToArray();
        }
    }
}
