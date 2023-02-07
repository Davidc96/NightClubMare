/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class PUTFH4args : XdrAble
    {
        public nfs_fh4 object1;

        public PUTFH4args()
        {
        }

        public PUTFH4args(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            object1.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            object1 = new nfs_fh4(xdr);
        }
    }
} // End of PUTFH4args.cs