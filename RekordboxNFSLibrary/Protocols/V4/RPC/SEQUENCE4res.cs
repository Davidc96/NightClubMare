/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class SEQUENCE4res : XdrAble
    {
        public int sr_status;
        public SEQUENCE4resok sr_resok4;

        public SEQUENCE4res()
        {
        }

        public SEQUENCE4res(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt(sr_status);
            switch (sr_status)
            {
                case nfsstat4.NFS4_OK:
                    sr_resok4.xdrEncode(xdr);
                    break;

                default:
                    break;
            }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            sr_status = xdr.xdrDecodeInt();
            switch (sr_status)
            {
                case nfsstat4.NFS4_OK:
                    sr_resok4 = new SEQUENCE4resok(xdr);
                    break;

                default:
                    break;
            }
        }
    }
} // End of SEQUENCE4res.cs