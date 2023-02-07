/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class nfs_modified_limit4 : XdrAble
    {
        public int num_blocks;
        public int bytes_per_block;

        public nfs_modified_limit4()
        {
        }

        public nfs_modified_limit4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt(bytes_per_block);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            bytes_per_block = xdr.xdrDecodeInt();
        }
    }
} // End of nfs_modified_limit4.cs