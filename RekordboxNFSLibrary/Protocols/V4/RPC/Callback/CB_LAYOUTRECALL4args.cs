/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC.Callback
{
    using org.acplt.oncrpc;

    public class CB_LAYOUTRECALL4args : XdrAble
    {
        public layouttype4 clora_type;
        public int clora_iomode;
        public bool clora_changed;
        public layoutrecall4 clora_recall;

        public CB_LAYOUTRECALL4args()
        {
        }

        public CB_LAYOUTRECALL4args(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt(clora_iomode);
            xdr.xdrEncodeBoolean(clora_changed);
            clora_recall.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            clora_iomode = xdr.xdrDecodeInt();
            clora_changed = xdr.xdrDecodeBoolean();
            clora_recall = new layoutrecall4(xdr);
        }
    }
} // End of CB_LAYOUTRECALL4args.cs