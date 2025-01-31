﻿namespace CDJNFSLibrary.Protocols.V4.RPC.Stubs
{
    internal class DestroySessionStub
    {
        public static nfs_argop4 standard(sessionid4 sessionid)
        {
            nfs_argop4 op = new nfs_argop4();
            op.argop = nfs_opnum4.OP_DESTROY_SESSION;
            op.opdestroy_session = new DESTROY_SESSION4args();

            op.opdestroy_session.dsa_sessionid = sessionid;

            return op;
        }
    }
}