/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC.Callback
{
    using org.acplt.oncrpc;

    public class referring_call4 : XdrAble
    {
        public sequenceid4 rc_sequenceid;
        public slotid4 rc_slotid;

        public referring_call4()
        {
        }

        public referring_call4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            rc_slotid.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            rc_slotid = new slotid4(xdr);
        }
    }
} // End of referring_call4.cs