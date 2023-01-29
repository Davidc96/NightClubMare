﻿using ConsoleTables;
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
            
            Console.WriteLine("Let's wait 5s to reflect the changes into the network");
            vcdj.StopTasks();
            vcdj.ConnectToTheNetwork();
            Task.Delay(5000);
            Console.WriteLine("Retrieving tracks, this operation will take a while...");
            Console.WriteLine("[WARNING] This operation will show only unique name songs so maybe there are less than expected!");
            foreach(IDevice device in devices.Values)
            {
                if((device is CDJ) || device.GetDeviceName().Contains("rekordbox"))
                {
                    if(device.GetDeviceName().Contains("rekordbox"))
                    {
                        Console.WriteLine("Rekordbox found! Getting tracks");
                        // Rekordbox Tracklist found
                        Console.WriteLine("Rekordbox tracks (Maximum 100):");
                        
                        plc.GetTrackMetadata().InitTrackMetadataConnection(device.GetIPAddress());
                        plc.GetTrackMetadata().GetAllTracks(device.GetIPAddress(), (byte)device.GetChannelID(), vcdj.ChannelID, 0x04, 0x01);

                        var table = new ConsoleTable("ID", "Track Name");
                        for (int i = 1; i < plc.GetTrackMetadata().GetNumberOfTracks() + 1; i++)
                        {
                            var trck = plc.GetTrackMetadata().GetTrackByID(i);
                            table.AddRow(i, trck.TitleName);
                        }

                        table.Write();
                    }
                    else
                    {
                        var cdj = (CDJ)device;
                        // Get Tracks from USB or SD Card only
                        if(cdj.UsbLocalStatus == 0x00 || cdj.SdLocalStatus == 0x00)
                        {
                            Console.WriteLine("CDJ ID: " + cdj.GetChannelID() + " tracks (Maximum 100)");
                            
                            plc.GetTrackMetadata().InitTrackMetadataConnection(device.GetIPAddress());
                            plc.GetTrackMetadata().GetAllTracks(device.GetIPAddress(), (byte)device.GetChannelID(), vcdj.ChannelID, (cdj.UsbLocalStatus == 0x00) ? (byte)0x03 : (byte)0x02, 0x01);
                            
                            var table = new ConsoleTable("ID", "Track Name");
                            for (int i = 1; i < plc.GetTrackMetadata().GetNumberOfTracks() + 1; i++)
                            {
                                var trck = plc.GetTrackMetadata().GetTrackByID(i);
                                table.AddRow(i, trck.TitleName);
                            }

                            table.Write();
                        }
                    }
                }
            }
            
        }
    }
}