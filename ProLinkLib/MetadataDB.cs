using Newtonsoft.Json;
using ProLinkLib.Database.Objects;
using ProLinkLib.Devices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib
{
    /**
     * Class which contains all related to obtain and classify rekordbox elements
     * This class also connects to the NFS CDJ system to retrieve the database and specific tracks and artists cover
     */
    public class MetadataDB
    {
        private List<Track> tracks;
        private Dictionary<uint, Artist> artists;

        private TrackMetadata trackMetadata;
        private Database.RekordboxDB rekordboxDB;
        private NFS.NFSController nfs;
        private CDJ connected_device;

        private int device_metadata_location;
        private bool is_offlinemode = false;

        public MetadataDB() 
        {
            tracks = new List<Track>();
            artists = new Dictionary<uint, Artist>();
            device_metadata_location = 0;

            trackMetadata = new TrackMetadata();
            rekordboxDB = new Database.RekordboxDB();
            connected_device = null;
        }

        #region RekordboxDB Element Getters
        public void GetElementsFromRekordboxDB(string file_db, byte TrackChannelID, byte TrackPhysicallyLocated, CDJ cdj_location)
        {
            rekordboxDB.InitDBFromFile(file_db, TrackChannelID, TrackPhysicallyLocated, cdj_location);
            artists = rekordboxDB.GetArtists();

            var trks = rekordboxDB.GetAllTracks();

            // If the track is already listed, let's update the info
            foreach(Track track in trks)
            {
                if(tracks.Contains(track))
                {
                    foreach (Track track2 in tracks)
                    {
                        if (track.RekordboxID == track2.RekordboxID)
                        {
                            track2.ArtistID = track.ArtistID;
                            track2.FileSize = track.FileSize;
                            track2.Tempo = track.Tempo;
                            track2.TrackNumber = track.TrackNumber;
                            track2.Year = track.Year;
                            track2.TrackChannelID = TrackChannelID;
                            track2.TrackPhysicallyLocated = TrackPhysicallyLocated;
                        }
                    }
                }
                else
                {
                    tracks.Add(track);
                }
            }
        }

        public void GetTracksFromTCP(string IP, byte ChannelID, byte TrackChannelID, byte TrackPhysicallyLocated, byte TrackType, bool isRekordbox = false)
        {
            var trks = trackMetadata.GetAllTracks(IP, ChannelID, TrackChannelID, TrackPhysicallyLocated, TrackType, isRekordbox);

            // If the track is already listed, let's update the info
            foreach(Track track in trks)
            {
                if (tracks.Contains(track))
                {
                    foreach (Track track2 in tracks)
                    {
                        if (track.RekordboxID == track2.RekordboxID)
                        {
                            track2.TrackChannelID = track.TrackChannelID;
                            track2.TrackPhysicallyLocated = track.TrackPhysicallyLocated;
                        }
                    }
                }
                else
                {
                    tracks.Add(track);
                }
            }
        }
        #endregion

        #region Getters and Imports
        public void SetIsOfflineMode(bool isOfflineMode)
        {
            this.is_offlinemode = isOfflineMode;
        }

        public bool IsOfflineMode()
        {
            return is_offlinemode;
        }

        public int GetDeviceMetadataLocation()
        {
            return device_metadata_location;
        }

        public void SetDeviceMetadataLocation(int location)
        {
            device_metadata_location = location;
        }
        public string GetTrackListJSON()
        {
            return JsonConvert.SerializeObject(tracks);    
        }

        public void ImportTrackListJson(string json)
        {
            tracks = JsonConvert.DeserializeObject<List<Track>>(json);
        }

        public Track GetTrackById(int id)
        {
            if(tracks.Count > id)
            {
                return tracks[id];
            }
            else
            {
                return null;
            }
        }
        public Track GetTrackByRBId(uint rb_id)
        {
            foreach (var track in tracks)
            {
                if (track.RekordboxID == rb_id)
                {
                    return track;
                }
            }

            return null;
        }
        public Artist GetArtistById(uint id)
        {
            return artists[id];
        }

        public List<Track> GetTracks()
        {
            return tracks;
        }

        public List<Artist> GetArtists()
        {
            return artists.Values.ToList();
        }
        #endregion

        #region NFS Connection and Downloads
        public bool ConnectDB(CDJ connected_device)
        {
            nfs.Connect(connected_device.IpAddress, false);

            if (connected_device.UsbLocalStatus == 0x00)
            {
                nfs.MountDevice("/C/", false);
                nfs.GetRekordboxDB();
                GetElementsFromRekordboxDB("db\\database.pdb", (byte)connected_device.ChannelID, (byte)0x03, connected_device);
                this.connected_device = connected_device;
                return true;

            }

            if (connected_device.SdLocalStatus == 0x00)
            {
                nfs.MountDevice("/U/", false);
                nfs.GetRekordboxDB();
                GetElementsFromRekordboxDB("db\\database.pdb", (byte)connected_device.ChannelID, (byte)0x02, connected_device);
                this.connected_device = connected_device;
                return true;
            }

            return false;
        }

        public void DownloadTrack(Track track)
        {
            // Check if is connected to the cdj via nfs, if not, connect it.
            // Moreover: check if we are not in the offline mode (cdj_location != null)
            if(connected_device == null && track.cdj_location != null)
            {
                connected_device = track.cdj_location;
                ConnectDB(connected_device);
            }

            if (connected_device.UsbLocalStatus == 0x00)
            {
                nfs.MountDevice("/C/", false);
                nfs.DownloadFile(track.TrackPath);
                return;
            }

            if (connected_device.SdLocalStatus == 0x00)
            {
                nfs.MountDevice("/U/", false);
                nfs.DownloadFile(track.TrackPath);
                return;
            }
        }

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

            if (System.IO.File.Exists("db\\" + file_name))
            {
                base64content = Convert.ToBase64String(System.IO.File.ReadAllBytes("db\\" + file_name));
            }

            return base64content;

        }

        public void DisconnectDB()
        {
            if (connected_device != null)
            {
                nfs.UnmountDevice(false);
            }
        }
        #endregion
    } 
}
