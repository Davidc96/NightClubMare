/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using RekordboxNFSLibrary.Protocols.Commons;
using org.acplt.oncrpc;

namespace RekordboxNFSLibrary.Protocols.V2.RPC
{
    public class FileStatus : XdrAble
    {
        private NFSStats _status;
        private FileAttributes _attributes;

        public FileStatus()
        { }

        public FileStatus(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt((int)this._status);

            switch (this._status)
            {
                case NFSStats.NFS_OK:
                    this._attributes.xdrEncode(xdr);
                    break;

                default:
                    break;
            }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            this._status = (NFSStats)xdr.xdrDecodeInt();

            switch (this._status)
            {
                case NFSStats.NFS_OK:
                    this._attributes = new FileAttributes(xdr);
                    break;

                default:
                    break;
            }
        }

        public NFSStats Status
        {
            get
            { return this._status; }
        }

        public FileAttributes Attributes
        {
            get
            { return this._attributes; }
        }
    }

    // End of attrstat.cs
}