/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

using CDJNFSLibrary.Protocols.Commons;
using org.acplt.oncrpc;
using org.acplt.oncrpc.server;
using System.Net;

/**
 */

namespace CDJNFSLibrary.Protocols.V2.RPC.Mount
{
    public abstract class NFSv2MountProtocolServerStub : OncRpcServerStub, OncRpcDispatchable
    {
        public NFSv2MountProtocolServerStub()
            : this(0)
        { }

        public NFSv2MountProtocolServerStub(int port)
            : this(null, port)
        { }

        public NFSv2MountProtocolServerStub(IPAddress bindAddr, int port)
        {
            info = new OncRpcServerTransportRegistrationInfo[] {
            new OncRpcServerTransportRegistrationInfo(NFSv2MountProtocol.MOUNTPROG, 1),
        };

            transports = new OncRpcServerTransport[] {
            new OncRpcUdpServerTransport(this, bindAddr, port, info, 32768),
            new OncRpcTcpServerTransport(this, bindAddr, port, info, 32768)
        };
        }

        public void dispatchOncRpcCall(OncRpcCallInformation call, int program, int version, int procedure)
        {
            if (version == 1)
            {
                switch (procedure)
                {
                    case 0:
                        {
                            call.retrieveCall(XdrVoid.XDR_VOID);
                            MOUNTPROC_NULL();
                            call.reply(XdrVoid.XDR_VOID);
                            break;
                        }
                    case 1:
                        {
                            Name args_ = new Name();
                            call.retrieveCall(args_);

                            MountStatus result_ = MOUNTPROC_MNT(args_);
                            call.reply(result_);

                            break;
                        }
                    case 2:
                        {
                            call.retrieveCall(XdrVoid.XDR_VOID);
                            MountList result_ = MOUNTPROC_DUMP();

                            call.reply(result_);

                            break;
                        }
                    case 3:
                        {
                            Name args_ = new Name();
                            call.retrieveCall(args_);

                            MOUNTPROC_UMNT(args_);
                            call.reply(XdrVoid.XDR_VOID);

                            break;
                        }
                    case 4:
                        {
                            call.retrieveCall(XdrVoid.XDR_VOID);
                            MOUNTPROC_UMNTALL();

                            call.reply(XdrVoid.XDR_VOID);

                            break;
                        }
                    case 5:
                        {
                            call.retrieveCall(XdrVoid.XDR_VOID);
                            Exports result_ = MOUNTPROC_EXPORT();

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

        public abstract void MOUNTPROC_NULL();

        public abstract MountStatus MOUNTPROC_MNT(Name arg1);

        public abstract MountList MOUNTPROC_DUMP();

        public abstract void MOUNTPROC_UMNT(Name arg1);

        public abstract void MOUNTPROC_UMNTALL();

        public abstract Exports MOUNTPROC_EXPORT();
    }

    // End of NFSv2MountProtocolServerStub.cs
}