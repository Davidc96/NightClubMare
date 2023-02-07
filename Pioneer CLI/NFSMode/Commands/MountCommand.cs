using ProLinkLib.NFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.NFSMode.Commands
{
    public class MountCommand : ICommand
    {
        public void Run(NFSCommandLineController nfs_clc, NFSController nfs, string args, bool isRekordbox)
        {
            var unit = args;
            nfs.MountDevice(unit, isRekordbox);
            nfs_clc.SetUnit(unit);
            
            Console.WriteLine(unit + " Mounted!");
        }
    }
}
