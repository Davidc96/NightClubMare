namespace CDJNFSLibrary.Protocols.V4.RPC.Stubs
{
    internal class GetfhStub
    {
        public static nfs_argop4 generateRequest()
        {
            nfs_argop4 op = new nfs_argop4();

            op.argop = nfs_opnum4.OP_GETFH;

            return op;
        }
    }
}