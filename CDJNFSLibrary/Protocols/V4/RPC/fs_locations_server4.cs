/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class fs_locations_server4 : XdrAble
    {
        public int fls_currency;
        public byte[] fls_info;
        public utf8str_cis fls_server;

        public fs_locations_server4()
        {
        }

        public fs_locations_server4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeDynamicOpaque(fls_info);
            fls_server.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            fls_info = xdr.xdrDecodeDynamicOpaque();
            fls_server = new utf8str_cis(xdr);
        }
    }
} // End of  fs_locations_server4.cs