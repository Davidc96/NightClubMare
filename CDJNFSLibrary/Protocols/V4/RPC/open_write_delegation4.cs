/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class open_write_delegation4 : XdrAble
    {
        public stateid4 stateid;
        public bool recall;
        public nfs_space_limit4 space_limit;
        public nfsace4 permissions;

        public open_write_delegation4()
        {
        }

        public open_write_delegation4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeBoolean(recall);
            space_limit.xdrEncode(xdr);
            permissions.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            recall = xdr.xdrDecodeBoolean();
            space_limit = new nfs_space_limit4(xdr);
            permissions = new nfsace4(xdr);
        }
    }
} // End of open_write_delegation4.cs