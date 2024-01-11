using ProLinkLib;
using ProLinkLib.Database.Objects;
using ProLinkLib.Devices;
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

        public bool ConnectDB(CDJ connected_device)
        {
            return metadata.ConnectDB(connected_device);
        }

        public void LoadDatabaseLocally(string db_location)
        {
            metadata.GetElementsFromRekordboxDB(db_location, 0, 0, null);
        }

        public ProLinkController GetProLinkController()
        {
            return prolink;
        }

        public void DisconnectDB()
        {
            // Check if db is not loaded locally
            if(connected_device != null)
            {
                metadata.DisconnectDB();
            }

            connected_device = null;
            db_loaded = false;

        }
    }
}
