using ProLinkLib.NFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.NFSMode.Commands
{
    public class LSCommand : ICommand
    {
        public void Run(NFSCommandLineController nfs_clc, NFSController nfs, string args, bool isRekordbox)
        {
            string currentDir = nfs_clc.GetCurrentDirectory();

            // If current Dir is empty, it means we are in root directory so show devices instead of files
            if(currentDir == "")
            {
                List<string> devices = nfs.GetExportedDevices(isRekordbox);
                foreach(string device in devices)
                {
                    Console.WriteLine(device);
                }
            }
            else
            {
                List<string> items = nfs.GetItemsFromDirectory(currentDir, isRekordbox);
                foreach (string item in items)
                {
                    IObject obj = nfs.GetItemProperties(currentDir, item, isRekordbox);
                    if (obj != null)
                    {
                        if (obj is File)
                        {
                            File file = (File)obj;
                            Console.WriteLine("-r--r--r-- X root root    " + file.Size + " " + file.CreationDate + "    " + item);
                        }
                        if (obj is Folder)
                        {
                            Folder folder = (Folder)obj;
                            Console.WriteLine("dr--r--r-- X root root    " + folder.Size + " " + folder.CreationDate + "     " + item);
                        }
                    }
                }
                
            }
        }
    }
}
