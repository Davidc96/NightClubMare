/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using RekordboxNFSLibrary.Protocols.Commons;
using org.acplt.oncrpc;
using org.acplt.oncrpc.server;
using System.Net;

/**
 */

namespace RekordboxNFSLibrary.Protocols.V2.RPC
{
    public abstract class NFSv2ProtocolServerStub : OncRpcServerStub, OncRpcDispatchable
    {
        public NFSv2ProtocolServerStub()
            : this(0)
        { }

        public NFSv2ProtocolServerStub(int port)
            : this(null, port)
        { }

        public NFSv2ProtocolServerStub(IPAddress bindAddr, int port)
        {
            info = new OncRpcServerTransportRegistrationInfo[] {
            new OncRpcServerTransportRegistrationInfo(NFSv2Protocol.NFS_PROGRAM, 2),
        };

            transports = new OncRpcServerTransport[] {
            new OncRpcUdpServerTransport(this, bindAddr, port, info, 32768),
            new OncRpcTcpServerTransport(this, bindAddr, port, info, 32768)
        };
        }

        public void dispatchOncRpcCall(OncRpcCallInformation call, int program, int version, int procedure)
        {
            if (version == 2)
            {
                switch (procedure)
                {
                    case 0:
                        {
                            call.retrieveCall(XdrVoid.XDR_VOID);
                            NFSPROC_NULL();
                            call.reply(XdrVoid.XDR_VOID);

                            break;
                        }
                    case 1:
                        {
                            NFSHandle args_ = new NFSHandle();
                            args_.Version = V2.RPC.NFSv2Protocol.NFS_VERSION;
                            call.retrieveCall(args_);

                            FileStatus result_ = NFSPROC_GETATTR(args_);
                            call.reply(result_);

                            break;
                        }
                    case 2:
                        {
                            CreateArguments args_ = new CreateArguments();
                            call.retrieveCall(args_);

                            FileStatus result_ = NFSPROC_SETATTR(args_);
                            call.reply(result_);

                            break;
                        }
                    case 3:
                        {
                            call.retrieveCall(XdrVoid.XDR_VOID);
                            NFSPROC_ROOT();
                            call.reply(XdrVoid.XDR_VOID);

                            break;
                        }
                    case 4:
                        {
                            ItemOperationArguments args_ = new ItemOperationArguments();
                            call.retrieveCall(args_);

                            ItemOperationStatus result_ = NFSPROC_LOOKUP(args_);
                            call.reply(result_);

                            break;
                        }
                    case 5:
                        {
                            NFSHandle args_ = new NFSHandle();
                            args_.Version = V2.RPC.NFSv2Protocol.NFS_VERSION;
                            call.retrieveCall(args_);

                            LinkStatus result_ = NFSPROC_READLINK(args_);
                            call.reply(result_);

                            break;
                        }
                    case 6:
                        {
                            ReadArguments args_ = new ReadArguments();
                            call.retrieveCall(args_);

                            ReadStatus result_ = NFSPROC_READ(args_);
                            call.reply(result_);

                            break;
                        }
                    case 7:
                        {
                            call.retrieveCall(XdrVoid.XDR_VOID);
                            NFSPROC_WRITECACHE();
                            call.reply(XdrVoid.XDR_VOID);

                            break;
                        }
                    case 8:
                        {
                            WriteArguments args_ = new WriteArguments();
                            call.retrieveCall(args_);

                            FileStatus result_ = NFSPROC_WRITE(args_);
                            call.reply(result_);

                            break;
                        }
                    case 9:
                        {
                            CreateArguments args_ = new CreateArguments();
                            call.retrieveCall(args_);

                            ItemOperationStatus result_ = NFSPROC_CREATE(args_);
                            call.reply(result_);

                            break;
                        }
                    case 10:
                        {
                            ItemOperationArguments args_ = new ItemOperationArguments();
                            call.retrieveCall(args_);

                            XdrInt result_ = new XdrInt(NFSPROC_REMOVE(args_));
                            call.reply(result_);

                            break;
                        }
                    case 11:
                        {
                            RenameArguments args_ = new RenameArguments();
                            call.retrieveCall(args_);

                            XdrInt result_ = new XdrInt(NFSPROC_RENAME(args_));
                            call.reply(result_);

                            break;
                        }
                    case 12:
                        {
                            LinkArguments args_ = new LinkArguments();
                            call.retrieveCall(args_);

                            XdrInt result_ = new XdrInt(NFSPROC_LINK(args_));
                            call.reply(result_);

                            break;
                        }
                    case 13:
                        {
                            SymlinkArguments args_ = new SymlinkArguments();
                            call.retrieveCall(args_);

                            XdrInt result_ = new XdrInt(NFSPROC_SYMLINK(args_));
                            call.reply(result_);

                            break;
                        }
                    case 14:
                        {
                            CreateArguments args_ = new CreateArguments();
                            call.retrieveCall(args_);

                            ItemOperationStatus result_ = NFSPROC_MKDIR(args_);
                            call.reply(result_);

                            break;
                        }
                    case 15:
                        {
                            ItemOperationArguments args_ = new ItemOperationArguments();
                            call.retrieveCall(args_);

                            XdrInt result_ = new XdrInt(NFSPROC_RMDIR(args_));
                            call.reply(result_);

                            break;
                        }
                    case 16:
                        {
                            ItemArguments args_ = new ItemArguments();
                            call.retrieveCall(args_);

                            ItemStatus result_ = NFSPROC_READDIR(args_);
                            call.reply(result_);

                            break;
                        }
                    case 17:
                        {
                            NFSHandle args_ = new NFSHandle();
                            args_.Version = V2.RPC.NFSv2Protocol.NFS_VERSION;
                            call.retrieveCall(args_);

                            FSStatStatus result_ = NFSPROC_STATFS(args_);
                            call.reply(result_);

                            break;
                        }
                    default:
                        {
                            call.failProcedureUnavailable();

                            break;
                        }
                }
            }
            else
            { call.failProgramUnavailable(); }
        }

        public abstract void NFSPROC_NULL();

        public abstract FileStatus NFSPROC_GETATTR(NFSHandle arg1);

        public abstract FileStatus NFSPROC_SETATTR(CreateArguments arg1);

        public abstract void NFSPROC_ROOT();

        public abstract ItemOperationStatus NFSPROC_LOOKUP(ItemOperationArguments arg1);

        public abstract LinkStatus NFSPROC_READLINK(NFSHandle arg1);

        public abstract ReadStatus NFSPROC_READ(ReadArguments arg1);

        public abstract void NFSPROC_WRITECACHE();

        public abstract FileStatus NFSPROC_WRITE(WriteArguments arg1);

        public abstract ItemOperationStatus NFSPROC_CREATE(CreateArguments arg1);

        public abstract int NFSPROC_REMOVE(ItemOperationArguments arg1);

        public abstract int NFSPROC_RENAME(RenameArguments arg1);

        public abstract int NFSPROC_LINK(LinkArguments arg1);

        public abstract int NFSPROC_SYMLINK(SymlinkArguments arg1);

        public abstract ItemOperationStatus NFSPROC_MKDIR(CreateArguments arg1);

        public abstract int NFSPROC_RMDIR(ItemOperationArguments arg1);

        public abstract ItemStatus NFSPROC_READDIR(ItemArguments arg1);

        public abstract FSStatStatus NFSPROC_STATFS(NFSHandle arg1);
    }

    // End of NFSv2ProtocolServerStub.cs
}