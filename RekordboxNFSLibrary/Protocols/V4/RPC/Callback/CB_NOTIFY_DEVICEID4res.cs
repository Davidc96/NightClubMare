/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC.Callback
{
    using org.acplt.oncrpc;

    public class CB_NOTIFY_DEVICEID4res : XdrAble
    {
        public nfsstat4 cndr_status;

        public CB_NOTIFY_DEVICEID4res()
        {
        }

        public CB_NOTIFY_DEVICEID4res(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
        }
    }
} // End of CB_NOTIFY_DEVICEID4res.cs