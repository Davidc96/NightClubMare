/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class OPEN_CONFIRM4res : XdrAble
    {
        public int status;
        public OPEN_CONFIRM4resok resok4;

        public OPEN_CONFIRM4res()
        {
        }

        public OPEN_CONFIRM4res(XdrDecodingStream xdr)
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
                    resok4 = new OPEN_CONFIRM4resok(xdr);
                    break;

                default:
                    break;
            }
        }
    }
} // End of OPEN_CONFIRM4res.cs