using Pioneer_CLI.Devices;
using ProLinkLib;
using ProLinkLib.Database.Objects;
using ProLinkLib.NFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.DBManager
{
    public class DBManagerController
    {
        MetadataDB metadata;
        NFSController nfs;
        CDJ connected_device;
        ProLinkController prolink;
        bool db_loaded;

        public DBManagerController(ProLinkController plc)
        {
            metadata = new MetadataDB();
            connected_device = null;
            db_loaded = false;
            nfs = new NFSController();
            prolink = plc;
        }

        public MetadataDB GetMetadataDB()
        {
            return metadata;
        }

        public bool IsLoaded()
        {
            return db_loaded;
        }

        public void SetLoaded(bool flag)
        {
            db_loaded = flag;
        }

        public CDJ GetConnectedDevice()
        {
            return connected_device;
        }

        public void ConnectDB(CDJ connected_device)
        {
            nfs.Connect(connected_device.IpAddress, false);

            if (connected_device.UsbLocalStatus == 0x00)
            {
                nfs.MountDevice("/C/", false);
                nfs.GetRekordboxDB();
                metadata.GetElementsFromRekordboxDB("db\\database.pdb", (byte)connected_device.ChannelID, (byte)0x03);
                this.connected_device = connected_device;
                this.db_loaded = true;
                return;

            }

            if (connected_device.SdLocalStatus == 0x00)
            {
                nfs.MountDevice("/U/", false);
                nfs.GetRekordboxDB();
                metadata.GetElementsFromRekordboxDB("db\\database.pdb", (byte)connected_device.ChannelID, (byte)0x02);
                this.connected_device = connected_device;
                this.db_loaded = true;
                return;
            }
        }

        public void LoadDatabaseLocally(string db_location)
        {
            metadata.GetElementsFromRekordboxDB(db_location, 0, 0);
        }

        public ProLinkController GetProLinkController()
        {
            return prolink;
        }

        public void DisconnectDB()
        {
            connected_device = null;
            db_loaded = false;
        }

        public void DownloadTrack(string path)
        {
            if (connected_device.UsbLocalStatus == 0x00)
            {
                nfs.MountDevice("/C/", false);
                nfs.DownloadFile(path);
                return;
            }

            if (connected_device.SdLocalStatus == 0x00)
            {
                nfs.MountDevice("/U/", false);
                nfs.DownloadFile(path);
                return;
            }
        }

        // Download artwork and get the content in base64
        public string DownloadArtWork(string path)
        {
            if (connected_device.UsbLocalStatus == 0x00)
            {
                nfs.MountDevice("/C/", false);
                nfs.DownloadFile(path);
            }

            else if (connected_device.SdLocalStatus == 0x00)
            {
                nfs.MountDevice("/U/", false);
                nfs.DownloadFile(path);
            }

            string[] path_splitted = path.Split('/');
            string file_name = path_splitted[path_splitted.Length - 1];
            string base64content = "";

            if(System.IO.File.Exists("db\\" + file_name))
            {
                base64content = Convert.ToBase64String(System.IO.File.ReadAllBytes("db\\" + file_name));
            }

            return base64content;

        }


    }
}
