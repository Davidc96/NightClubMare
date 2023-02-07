/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC.Callback
{
    using org.acplt.oncrpc;

    public class layoutrecall4 : XdrAble
    {
        public int lor_recalltype;
        public layoutrecall_file4 lor_layout;
        public fsid4 lor_fsid;

        public layoutrecall4()
        {
        }

        public layoutrecall4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt(lor_recalltype);
            switch (lor_recalltype)
            {
                case layoutrecall_type4.LAYOUTRECALL4_FILE:
                    lor_layout.xdrEncode(xdr);
                    break;

                case layoutrecall_type4.LAYOUTRECALL4_FSID:
                    lor_fsid.xdrEncode(xdr);
                    break;

                case layoutrecall_type4.LAYOUTRECALL4_ALL:
                    break;
            }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            lor_recalltype = xdr.xdrDecodeInt();
            switch (lor_recalltype)
            {
                case layoutrecall_type4.LAYOUTRECALL4_FILE:
                    lor_layout = new layoutrecall_file4(xdr);
                    break;

                case layoutrecall_type4.LAYOUTRECALL4_FSID:
                    lor_fsid = new fsid4(xdr);
                    break;

                case layoutrecall_type4.LAYOUTRECALL4_ALL:
                    break;
            }
        }
    }
} // End of layoutrecall4.cs