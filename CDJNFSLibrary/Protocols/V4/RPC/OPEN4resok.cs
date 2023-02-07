/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class OPEN4resok : XdrAble
    {
        public stateid4 stateid;
        public change_info4 cinfo;
        public uint32_t rflags;
        public bitmap4 attrset;
        public open_delegation4 delegation;

        public OPEN4resok()
        {
        }

        public OPEN4resok(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            stateid.xdrEncode(xdr);
            cinfo.xdrEncode(xdr);
            rflags.xdrEncode(xdr);
            attrset.xdrEncode(xdr);
            delegation.xdrEncode(xdr);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            stateid = new stateid4(xdr);
            cinfo = new change_info4(xdr);
            rflags = new uint32_t(xdr);
            attrset = new bitmap4(xdr);
            delegation = new open_delegation4(xdr);
        }
    }
} // End of OPEN4resok.cs