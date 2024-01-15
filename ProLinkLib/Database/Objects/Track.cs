using ProLinkLib.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Database.Objects
{
    public class Track
    {
        public uint SampleRate;
        public uint FileSize;
        public uint TrackNumber;
        public uint Tempo;
        public uint ArtistID;
        public uint ArtworkID;
        public uint RekordboxID;
        public uint Year;

        public string TrackName;
        public string TrackPath;
        public string ArtWorkPath;
        public uint TrackChannelID;
        public uint TrackPhysicallyLocated;
        public CDJ cdj_location;


    }
}
