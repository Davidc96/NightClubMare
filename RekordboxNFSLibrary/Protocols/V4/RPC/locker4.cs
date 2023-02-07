/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class locker4 : XdrAble
    {
        public bool new_lock_owner;
        public open_to_lock_owner4 open_owner;
        public exist_lock_owner4 lock_owner;

        public locker4()
        {
        }

        public locker4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeBoolean(new_lock_owner);
            if (new_lock_owner == true)
            {
                open_owner.xdrEncode(xdr);
            }
            else if (new_lock_owner == false)
            {
                lock_owner.xdrEncode(xdr);
            }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            new_lock_owner = xdr.xdrDecodeBoolean();
            if (new_lock_owner == true)
            {
                open_owner = new open_to_lock_owner4(xdr);
            }
            else if (new_lock_owner == false)
            {
                lock_owner = new exist_lock_owner4(xdr);
            }
        }
    }
} // End of locker4.cs