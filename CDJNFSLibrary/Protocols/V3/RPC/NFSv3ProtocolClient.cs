/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using org.acplt.oncrpc;
using System.Net;

/**
 * The class <code>NFSv3ProtocolClient</code> implements the client stub proxy
 * for the NFS_PROGRAM remote program. It provides method stubs
 * which, when called, in turn call the appropriate remote method (procedure).
 */

namespace CDJNFSLibrary.Protocols.V3.RPC
{
    public class NFSv3ProtocolClient : OncRpcClientStub
    {
        /**
         * Constructs a <code>NFSv3ProtocolClient</code> client stub proxy object
         * from which the NFS_PROGRAM remote program can be accessed.
         * @param host Internet address of host where to contact the remote program.
         * @param protocol {@link org.acplt.oncrpc.OncRpcProtocols Protocol} to be
         *   used for ONC/RPC calls.
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public NFSv3ProtocolClient(IPAddress host, int protocol)
            : base(host, NFSv3Protocol.NFS_PROGRAM, 3, 0, protocol, true)
        {
        }

        public NFSv3ProtocolClient(IPAddress host, int protocol, bool useSecurePort)
            : base(host, NFSv3Protocol.NFS_PROGRAM, 3, 0, protocol, useSecurePort)
        {
        }

        /**
         * Constructs a <code>NFSv3ProtocolClient</code> client stub proxy object
         * from which the NFS_PROGRAM remote program can be accessed.
         * @param host Internet address of host where to contact the remote program.
         * @param port Port number at host where the remote program can be reached.
         * @param protocol {@link org.acplt.oncrpc.OncRpcProtocols Protocol} to be
         *   used for ONC/RPC calls.
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public NFSv3ProtocolClient(IPAddress host, int port, int protocol)
            : base(host, NFSv3Protocol.NFS_PROGRAM, 3, port, protocol, true)
        {
        }

        /**
         * Constructs a <code>NFSv3ProtocolClient</code> client stub proxy object
         * from which the NFS_PROGRAM remote program can be accessed.
         * @param client ONC/RPC client connection object implementing a particular
         *   protocol.
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public NFSv3ProtocolClient(OncRpcClient client)
            : base(client)
        {
        }

        /**
         * Constructs a <code>NFSv3ProtocolClient</code> client stub proxy object
         * from which the NFS_PROGRAM remote program can be accessed.
         * @param host Internet address of host where to contact the remote program.
         * @param program Remote program number.
         * @param version Remote program version number.
         * @param protocol {@link org.acplt.oncrpc.OncRpcProtocols Protocol} to be
         *   used for ONC/RPC calls.
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public NFSv3ProtocolClient(IPAddress host, int program, int version, int protocol)
            : base(host, program, version, 0, protocol, true)
        {
        }

        /**
         * Constructs a <code>NFSv3ProtocolClient</code> client stub proxy object
         * from which the NFS_PROGRAM remote program can be accessed.
         * @param host Internet address of host where to contact the remote program.
         * @param program Remote program number.
         * @param version Remote program version number.
         * @param port Port number at host where the remote program can be reached.
         * @param protocol {@link org.acplt.oncrpc.OncRpcProtocols Protocol} to be
         *   used for ONC/RPC calls.
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public NFSv3ProtocolClient(IPAddress host, int program, int version, int port, int protocol)
            : base(host, program, version, port, protocol, true)
        {
        }

        /**
        * Constructs a <code>NFSv3ProtocolClient</code> client stub proxy object
        * from which the NFS_PROGRAM remote program can be accessed.
        * @param host Internet address of host where to contact the remote program.
        * @param program Remote program number.
        * @param version Remote program version number.
        * @param port Port number at host where the remote program can be reached.
        * @param protocol {@link org.acplt.oncrpc.OncRpcProtocols Protocol} to be
        *   used for ONC/RPC calls.
        * @throws OncRpcException if an ONC/RPC error occurs.
        * @throws IOException if an I/O error occurs.
        */

        public NFSv3ProtocolClient(IPAddress host, int program, int version, int port, int protocol, bool useSecurePort)
            : base(host, program, version, port, protocol, useSecurePort)
        {
        }

