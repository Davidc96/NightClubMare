/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class GETDEVICELIST4args : XdrAble
    {
        public int gdla_layout_type;
        public count4 gdla_maxdevices;
        public nfs_cookie4 gdla_cookie;
        public verifier4 gdla_cookieverf;

        public GETDEVICELIST4args()
        {
        }

        public GETDEVICELIST4args(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            gdla_maxdevices.xdrEncode(xdr);
            gdla_cookie.xdrEncode(xdr);
            gdla_cookieverf.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            gdla_maxdevices = new count4(xdr);
            gdla_cookie = new nfs_cookie4(xdr);
            gdla_cookieverf = new verifier4(xdr);
        }
    }
} // End of  GETDEVICELIST4args.cs