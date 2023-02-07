/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class LOCK4res : XdrAble
    {
        public int status;
        public LOCK4resok resok4;
        public LOCK4denied denied;

        public LOCK4res()
        {
        }

        public LOCK4res(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt(status);
            switch (status)
            {
                case nfsstat4.NFS4_OK:
                    resok4.xdrEncode(xdr);
                    break;

                case nfsstat4.NFS4ERR_DENIED:
                    denied.xdrEncode(xdr);
                    break;

                default:
                    break;
            }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            status = xdr.xdrDecodeInt();
            switch (status)
            {
                case nfsstat4.NFS4_OK:
                    resok4 = new LOCK4resok(xdr);
                    break;

                case nfsstat4.NFS4ERR_DENIED:
                    denied = new LOCK4denied(xdr);
                    break;

                default:
                    break;
            }
        }
    }
} // End of LOCK4res.cs