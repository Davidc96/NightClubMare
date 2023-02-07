/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class GET_DIR_DELEGATION4args : XdrAble
    {
        public bool gdda_signal_deleg_avail;
        public bitmap4 gdda_notification_types;
        public attr_notice4 gdda_child_attr_delay;
        public attr_notice4 gdda_dir_attr_delay;
        public bitmap4 gdda_child_attributes;
        public bitmap4 gdda_dir_attributes;

        public GET_DIR_DELEGATION4args()
        {
        }

        public GET_DIR_DELEGATION4args(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            gdda_notification_types.xdrEncode(xdr);
            gdda_child_attr_delay.xdrEncode(xdr);
            gdda_dir_attr_delay.xdrEncode(xdr);
            gdda_child_attributes.xdrEncode(xdr);
            gdda_dir_attributes.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            gdda_notification_types = new bitmap4(xdr);
            gdda_child_attr_delay = new attr_notice4(xdr);
            gdda_dir_attr_delay = new attr_notice4(xdr);
            gdda_child_attributes = new bitmap4(xdr);
            gdda_dir_attributes = new bitmap4(xdr);
        }
    }
}

// End of GET_DIR_DELEGATION4args.cs