/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class ACCESS4res : XdrAble
    {
        public int status;
        public ACCESS4resok resok4;

        public ACCESS4res()
        {
        }

        public ACCESS4res(XdrDecodingStream xdr)
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
                    resok4 = new ACCESS4resok(xdr);
                    break;

                default:
                    break;
            }
        }
    }
}

// End of ACCESS4res.cs