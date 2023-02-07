/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC.Callback
{
    using org.acplt.oncrpc;

    public class notify_deviceid_change4 : XdrAble
    {
        public layouttype4 ndc_layouttype;
        public deviceid4 ndc_deviceid;
        public bool ndc_immediate;

        public notify_deviceid_change4()
        {
        }

        public notify_deviceid_change4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            ndc_deviceid.xdrEncode(xdr);
            xdr.xdrEncodeBoolean(ndc_immediate);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            ndc_deviceid = new deviceid4(xdr);
            ndc_immediate = xdr.xdrDecodeBoolean();
        }
    }
} // End of notify_deviceid_change4.cs