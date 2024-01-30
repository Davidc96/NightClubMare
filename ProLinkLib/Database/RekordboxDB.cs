using ProLinkLib.Database.Objects;
using ProLinkLib.Devices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProLinkLib.Database.RekordboxPdb;

namespace ProLinkLib.Database
{
    public class RekordboxDB
    {
        private RekordboxPdb rekordbox;
        private List<Track> trackTable;
        private Dictionary<uint, Artist> artistTable; 


        public RekordboxDB()
        {
            trackTable = new List<Track>();
            artistTable = new Dictionary<uint, Artist>();


        }

        public void InitDBFromFile(string file_db, byte TrackChannelID, byte TrackPhysicallyLocated, CDJ cdj_location)
        {
            Logger.WriteLogFile("DB File - Logs", Logger.LOG_TYPE.INFO, "RekordboxDB::InitDBFromFile Reading Rekordbox DB File: " + file_db + "\nChannelID: " + TrackChannelID + "\nTrackPhysicallyLocated: " + TrackPhysicallyLocated);
            // Create empty value as first element
            trackTable.Add(new Track());
            artistTable.Add(0, new Artist());
            
            // Retrieve all tracks
            rekordbox = RekordboxPdb.FromFile(file_db);
            // TODO: Close file

            var tracksTable = rekordbox.Tables[Constants.TRACKS];
            var firstElement = tracksTable.FirstPage.Body.NextPage.Body; // Always the very first element is empty
            var currentElement = firstElement;
            try
            {
                while (currentElement != null)
                {
                    foreach (var row_group in currentElement.RowGroups)
                    {
                        foreach (var row in row_group.Rows)
                        {
                            Track track = new Track();
                            var track_info = (TrackRow)row.Body;
                            if (track_info != null)
                            {
                                track.ArtistID = track_info.ArtistId;
                                track.FileSize = track_info.FileSize;
                                track.RekordboxID = track_info.Id;
                                track.Tempo = track_info.Tempo;
                                track.TrackNumber = track_info.TrackNumber;
                                track.Year = track_info.Year;

                                if (track_info.Title.Body is DeviceSqlLongAscii)
                                {
                                    track.TrackName = ((DeviceSqlLongAscii)track_info.Title.Body).Text;
                                }
                                else if (track_info.Title.Body is DeviceSqlShortAscii)
                                {
                                    track.TrackName = ((DeviceSqlShortAscii)track_info.Title.Body).Text;
                                }
                                else if (track_info.Title.Body is DeviceSqlLongUtf16le)
                                {
                                    track.TrackName = ((DeviceSqlLongUtf16le)track_info.Title.Body).Text;
                                }

                                if (track_info.FilePath.Body is DeviceSqlLongAscii)
                                {
                                    track.TrackPath = ((DeviceSqlLongAscii)track_info.FilePath.Body).Text;
                                }
                                else if (track_info.FilePath.Body is DeviceSqlShortAscii)
                                {
                                    track.TrackPath = ((DeviceSqlShortAscii)track_info.FilePath.Body).Text;
                                }
                                else if (track_info.FilePath.Body is DeviceSqlLongUtf16le)
                                {
                                    track.TrackPath = ((DeviceSqlLongUtf16le)track_info.FilePath.Body).Text;
                                }

                                track.TrackPath = track.TrackPath.Replace("-1", ""); // TO BE OBSERVED
                                track.TrackChannelID = TrackChannelID;
                                track.TrackPhysicallyLocated = TrackPhysicallyLocated;
                                track.ArtworkID = track_info.ArtworkId;

                                track.ArtWorkPath = GetArtworkPath(track.ArtworkID);
                                track.cdj_location = cdj_location;

                                trackTable.Add(track);
                            }
                        }
                    }

                    currentElement = currentElement.NextPage.Body;
                }
            }
            catch(EndOfStreamException)
            {
                Console.WriteLine(trackTable.Count + " Tracks loaded!");
                Logger.WriteLogFile("DB File - Logs", Logger.LOG_TYPE.INFO, trackTable.Count + " Tracks loaded!");
            }

            // Artists
            var artiststable = rekordbox.Tables[Constants.ARTISTS];
            firstElement = artiststable.FirstPage.Body.NextPage.Body;
            currentElement = firstElement;

            try 
            { 
                while(currentElement != null)
                {
                    foreach (var row_group in currentElement.RowGroups)
                    {
                        foreach (var row in row_group.Rows)
                        {
                            Artist artist = new Artist();
                            var artist_info = (ArtistRow)row.Body;
                            if (artist_info != null)
                            {
                                if (!artistTable.ContainsKey(artist_info.Id))
                                {
                                    artist.ArtistID = artist_info.Id;
                                    if (artist_info.Name.Body is DeviceSqlLongAscii)
                                    {
                                        artist.ArtistName = ((DeviceSqlLongAscii)artist_info.Name.Body).Text;
                                    }
                                    else if (artist_info.Name.Body is DeviceSqlLongUtf16le)
                                    {
                                        artist.ArtistName = ((DeviceSqlLongUtf16le)artist_info.Name.Body).Text;
                                    }
                                    else if (artist_info.Name.Body is DeviceSqlShortAscii)
                                    {
                                        artist.ArtistName = ((DeviceSqlShortAscii)artist_info.Name.Body).Text;
                                    }
                                    else if (artist_info.Name.Body is DeviceSqlString)
                                    {
                                        artist.ArtistName = ((DeviceSqlShortAscii)((DeviceSqlString)artist_info.Name.Body).Body).Text;
                                    }

                                    artistTable.Add(artist_info.Id, artist);
                                }
                            }
                        }
                    }
                    currentElement = currentElement.NextPage.Body;
                }
            }
            catch(EndOfStreamException)
            {
                Console.WriteLine(artistTable.Count + " Artists loaded!");
                Logger.WriteLogFile("DB File - Logs", Logger.LOG_TYPE.INFO, artistTable.Count + " Artists loaded!");
            }
            // Next table

        }

