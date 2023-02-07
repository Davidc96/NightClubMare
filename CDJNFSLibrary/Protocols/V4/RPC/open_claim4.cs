/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class open_claim4 : XdrAble
    {
        public int claim;
        public component4 file;
        public int delegate_type;
        public open_claim_delegate_cur4 delegate_cur_info;
        public component4 file_delegate_prev;
        public stateid4 oc_delegate_stateid;

        public open_claim4()
        {
        }

        public open_claim4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt(claim);
            switch (claim)
            {
                case open_claim_type4.CLAIM_NULL:
                    file.xdrEncode(xdr);
                    break;

                case open_claim_type4.CLAIM_PREVIOUS:
                    xdr.xdrEncodeInt(delegate_type);
                    break;

                case open_claim_type4.CLAIM_DELEGATE_CUR:
                    delegate_cur_info.xdrEncode(xdr);
                    break;

                case open_claim_type4.CLAIM_DELEGATE_PREV:
                    file_delegate_prev.xdrEncode(xdr);
                    break;

                case open_claim_type4.CLAIM_FH:
                    break;

                case open_claim_type4.CLAIM_DELEG_PREV_FH:
                    break;

                case open_claim_type4.CLAIM_DELEG_CUR_FH:
                    oc_delegate_stateid.xdrEncode(xdr);
                    break;
            }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            claim = xdr.xdrDecodeInt();
            switch (claim)
            {
                case open_claim_type4.CLAIM_NULL:
                    file = new component4(xdr);
                    break;

                case open_claim_type4.CLAIM_PREVIOUS:
                    delegate_type = xdr.xdrDecodeInt();
                    break;

                case open_claim_type4.CLAIM_DELEGATE_CUR:
                    delegate_cur_info = new open_claim_delegate_cur4(xdr);
                    break;

                case open_claim_type4.CLAIM_DELEGATE_PREV:
                    file_delegate_prev = new component4(xdr);
                    break;

                case open_claim_type4.CLAIM_FH:
                    break;

                case open_claim_type4.CLAIM_DELEG_PREV_FH:
                    break;

                case open_claim_type4.CLAIM_DELEG_CUR_FH:
                    oc_delegate_stateid = new stateid4(xdr);
                    break;
            }
        }
    }
} // End of open_claim4.cs