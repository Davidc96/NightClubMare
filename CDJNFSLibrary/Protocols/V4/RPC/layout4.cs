/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class layout4 : XdrAble
    {
        public offset4 lo_offset;
        public length4 lo_length;
        public int lo_iomode;
        public layout_content4 lo_content;

        public layout4()
        {
        }

        public layout4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            lo_length.xdrEncode(xdr);
            xdr.xdrEncodeInt(lo_iomode);
            lo_content.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            lo_length = new length4(xdr);
            lo_iomode = xdr.xdrDecodeInt();
            lo_content = new layout_content4(xdr);
        }
    }
} // End of layout4.cs