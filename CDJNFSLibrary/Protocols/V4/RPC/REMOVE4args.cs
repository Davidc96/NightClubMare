/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class REMOVE4args : XdrAble
    {
        public component4 target;

        public REMOVE4args()
        {
        }

        public REMOVE4args(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            target.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            target = new component4(xdr);
        }
    }
} // End of REMOVE4args.cs