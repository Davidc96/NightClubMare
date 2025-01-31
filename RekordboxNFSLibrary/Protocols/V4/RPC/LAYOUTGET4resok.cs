/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class LAYOUTGET4resok : XdrAble
    {
        public bool logr_return_on_close;
        public stateid4 logr_stateid;
        public layout4[] logr_layout;

        public LAYOUTGET4resok()
        {
        }

        public LAYOUTGET4resok(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            logr_stateid.xdrEncode(xdr);
            { int _size = logr_layout.Length; xdr.xdrEncodeInt(_size); for (int _idx = 0; _idx < _size; ++_idx) { logr_layout[_idx].xdrEncode(xdr); } }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            logr_stateid = new stateid4(xdr);
            { int _size = xdr.xdrDecodeInt(); logr_layout = new layout4[_size]; for (int _idx = 0; _idx < _size; ++_idx) { logr_layout[_idx] = new layout4(xdr); } }
        }
    }
} // End of LAYOUTGET4resok.cs