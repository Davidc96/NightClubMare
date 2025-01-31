/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using CDJNFSLibrary.Protocols.Commons;
using org.acplt.oncrpc;

namespace CDJNFSLibrary.Protocols.V2.RPC
{
    public class LinkStatus : XdrAble
    {
        private NFSStats _status;
        private Name _linkname;

        public LinkStatus()
        { }

        public LinkStatus(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt((int)this._status);

            switch (this._status)
            {
                case NFSStats.NFS_OK:
                    this._linkname.xdrEncode(xdr);
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
                    this._linkname = new Name(xdr);
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

        public Name LinkName
        {
            get
            { return this._linkname; }
        }
    }

    // End of readlinkres.cs
}