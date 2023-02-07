/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class callback_sec_parms4 : XdrAble
    {
        public int cb_secflavor;
        public authsys_parms cbsp_sys_cred;
        public gss_cb_handles4 cbsp_gss_handles;

        public callback_sec_parms4()
        {
        }

        public callback_sec_parms4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt(cb_secflavor);
            switch (cb_secflavor)
            {
                case auth_flavor.AUTH_NONE:
                    break;

                case auth_flavor.AUTH_SYS:
                    cbsp_sys_cred.xdrEncode(xdr);
                    break;

                case auth_flavor.RPCSEC_GSS:
                    cbsp_gss_handles.xdrEncode(xdr);
                    break;
            }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            cb_secflavor = xdr.xdrDecodeInt();
            switch (cb_secflavor)
            {
                case auth_flavor.AUTH_NONE:
                    break;

                case auth_flavor.AUTH_SYS:
                    cbsp_sys_cred = new authsys_parms(xdr);
                    break;

                case auth_flavor.RPCSEC_GSS:
                    cbsp_gss_handles = new gss_cb_handles4(xdr);
                    break;
            }
        }
    }
}

// End of callback_sec_parms4.cs