/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class fs_locations4 : XdrAble
    {
        public pathname4 fs_root;
        public fs_location4[] locations;

        public fs_locations4()
        {
        }

        public fs_locations4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            { int _size = locations.Length; xdr.xdrEncodeInt(_size); for (int _idx = 0; _idx < _size; ++_idx) { locations[_idx].xdrEncode(xdr); } }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            { int _size = xdr.xdrDecodeInt(); locations = new fs_location4[_size]; for (int _idx = 0; _idx < _size; ++_idx) { locations[_idx] = new fs_location4(xdr); } }
        }
    }
} // End of  fs_locations4.cs