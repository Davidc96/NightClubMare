using ConsoleTables;
using Pioneer_CLI.Devices;
using ProLinkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.Commands
{
    public class MusicCommand : ICommand
    {
        public void BuildCustom(ProLinkController plc, CommandLineController clc, string args)
        {
            Console.WriteLine("[INFO] TCP Packets are not available to customize");
        }

        public void Run(ProLinkController plc, CommandLineController clc, string args)
        {
            VirtualCDJ vcdj = plc.GetVirtualCDJ();
            var devices = plc.GetDevices();

            if(vcdj.ChannelID != 0x04)
            {
                for(byte i =  1; i <= 4; i++)
                {
                    if (!devices.Keys.Contains(i))
                    {
                        vcdj.ChannelID = i;
                    }
                }

                if(vcdj.ChannelID > 4)
                {
                    Console.WriteLine("[INFO] Cannot get music from CDJ because there are 4 CDJ connected");
                    return;
                }
            }

            // If there are tracks, let's print it instead of 
            if (clc.GetMetadataDB().GetTracks().Count > 0 && !args.Contains("reset"))
            {
                var table = new ConsoleTable("ID", "Track Name");
                for (int i = 1; i < clc.GetMetadataDB().GetTracks().Count; i++)
                {
                    var trck = clc.GetMetadataDB().GetTrackById(i);
                    table.AddRow(i, trck.TrackName);
                }

                table.Write();
            }
            else
            {
                Console.WriteLine("Let's wait 5s to reflect the changes into the network");
                vcdj.StopTasks();
                vcdj.ConnectToTheNetwork();
                Task.Delay(5000);
                Console.WriteLine("Retrieving tracks, this operation will take a while...");
                Console.WriteLine("[WARNING] This operation will show only unique name songs so maybe there are less than expected!");
                foreach (IDevice device in devices.Values)
                {
                    if ((device is CDJ) || device.GetDeviceName().Contains("rekordbox"))
                    {
                        if (device.GetDeviceName().Contains("rekordbox"))
                        {
                            Console.WriteLine("Rekordbox found! Getting tracks");
                            // Rekordbox Tracklist found
                            Console.WriteLine("Rekordbox tracks (Maximum 100):");

                            clc.GetMetadataDB().GetTracksFromTCP(device.GetIPAddress(), vcdj.ChannelID, (byte)device.GetChannelID(), 0x04, 0x01, true); // Rekordbox handle messages diferent from CDJ

                            var table = new ConsoleTable("ID", "Track Name");
                            for (int i = 1; i < clc.GetMetadataDB().GetTracks().Count + 1; i++)
                            {
                                var trck = clc.GetMetadataDB().GetTrackById(i-1);
                                table.AddRow(i, trck.TrackName);
                            }

                            table.Write();
                        }
                        else
                        {
                            var cdj = (CDJ)device;
                            // Get Tracks from USB or SD Card only
                            if (cdj.UsbLocalStatus == 0x00 || cdj.SdLocalStatus == 0x00)
                            {
                                Console.WriteLine("CDJ ID: " + cdj.GetChannelID() + " tracks (Maximum 100)");

                                clc.GetMetadataDB().GetTracksFromTCP(device.GetIPAddress(), vcdj.ChannelID, (byte)device.GetChannelID(), (cdj.UsbLocalStatus == 0x00) ? (byte)0x03 : (byte)0x02, 0x01, false);

                                var table = new ConsoleTable("ID", "Track Name");
                                for (int i = 1; i < clc.GetMetadataDB().GetTracks().Count + 1; i++)
                                {
                                    var trck = clc.GetMetadataDB().GetTrackById(i - 1);
                                    table.AddRow(i, trck.TrackName);
                                }

                                table.Write();
                            }
                        }
                    }
                }
            }
        }
    }
}
