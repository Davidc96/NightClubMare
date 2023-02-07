/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class SETCLIENTID_CONFIRM4args : XdrAble
    {
        public clientid4 clientid;
        public verifier4 setclientid_confirm;

        public SETCLIENTID_CONFIRM4args()
        {
        }

        public SETCLIENTID_CONFIRM4args(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            setclientid_confirm.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            setclientid_confirm = new verifier4(xdr);
        }
    }
} // End of SETCLIENTID_CONFIRM4args.cs