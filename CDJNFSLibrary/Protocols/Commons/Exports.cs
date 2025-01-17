/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using org.acplt.oncrpc;

namespace CDJNFSLibrary.Protocols.Commons
{
    public class Exports : XdrAble
    {
        private ExportNode _value;

        public Exports()
        { }

        public Exports(ExportNode value)
        { this._value = value; }

        public Exports(XdrDecodingStream xdr)
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
            this._value = xdr.xdrDecodeBoolean() ? new ExportNode(xdr) : null;
        }

        public ExportNode Value
        {
            get
            { return this._value; }
        }
    }

    public class ExportNode : XdrAble
    {
        private Name _mountpath;
        private Groups _exgroups;
        private Exports _next;

        public ExportNode()
        { }

        public ExportNode(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            this._mountpath.xdrEncode(xdr);
            this._exgroups.xdrEncode(xdr);
            this._next.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            this._mountpath = new Name(xdr);
            this._exgroups = new Groups(xdr);
            this._next = new Exports(xdr);
        }

        public Name MountPath
        {
            get
            { return this._mountpath; }
        }

        public Groups ExportGroups
        {
            get
            { return this._exgroups; }
        }

        public Exports Next
        {
            get
            { return this._next; }
        }
    }

    // End of exports.cs
}