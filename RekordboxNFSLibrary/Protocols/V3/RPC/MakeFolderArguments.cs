/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using org.acplt.oncrpc;

namespace RekordboxNFSLibrary.Protocols.V3.RPC
{
    public class MakeFolderArguments : XdrAble
    {
        private ItemOperationArguments _where;
        private MakeAttributes _attributes;

        public MakeFolderArguments()
        { }

        public MakeFolderArguments(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            this._where.xdrEncode(xdr);
            this._attributes.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            this._where = new ItemOperationArguments(xdr);
            this._attributes = new MakeAttributes(xdr);
        }

        public ItemOperationArguments Where
        {
            get
            { return this._where; }
            set
            { this._where = value; }
        }

        public MakeAttributes Attributes
        {
            get
            { return this._attributes; }
            set
            { this._attributes = value; }
        }
    }

    // End of MKDIR3args.cs
}