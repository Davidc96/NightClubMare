/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class fattr4_case_insensitive : XdrAble
    {
        public bool value;

        public fattr4_case_insensitive()
        {
        }

        public fattr4_case_insensitive(bool value)
        {
            this.value = value;
        }

        public fattr4_case_insensitive(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeBoolean(value);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            value = xdr.xdrDecodeBoolean();
        }
    }
}

// End of fattr4_case_insensitive.cs