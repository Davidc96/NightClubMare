using ConsoleTables;
using ProLinkLib;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.DBManager.DBCommands
{
    public class ShowCommand : IDBCommand
    {
        public void Execute(DBManagerController db_manager, string args)
        {
            string command = args;
           
            switch(command)
            {
                case "tracks":
                    PrintAllTracks(db_manager);
                    break;
                case "artists":
                    PrintAllArtists(db_manager);
                    break;
                case "playlists":
                    Console.WriteLine("Not implemented yet!"); // TODO
                    break;
                default:
                    Console.WriteLine("Usage \"show <tracks|artists|playlists>\"");
                    break;
            }
        }

        public void PrintAllTracks(DBManagerController db_manager)
        {
            var tracks = db_manager.GetMetadataDB().GetTracks();
            ConsoleTable table = new ConsoleTable("ID", "TrackName", "Artist", "BPM");
            var id = 0;
            foreach (var track in tracks)
            {
                if (id != 0)
                    table.AddRow(id, track.TrackName, db_manager.GetMetadataDB().GetArtistById(track.ArtistID).ArtistName, (track.Tempo / 100.0));

                id += 1;
            }

            table.Write();
        }

        public void PrintAllArtists(DBManagerController db_manager)
        {
            var artists = db_manager.GetMetadataDB().GetArtists();
            ConsoleTable table = new ConsoleTable("ID", "Name");
            foreach(var artist in artists)
            {
                table.AddRow(artist.ArtistID, artist.ArtistName);
            }

            table.Write();
        }
    }
}
