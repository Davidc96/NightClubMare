/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC.Callback
{
    using org.acplt.oncrpc;

    public class CB_RECALLABLE_OBJ_AVAIL4res : XdrAble
    {
        public nfsstat4 croa_status;

        public CB_RECALLABLE_OBJ_AVAIL4res()
        {
        }

        public CB_RECALLABLE_OBJ_AVAIL4res(XdrDecodingStream xdr)
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
} // End of CB_RECALLABLE_OBJ_AVAIL4res.cs