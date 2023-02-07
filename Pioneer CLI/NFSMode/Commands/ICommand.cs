using ProLinkLib.NFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.NFSMode.Commands
{
    public interface ICommand
    {
        void Run(NFSCommandLineController nfs_clc, NFSController nfs, string args, bool isRekordbox);
    }
}
