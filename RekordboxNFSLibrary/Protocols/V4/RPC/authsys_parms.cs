/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class authsys_parms : XdrAble
    {
        public int stamp;
        public string machinename;
        public int uid;
        public int gid;
        public int[] gids;

        public authsys_parms()
        {
        }

        public authsys_parms(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt(stamp);
            xdr.xdrEncodeString(machinename);
            xdr.xdrEncodeInt(uid);
            xdr.xdrEncodeInt(gid);
            xdr.xdrEncodeIntVector(gids);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            stamp = xdr.xdrDecodeInt();
            machinename = xdr.xdrDecodeString();
            uid = xdr.xdrDecodeInt();
            gid = xdr.xdrDecodeInt();
            gids = xdr.xdrDecodeIntVector();
        }
    }
}

// End of authsys_parms.cs