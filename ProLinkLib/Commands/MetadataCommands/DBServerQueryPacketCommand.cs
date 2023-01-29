using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.MetadataCommands
{
    public class DBServerQueryPacketCommand : ICommand
    {

        private byte[] Header = { 0x00, 0x00, 0x00, 0x0f };
        private string Message = "RemoteDBServer";
        private byte BlankByte = 0x00;
        private byte[] RawBytes;

        public void FromBytes(byte[] packet)
        {
            using (BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
            {
                Header = bin.ReadBytes(0x04);
                Message = Encoding.UTF8.GetString(bin.ReadBytes(0xE));
                BlankByte = bin.ReadByte();
            }

            RawBytes = packet;
        }

        public byte[] GetRawData()
        {
            return RawBytes;
        }

        public int GetSize()
        {
            return 0x13;
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
                bin.Write(Header);
                bin.Write(Utils.NameToBytes(Message, 0xE));
                bin.Write(0x00);
            }

            return stream.ToArray();
        }
    }
}
