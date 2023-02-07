/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using org.acplt.oncrpc;
using System;

namespace CDJNFSLibrary.Protocols.Commons
{
    public class Name : XdrAble
    {
        private String _value;

        public Name()
        { }

        public Name(String value)
        { this._value = value; }

        public Name(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        { xdr.xdrEncodeString(this._value); }

        public void xdrDecode(XdrDecodingStream xdr)
        { this._value = xdr.xdrDecodeString(); }

        public String Value
        {
            get
            { return this._value; }
            set
            { this._value = value; }
        }
    }

    // End of filename.cs
}