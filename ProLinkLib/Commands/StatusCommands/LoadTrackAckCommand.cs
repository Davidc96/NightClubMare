using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.StatusCommands
{
    public class LoadTrackAckCommand : ICommand
    {
        public byte ID = 0x1A;

        private byte[] rawData;
        public void FromBytes(byte[] packet)
        {
            rawData = packet;
            Console.WriteLine("LoadTrackACK Packet:\n");
            Console.WriteLine(Hex.Dump(rawData));
        }

        public byte[] GetRawData()
        {
            return rawData;
        }

        public int GetSize()
        {
            throw new NotImplementedException();
        }

        public void PrintCommand()
        {
            throw new NotImplementedException();
        }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }
}
