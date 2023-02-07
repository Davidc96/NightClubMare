/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class SET_SSV4res : XdrAble
    {
        public int ssr_status;
        public SET_SSV4resok ssr_resok4;

        public SET_SSV4res()
        {
        }

        public SET_SSV4res(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt(ssr_status);
            switch (ssr_status)
            {
                case nfsstat4.NFS4_OK:
                    ssr_resok4.xdrEncode(xdr);
                    break;

                default:
                    break;
            }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            ssr_status = xdr.xdrDecodeInt();
            switch (ssr_status)
            {
                case nfsstat4.NFS4_OK:
                    ssr_resok4 = new SET_SSV4resok(xdr);
                    break;

                default:
                    break;
            }
        }
    }
} // End of SET_SSV4res.cs