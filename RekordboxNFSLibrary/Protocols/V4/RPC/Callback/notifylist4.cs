/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC.Callback
{
    using org.acplt.oncrpc;

    public class notifylist4 : XdrAble
    {
        public byte[] value;

        public notifylist4()
        {
        }

        public notifylist4(byte[] value)
        {
            this.value = value;
        }

        public notifylist4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeDynamicOpaque(value);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            value = xdr.xdrDecodeDynamicOpaque();
        }
    }
} // End of notifylist4.cs