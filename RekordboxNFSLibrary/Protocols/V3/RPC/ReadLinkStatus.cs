/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using RekordboxNFSLibrary.Protocols.Commons;
using org.acplt.oncrpc;

namespace RekordboxNFSLibrary.Protocols.V3.RPC
{
    public class ReadLinkAccessOK : XdrAble
    {
        private PostOperationAttributes _symlink_attributes;
        private Name _data;

        public ReadLinkAccessOK()
        { }

        public ReadLinkAccessOK(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            this._symlink_attributes.xdrEncode(xdr);
            this._data.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            this._symlink_attributes = new PostOperationAttributes(xdr);
            this._data = new Name(xdr);
        }

        public PostOperationAttributes Attributes
        {
            get
            { return this._symlink_attributes; }
        }

        public Name Name
        {
            get
            { return this._data; }
        }
    }

    public class ReadLinkAccessFAIL : XdrAble
    {
        private PostOperationAttributes _symlink_attributes;

        public ReadLinkAccessFAIL()
        { }

        public ReadLinkAccessFAIL(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        { this._symlink_attributes.xdrEncode(xdr); }

        public void xdrDecode(XdrDecodingStream xdr)
        { this._symlink_attributes = new PostOperationAttributes(xdr); }

        public PostOperationAttributes Attributes
        {
            get
            { return this._symlink_attributes; }
        }
    }

    // End of READLINK3res.cs
}