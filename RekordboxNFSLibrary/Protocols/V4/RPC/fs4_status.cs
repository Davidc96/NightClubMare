/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class fs4_status : XdrAble
    {
        public bool fss_absent;
        public int fss_type;
        public utf8str_cs fss_source;
        public utf8str_cs fss_current;
        public int fss_age;
        public nfstime4 fss_version;

        public fs4_status()
        {
        }

        public fs4_status(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt(fss_type);
            fss_source.xdrEncode(xdr);
            fss_current.xdrEncode(xdr);
            xdr.xdrEncodeInt(fss_age);
            fss_version.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            fss_type = xdr.xdrDecodeInt();
            fss_source = new utf8str_cs(xdr);
            fss_current = new utf8str_cs(xdr);
            fss_age = xdr.xdrDecodeInt();
            fss_version = new nfstime4(xdr);
        }
    }
} // End of  fs4_status.cs