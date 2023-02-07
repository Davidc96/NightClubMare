/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using org.acplt.oncrpc;

namespace RekordboxNFSLibrary.Protocols.V3.RPC
{
    public class RenameArguments : XdrAble
    {
        private ItemOperationArguments _from;
        private ItemOperationArguments _to;

        public RenameArguments()
        { }

        public RenameArguments(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            this._from.xdrEncode(xdr);
            this._to.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            this._from = new ItemOperationArguments(xdr);
            this._to = new ItemOperationArguments(xdr);
        }

        public ItemOperationArguments From
        {
            get
            { return this._from; }
            set
            { this._from = value; }
        }

        public ItemOperationArguments To
        {
            get
            { return this._to; }
            set
            { this._to = value; }
        }
    }

    // End of renameargs.cs
}