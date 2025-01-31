/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class CLOSE4res : XdrAble
    {
        public int status;
        public stateid4 open_stateid;

        public CLOSE4res()
        {
        }

        public CLOSE4res(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt(status);
            switch (status)
            {
                case nfsstat4.NFS4_OK:
                    open_stateid.xdrEncode(xdr);
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
                    open_stateid = new stateid4(xdr);
                    break;

                default:
                    break;
            }
        }
    }
} // End of CLOSE4res.cs