/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class fs_location4 : XdrAble
    {
        public utf8str_cis[] server;
        public pathname4 rootpath;

        public fs_location4()
        {
        }

        public fs_location4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            rootpath.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            rootpath = new pathname4(xdr);
        }
    }
} // End of  fs_location4.cs