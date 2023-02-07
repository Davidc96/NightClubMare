/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class state_protect_ops4 : XdrAble
    {
        public bitmap4 spo_must_enforce;
        public bitmap4 spo_must_allow;

        public state_protect_ops4()
        {
        }

        public state_protect_ops4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            spo_must_allow.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            spo_must_allow = new bitmap4(xdr);
        }
    }
} // End of state_protect_ops4.cs