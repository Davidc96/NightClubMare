/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class PUTFH4res : XdrAble
    {
        public int status;

        public PUTFH4res()
        {
        }

        public PUTFH4res(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt(status);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            status = xdr.xdrDecodeInt();
        }
    }
} // End of PUTFH4res.cs