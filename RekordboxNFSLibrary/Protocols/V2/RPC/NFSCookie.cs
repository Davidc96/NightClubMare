/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using org.acplt.oncrpc;

namespace RekordboxNFSLibrary.Protocols.V2.RPC
{
    public class NFSCookie : XdrAble
    {
        private int _value;

        public NFSCookie()
        { }

        public NFSCookie(int value)
        { this._value = value; }

        public NFSCookie(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        { xdr.xdrEncodeInt(this._value); }

        public void xdrDecode(XdrDecodingStream xdr)
        { this._value = xdr.xdrDecodeInt(); }

        public int Value
        {
            get
            { return this._value; }
        }
    }

    // End of nfscookie.cs
}