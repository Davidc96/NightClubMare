/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using CDJNFSLibrary.Protocols.Commons;
using org.acplt.oncrpc;

namespace CDJNFSLibrary.Protocols.V3.RPC
{
    public class SymlinkArguments : XdrAble
    {
        private ItemOperationArguments _where;
        private SymlinkData _symlink;

        public SymlinkArguments()
        { }

        public SymlinkArguments(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            this._where.xdrEncode(xdr);
            this._symlink.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            this._where = new ItemOperationArguments(xdr);
            this._symlink = new SymlinkData(xdr);
        }

        public ItemOperationArguments Where
        {
            get
            { return this._where; }
        }

        public SymlinkData Symlink
        {
            get
            { return this._symlink; }
        }
    }

    public class SymlinkData : XdrAble
    {
        private MakeAttributes _symlink_attributes;
        private Name _symlink_data;

        public SymlinkData()
        { }

        public SymlinkData(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            this._symlink_attributes.xdrEncode(xdr);
            this._symlink_data.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            this._symlink_attributes = new MakeAttributes(xdr);
            this._symlink_data = new Name(xdr);
        }

        public MakeAttributes Attributes
        {
            get
            { return this._symlink_attributes; }
        }

        public Name Name
        {
            get
            { return this._symlink_data; }
        }
    }

    // End of SYMLINK3args.cs
}