/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class ssv_mic_plain_tkn4 : XdrAble
    {
        public int smpt_ssv_seq;
        public byte[] smpt_orig_plain;

        public ssv_mic_plain_tkn4()
        {
        }

        public ssv_mic_plain_tkn4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeDynamicOpaque(smpt_orig_plain);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            smpt_orig_plain = xdr.xdrDecodeDynamicOpaque();
        }
    }
} // End of ssv_mic_plain_tkn4.cs