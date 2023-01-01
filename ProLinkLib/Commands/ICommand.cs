using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands
{
    public interface ICommand
    {
        void FromBytes(byte[] packet);
        byte[] ToBytes();
        void PrintCommand();
        int GetSize();

        byte[] GetRawData();
    }
}
