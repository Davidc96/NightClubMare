/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class DELEGRETURN4args : XdrAble
    {
        public stateid4 deleg_stateid;

        public DELEGRETURN4args()
        {
        }

        public DELEGRETURN4args(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
        }
    }
} // End of  DELEGRETURN4args.cs