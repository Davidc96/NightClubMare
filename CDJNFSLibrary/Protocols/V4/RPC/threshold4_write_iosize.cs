/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class threshold4_write_iosize : XdrAble
    {
        public length4 value;

        public threshold4_write_iosize()
        {
        }

        public threshold4_write_iosize(length4 value)
        {
            this.value = value;
        }

        public threshold4_write_iosize(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            value.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            value = new length4(xdr);
        }
    }
} // End of threshold4_write_iosize.cs