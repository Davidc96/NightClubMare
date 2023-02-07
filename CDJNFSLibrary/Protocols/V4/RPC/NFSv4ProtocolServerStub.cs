/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */

namespace CDJNFSLibrary.Protocols.V4.RPC
{
    using org.acplt.oncrpc;
    using org.acplt.oncrpc.server;
    using System.Net;

    /**
     */

    public abstract class NFSv4ProtocolServerStub : OncRpcServerStub, OncRpcDispatchable
    {
        public NFSv4ProtocolServerStub() : this(0)
        {
        }

        public NFSv4ProtocolServerStub(int port) : this(null, port)
        {
        }

        public NFSv4ProtocolServerStub(IPAddress bindAddr, int port)
        {
            info = new OncRpcServerTransportRegistrationInfo[] {
            new OncRpcServerTransportRegistrationInfo(NFSv4Protocol.NFS4_PROGRAM, 4),
        };
            transports = new OncRpcServerTransport[] {
            new OncRpcUdpServerTransport(this, bindAddr, port, info, 32768),
            new OncRpcTcpServerTransport(this, bindAddr, port, info, 32768)
        };
        }

        public void dispatchOncRpcCall(OncRpcCallInformation call, int program, int version, int procedure)
        {
            if (version == 4)
            {
                switch (procedure)
                {
                    case 0:
                        {
                            call.retrieveCall(XdrVoid.XDR_VOID);
                            NFSPROC4_NULL_4();
                            call.reply(XdrVoid.XDR_VOID);
                            break;
                        }
                    case 1:
                        {
                            COMPOUND4args args_ = new COMPOUND4args();
                            call.retrieveCall(args_);
                            COMPOUND4res result_ = NFSPROC4_COMPOUND_4(args_);
                            call.reply(result_);
                            break;
                        }
                    default:
                        call.failProcedureUnavailable();
                        break;
                }
            }
            else
            {
                call.failProgramUnavailable();
            }
        }

        public abstract void NFSPROC4_NULL_4();

        public abstract COMPOUND4res NFSPROC4_COMPOUND_4(COMPOUND4args arg1);
    }
} // End of NFSv4ProtocolServerStub.cs