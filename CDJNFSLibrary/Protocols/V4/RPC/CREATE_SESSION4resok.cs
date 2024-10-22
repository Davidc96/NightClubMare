/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class CREATE_SESSION4resok : XdrAble
    {
        public sessionid4 csr_sessionid;
        public sequenceid4 csr_sequence;
        public uint32_t csr_flags;
        public channel_attrs4 csr_fore_chan_attrs;
        public channel_attrs4 csr_back_chan_attrs;

        public CREATE_SESSION4resok()
        {
        }

        public CREATE_SESSION4resok(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            csr_sessionid.xdrEncode(xdr);
            csr_sequence.xdrEncode(xdr);
            csr_flags.xdrEncode(xdr);
            csr_fore_chan_attrs.xdrEncode(xdr);
            csr_back_chan_attrs.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            csr_sessionid = new sessionid4(xdr);
            csr_sequence = new sequenceid4(xdr);
            csr_flags = new uint32_t(xdr);
            csr_fore_chan_attrs = new channel_attrs4(xdr);
            csr_back_chan_attrs = new channel_attrs4(xdr);
        }
    }
} // End of CREATE_SESSION4resok.cs