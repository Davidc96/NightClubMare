/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class LOCK4denied : XdrAble
    {
        public offset4 offset;
        public length4 length;
        public int locktype;
        public lock_owner4 owner;

        public LOCK4denied()
        {
        }

        public LOCK4denied(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            length.xdrEncode(xdr);
            xdr.xdrEncodeInt(locktype);
            owner.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            length = new length4(xdr);
            locktype = xdr.xdrDecodeInt();
            owner = new lock_owner4(xdr);
        }
    }
} // End of LOCK4denied.cs