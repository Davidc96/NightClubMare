/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class layout_content4 : XdrAble
    {
        public int loc_type;
        public byte[] loc_body;

        public layout_content4()
        {
        }

        public layout_content4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeDynamicOpaque(loc_body);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            loc_body = xdr.xdrDecodeDynamicOpaque();
        }
    }
} // End of layout_content4.cs