/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class dirlist4 : XdrAble
    {
        public entry4 entries;
        public bool eof;

        public dirlist4()
        {
        }

        public dirlist4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            if (entries != null) { xdr.xdrEncodeBoolean(true); entries.xdrEncode(xdr); } else { xdr.xdrEncodeBoolean(false); };
            xdr.xdrEncodeBoolean(eof);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            entries = xdr.xdrDecodeBoolean() ? new entry4(xdr) : null;
            eof = xdr.xdrDecodeBoolean();
        }
    }
} // End of  dirlist4.cs