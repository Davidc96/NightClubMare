/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class LOOKUP4args : XdrAble
    {
        public component4 objname;

        public LOOKUP4args()
        {
        }

        public LOOKUP4args(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            objname.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            objname = new component4(xdr);
        }
    }
} // End of LOOKUP4args.cs