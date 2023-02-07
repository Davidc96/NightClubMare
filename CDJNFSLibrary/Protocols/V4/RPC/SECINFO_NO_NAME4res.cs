/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class SECINFO_NO_NAME4res : XdrAble
    {
        public SECINFO4res value;

        public SECINFO_NO_NAME4res()
        {
        }

        public SECINFO_NO_NAME4res(SECINFO4res value)
        {
            this.value = value;
        }

        public SECINFO_NO_NAME4res(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            value.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            value = new SECINFO4res(xdr);
        }
    }
} // End of SECINFO_NO_NAME4res.cs