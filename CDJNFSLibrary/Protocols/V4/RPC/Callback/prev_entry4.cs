/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC.Callback
{
    using org.acplt.oncrpc;

    public class prev_entry4 : XdrAble
    {
        public notify_entry4 pe_prev_entry;
        public nfs_cookie4 pe_prev_entry_cookie;

        public prev_entry4()
        {
        }

        public prev_entry4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            pe_prev_entry_cookie.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            pe_prev_entry_cookie = new nfs_cookie4(xdr);
        }
    }
} // End of prev_entry4.cs