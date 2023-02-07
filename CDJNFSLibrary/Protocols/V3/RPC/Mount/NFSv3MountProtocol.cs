/*
 * Automatically generated by jrpcgen 1.0.7 on 27/08/2010
 * jrpcgen is part of the "Remote Tea.Net" ONC/RPC package for C#
 * See http://remotetea.sourceforge.net for details
 */
/**
 * A collection of constants used by the "NFSv3MountProtocol" ONC/RPC program.
 */

namespace CDJNFSLibrary.Protocols.V3.RPC.Mount
{
    public class NFSv3MountProtocol
    {
        public const int MOUNTPROG = 100005;
        public const int MOUNTVERS = 3;

        public const int MOUNTPROC3_NULL = 0;
        public const int MOUNTPROC3_MNT = 1;
        public const int MOUNTPROC3_DUMP = 2;
        public const int MOUNTPROC3_UMNT = 3;
        public const int MOUNTPROC3_UMNTALL = 4;
        public const int MOUNTPROC3_EXPORT = 5;

        public const int MNTPATHLEN = 1024;
        public const int MNTNAMLEN = 255;
        public const int FHSIZE = 64;
    }

    // End of NFSv3MountProtocol.cs
}