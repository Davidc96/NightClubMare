using ProLinkLib.Database.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib
{
    public class MetadataDB
    {
        private List<Track> tracks;
        private Dictionary<uint, Artist> artists;

        private TrackMetadata trackMetadata;
        private Database.RekordboxDB rekordboxDB;

        private int device_metadata_location;

        public MetadataDB() 
        {
            tracks = new List<Track>();
            artists = new Dictionary<uint, Artist>();
            device_metadata_location = 0;

            trackMetadata = new TrackMetadata();
            rekordboxDB = new Database.RekordboxDB();
        }
        public int GetDeviceMetadataLocation()
        {
            return device_metadata_location;
        }

        public void SetDeviceMetadataLocation(int location)
        {
            device_metadata_location = location;
        }

        public void GetElementsFromRekordboxDB(string file_db, byte TrackChannelID, byte TrackPhysicallyLocated)
        {
            rekordboxDB.InitDBFromFile(file_db, TrackChannelID, TrackPhysicallyLocated);
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
    } 
}
