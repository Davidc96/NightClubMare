/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class fattr4_retentevt_get : XdrAble
    {
        public retention_get4 value;

        public fattr4_retentevt_get()
        {
        }

        public fattr4_retentevt_get(retention_get4 value)
        {
            this.value = value;
        }

        public fattr4_retentevt_get(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            value.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            value = new retention_get4(xdr);
        }
    }
}

// End of fattr4_retentevt_get.cs