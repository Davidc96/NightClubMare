/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class LAYOUTRETURN4res : XdrAble
    {
        public int lorr_status;
        public layoutreturn_stateid lorr_stateid;

        public LAYOUTRETURN4res()
        {
        }

        public LAYOUTRETURN4res(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt(lorr_status);
            switch (lorr_status)
            {
                case nfsstat4.NFS4_OK:
                    lorr_stateid.xdrEncode(xdr);
                    break;

                default:
                    break;
            }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            lorr_status = xdr.xdrDecodeInt();
            switch (lorr_status)
            {
                case nfsstat4.NFS4_OK:
                    lorr_stateid = new layoutreturn_stateid(xdr);
                    break;

                default:
                    break;
            }
        }
    }
} // End of LAYOUTRETURN4res.cs