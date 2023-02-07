/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class newsize4 : XdrAble
    {
        public bool ns_sizechanged;
        public length4 ns_size;

        public newsize4()
        {
        }

        public newsize4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeBoolean(ns_sizechanged);
            if (ns_sizechanged == true)
            {
                ns_size.xdrEncode(xdr);
            }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            ns_sizechanged = xdr.xdrDecodeBoolean();
            if (ns_sizechanged == true)
            {
                ns_size = new length4(xdr);
            }
        }
    }
} // End of newsize4.cs