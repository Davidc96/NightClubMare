/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class PUTPUBFH4res : XdrAble
    {
        public int status;

        public PUTPUBFH4res()
        {
        }

        public PUTPUBFH4res(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
        }
    }
} // End of PUTPUBFH4res.cs