/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace RekordboxNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;

    public class nfs_argop4 : XdrAble
    {
        public int argop;
        public ACCESS4args opaccess;
        public CLOSE4args opclose;
        public COMMIT4args opcommit;
        public CREATE4args opcreate;
        public DELEGPURGE4args opdelegpurge;
        public DELEGRETURN4args opdelegreturn;
        public GETATTR4args opgetattr;
        public LINK4args oplink;
        public LOCK4args oplock;
        public LOCKT4args oplockt;
        public LOCKU4args oplocku;
        public LOOKUP4args oplookup;
        public NVERIFY4args opnverify;
        public OPEN4args opopen;
        public OPENATTR4args opopenattr;
        public OPEN_CONFIRM4args opopen_confirm;
        public OPEN_DOWNGRADE4args opopen_downgrade;
        public PUTFH4args opputfh;
        public READ4args opread;
        public READDIR4args opreaddir;
        public REMOVE4args opremove;
        public RENAME4args oprename;
        public RENEW4args oprenew;
        public SECINFO4args opsecinfo;
        public SETATTR4args opsetattr;
        public SETCLIENTID4args opsetclientid;
        public SETCLIENTID_CONFIRM4args opsetclientid_confirm;
        public VERIFY4args opverify;
        public WRITE4args opwrite;
        public RELEASE_LOCKOWNER4args oprelease_lockowner;
        public BACKCHANNEL_CTL4args opbackchannel_ctl;
        public BIND_CONN_TO_SESSION4args opbind_conn_to_session;
        public EXCHANGE_ID4args opexchange_id;
        public CREATE_SESSION4args opcreate_session;
        public DESTROY_SESSION4args opdestroy_session;
        public FREE_STATEID4args opfree_stateid;
        public GET_DIR_DELEGATION4args opget_dir_delegation;
        public GETDEVICEINFO4args opgetdeviceinfo;
        public GETDEVICELIST4args opgetdevicelist;
        public LAYOUTCOMMIT4args oplayoutcommit;
        public LAYOUTGET4args oplayoutget;
        public LAYOUTRETURN4args oplayoutreturn;
        public SECINFO_NO_NAME4args opsecinfo_no_name;
        public SEQUENCE4args opsequence;
        public SET_SSV4args opset_ssv;
        public TEST_STATEID4args optest_stateid;
        public WANT_DELEGATION4args opwant_delegation;
        public DESTROY_CLIENTID4args opdestroy_clientid;
        public RECLAIM_COMPLETE4args opreclaim_complete;

        public nfs_argop4()
        {
        }

        public nfs_argop4(XdrDecodingStream xdr)
        {
            xdrDecode(xdr);
        }

        public void xdrEncode(XdrEncodingStream xdr)
        {
            xdr.xdrEncodeInt(argop);
            switch (argop)
            {
                case nfs_opnum4.OP_ACCESS:
                    opaccess.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_CLOSE:
                    opclose.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_COMMIT:
                    opcommit.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_CREATE:
                    opcreate.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_DELEGPURGE:
                    opdelegpurge.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_DELEGRETURN:
                    opdelegreturn.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_GETATTR:
                    opgetattr.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_GETFH:
                    break;

                case nfs_opnum4.OP_LINK:
                    oplink.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_LOCK:
                    oplock.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_LOCKT:
                    oplockt.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_LOCKU:
                    oplocku.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_LOOKUP:
                    oplookup.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_LOOKUPP:
                    break;

                case nfs_opnum4.OP_NVERIFY:
                    opnverify.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_OPEN:
                    opopen.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_OPENATTR:
                    opopenattr.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_OPEN_CONFIRM:
                    opopen_confirm.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_OPEN_DOWNGRADE:
                    opopen_downgrade.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_PUTFH:
                    opputfh.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_PUTPUBFH:
                    break;

                case nfs_opnum4.OP_PUTROOTFH:
                    break;

                case nfs_opnum4.OP_READ:
                    opread.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_READDIR:
                    opreaddir.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_READLINK:
                    break;

                case nfs_opnum4.OP_REMOVE:
                    opremove.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_RENAME:
                    oprename.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_RENEW:
                    oprenew.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_RESTOREFH:
                    break;

                case nfs_opnum4.OP_SAVEFH:
                    break;

                case nfs_opnum4.OP_SECINFO:
                    opsecinfo.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_SETATTR:
                    opsetattr.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_SETCLIENTID:
                    opsetclientid.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_SETCLIENTID_CONFIRM:
                    opsetclientid_confirm.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_VERIFY:
                    opverify.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_WRITE:
                    opwrite.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_RELEASE_LOCKOWNER:
                    oprelease_lockowner.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_BACKCHANNEL_CTL:
                    opbackchannel_ctl.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_BIND_CONN_TO_SESSION:
                    opbind_conn_to_session.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_EXCHANGE_ID:
                    opexchange_id.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_CREATE_SESSION:
                    opcreate_session.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_DESTROY_SESSION:
                    opdestroy_session.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_FREE_STATEID:
                    opfree_stateid.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_GET_DIR_DELEGATION:
                    opget_dir_delegation.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_GETDEVICEINFO:
                    opgetdeviceinfo.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_GETDEVICELIST:
                    opgetdevicelist.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_LAYOUTCOMMIT:
                    oplayoutcommit.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_LAYOUTGET:
                    oplayoutget.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_LAYOUTRETURN:
                    oplayoutreturn.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_SECINFO_NO_NAME:
                    opsecinfo_no_name.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_SEQUENCE:
                    opsequence.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_SET_SSV:
                    opset_ssv.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_TEST_STATEID:
                    optest_stateid.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_WANT_DELEGATION:
                    opwant_delegation.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_DESTROY_CLIENTID:
                    opdestroy_clientid.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_RECLAIM_COMPLETE:
                    opreclaim_complete.xdrEncode(xdr);
                    break;

                case nfs_opnum4.OP_ILLEGAL:
                    break;
            }
        }

        public void xdrDecode(XdrDecodingStream xdr)
        {
            argop = xdr.xdrDecodeInt();
            switch (argop)
            {
                case nfs_opnum4.OP_ACCESS:
                    opaccess = new ACCESS4args(xdr);
                    break;

                case nfs_opnum4.OP_CLOSE:
                    opclose = new CLOSE4args(xdr);
                    break;

                case nfs_opnum4.OP_COMMIT:
                    opcommit = new COMMIT4args(xdr);
                    break;

                case nfs_opnum4.OP_CREATE:
                    opcreate = new CREATE4args(xdr);
                    break;

                case nfs_opnum4.OP_DELEGPURGE:
                    opdelegpurge = new DELEGPURGE4args(xdr);
                    break;

                case nfs_opnum4.OP_DELEGRETURN:
                    opdelegreturn = new DELEGRETURN4args(xdr);
                    break;

                case nfs_opnum4.OP_GETATTR:
                    opgetattr = new GETATTR4args(xdr);
                    break;

                case nfs_opnum4.OP_GETFH:
                    break;

                case nfs_opnum4.OP_LINK:
                    oplink = new LINK4args(xdr);
                    break;

                case nfs_opnum4.OP_LOCK:
                    oplock = new LOCK4args(xdr);
                    break;

                case nfs_opnum4.OP_LOCKT:
                    oplockt = new LOCKT4args(xdr);
                    break;

                case nfs_opnum4.OP_LOCKU:
                    oplocku = new LOCKU4args(xdr);
                    break;

                case nfs_opnum4.OP_LOOKUP:
                    oplookup = new LOOKUP4args(xdr);
                    break;

                case nfs_opnum4.OP_LOOKUPP:
                    break;

                case nfs_opnum4.OP_NVERIFY:
                    opnverify = new NVERIFY4args(xdr);
                    break;

                case nfs_opnum4.OP_OPEN:
                    opopen = new OPEN4args(xdr);
                    break;

                case nfs_opnum4.OP_OPENATTR:
                    opopenattr = new OPENATTR4args(xdr);
                    break;

                case nfs_opnum4.OP_OPEN_CONFIRM:
                    opopen_confirm = new OPEN_CONFIRM4args(xdr);
                    break;

                case nfs_opnum4.OP_OPEN_DOWNGRADE:
                    opopen_downgrade = new OPEN_DOWNGRADE4args(xdr);
                    break;

                case nfs_opnum4.OP_PUTFH:
                    opputfh = new PUTFH4args(xdr);
                    break;

                case nfs_opnum4.OP_PUTPUBFH:
                    break;

                case nfs_opnum4.OP_PUTROOTFH:
                    break;

                case nfs_opnum4.OP_READ:
                    opread = new READ4args(xdr);
                    break;

                case nfs_opnum4.OP_READDIR:
                    opreaddir = new READDIR4args(xdr);
                    break;

                case nfs_opnum4.OP_READLINK:
                    break;

                case nfs_opnum4.OP_REMOVE:
                    opremove = new REMOVE4args(xdr);
                    break;

                case nfs_opnum4.OP_RENAME:
                    oprename = new RENAME4args(xdr);
                    break;

                case nfs_opnum4.OP_RENEW:
                    oprenew = new RENEW4args(xdr);
                    break;

                case nfs_opnum4.OP_RESTOREFH:
                    break;

                case nfs_opnum4.OP_SAVEFH:
                    break;

                case nfs_opnum4.OP_SECINFO:
                    opsecinfo = new SECINFO4args(xdr);
                    break;

                case nfs_opnum4.OP_SETATTR:
                    opsetattr = new SETATTR4args(xdr);
                    break;

                case nfs_opnum4.OP_SETCLIENTID:
                    opsetclientid = new SETCLIENTID4args(xdr);
                    break;

                case nfs_opnum4.OP_SETCLIENTID_CONFIRM:
                    opsetclientid_confirm = new SETCLIENTID_CONFIRM4args(xdr);
                    break;

                case nfs_opnum4.OP_VERIFY:
                    opverify = new VERIFY4args(xdr);
                    break;

                case nfs_opnum4.OP_WRITE:
                    opwrite = new WRITE4args(xdr);
                    break;

                case nfs_opnum4.OP_RELEASE_LOCKOWNER:
                    oprelease_lockowner = new RELEASE_LOCKOWNER4args(xdr);
                    break;

                case nfs_opnum4.OP_BACKCHANNEL_CTL:
                    opbackchannel_ctl = new BACKCHANNEL_CTL4args(xdr);
                    break;

                case nfs_opnum4.OP_BIND_CONN_TO_SESSION:
                    opbind_conn_to_session = new BIND_CONN_TO_SESSION4args(xdr);
                    break;

                case nfs_opnum4.OP_EXCHANGE_ID:
                    opexchange_id = new EXCHANGE_ID4args(xdr);
                    break;

                case nfs_opnum4.OP_CREATE_SESSION:
                    opcreate_session = new CREATE_SESSION4args(xdr);
                    break;

                case nfs_opnum4.OP_DESTROY_SESSION:
                    opdestroy_session = new DESTROY_SESSION4args(xdr);
                    break;

                case nfs_opnum4.OP_FREE_STATEID:
                    opfree_stateid = new FREE_STATEID4args(xdr);
                    break;

                case nfs_opnum4.OP_GET_DIR_DELEGATION:
                    opget_dir_delegation = new GET_DIR_DELEGATION4args(xdr);
                    break;

                case nfs_opnum4.OP_GETDEVICEINFO:
                    opgetdeviceinfo = new GETDEVICEINFO4args(xdr);
                    break;

                case nfs_opnum4.OP_GETDEVICELIST:
                    opgetdevicelist = new GETDEVICELIST4args(xdr);
                    break;

                case nfs_opnum4.OP_LAYOUTCOMMIT:
                    oplayoutcommit = new LAYOUTCOMMIT4args(xdr);
                    break;

                case nfs_opnum4.OP_LAYOUTGET:
                    oplayoutget = new LAYOUTGET4args(xdr);
                    break;

                case nfs_opnum4.OP_LAYOUTRETURN:
                    oplayoutreturn = new LAYOUTRETURN4args(xdr);
                    break;

                case nfs_opnum4.OP_SECINFO_NO_NAME:
                    opsecinfo_no_name = new SECINFO_NO_NAME4args(xdr);
                    break;

                case nfs_opnum4.OP_SEQUENCE:
                    opsequence = new SEQUENCE4args(xdr);
                    break;

                case nfs_opnum4.OP_SET_SSV:
                    opset_ssv = new SET_SSV4args(xdr);
                    break;

                case nfs_opnum4.OP_TEST_STATEID:
                    optest_stateid = new TEST_STATEID4args(xdr);
                    break;

                case nfs_opnum4.OP_WANT_DELEGATION:
                    opwant_delegation = new WANT_DELEGATION4args(xdr);
                    break;

                case nfs_opnum4.OP_DESTROY_CLIENTID:
                    opdestroy_clientid = new DESTROY_CLIENTID4args(xdr);
                    break;

                case nfs_opnum4.OP_RECLAIM_COMPLETE:
                    opreclaim_complete = new RECLAIM_COMPLETE4args(xdr);
                    break;

                case nfs_opnum4.OP_ILLEGAL:
                    break;
            }
        }
    }
} // End of nfs_argop4.cs