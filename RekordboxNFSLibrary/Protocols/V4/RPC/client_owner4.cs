/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class client_owner4 : XdrAble
    {
        public verifier4 co_verifier;
        public byte[] co_ownerid;

        public client_owner4()
        {
        }

        public client_owner4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            co_verifier.xdrEncode(xdr);
            xdr.xdrEncodeDynamicOpaque(co_ownerid);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            co_verifier = new verifier4(xdr);
            co_ownerid = xdr.xdrDecodeDynamicOpaque();
        }
    }
}

// End of client_owner4.cs