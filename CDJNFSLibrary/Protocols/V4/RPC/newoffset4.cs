/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class newoffset4 : XdrAble
    {
        public bool no_newoffset;
        public offset4 no_offset;

        public newoffset4()
        {
        }

        public newoffset4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeBoolean(no_newoffset);
            if (no_newoffset == true)
            {
                no_offset.xdrEncode(xdr);
            }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            no_newoffset = xdr.xdrDecodeBoolean();
            if (no_newoffset == true)
            {
                no_offset = new offset4(xdr);
            }
        }
    }
} // End of newoffset4.cs