        /**
         * Call remote procedure NFSPROC3_NULL_3.
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public void NFSPROC3_NULL()
        {
            XdrVoid args_ = XdrVoid.XDR_VOID;
            XdrVoid result_ = XdrVoid.XDR_VOID;
            client.call(NFSv3Protocol.NFSPROC3_NULL, NFSv3Protocol.NFS_V3, args_, result_);
        }

        /**
         * Call remote procedure NFSPROC3_GETATTR_3.
         * @param arg1 parameter (of type GETATTR3args) to the remote procedure call.
         * @return Result from remote procedure call (of type GETATTR3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<GetAttributeAccessOK, GetAttributeAccessOK> NFSPROC3_GETATTR(GetAttributeArguments arg1)
        {
            ResultObject<GetAttributeAccessOK, GetAttributeAccessOK> result_ =
                new ResultObject<GetAttributeAccessOK, GetAttributeAccessOK>();

            client.call(NFSv3Protocol.NFSPROC3_GETATTR, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_SETATTR_3.
         * @param arg1 parameter (of type SETATTR3args) to the remote procedure call.
         * @return Result from remote procedure call (of type SETATTR3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<SetAttributeAccessOK, SetAttributeAccessFAIL> NFSPROC3_SETATTR(SetAttributeArguments arg1)
        {
            ResultObject<SetAttributeAccessOK, SetAttributeAccessFAIL> result_ =
                new ResultObject<SetAttributeAccessOK, SetAttributeAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_SETATTR, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_LOOKUP_3.
         * @param arg1 parameter (of type LOOKUP3args) to the remote procedure call.
         * @return Result from remote procedure call (of type LOOKUP3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<ItemOperationAccessResultOK, ItemOperationAccessResultFAIL> NFSPROC3_LOOKUP(ItemOperationArguments arg1)
        {
            ResultObject<ItemOperationAccessResultOK, ItemOperationAccessResultFAIL> result_ =
                new ResultObject<ItemOperationAccessResultOK, ItemOperationAccessResultFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_LOOKUP, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_ACCESS_3.
         * @param arg1 parameter (of type ACCESS3args) to the remote procedure call.
         * @return Result from remote procedure call (of type ACCESS3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<AccessAccessOK, AccessAccessFAIL> NFSPROC3_ACCESS(AccessArguments arg1)
        {
            ResultObject<AccessAccessOK, AccessAccessFAIL> result_ =
                new ResultObject<AccessAccessOK, AccessAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_ACCESS, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_READLINK_3.
         * @param arg1 parameter (of type READLINK3args) to the remote procedure call.
         * @return Result from remote procedure call (of type READLINK3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<ReadLinkAccessOK, ReadLinkAccessFAIL> NFSPROC3_READLINK(ReadLinkArguments arg1)
        {
            ResultObject<ReadLinkAccessOK, ReadLinkAccessFAIL> result_ =
                new ResultObject<ReadLinkAccessOK, ReadLinkAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_READLINK, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_READ_3.
         * @param arg1 parameter (of type READ3args) to the remote procedure call.
         * @return Result from remote procedure call (of type READ3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<ReadAccessOK, ReadAccessFAIL> NFSPROC3_READ(ReadArguments arg1)
        {
            ResultObject<ReadAccessOK, ReadAccessFAIL> result_ =
                new ResultObject<ReadAccessOK, ReadAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_READ, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_WRITE_3.
         * @param arg1 parameter (of type WRITE3args) to the remote procedure call.
         * @return Result from remote procedure call (of type WRITE3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<WriteAccessOK, WriteAccessFAIL> NFSPROC3_WRITE(WriteArguments arg1)
        {
            ResultObject<WriteAccessOK, WriteAccessFAIL> result_ =
                new ResultObject<WriteAccessOK, WriteAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_WRITE, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_CREATE_3.
         * @param arg1 parameter (of type CREATE3args) to the remote procedure call.
         * @return Result from remote procedure call (of type CREATE3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<MakeFileAccessOK, MakeFileAccessFAIL> NFSPROC3_CREATE(MakeFileArguments arg1)
        {
            ResultObject<MakeFileAccessOK, MakeFileAccessFAIL> result_ =
                new ResultObject<MakeFileAccessOK, MakeFileAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_CREATE, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_MKDIR_3.
         * @param arg1 parameter (of type MKDIR3args) to the remote procedure call.
         * @return Result from remote procedure call (of type MKDIR3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<MakeFolderAccessOK, MakeFolderAccessFAIL> NFSPROC3_MKDIR(MakeFolderArguments arg1)
        {
            ResultObject<MakeFolderAccessOK, MakeFolderAccessFAIL> result_ =
                new ResultObject<MakeFolderAccessOK, MakeFolderAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_MKDIR, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_SYMLINK_3.
         * @param arg1 parameter (of type SYMLINK3args) to the remote procedure call.
         * @return Result from remote procedure call (of type SYMLINK3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<SymlinkAccessOK, SymlinkAccessFAIL> NFSPROC3_SYMLINK(SymlinkArguments arg1)
        {
            ResultObject<SymlinkAccessOK, SymlinkAccessFAIL> result_ =
                new ResultObject<SymlinkAccessOK, SymlinkAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_SYMLINK, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_MKNOD_3.
         * @param arg1 parameter (of type MKNOD3args) to the remote procedure call.
         * @return Result from remote procedure call (of type MKNOD3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<MakeNodeAccessOK, MakeNodeAccessFAIL> NFSPROC3_MKNOD(MakeNodeArguments arg1)
        {
            ResultObject<MakeNodeAccessOK, MakeNodeAccessFAIL> result_ =
                new ResultObject<MakeNodeAccessOK, MakeNodeAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_MKNOD, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_REMOVE_3.
         * @param arg1 parameter (of type REMOVE3args) to the remote procedure call.
         * @return Result from remote procedure call (of type REMOVE3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<RemoveAccessOK, RemoveAccessFAIL> NFSPROC3_REMOVE(ItemOperationArguments arg1)
        {
            ResultObject<RemoveAccessOK, RemoveAccessFAIL> result_ =
                new ResultObject<RemoveAccessOK, RemoveAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_REMOVE, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_RMDIR_3.
         * @param arg1 parameter (of type RMDIR3args) to the remote procedure call.
         * @return Result from remote procedure call (of type RMDIR3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<RemoveAccessOK, RemoveAccessFAIL> NFSPROC3_RMDIR(ItemOperationArguments arg1)
        {
            ResultObject<RemoveAccessOK, RemoveAccessFAIL> result_ =
                new ResultObject<RemoveAccessOK, RemoveAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_RMDIR, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_RENAME_3.
         * @param arg1 parameter (of type RENAME3args) to the remote procedure call.
         * @return Result from remote procedure call (of type RENAME3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<RenameAccessOK, RenameAccessFAIL> NFSPROC3_RENAME(RenameArguments arg1)
        {
            ResultObject<RenameAccessOK, RenameAccessFAIL> result_ =
                new ResultObject<RenameAccessOK, RenameAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_RENAME, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_LINK_3.
         * @param arg1 parameter (of type LINK3args) to the remote procedure call.
         * @return Result from remote procedure call (of type LINK3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<LinkAccessOK, LinkAccessFAIL> NFSPROC3_LINK(LinkArguments arg1)
        {
            ResultObject<LinkAccessOK, LinkAccessFAIL> result_ =
                new ResultObject<LinkAccessOK, LinkAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_LINK, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_READDIR_3.
         * @param arg1 parameter (of type READDIR3args) to the remote procedure call.
         * @return Result from remote procedure call (of type READDIR3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<ReadFolderAccessResultOK, ReadFolderAccessResultFAIL> NFSPROC3_READDIR(ReadFolderArguments arg1)
        {
            ResultObject<ReadFolderAccessResultOK, ReadFolderAccessResultFAIL> result_ =
                new ResultObject<ReadFolderAccessResultOK, ReadFolderAccessResultFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_READDIR, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_READDIRPLUS_3.
         * @param arg1 parameter (of type READDIRPLUS3args) to the remote procedure call.
         * @return Result from remote procedure call (of type READDIRPLUS3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<ExtendedReadFolderAccessOK, ExtendedReadFolderAccessFAIL> NFSPROC3_READDIRPLUS(ExtendedReadFolderArguments arg1)
        {
            ResultObject<ExtendedReadFolderAccessOK, ExtendedReadFolderAccessFAIL> result_ =
                new ResultObject<ExtendedReadFolderAccessOK, ExtendedReadFolderAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_READDIRPLUS, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_FSSTAT_3.
         * @param arg1 parameter (of type FSSTAT3args) to the remote procedure call.
         * @return Result from remote procedure call (of type FSSTAT3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<FSStatisticsAccessOK, FSStatisticsAccessFAIL> NFSPROC3_FSSTAT(FSStatisticsArguments arg1)
        {
            ResultObject<FSStatisticsAccessOK, FSStatisticsAccessFAIL> result_ =
                new ResultObject<FSStatisticsAccessOK, FSStatisticsAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_FSSTAT, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_FSINFO_3.
         * @param arg1 parameter (of type FSINFO3args) to the remote procedure call.
         * @return Result from remote procedure call (of type FSINFO3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<FSInfoAccessOK, FSInfoAccessFAIL> NFSPROC3_FSINFO(FSInfoArguments arg1)
        {
            ResultObject<FSInfoAccessOK, FSInfoAccessFAIL> result_ =
                new ResultObject<FSInfoAccessOK, FSInfoAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_FSINFO, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_PATHCONF_3.
         * @param arg1 parameter (of type PATHCONF3args) to the remote procedure call.
         * @return Result from remote procedure call (of type PATHCONF3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<PathConfigurationAccessOK, PathConfigurationAccessFAIL> NFSPROC3_PATHCONF(PathConfigurationArguments arg1)
        {
            ResultObject<PathConfigurationAccessOK, PathConfigurationAccessFAIL> result_ =
                new ResultObject<PathConfigurationAccessOK, PathConfigurationAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_PATHCONF, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }

        /**
         * Call remote procedure NFSPROC3_COMMIT_3.
         * @param arg1 parameter (of type COMMIT3args) to the remote procedure call.
         * @return Result from remote procedure call (of type COMMIT3res).
         * @throws OncRpcException if an ONC/RPC error occurs.
         * @throws IOException if an I/O error occurs.
         */

        public ResultObject<CommitAccessOK, CommitAccessFAIL> NFSPROC3_COMMIT(CommitArguments arg1)
        {
            ResultObject<CommitAccessOK, CommitAccessFAIL> result_ =
                new ResultObject<CommitAccessOK, CommitAccessFAIL>();

            client.call(NFSv3Protocol.NFSPROC3_COMMIT, NFSv3Protocol.NFS_V3, arg1, result_);

            return result_;
        }
    }

    // End of NFSv3ProtocolClient.cs
}