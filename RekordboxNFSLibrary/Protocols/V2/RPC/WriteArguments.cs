/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using RekordboxNFSLibrary.Protocols.Commons;
using org.acplt.oncrpc;

namespace RekordboxNFSLibrary.Protocols.V2.RPC
{
    public class WriteArguments : XdrAble
    {
        private NFSHandle _file;
        private int _beginoffset;
        private int _offset;
        private int _totalcount;
        private byte[] _data;

        public WriteArguments()
        { }

        public WriteArguments(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            this._file.xdrEncode(xdr);
            xdr.xdrEncodeInt(this._beginoffset);
            xdr.xdrEncodeInt(this._offset);
            xdr.xdrEncodeInt(this._totalcount);
            xdr.xdrEncodeDynamicOpaque(this._data);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            this._file = new NFSHandle();
            this._file.Version = V2.RPC.NFSv2Protocol.NFS_VERSION;
            this._file.xdrDecode(xdr);
            this._beginoffset = xdr.xdrDecodeInt();
            this._offset = xdr.xdrDecodeInt();
            this._totalcount = xdr.xdrDecodeInt();
            this._data = xdr.xdrDecodeDynamicOpaque();
        }

        public NFSHandle File
        {
            get
            { return this._file; }
            set
            { this._file = value; }
        }

        public int Offset
        {
            get
            { return this._offset; }
            set
            { this._offset = value; }
        }

        public int BeginOffset
        {
            get
            { return this._beginoffset; }
            set
            { this._beginoffset = value; }
        }

        public int TotalCount
        {
            get
            { return this._totalcount; }
            set
            { this._totalcount = value; }
        }

        public byte[] Data
        {
            get
            { return this._data; }
            set
            { this._data = value; }
        }
    }

    // End of writeargs.cs
}