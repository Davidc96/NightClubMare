/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class fattr4_acl : XdrAble
    {
        public nfsace4[] value;

        public fattr4_acl()
        {
        }

        public fattr4_acl(nfsace4[] value)
        {
            this.value = value;
        }

        public fattr4_acl(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            { int _size = value.Length; xdr.xdrEncodeInt(_size); for (int _idx = 0; _idx < _size; ++_idx) { value[_idx].xdrEncode(xdr); } }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            { int _size = xdr.xdrDecodeInt(); value = new nfsace4[_size]; for (int _idx = 0; _idx < _size; ++_idx) { value[_idx] = new nfsace4(xdr); } }
        }
    }
}

// End of fattr4_acl.cs