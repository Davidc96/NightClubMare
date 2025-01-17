/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class SET_SSV4args : XdrAble
    {
        public byte[] ssa_ssv;
        public byte[] ssa_digest;

        public SET_SSV4args()
        {
        }

        public SET_SSV4args(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeDynamicOpaque(ssa_digest);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            ssa_digest = xdr.xdrDecodeDynamicOpaque();
        }
    }
} // End of SET_SSV4args.cs