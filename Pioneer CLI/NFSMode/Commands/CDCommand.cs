using ProLinkLib.NFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.NFSMode.Commands
{
    public class CDCommand : ICommand
    {
        public void Run(NFSCommandLineController nfs_clc, NFSController nfs, string args, bool isRekordbox)
        {
            string new_folder = args;
            if(new_folder != "")
            {
                var next_directory = nfs.Combine(new_folder, nfs_clc.GetCurrentDirectory(), isRekordbox);
                nfs_clc.SetCurrentDirectory(next_directory);
            }
        }
    }
}
