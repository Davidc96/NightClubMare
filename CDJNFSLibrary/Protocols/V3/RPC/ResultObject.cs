/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using CDJNFSLibrary.Protocols.Commons;
using org.acplt.oncrpc;
using System;

namespace CDJNFSLibrary.Protocols.V3.RPC
{
    public class ResultObject<O, F> : XdrAble
    {
        private NFSStats _status;
        private O _resok;
        private F _resfail;

        public ResultObject()
        { }

        public ResultObject(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt((int)this._status);

            switch (this._status)
            {
                case NFSStats.NFS_OK:
                    ((XdrAble)this._resok).xdrEncode(xdr);
                    break;

                default:
                    ((XdrAble)this._resfail).xdrEncode(xdr);
                    break;
            }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            this._status = (NFSStats)xdr.xdrDecodeInt();

            switch (this._status)
            {
                case NFSStats.NFS_OK:
                    Type OK = typeof(O);

                    this._resok = (O)Activator.CreateInstance(OK, new object[] { xdr });
                    break;

                default:
                    Type FAIL = typeof(F);

                    this._resfail = (F)Activator.CreateInstance(FAIL, new object[] { xdr });
                    break;
            }
        }

        public NFSStats Status
        {
            get
            { return this._status; }
        }

        public O OK
        {
            get
            { return this._resok; }
        }

        public F FAIL
        {
            get
            { return this._resfail; }
        }
    }

    // End of FSSTAT3res.cs
}