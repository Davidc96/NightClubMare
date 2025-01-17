/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC.Callback
{
    using org.acplt.oncrpc;

    public class CB_SEQUENCE4res : XdrAble
    {
        public int csr_status;
        public CB_SEQUENCE4resok csr_resok4;

        public CB_SEQUENCE4res()
        {
        }

        public CB_SEQUENCE4res(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt(csr_status);
            switch (csr_status)
            {
                case nfsstat4.NFS4_OK:
                    csr_resok4.xdrEncode(xdr);
                    break;

                default:
                    break;
            }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            csr_status = xdr.xdrDecodeInt();
            switch (csr_status)
            {
                case nfsstat4.NFS4_OK:
                    csr_resok4 = new CB_SEQUENCE4resok(xdr);
                    break;

                default:
                    break;
            }
        }
    }
} // End of CB_SEQUENCE4res.cs