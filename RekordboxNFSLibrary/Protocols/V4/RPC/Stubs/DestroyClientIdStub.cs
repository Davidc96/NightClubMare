namespace RekordboxNFSLibrary.Protocols.V4.RPC.Stubs
{
    internal class DestroyClientIdStub
    {
        public static nfs_argop4 standard(clientid4 client)
        {
            nfs_argop4 op = new nfs_argop4();
            op.argop = nfs_opnum4.OP_DESTROY_CLIENTID;
            op.opdestroy_clientid = new DESTROY_CLIENTID4args();

            op.opdestroy_clientid.dca_clientid = client;

            return op;
        }
    }
}