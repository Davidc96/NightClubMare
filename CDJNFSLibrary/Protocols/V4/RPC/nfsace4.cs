/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class nfsace4 : XdrAble
    {
        public acetype4 type;
        public aceflag4 flag;
        public acemask4 access_mask;
        public utf8str_mixed who;

        public nfsace4()
        {
        }

        public nfsace4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            flag.xdrEncode(xdr);
            access_mask.xdrEncode(xdr);
            who.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            flag = new aceflag4(xdr);
            access_mask = new acemask4(xdr);
            who = new utf8str_mixed(xdr);
        }
    }
} // End of nfsace4.cs