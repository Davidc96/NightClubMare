/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class EXCHANGE_ID4res : XdrAble
    {
        public int eir_status;
        public EXCHANGE_ID4resok eir_resok4;

        public EXCHANGE_ID4res()
        {
        }

        public EXCHANGE_ID4res(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt(eir_status);
            switch (eir_status)
            {
                case nfsstat4.NFS4_OK:
                    eir_resok4.xdrEncode(xdr);
                    break;

                default:
                    break;
            }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            eir_status = xdr.xdrDecodeInt();
            switch (eir_status)
            {
                case nfsstat4.NFS4_OK:
                    eir_resok4 = new EXCHANGE_ID4resok(xdr);
                    break;

                default:
                    break;
            }
        }
    }
} // End of  EXCHANGE_ID4res.cs