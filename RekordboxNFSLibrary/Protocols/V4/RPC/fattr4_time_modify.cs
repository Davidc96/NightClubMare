/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class fattr4_time_modify : XdrAble
    {
        public nfstime4 value;

        public fattr4_time_modify()
        {
        }

        public fattr4_time_modify(nfstime4 value)
        {
            this.value = value;
        }

        public fattr4_time_modify(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            value.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            value = new nfstime4(xdr);
        }
    }
} // End of  fattr4_time_modify.cs