        public List<Track> GetAllTracks()
        {
            return trackTable;
        }

        public Dictionary<uint, Artist> GetArtists()
        {
            return artistTable;
        }

        public void ClearTables()
        {
            trackTable.Clear();
            artistTable.Clear();
        }

        private string GetArtworkPath(uint artwork_id)
        {
            var artworkTable = rekordbox.Tables[Constants.ARTWORK];
            var firstElement = artworkTable.FirstPage.Body.NextPage.Body;
            var currentElement = firstElement;

            if (artwork_id == 0)
            {
                Logger.WriteLogFile("DB File - Logs", Logger.LOG_TYPE.WARNING, "No Artwork found... skipping....");
                return "";
            }

            try
            {
                while(currentElement != null)
                {
                    foreach (var row_group in currentElement.RowGroups)
                    {
                        foreach (var row in row_group.Rows)
                        {
                            var artwork = (ArtworkRow)row.Body;
                            if (artwork != null)
                            {
                                if (artwork.Id == artwork_id)
                                {
                                    if (artwork.Path.Body is DeviceSqlLongAscii)
                                    {
                                        return ((DeviceSqlLongAscii)artwork.Path.Body).Text;
                                    }
                                    else if (artwork.Path.Body is DeviceSqlLongUtf16le)
                                    {
                                        return ((DeviceSqlLongUtf16le)artwork.Path.Body).Text;
                                    }
                                    else if (artwork.Path.Body is DeviceSqlShortAscii)
                                    {
                                        return ((DeviceSqlShortAscii)artwork.Path.Body).Text;
                                    }
                                    else if (artwork.Path.Body is DeviceSqlString)
                                    {
                                        return ((DeviceSqlShortAscii)((DeviceSqlString)artwork.Path.Body).Body).Text;
                                    }
                                    //return (artwork.Path.Body).ToString();
                                }
                            }
                        }
                    }
                    currentElement = currentElement.NextPage.Body;
                }
            }
            catch (Exception)
            {
                Logger.WriteLogFile("DB File - Logs", Logger.LOG_TYPE.WARNING, "No Artwork found... skipping....");
            }

            return "";
        }

    }
}
