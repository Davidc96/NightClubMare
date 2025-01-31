/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class creatverfattr : XdrAble
    {
        public verifier4 cva_verf;
        public fattr4 cva_attrs;

        public creatverfattr()
        {
        }

        public creatverfattr(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            cva_verf.xdrEncode(xdr);
            cva_attrs.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            cva_verf = new verifier4(xdr);
            cva_attrs = new fattr4(xdr);
        }
    }
} // End of creatverfattr.cs