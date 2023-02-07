/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using CDJNFSLibrary.Protocols.Commons;
using org.acplt.oncrpc;

namespace CDJNFSLibrary.Protocols.V3.RPC
{
    public class FSInfoAccessOK : XdrAble
    {
        private PostOperationAttributes _obj_attributes;
        private int _rtmax;
        private int _rtpref;
        private int _rtmult;
        private int _wtmax;
        private int _wtpref;
        private int _wtmult;
        private int _dtpref;
        private long _maxfilesize;
        private NFSTimeValue _time_delta;
        private int _properties;

        public FSInfoAccessOK()
        { }

        public FSInfoAccessOK(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            this._obj_attributes.xdrEncode(xdr);
            xdr.xdrEncodeInt(this._rtmax);
            xdr.xdrEncodeInt(this._rtpref);
            xdr.xdrEncodeInt(this._rtmult);
            xdr.xdrEncodeInt(this._wtmax);
            xdr.xdrEncodeInt(this._wtpref);
            xdr.xdrEncodeInt(this._wtmult);
            xdr.xdrEncodeInt(this._dtpref);
            xdr.xdrEncodeLong(this._maxfilesize);
            this._time_delta.xdrEncode(xdr);
            xdr.xdrEncodeInt(this._properties);
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            this._obj_attributes = new PostOperationAttributes(xdr);
            this._rtmax = xdr.xdrDecodeInt();
            this._rtpref = xdr.xdrDecodeInt();
            this._rtmult = xdr.xdrDecodeInt();
            this._wtmax = xdr.xdrDecodeInt();
            this._wtpref = xdr.xdrDecodeInt();
            this._wtmult = xdr.xdrDecodeInt();
            this._dtpref = xdr.xdrDecodeInt();
            this._maxfilesize = xdr.xdrDecodeLong();
            this._time_delta = new NFSTimeValue(xdr);
            this._properties = xdr.xdrDecodeInt();
        }

        public int MaximumReadRequestSize
        {
            get
            { return this._rtmax; }
        }

        public int PreferredReadRequestSize
        {
            get
            { return this._rtpref; }
        }

        public int ReadRequestSizeMultiplier
        {
            get
            { return this._rtmult; }
        }

        public int MaximumWriteRequestSize
        {
            get
            { return this._wtmax; }
        }

        public int PreferredWriteRequestSize
        {
            get
            { return this._wtpref; }
        }

        public int WriteRequestSizeMultiplier
        {
            get
            { return this._wtmult; }
        }

        public int PreferredReadFolderRequestSize
        {
            get
            { return this._dtpref; }
        }

        public long MaximumFileSize
        {
            get
            { return this._maxfilesize; }
        }

        public NFSTimeValue DeltaTime
        {
            get
            { return this._time_delta; }
        }

        public PostOperationAttributes Attributes
        {
            get
            { return this._obj_attributes; }
        }

        public int Properties
        {
            get
            { return this._properties; }
        }
    }

    public class FSInfoAccessFAIL : XdrAble
    {
        private PostOperationAttributes obj_attributes;

        public FSInfoAccessFAIL()
        { }

        public FSInfoAccessFAIL(XdrDecodingStream xdr)
        { xdrDecode(xdr); }

        public void xdrEncode(XdrEncodingStream xdr)
        { this.obj_attributes.xdrEncode(xdr); }

        public void xdrDecode(XdrDecodingStream xdr)
        { this.obj_attributes = new PostOperationAttributes(xdr); }

        public PostOperationAttributes Attributes
        {
            get
            { return this.obj_attributes; }
        }
    }

    // End of FSINFO3res.cs
}