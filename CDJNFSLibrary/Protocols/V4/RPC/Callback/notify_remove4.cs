/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC.Callback
{
    using org.acplt.oncrpc;

    public class notify_remove4 : XdrAble
    {
        public notify_entry4 nrm_old_entry;
        public nfs_cookie4 nrm_old_entry_cookie;

        public notify_remove4()
        {
        }

        public notify_remove4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            nrm_old_entry_cookie.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            nrm_old_entry_cookie = new nfs_cookie4(xdr);
        }
    }
} // End of notify_remove4.cs