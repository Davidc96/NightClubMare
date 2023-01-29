using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.MetadataCommands
{
    public class MessageStructure : ICommand
    {
        public uint TXID = 0xA0A0A0A0;
        byte Unknown1 = 0x10;
        public ushort Type = 0xA0A0;
        public byte FieldType = 0xA1;                       // 0x0F -> Length field 1, 0x10 -> Length field 2, 0x11 -> Length field 4 Normally 0x0F
        public byte NumberOfFields = 0xA2;                  // Set the number of fields in the Argument Section
        byte Tag = 0x14;
        byte[] UnknownBytes = { 0x00, 0x00, 0x00, 0x0c };
        public byte[] ArgumentInformation = { 0xA3, 0xA3, 0xA3, 0xA3, 0xA3, 0xA3,   // Set the size of each argument up to 12 arguments.
                                              0xA3, 0xA3, 0xA3, 0xA3, 0xA3, 0xA3 }; // 0x02 -> UTF16 Big Endian String, 0x03 -> Binary Blob, 0x06 -> 4 Byte big-endian integer
                                                                                    // This works with only the arguments specified in Number of fields, Only affected by responses
        public byte[] Arguments;

        private byte[] RawBytes;

        public void FromBytes(byte[] packet)
        {
            using (BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
            {
                bin.BaseStream.Seek(0x06, SeekOrigin.Begin);
                TXID = bin.ReadUInt32();
                Unknown1 = bin.ReadByte();
                Type = bin.ReadUInt16();
                FieldType = bin.ReadByte();
                NumberOfFields = bin.ReadByte();
                Tag = bin.ReadByte();
                UnknownBytes = bin.ReadBytes(0x04);
                ArgumentInformation = bin.ReadBytes(0x0C);
            }

            RawBytes = packet;
        }

        public byte[] ToBytes()
        {
            MemoryStream stream = new MemoryStream();
            using (BinaryWriter bin = new BinaryWriter(stream))
            {
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(TXID)));
                bin.Write(Unknown1);
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(Type)));
                bin.Write(FieldType);
                bin.Write(NumberOfFields);
                bin.Write(Tag);
                bin.Write(UnknownBytes);
                bin.Write(ArgumentInformation);
                
                if(Arguments != null)
                    bin.Write(Arguments);
            }

            return stream.ToArray();
        }

        public void PrintCommand()
        {
            throw new NotImplementedException();
        }

        public int GetSize()
        {
            return RawBytes.Length;
        }

        public byte[] GetRawData()
        {
            return RawBytes;
        }
    }
}
