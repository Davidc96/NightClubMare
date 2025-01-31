/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class fattr4 : XdrAble
    {
        public bitmap4 attrmask;
        public attrlist4 attr_vals;

        public fattr4()
        {
        }

        public fattr4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            attrmask.xdrEncode(xdr);
            attr_vals.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            attrmask = new bitmap4(xdr);
            attr_vals = new attrlist4(xdr);
        }
    }
}

// End of fattr4.cs