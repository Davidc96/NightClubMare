using Newtonsoft.Json;
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
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ID = 0x0A;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] DeviceName = Enumerable.Repeat((byte)0xB0, 0x14).ToArray();
        [JsonConverter(typeof(HexJsonConverter))]
        private byte Unknown = 0x01;
        [JsonConverter(typeof(HexJsonConverter))]
        private byte SubCategory = 0x05;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ChannelID1 = 0xA0;
        [JsonConverter(typeof(HexJsonConverter))]
        public ushort Length = 0x0100;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ChannelID2 = 0xA1;
        [JsonConverter(typeof(HexJsonConverter))]
        private byte Unknown3 = 0x00;
        [JsonConverter(typeof(HexJsonConverter))]
        private byte Unknown4 = 0x01;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ActivityFlag = 0xA2;                               // 0x00 -> IDLE 0x01 -> Playing, Loading or Searching track
        [JsonConverter(typeof(HexJsonConverter))]
        public byte TrackDeviceLocatedID = 0xA3;                       // ChannelID if is in local, CDJ ChannelID where is located the track
        [JsonConverter(typeof(HexJsonConverter))]
        public byte TrackPhysicallyLocated = 0xA4;                     // 0x00 -> NO TRACK, 0x01 -> CD-Drive, 0x02 -> SD Slot, 0x03 -> USB Slot, 0x04 -> Laptop
        [JsonConverter(typeof(HexJsonConverter))]
        public byte TrackType = 0xA5;                                  // 0x00 -> No track loaded, 0x01 -> Rekordbox track, 0x02 -> Unanalized track loaded, 0x05 -> CD track loaded
        [JsonConverter(typeof(HexJsonConverter))]
        private byte Unknown5 = 0x00;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] RekordboxTrackID = { 0xA6, 0xA6, 0xA6, 0xA6 };         // RekordboxTrackID
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] BlankBytes = new byte[0x2];
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] RekordBoxTrackListPos = { 0xA7, 0xA7 };
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] BlankBytes2 = new byte[0x3];
        [JsonConverter(typeof(HexJsonConverter))]
        public byte UnknownFeatureDl;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] BlankBytes3 = new byte[0xE];
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] NumberTracksInPlaylist = { 0xA8, 0xA8 };
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] BlankBytes4 = new byte[0x22];
        [JsonConverter(typeof(HexJsonConverter))]
        public byte USBActivity = 0xA9;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte SDActivity = 0xAA;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] BlankBytes5 = new byte[0x3];
        [JsonConverter(typeof(HexJsonConverter))]
        public byte USBLocalStatus = 0xAB;                                 // 0x04 -> No USB Media Loaded, 0x00 -> USB Loaded, 0x02 or 0x03 -> USB Stop button
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] BlankBytes6 = new byte[0x3];
        [JsonConverter(typeof(HexJsonConverter))]
        public byte SDLocalStatus = 0xAC;                                 // 0x04 -> No SD Media Loaded, 0x00 -> SD Loaded, 0x02 or 0x03 -> SD tap open and SD unmounted
        [JsonConverter(typeof(HexJsonConverter))]
        private byte BlankByte;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte LinkAvailable = 0xAD;                                 // 0x01 -> True 0x00 False
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] BlankBytes7 = new byte[0x5];
        [JsonConverter(typeof(HexJsonConverter))]
        public byte PlayerStatus = 0xAE;                                  // 0x00 No track, 0x02 -> Track loading, 0x03 -> Play Normal, 0x04 -> Play Loop, 0x05 Player paused in other than cue point
                                                                          // 0x06 Player paused cue point, 0x07 Cue play in progres, 0x08 Cue scratch in progress, 0x09 -> Player searching
                                                                          // 0x0e Audio CD Shutdown 0x11 -> Reached End of the track
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] FirmwareVersion = { 0xAF, 0xAF, 0xAF, 0xAF };             // ASCII Representation
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] BlankBytes8 = new byte[0x4];
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] SyncCounter = { 0xB0, 0xB0, 0xB0, 0xB0 };                 // Increase the counter when delegate Master Tempo
        [JsonConverter(typeof(HexJsonConverter))]
        private byte BlankByte2 = 0x00;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte FlagStatus = 0xB1;                                    // bit 0 -> 0 bit 1 -> BPM  bit 2 -> 1 bit 3 -> On-Air bit4 -> SyncMode bit 5 -> Master bit 6 -> Play bit 7 -> 1
        [JsonConverter(typeof(HexJsonConverter))]
        public byte Unknown6 = 0xFF;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte PlayerStatus2 = 0xB2;                                 // 0x7A -> Playing normally, 0x7E Playing but not due to someone is holding the Jog
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] Pitch1 = { 0xB3, 0xB3, 0xB3, 0xB3 };                      // First Pitch Byte
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] BPMControl = { 0xB4, 0xB4 };                  // 80 00 -> Rekordbox track loaded 00 00 No rekordbox track loaded 7F FF No track loaded
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] BPMTrack = { 0xB5, 0xB5 };                    // (byte[0] * 256 + byte[1])
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] BlankBytes9 = { 0x7F, 0xFF, 0xFF, 0xFF };
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] Pitch2 = { 0xB6, 0xB6, 0xB6, 0xB6 };                      // Second Pitch Byte
        [JsonConverter(typeof(HexJsonConverter))]
        private byte Unknown7 = 0x00;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte PlayerModeStatus = 0xB7;                              // 0x00 No track loaded 0x01 Reverse mode or paused 0x09 Playing with Vinyl mode 0x0d Playing with CDJ mode
        [JsonConverter(typeof(HexJsonConverter))]
        public byte MasterMeaningful = 0xB8;                              // 0x00 Not Master 0x01 Master and track is playing 0x02 Master but no track playing
        [JsonConverter(typeof(HexJsonConverter))]
        public byte MasterHandoff = 0xB9;                                 // FF if is not master Other device ChannelID -> locate the master
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] Beat = { 0xB0, 0xB0, 0xB0, 0xB0 };
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] CueDistance = { 0xB1, 0xB1 };
        [JsonConverter(typeof(HexJsonConverter))]
        public byte BeatCounter = 0xB2;                                   // Beat Counter 01 02 03 04
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] BlankBytes10 = new byte[16];
        [JsonConverter(typeof(HexJsonConverter))]
        public byte MediaPresence = 0xB3;                                 // Works only on CDJ-3000
        [JsonConverter(typeof(HexJsonConverter))]
        public byte USBPresence = 0xB4;                                   // 0x01 -> Unsafe ejected 0x00 -> All ok
        [JsonConverter(typeof(HexJsonConverter))]
        public byte SDPresence = 0xB5;                                    // 0x01 -> SD unsafe ejected 0x00 -> All Ok
        [JsonConverter(typeof(HexJsonConverter))]
        public byte EmergencyLoop = 0xB6;                                 // 0x01 -> If emergency loop is active
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] BlankBytes11 = new byte[0x5];
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] Pitch3 = { 0xB7, 0xB7, 0xB7, 0xB7 };
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] Pitch4 = { 0xB8, 0xB8, 0xB8, 0xB8 };
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] PacketCounter = { 0xB9, 0xB9, 0xB9, 0xB9 };
        [JsonConverter(typeof(HexJsonConverter))]
        public byte IsNexus = 0xBA;                                        // 0x0f if is nexus 0x1f XDJ-XZ and CDJ-3000 0x05 older players
        [JsonConverter(typeof(HexJsonConverter))]
        public byte PreviewTrackSupport = 0xBB;                            // 0x20 is supported 0x00 No
        private byte[] BlankBytes12 = new byte[0x2];
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] UnknownBytes = { 0x12, 0x34, 0x56, 0x78, 0x00, 0x00, 0x00 }; // Starts with 12 34 56 78
        [JsonConverter(typeof(HexJsonConverter))]
        public byte WaveColor = 0xBC;                                      // 0x01 -> Blue 0x03 -> RGB 0x04 -> 3-Band
        [JsonConverter(typeof(HexJsonConverter))]
        private byte Unknown8 = 0x01;
        [JsonConverter(typeof(HexJsonConverter))]
        private byte Unknown9 = 0x01;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte WavePosition = 0xBD;                                   // 0x01 -> Center 0x02 -> Left
        [JsonConverter(typeof(HexJsonConverter))]
        private byte Unknown10 = 0x02;
        [JsonConverter(typeof(HexJsonConverter))]
        private byte Unknown11 = 0x01;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] BlankBytes13 = new byte[0x47];

        // TODO ADD CDJ 3000 Support

        public byte[] RawData;
        public void FromBytes(byte[] packet)
        {
            using (BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
            {
                bin.BaseStream.Seek(0x0B, SeekOrigin.Begin);
                DeviceName = bin.ReadBytes(0x14);
                Unknown = bin.ReadByte();
                SubCategory = bin.ReadByte();
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
                Unknown10 = bin.ReadByte();
                Unknown11 = bin.ReadByte();
                BlankBytes13 = bin.ReadBytes(0x46);
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
            MemoryStream stream = new MemoryStream();
            using(BinaryWriter bin = new BinaryWriter(stream))
            {
                bin.Write(ID);
                bin.Write(DeviceName);
                bin.Write(Unknown);
                bin.Write(SubCategory);
                bin.Write(ChannelID1);
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(Length)));
                bin.Write(ChannelID2);
                bin.Write(Unknown3);
                bin.Write(Unknown4);
                bin.Write(ActivityFlag);
                bin.Write(TrackDeviceLocatedID);
                bin.Write(TrackPhysicallyLocated);
                bin.Write(TrackType);
                bin.Write(Unknown5);
                bin.Write(RekordboxTrackID);
                bin.Write(BlankBytes);
                bin.Write(RekordBoxTrackListPos);
                bin.Write(BlankBytes2);
                bin.Write(UnknownFeatureDl);
                bin.Write(BlankBytes3);
                bin.Write(NumberTracksInPlaylist);
                bin.Write(BlankBytes4);
                bin.Write(USBActivity);
                bin.Write(SDActivity);
                bin.Write(BlankBytes5);
                bin.Write(USBLocalStatus);
                bin.Write(BlankBytes6);
                bin.Write(SDLocalStatus);
                bin.Write(BlankByte);
                bin.Write(LinkAvailable);
                bin.Write(BlankBytes7);
                bin.Write(PlayerStatus);
                bin.Write(FirmwareVersion);
                bin.Write(BlankBytes8);
                bin.Write(SyncCounter);
                bin.Write(BlankByte2);
                bin.Write(FlagStatus);
                bin.Write(Unknown6);
                bin.Write(PlayerStatus2);
                bin.Write(Pitch1);
                bin.Write(BPMControl);
                bin.Write(BPMTrack);
                bin.Write(BlankBytes9);
                bin.Write(Pitch2);
                bin.Write(Unknown7);
                bin.Write(PlayerModeStatus);
                bin.Write(MasterMeaningful);
                bin.Write(MasterHandoff);
                bin.Write(Beat);
                bin.Write(CueDistance);
                bin.Write(BeatCounter);
                bin.Write(BlankBytes10);
                bin.Write(MediaPresence);
                bin.Write(USBPresence);
                bin.Write(SDPresence);
                bin.Write(EmergencyLoop);
                bin.Write(BlankBytes11);
                bin.Write(Pitch3);
                bin.Write(Pitch4);
                bin.Write(PacketCounter);
                bin.Write(IsNexus);
                bin.Write(PreviewTrackSupport);
                bin.Write(BlankBytes12);
                bin.Write(UnknownBytes);
                bin.Write(WaveColor);
                bin.Write(Unknown8);
                bin.Write(Unknown9);
                bin.Write(WavePosition);
                bin.Write(Unknown10);
                bin.Write(Unknown11);
                bin.Write(BlankBytes13);
            }

            return stream.ToArray();
        }
        public byte[] GetRawData()
        {
            return RawData;
        }
    }
}
