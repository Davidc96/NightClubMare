/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC.Callback
{
    using org.acplt.oncrpc;

    public class notify_add4 : XdrAble
    {
        public notify_remove4[] nad_old_entry;
        public notify_entry4 nad_new_entry;
        public nfs_cookie4[] nad_new_entry_cookie;
        public prev_entry4[] nad_prev_entry;
        public bool nad_last_entry;

        public notify_add4()
        {
        }

        public notify_add4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            nad_new_entry.xdrEncode(xdr);
            { int _size = nad_new_entry_cookie.Length; xdr.xdrEncodeInt(_size); for (int _idx = 0; _idx < _size; ++_idx) { nad_new_entry_cookie[_idx].xdrEncode(xdr); } }
            { int _size = nad_prev_entry.Length; xdr.xdrEncodeInt(_size); for (int _idx = 0; _idx < _size; ++_idx) { nad_prev_entry[_idx].xdrEncode(xdr); } }
            xdr.xdrEncodeBoolean(nad_last_entry);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            nad_new_entry = new notify_entry4(xdr);
            { int _size = xdr.xdrDecodeInt(); nad_new_entry_cookie = new nfs_cookie4[_size]; for (int _idx = 0; _idx < _size; ++_idx) { nad_new_entry_cookie[_idx] = new nfs_cookie4(xdr); } }
            { int _size = xdr.xdrDecodeInt(); nad_prev_entry = new prev_entry4[_size]; for (int _idx = 0; _idx < _size; ++_idx) { nad_prev_entry[_idx] = new prev_entry4(xdr); } }
            nad_last_entry = xdr.xdrDecodeBoolean();
        }
    }
} // End of notify_add4.cs