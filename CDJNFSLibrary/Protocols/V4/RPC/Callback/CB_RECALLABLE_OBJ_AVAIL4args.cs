/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC.Callback
{
    using org.acplt.oncrpc;

    public class CB_RECALLABLE_OBJ_AVAIL4args : XdrAble
    {
        public CB_RECALL_ANY4args value;

        public CB_RECALLABLE_OBJ_AVAIL4args()
        {
        }

        public CB_RECALLABLE_OBJ_AVAIL4args(CB_RECALL_ANY4args value)
        {
            this.value = value;
        }

        public CB_RECALLABLE_OBJ_AVAIL4args(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            value.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            value = new CB_RECALL_ANY4args(xdr);
        }
    }
} // End of CB_RECALLABLE_OBJ_AVAIL4args.cs