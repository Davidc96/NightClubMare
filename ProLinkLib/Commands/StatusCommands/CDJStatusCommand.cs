using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.StatusCommands
{
    public class CDJStatusCommand : ICommand
    {
        public byte ID = 0x0A;
        public byte[] DeviceName = new byte[0x14];
        private byte Unknown = 0x01;
        private byte Unknown2 = 0x05;
        public byte ChannelID1;
        public ushort Length;
        public byte ChannelID2;
        private byte Unknown3 = 0x00;
        private byte Unknown4 = 0x01;
        public byte ActivityFlag;                               // 0x00 -> IDLE 0x01 -> Playing, Loading or Searching track
        public byte TrackDeviceLocatedID;                       // ChannelID if is in local, CDJ ChannelID where is located the track
        public byte TrackPhysicallyLocated;                     // 0x00 -> NO TRACK, 0x01 -> CD-Drive, 0x02 -> SD Slot, 0x03 -> USB Slot, 0x04 -> Laptop
        public byte TrackType;                                  // 0x00 -> No track loaded, 0x01 -> Rekordbox track, 0x02 -> Unanalized track loaded, 0x05 -> CD track loaded
        private byte Unknown5 = 0x00;
        public byte[] RekordboxTrackID = new byte[0x4];         // RekordboxTrackID
        public byte[] BlankBytes = new byte[0x2];
        public byte[] RekordBoxTrackListPos = new byte[0x2];
        public byte[] BlankBytes2 = new byte[0x3];
        public byte UnknownFeatureDl;
        private byte[] BlankBytes3 = new byte[0xE];
        public byte[] NumberTracksInPlaylist = new byte[0x2];
        private byte[] BlankBytes4 = new byte[0x22];
        public byte USBActivity;
        public byte SDActivity;
        private byte[] BlankBytes5 = new byte[0x3];
        public byte USBLocalStatus;                                // 0x04 -> No USB Media Loaded, 0x00 -> USB Loaded, 0x02 or 0x03 -> USB Stop button
        private byte[] BlankBytes6 = new byte[0x3];
        public byte SDLocalStatus;                                 // 0x04 -> No SD Media Loaded, 0x00 -> SD Loaded, 0x02 or 0x03 -> SD tap open and SD unmounted
        private byte BlankByte;
        public byte LinkAvailable;                                 // 0x01 -> True 0x00 False
        private byte[] BlankBytes7 = new byte[0x5];
        public byte PlayerStatus;                                  // 0x00 No track, 0x02 -> Track loading, 0x03 -> Play Normal, 0x04 -> Play Loop, 0x05 Player paused in other than cue point
                                                                   // 0x06 Player paused cue point, 0x07 Cue play in progres, 0x08 Cue scratch in progress, 0x09 -> Player searching
                                                                   // 0x0e Audio CD Shutdown 0x11 -> Reached End of the track
        public byte[] FirmwareVersion = new byte[0x4];             // ASCII Representation
        private byte[] BlankBytes8 = new byte[0x4];
        public byte[] SyncCounter = new byte[0x4];                 // Increase the counter when delegate Master Tempo
        private byte BlankByte2 = 0x00;
        public byte FlagStatus;                                    // bit 0 -> 0 bit 1 -> BPM  bit 2 -> 1 bit 3 -> On-Air bit4 -> SyncMode bit 5 -> Master bit 6 -> Play bit 7 -> 1
        public byte Unknown6 = 0xFF;
        public byte PlayerStatus2;                                 // 0x7A -> Playing normally, 0x7E Playing but not due to someone is holding the Jog
        public byte[] Pitch1 = new byte[0x4];                      // First Pitch Byte
        public byte[] BPMControl = new byte[0x2];                  // 80 00 -> Rekordbox track loaded 00 00 No rekordbox track loaded 7F FF No track loaded
        public byte[] BPMTrack = new byte[0x2];                    // (byte[0] * 256 + byte[1])
        private byte[] BlankBytes9 = new byte[0x4];
        public byte[] Pitch2 = new byte[0x4];                      // Second Pitch Byte
        private byte Unknown7 = 0x00;
        public byte PlayerModeStatus;                              // 0x00 No track loaded 0x01 Reverse mode or paused 0x09 Playing with Vinyl mode 0x0d Playing with CDJ mode
        public byte MasterMeaningful;                              // 0x00 Not Master 0x01 Master and track is playing 0x02 Master but no track playing
        public byte MasterHandoff;                                 // FF if is not master Other device ChannelID -> locate the master
        public byte[] Beat = new byte[0x4];
        public byte[] CueDistance = new byte[0x2];
        public byte BeatCounter;                                   // Beat Counter 01 02 03 04
        private byte[] BlankBytes10 = new byte[16];
        public byte MediaPresence;                                 // Works only on CDJ-3000
        public byte USBPresence;                                   // 0x01 -> Unsafe ejected 0x00 -> All ok
        public byte SDPresence;                                    // 0x01 -> SD unsafe ejected 0x00 -> All Ok
        public byte EmergencyLoop;                                 // 0x01 -> If emergency loop is active
        private byte[] BlankBytes11 = new byte[0x5];
        public byte[] Pitch3 = new byte[0x4];
        public byte[] Pitch4 = new byte[0x4];
        public byte[] PacketCounter = new byte[0x4];
        public byte IsNexus;                                        // 0x0f if is nexus 0x1f XDJ-XZ and CDJ-3000 0x05 older players
        public byte PreviewTrackSupport;                            // 0x20 is supported 0x00 No
        private byte[] BlankBytes12 = new byte[0x2];
        private byte[] UnknownBytes = new byte[0xA];                // Starts with 12 34 56 78
        public byte WaveColor;                                      // 0x01 -> Blue 0x03 -> RGB 0x04 -> 3-Band
        private byte Unknown8;
        private byte Unknown9;
        public byte WavePosition;                                   // 0x01 -> Center 0x02 -> Left
        private byte[] BlankBytes13 = new byte[0x50];

        // TODO ADD CDJ 3000 Support

        public byte[] RawData;
        public void FromBytes(byte[] packet)
        {
            using (BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
            {
                bin.BaseStream.Seek(0x0B, SeekOrigin.Begin);
                DeviceName = bin.ReadBytes(0x14);
                Unknown = bin.ReadByte();
                Unknown2 = bin.ReadByte();
                ChannelID1 = bin.ReadByte();
                Length = BitConverter.ToUInt16(Utils.SwapEndianesss(bin.ReadBytes(2)), 0);
                ChannelID2 = bin.ReadByte();
                Unknown3 = bin.ReadByte();
                Unknown4 = bin.ReadByte();
                ActivityFlag = bin.ReadByte();
                TrackDeviceLocatedID = bin.ReadByte();
                TrackPhysicallyLocated = bin.ReadByte();
                TrackType = bin.ReadByte();
                Unknown5 = bin.ReadByte();
                RekordboxTrackID = Utils.SwapEndianesss(bin.ReadBytes(0x4));
                BlankBytes = bin.ReadBytes(0x2);
                RekordBoxTrackListPos = Utils.SwapEndianesss(bin.ReadBytes(0x2));
                BlankBytes2 = bin.ReadBytes(0x3);
                UnknownFeatureDl = bin.ReadByte();
                BlankBytes3 = bin.ReadBytes(0xE);
                NumberTracksInPlaylist = bin.ReadBytes(0x2);
                BlankBytes4 = bin.ReadBytes(0x22);
                USBActivity = bin.ReadByte();
                SDActivity = bin.ReadByte();
                BlankBytes5 = bin.ReadBytes(0x3);
                USBLocalStatus = bin.ReadByte();
                BlankBytes6 = bin.ReadBytes(0x3);
                SDLocalStatus = bin.ReadByte();
                BlankByte = bin.ReadByte();
                LinkAvailable = bin.ReadByte();
                BlankBytes7 = bin.ReadBytes(0x5);
                PlayerStatus = bin.ReadByte();
                FirmwareVersion = bin.ReadBytes(0x4);
                BlankBytes8 = bin.ReadBytes(0x4);
                SyncCounter = bin.ReadBytes(0x4);
                BlankByte2 = bin.ReadByte();
                FlagStatus = bin.ReadByte();
                Unknown6 = bin.ReadByte();
                PlayerStatus2 = bin.ReadByte();
                Pitch1 = bin.ReadBytes(0x4);
                BPMControl = bin.ReadBytes(0x2);
                BPMTrack = bin.ReadBytes(0x2);
                BlankBytes9 = bin.ReadBytes(0x4);
                Pitch2 = bin.ReadBytes(0x4);
                Unknown7 = bin.ReadByte();
                PlayerModeStatus = bin.ReadByte();
                MasterMeaningful = bin.ReadByte();
                MasterHandoff = bin.ReadByte();
                Beat = bin.ReadBytes(0x4);
                CueDistance = bin.ReadBytes(0x2);
                BeatCounter = bin.ReadByte();
                BlankBytes10 = bin.ReadBytes(16);
                MediaPresence = bin.ReadByte();
                USBPresence = bin.ReadByte();
                SDPresence = bin.ReadByte();
                EmergencyLoop = bin.ReadByte();
                BlankBytes11 = bin.ReadBytes(0x5);
                Pitch3 = bin.ReadBytes(0x4);
                Pitch4 = bin.ReadBytes(0x4);
                PacketCounter = Utils.SwapEndianesss(bin.ReadBytes(0x4));
                IsNexus = bin.ReadByte();
                PreviewTrackSupport = bin.ReadByte();
                UnknownBytes = bin.ReadBytes(0xA);
                WaveColor = bin.ReadByte();
                Unknown8 = bin.ReadByte();
                Unknown9 = bin.ReadByte();
                WavePosition = bin.ReadByte();
                BlankBytes13 = bin.ReadBytes(0x50);
            }

            RawData = packet;
        }

        public int GetSize()
        {
            return Length;
        }

        public void PrintCommand()
        {
            throw new NotImplementedException();
        }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
        public byte[] GetRawData()
        {
            return RawData;
        }
    }
}
