/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class nfsv4_1_file_layouthint4 : XdrAble
    {
        public int nflh_care;
        public nfl_util4 nflh_util;
        public count4 nflh_stripe_count;

        public nfsv4_1_file_layouthint4()
        {
        }

        public nfsv4_1_file_layouthint4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            nflh_util.xdrEncode(xdr);
            nflh_stripe_count.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            nflh_util = new nfl_util4(xdr);
            nflh_stripe_count = new count4(xdr);
        }
    }
} // End of nfsv4_1_file_layouthint4.cs