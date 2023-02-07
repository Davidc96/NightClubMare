/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class stateid4 : XdrAble
    {
        public uint32_t seqid;
        public byte[] other;

        public stateid4()
        {
        }

        public stateid4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            seqid.xdrEncode(xdr);
            xdr.xdrEncodeOpaque(other, 12);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            seqid = new uint32_t(xdr);
            other = xdr.xdrDecodeOpaque(12);
        }
    }
} // End of stateid4.cs