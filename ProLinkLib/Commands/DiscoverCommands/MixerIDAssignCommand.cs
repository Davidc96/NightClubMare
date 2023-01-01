using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.DiscoverCommands
{
    public class MixerIDAssignCommand : ICommand
    {
        public byte ID = 0x03;                                 // 0x0A
        public byte ToMixer = 0x01;                            // 0x0B
        public byte[] DeviceName = new byte[0x14];             // 0x0C - 0x1F
        public byte Unknown = 0x01;                            // 0x20
        public byte Unknown2 = 0x02;                           // 0x21
        public ushort Length = 39;                             // 0x22 - 0x23
        public byte ChannelID;                                 // 0x24
        public byte PacketCounter;                             // 0x25
        public byte ZeroValue = 0x00;                          // 0x26

        public byte[] RawData;
        public void FromBytes(byte[] packet)
        {
            using (BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
            {
                bin.BaseStream.Seek(0x0B, SeekOrigin.Begin);
                ToMixer = bin.ReadByte();
                DeviceName = bin.ReadBytes(0x14);
                Unknown = bin.ReadByte();
                Unknown2 = bin.ReadByte();
                Length = BitConverter.ToUInt16(Utils.SwapEndianesss(bin.ReadBytes(2)), 0);
                ChannelID = bin.ReadByte();
                PacketCounter = bin.ReadByte();
                ZeroValue = bin.ReadByte();
            }

            RawData = packet;
        }

        public void PrintCommand()
        {
            Console.WriteLine("Packet Payload");
            Console.WriteLine("ToMixer: " + $"0x{ToMixer:X}");
            Console.WriteLine("DeviceName: " + Encoding.UTF8.GetString(DeviceName));
            Console.WriteLine("Unknown: " + $"0x{Unknown:X}");
            Console.WriteLine("Unknown2: " + $"0x{Unknown2:X}");
            Console.WriteLine("Length: " + Length);
            Console.WriteLine("ChannelID: " + ChannelID);
            Console.WriteLine("PacketCounter: " + PacketCounter);
        }

        public byte[] ToBytes()
        {
            MemoryStream stream = new MemoryStream();
            using (BinaryWriter bin = new BinaryWriter(stream))
            {
                bin.Write(ID);
                bin.Write(ToMixer);
                bin.Write(DeviceName);
                bin.Write(Unknown);
                bin.Write(Unknown2);
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(Length)));
                bin.Write(ChannelID);
                bin.Write(PacketCounter);
                bin.Write(ZeroValue);
            }

            return stream.ToArray();
        }
        public int GetSize()
        {
            return Length;
        }
        public byte[] GetRawData()
        {
            return RawData;
        }
    }
}
