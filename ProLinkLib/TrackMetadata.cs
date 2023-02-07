using ProLinkLib.Network.TCP;
using ProLinkLib.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib
{
    public class TrackMetadata
    {
        private TrackMetadataClient mt_client;
        private Dictionary<int, Track> tracks;

        public TrackMetadata()
        {
            mt_client = new TrackMetadataClient();
            tracks    = new Dictionary<int, Track>();
        }

        public void InitTrackMetadataConnection(string IP)
        {
            mt_client.ConnectToDB(IP);
        }

        public void GetAllTracks(string IP, byte ChannelID, byte TrackChannelID, byte TrackPhysicallyLocated, byte TrackType, bool isRekordbox = false)
        {
            mt_client.ConnectToDB(IP);
            mt_client.SendHello(ChannelID, isRekordbox);
            tracks = mt_client.GetAllTracks(ChannelID, TrackChannelID, TrackPhysicallyLocated, TrackType, isRekordbox);
            // mt_client.Disconnect();
        }

        public Track GetTrackByID(int id)
        {
            if (tracks.ContainsKey(id))
            {
                return null;
            }

            return tracks[id];
        }

        public int GetNumberOfTracks()
        {
            return tracks.Values.Count;
        }
    
    }
}
