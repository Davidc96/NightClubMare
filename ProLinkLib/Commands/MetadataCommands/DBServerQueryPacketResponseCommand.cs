using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.MetadataCommands
{
    public class DBServerQueryPacketResponseCommand : ICommand
    {
        public ushort Port;

        private byte[] RawBytes;
        public void FromBytes(byte[] packet)
        {
            using (BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
            {
                Port = BitConverter.ToUInt16(Utils.SwapEndianesss(bin.ReadBytes(0x02)), 0);
            }

            RawBytes = packet;
        }

        public byte[] GetRawData()
        {
            return RawBytes;
        }

        public int GetSize()
        {
            return 0x2;
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
                bin.Write(Port);
            }

            return stream.ToArray();
        }
    }
}
