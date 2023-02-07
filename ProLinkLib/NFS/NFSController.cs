using CDJNFSLibrary;
using RekordboxNFSLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.NFS
{
    public class NFSController
    {
        private CDJNFSLibrary.NfsClient cdj_nfs_client;
        private RekordboxNFSLibrary.NfsClient rb_nfsclient;

        public NFSController()
        {
            // Rekordbox uses RPC port 50111 and CDJ uses RPC port 111
            cdj_nfs_client = new CDJNFSLibrary.NfsClient(CDJNFSLibrary.NfsClient.NfsVersion.V2);
            rb_nfsclient = new RekordboxNFSLibrary.NfsClient(RekordboxNFSLibrary.NfsClient.NfsVersion.V2);
        }

        public void Connect(string IP, bool isRekordbox)
        {
            if(isRekordbox)
            {
                rb_nfsclient.Connect(IPAddress.Parse(IP), 0, 0, 1000, Encoding.Unicode, false, false);
            }
            else
            {
                cdj_nfs_client.Connect(IPAddress.Parse(IP), 0, 0, 1000, Encoding.Unicode, false, false);
            }
        }

        public List<string> GetExportedDevices(bool isRekordbox)
        {
            if(isRekordbox)
            {
                return rb_nfsclient.GetExportedDevices();
            }
            else
            {
                return cdj_nfs_client.GetExportedDevices();
            }
        }

        public void MountDevice(string deviceName, bool isRekordbox)
        {
            if(isRekordbox)
            {
                rb_nfsclient.MountDevice(deviceName);
            }
            else
            {
                cdj_nfs_client.MountDevice(deviceName);
            }
        }

        public List<string> GetItemsFromDirectory(string remoteFolder, bool isRekordbox)
        {
            List<string> Items = new List<string>();
            
            if(isRekordbox)
            {
                Items = rb_nfsclient.GetItemList(remoteFolder);  
            }
            else
            {
                Items = cdj_nfs_client.GetItemList(remoteFolder);
            }

            Items = RemoveDuplicatesAndInvalid(Items);
            Items.Insert(0, "..");

            return Items;
        }

        public bool IsMounted(bool isRekordbox)
        {
            return (isRekordbox == true) ? rb_nfsclient.IsMounted : cdj_nfs_client.IsMounted;
        }

        public string Combine(string filename, string directory_full, bool isRekordbox)
        {
            return (isRekordbox == true) ? rb_nfsclient.Combine(filename, directory_full) : cdj_nfs_client.Combine(filename, directory_full);
        }

        public IObject GetItemProperties(string path, string item, bool isRekordbox)
        {
            if(isRekordbox)
            {
                RekordboxNFSLibrary.Protocols.Commons.NFSAttributes nfs_atr = rb_nfsclient.GetItemAttributes(rb_nfsclient.Combine(item, path));
                if (nfs_atr != null)
                {
                    if (nfs_atr.NFSType == RekordboxNFSLibrary.Protocols.Commons.NFSItemTypes.NFDIR)
                    {
                        Folder dir = new Folder();
                        dir.FolderName = item;
                        dir.Size = nfs_atr.Size;
                        dir.CreationDate = nfs_atr.CreateDateTime.ToString();

                        return dir;
                    }

                    if (nfs_atr.NFSType == RekordboxNFSLibrary.Protocols.Commons.NFSItemTypes.NFREG)
                    {
                        File file = new File();
                        file.FileName = item;
                        file.Size = nfs_atr.Size;
                        file.CreationDate = nfs_atr.CreateDateTime.ToString();
                        file.ModifiedDate = nfs_atr.ModifiedDateTime.ToString();
                        file.LastAccessed = nfs_atr.LastAccessedDateTime.ToString();

                        return file;
                    }
                }
                else
                {
                    File file = new File();
                    file.FileName = item;
                    file.Size = 0;
                    file.CreationDate = "1999-99-99 99:99:99";
                    file.ModifiedDate = "1999-99-99 99:99:99";
                    file.LastAccessed = "1999-99-99 99:99:99";

                    return file;
                }
            }
            else
            {
                CDJNFSLibrary.Protocols.Commons.NFSAttributes nfs_atr = cdj_nfs_client.GetItemAttributes(cdj_nfs_client.Combine(path, item));
                if (nfs_atr != null)
                {
                    if (nfs_atr.NFSType == CDJNFSLibrary.Protocols.Commons.NFSItemTypes.NFDIR)
                    {
                        Folder dir = new Folder();
                        dir.FolderName = item;
                        dir.Size = nfs_atr.Size;
                        dir.CreationDate = nfs_atr.CreateDateTime.ToString();

                        return dir;
                    }

                    if (nfs_atr.NFSType == CDJNFSLibrary.Protocols.Commons.NFSItemTypes.NFREG)
                    {
                        File file = new File();
                        file.FileName = item;
                        file.Size = nfs_atr.Size;
                        file.CreationDate = nfs_atr.CreateDateTime.ToString();
                        file.ModifiedDate = nfs_atr.ModifiedDateTime.ToString();
                        file.LastAccessed = nfs_atr.LastAccessedDateTime.ToString();

                        return file;
                    }
                }
                else
                {
                    File file = new File();
                    file.FileName = item;
                    file.Size = 0;
                    file.CreationDate = "1999-99-99 99:99:99";
                    file.ModifiedDate = "1999-99-99 99:99:99";
                    file.LastAccessed = "1999-99-99 99:99:99";

                    return file;
                }

            }
            return null;
        }

        public void GetRekordboxDB(string export_path)
        {
            // Delete old database
            if (System.IO.File.Exists("db\\database.pdb"))
            {
                System.IO.File.Delete("db\\database.pdb");
            }

            cdj_nfs_client.Read(export_path, "db\\database.pdb");
        }

        private List<string> RemoveDuplicatesAndInvalid(List<string> items)
        {
            List<string> outputList = new List<string>();
            foreach (string item in items)
            {
                string cleanItem = item.Trim();
                if (!outputList.Contains(cleanItem) && cleanItem != ".." && cleanItem != "." && !cleanItem.Contains(".."))
                    outputList.Add(item);
            }
            return outputList;
        }

    }
}
