namespace CDJNFSLibrary.Protocols.V4.RPC.Stubs
{
    internal class PutrootfhStub
    {
        public static nfs_argop4 generateRequest()
        {
            nfs_argop4 op = new nfs_argop4();

            op.argop = nfs_opnum4.OP_PUTROOTFH;

            return op;
        }
    }
}