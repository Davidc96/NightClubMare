/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class sessionid4 : XdrAble
    {
        public byte[] value;

        public sessionid4()
        {
        }

        public sessionid4(byte[] value)
        {
            this.value = value;
        }

        public sessionid4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeOpaque(value, NFSv4Protocol.NFS4_SESSIONID_SIZE);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            value = xdr.xdrDecodeOpaque(NFSv4Protocol.NFS4_SESSIONID_SIZE);
        }
    }
} // End of sessionid4.cs