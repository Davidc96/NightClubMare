/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using RekordboxNFSLibrary.Protocols.Commons;
using org.acplt.oncrpc;

namespace RekordboxNFSLibrary.Protocols.V3.RPC
{
    public class WritingAttributes : XdrAble
    {
        private long _size;
        private NFSTimeValue _mtime;
        private NFSTimeValue _ctime;

        public WritingAttributes()
        { }

        public WritingAttributes(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeLong(this._size);
            this._mtime.xdrEncode(xdr);
            this._ctime.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            this._size = xdr.xdrDecodeLong();
            this._mtime = new NFSTimeValue(xdr);
            this._ctime = new NFSTimeValue(xdr);
        }

        public long Size
        {
            get
            { return this._size; }
        }

        public NFSTimeValue ModifiedTime
        {
            get
            { return this._mtime; }
        }

        public NFSTimeValue CreateTime
        {
            get
            { return this._ctime; }
        }
    }

    // End of wcc_attr.cs
}