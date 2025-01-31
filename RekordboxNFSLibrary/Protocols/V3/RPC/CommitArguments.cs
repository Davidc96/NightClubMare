/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using RekordboxNFSLibrary.Protocols.Commons;
using org.acplt.oncrpc;

namespace RekordboxNFSLibrary.Protocols.V3.RPC
{
    public class CommitArguments : XdrAble
    {
        private NFSHandle _file;
        private long _offset;
        private int _count;

        public CommitArguments()
        { }

        public CommitArguments(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            this._file.xdrEncode(xdr);
            xdr.xdrEncodeLong(this._offset);
            xdr.xdrEncodeInt(this._count);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            this._file = new NFSHandle();
            this._file.Version = V3.RPC.NFSv3Protocol.NFS_V3;
            this._file.xdrDecode(xdr);
            this._offset = xdr.xdrDecodeLong();
            this._count = xdr.xdrDecodeInt();
        }

        public NFSHandle File
        {
            get
            { return this._file; }
            set
            { this._file = value; }
        }

        public long Offset
        {
            get
            { return this._offset; }
            set
            { this._offset = value; }
        }

        public int Count
        {
            get
            { return this._count; }
            set
            { this._count = value; }
        }
    }

    // End of COMMIT3args.cs
}