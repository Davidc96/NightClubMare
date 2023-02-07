/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class CLOSE4args : XdrAble
    {
        public seqid4 seqid;
        public stateid4 open_stateid;

        public CLOSE4args()
        {
        }

        public CLOSE4args(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            seqid.xdrEncode(xdr);
            open_stateid.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            seqid = new seqid4(xdr);
            open_stateid = new stateid4(xdr);
        }
    }
} // End of CLOSE4args.cs