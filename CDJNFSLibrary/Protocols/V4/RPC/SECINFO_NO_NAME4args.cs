/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class SECINFO_NO_NAME4args : XdrAble
    {
        public int value;

        public SECINFO_NO_NAME4args()
        {
        }

        public SECINFO_NO_NAME4args(int value)
        {
            this.value = value;
        }

        public SECINFO_NO_NAME4args(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt(value);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            value = xdr.xdrDecodeInt();
        }
    }
} // End of SECINFO_NO_NAME4args.cs