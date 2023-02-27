using ProLinkLib.Database;
using ProLinkLib.Database.Objects;
using ProLinkLib.Network.TCP;
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


        public TrackMetadata()
        {
            mt_client = new TrackMetadataClient();

        }

        public void InitTrackMetadataConnection(string IP)
        {
            mt_client.ConnectToDB(IP);
        }

        public List<Track> GetAllTracks(string IP, byte ChannelID, byte TrackChannelID, byte TrackPhysicallyLocated, byte TrackType, bool isRekordbox = false)
        {
            mt_client.ConnectToDB(IP);
            mt_client.SendHello(ChannelID, isRekordbox);
            return mt_client.GetAllTracks(ChannelID, TrackChannelID, TrackPhysicallyLocated, TrackType, isRekordbox);
            // mt_client.Disconnect();
        }
    
    }
}
