using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.MetadataCommands.Messages
{
    public class QuerySetupMessage : MessageStructure
    {
        public void ParseBytes(byte[] packet)
        {
            this.FromBytes(packet);
        }

        public byte[] CreateBytes()
        {
            this.Type = 0x0100;
            this.FieldType = 0x0F;
            this.NumberOfFields = 0x00;
            this.ArgumentInformation = new byte[0xC];
            this.Arguments = null;

            return this.ToBytes();
        }
    }
}
