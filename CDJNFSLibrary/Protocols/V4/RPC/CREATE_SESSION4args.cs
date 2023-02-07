/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class CREATE_SESSION4args : XdrAble
    {
        public clientid4 csa_clientid;
        public sequenceid4 csa_sequence;
        public uint32_t csa_flags;
        public channel_attrs4 csa_fore_chan_attrs;
        public channel_attrs4 csa_back_chan_attrs;
        public uint32_t csa_cb_program;
        public callback_sec_parms4[] csa_sec_parms;

        public CREATE_SESSION4args()
        {
        }

        public CREATE_SESSION4args(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            csa_clientid.xdrEncode(xdr);
            csa_sequence.xdrEncode(xdr);
            csa_flags.xdrEncode(xdr);
            csa_fore_chan_attrs.xdrEncode(xdr);
            csa_back_chan_attrs.xdrEncode(xdr);
            csa_cb_program.xdrEncode(xdr);
            { int size = csa_sec_parms.Length; xdr.xdrEncodeInt(size); for (int idx = 0; idx < size; ++idx) { csa_sec_parms[idx].xdrEncode(xdr); } }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            csa_clientid = new clientid4(xdr);
            csa_sequence = new sequenceid4(xdr);
            csa_flags = new uint32_t(xdr);
            csa_fore_chan_attrs = new channel_attrs4(xdr);
            csa_back_chan_attrs = new channel_attrs4(xdr);
            csa_cb_program = new uint32_t(xdr);
            { int size = xdr.xdrDecodeInt(); csa_sec_parms = new callback_sec_parms4[size]; for (int idx = 0; idx < size; ++idx) { csa_sec_parms[idx] = new callback_sec_parms4(xdr); } }
        }
    }
} // End of CREATE_SESSION4args.cs