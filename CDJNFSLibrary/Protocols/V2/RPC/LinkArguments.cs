/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using CDJNFSLibrary.Protocols.Commons;
using org.acplt.oncrpc;

namespace CDJNFSLibrary.Protocols.V2.RPC
{
    public class LinkArguments : XdrAble
    {
        private NFSHandle _from;
        private ItemOperationArguments _to;

        public LinkArguments()
        { }

        public LinkArguments(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            this._from.xdrEncode(xdr);
            this._to.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            this._from = new NFSHandle();
            this._from.Version = V2.RPC.NFSv2Protocol.NFS_VERSION;
            this._from.xdrDecode(xdr);
            this._to = new ItemOperationArguments(xdr);
        }

        public NFSHandle From
        {
            get
            { return this._from; }
        }

        public ItemOperationArguments To
        {
            get
            { return this._to; }
        }
    }

    // End of linkargs.cs
}