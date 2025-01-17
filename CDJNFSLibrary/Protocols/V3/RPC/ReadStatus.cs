/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using org.acplt.oncrpc;

namespace CDJNFSLibrary.Protocols.V3.RPC
{
    public class ReadAccessOK : XdrAble
    {
        private PostOperationAttributes _attributes;
        private int _count;
        private bool _eof;
        private byte[] _data;

        public ReadAccessOK()
        { }

        public ReadAccessOK(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            this._attributes.xdrEncode(xdr);
            xdr.xdrEncodeInt(this._count);
            xdr.xdrEncodeBoolean(this._eof);
            xdr.xdrEncodeDynamicOpaque(this._data);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            this._attributes = new PostOperationAttributes(xdr);
            this._count = xdr.xdrDecodeInt();
            this._eof = xdr.xdrDecodeBoolean();
            this._data = xdr.xdrDecodeDynamicOpaque();
        }

        public PostOperationAttributes Attributes
        {
            get
            { return this._attributes; }
        }

        public int Count
        {
            get
            { return this._count; }
        }

        public bool EOF
        {
            get
            { return this._eof; }
        }

        public byte[] Data
        {
            get
            { return this._data; }
        }
    }

    public class ReadAccessFAIL : XdrAble
    {
        private PostOperationAttributes _attributes;

        public ReadAccessFAIL()
        { }

        public ReadAccessFAIL(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        { this._attributes.xdrEncode(xdr); }

        public void xdrDecode(XdrDecodingStream xdr)
        { this._attributes = new PostOperationAttributes(xdr); }

        public PostOperationAttributes Attributes
        {
            get
            { return this._attributes; }
        }
    }

    // End of readres.cs
}