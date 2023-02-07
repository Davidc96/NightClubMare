/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class DESTROY_SESSION4args : XdrAble
    {
        public sessionid4 dsa_sessionid;

        public DESTROY_SESSION4args()
        {
        }

        public DESTROY_SESSION4args(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            dsa_sessionid.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            dsa_sessionid = new sessionid4(xdr);
        }
    }
} // End of  DESTROY_SESSION4args.cs