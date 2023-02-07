/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class LOCK4args : XdrAble
    {
        public int locktype;
        public bool reclaim;
        public offset4 offset;
        public length4 length;
        public locker4 locker;

        public LOCK4args()
        {
        }

        public LOCK4args(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeBoolean(reclaim);
            offset.xdrEncode(xdr);
            length.xdrEncode(xdr);
            locker.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            reclaim = xdr.xdrDecodeBoolean();
            offset = new offset4(xdr);
            length = new length4(xdr);
            locker = new locker4(xdr);
        }
    }
} // End of LOCK4args.cs