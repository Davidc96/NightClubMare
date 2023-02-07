/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using org.acplt.oncrpc;

namespace CDJNFSLibrary.Protocols.Commons
{
    public class Groups : XdrAble
    {
        private GroupNode _value;

        public Groups()
        { }

        public Groups(GroupNode value)
        { this._value = value; }

        public Groups(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            if (this._value != null)
            {
                xdr.xdrEncodeBoolean(true);
                this._value.xdrEncode(xdr);
            }
            else { xdr.xdrEncodeBoolean(false); };
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            this._value = xdr.xdrDecodeBoolean() ? new GroupNode(xdr) : null;
        }

        public GroupNode Value
        {
            get
            { return this._value; }
        }
    }

    public class GroupNode : XdrAble
    {
        private Name _grname;
        private Groups _grnext;

        public GroupNode()
        { }

        public GroupNode(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            this._grname.xdrEncode(xdr);
            this._grnext.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            this._grname = new Name(xdr);
            this._grnext = new Groups(xdr);
        }

        public Name GroupName
        {
            get
            { return this._grname; }
        }

        public Groups NextGroup
        {
            get
            { return this._grnext; }
        }
    }

    // End of groups.cs
}