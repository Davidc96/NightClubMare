/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC.Callback
{
    using org.acplt.oncrpc;

    public class notify4 : XdrAble
    {
        public bitmap4 notify_mask;
        public notifylist4 notify_vals;

        public notify4()
        {
        }

        public notify4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            notify_vals.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            notify_vals = new notifylist4(xdr);
        }
    }
} // End of notify4.cs