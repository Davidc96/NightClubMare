/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class device_addr4 : XdrAble
    {
        public int da_layout_type;
        public byte[] da_addr_body;

        public device_addr4()
        {
        }

        public device_addr4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeDynamicOpaque(da_addr_body);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            da_addr_body = xdr.xdrDecodeDynamicOpaque();
        }
    }
} // End of  device_addr4.cs