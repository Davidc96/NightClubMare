/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class SETATTR4args : XdrAble
    {
        public stateid4 stateid;
        public fattr4 obj_attributes;

        public SETATTR4args()
        {
        }

        public SETATTR4args(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            obj_attributes.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            obj_attributes = new fattr4(xdr);
        }
    }
} // End of SETATTR4args.cs