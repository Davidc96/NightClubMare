/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class SECINFO4resok : XdrAble
    {
        public secinfo4[] value;

        public SECINFO4resok()
        {
        }

        public SECINFO4resok(secinfo4[] value)
        {
            this.value = value;
        }

        public SECINFO4resok(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            { int _size = value.Length; xdr.xdrEncodeInt(_size); for (int _idx = 0; _idx < _size; ++_idx) { value[_idx].xdrEncode(xdr); } }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            { int _size = xdr.xdrDecodeInt(); value = new secinfo4[_size]; for (int _idx = 0; _idx < _size; ++_idx) { value[_idx] = new secinfo4(xdr); } }
        }
    }
} // End of SECINFO4resok.cs