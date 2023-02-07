/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class fattr4_fs_layout_types : XdrAble
    {
        public int[] value;

        public fattr4_fs_layout_types()
        {
        }

        public fattr4_fs_layout_types(int[] value)
        {
            this.value = value;
        }

        public fattr4_fs_layout_types(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeIntVector(value);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            value = xdr.xdrDecodeIntVector();
        }
    }
} // End of  fattr4_fs_layout_types.cs