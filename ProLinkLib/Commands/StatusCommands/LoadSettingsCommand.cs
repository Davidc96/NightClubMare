using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.StatusCommands
{
    public class LoadSettingsCommand : ICommand
    {
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ID = 0x34;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] DeviceName = Enumerable.Repeat((byte)0xB0, 0x14).ToArray();
        [JsonConverter(typeof(HexJsonConverter))]
        private byte Unknown1 = 0x02;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ChannelID = 0xA0;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ChannelDestID = 0xA1;
        [JsonConverter(typeof(HexJsonConverter))]
        public ushort Length = 0x50;
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] UnknownBytes = { 0x12, 0x34, 0x56, 0x78, 0x00, 0x00, 0x00, 0x03 };
        [JsonConverter(typeof(HexJsonConverter))]
        public byte OnAirDisplay = 0xA2;                                                           // 0x81: ON 0x82: OFF
        [JsonConverter(typeof(HexJsonConverter))]
        public byte LCDBrightness = 0xA3;                                                          // 0x81: Min Brightness to 0x85 (Max Brightness)
        [JsonConverter(typeof(HexJsonConverter))]
        public byte QuantizeMode = 0xA4;                                                           // 0x81: ON 0x82: OFF
        [JsonConverter(typeof(HexJsonConverter))]
        public byte AutoCueLevel = 0xA5;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte Language = 0xA6;
        [JsonConverter(typeof(HexJsonConverter))]
        private byte SubCategory = 0x01;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte JogBrightness = 0xA7;                                                          // 0x81: Min Bright 0x82: Full Bright
        [JsonConverter(typeof(HexJsonConverter))]
        public byte JogLedEndTrackBehaviour = 0xA8;                                                // 0x80: Turn ON Led 0x82: Turn Off
        [JsonConverter(typeof(HexJsonConverter))]
        public byte SlipModeJogLed = 0xA9;                                                         // 0x80: ON 0x82: OFF
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] UnknownBytes2 = { 0x01, 0x01, 0x01 };
        [JsonConverter(typeof(HexJsonConverter))]
        public byte DiscSlotLedBehaviour = 0xAA;                                                   // 0x80 OFF 0x81 Min Led 0x82 Max Led
        [JsonConverter(typeof(HexJsonConverter))]
        public byte LockEjectLoadTracks = 0xAB;                                                    // 0x80 False 0x81 True
        [JsonConverter(typeof(HexJsonConverter))]
        public byte SyncModeActivate = 0xAC;                                                       // 0x80 False 0x81 True
        [JsonConverter(typeof(HexJsonConverter))]
        public byte AutoPlayMode = 0xAD;                                                           // 0x80 True 0x81 False
        [JsonConverter(typeof(HexJsonConverter))]
        public byte QuantizeModeValue = 0xAE;                                                      // 0x80 1 beat 0x81 1/2 beat 0x82 1/4 beat 0x83 8 beats
        [JsonConverter(typeof(HexJsonConverter))]
        public byte HotCueAutoLoad = 0xAF;                                                         // 0x80 OFF 0x81 Enabled 0x82 Rekordbox owner
        [JsonConverter(typeof(HexJsonConverter))]
        public byte HotCueColorMode = 0xB0;                                                        // 0x80 Disabled 0x81 Enabled
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] UnknownBytes3 = { 0x00, 0x00 };
        [JsonConverter(typeof(HexJsonConverter))]
        public byte NeedleLockMode = 0xB1;                                                         // 0x80 Unlocked 0x81 Locked
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        private byte[] UnknownBytes4 = { 0x00, 0x00 };
        [JsonConverter(typeof(HexJsonConverter))]
        public byte TimeDisplayMode = 0xB2;                                                        // 0x80 Elapsed 0x81 Remaining
        [JsonConverter(typeof(HexJsonConverter))]
        public byte JogMode = 0xB3;                                                                // 0x80 CDJ 0x81 Vinyl
        [JsonConverter(typeof(HexJsonConverter))]
        public byte AutoCueMode = 0xB4;                                                            // 0x80 Disabled 0x81 Enabled
        [JsonConverter(typeof(HexJsonConverter))]
        public byte MasterTempoMode = 0xB5;                                                        // 0x80 Enabled 0x81 Disabled
        [JsonConverter(typeof(HexJsonConverter))]
        public byte RangeTempo = 0xB6;
        [JsonConverter(typeof(HexJsonConverter))]
        public byte PhaseMeter = 0xB7;                                                             // 0x80 |===||===||===||===|  0x81 ---|---|---|---|---
        [JsonConverter(typeof(HexJsonConverter))]
        public byte VinylSpeedAdjust = 0xB8;                                                       // 0x80 Touch & Release 0x81 Touch Only 0x82 Release Only
        [JsonConverter(typeof(HexJsonConverter))]
        public byte JogLCDContent = 0xB9;                                                          // 0x80 Auto 0x81 Simple 0x82 Artwork
        [JsonConverter(typeof(HexJsonConverter))]
        public byte ButtonBrightness = 0xBA;                                                       // 0x81 Min Brightness to 0x84 Max Brightness
        [JsonConverter(typeof(HexJsonConverter))]
        public byte JogLCDBrightness = 0xBB;                                                       // 0x81 Min Brightness to 0x84 Max Brightness
        [JsonConverter(typeof(ByteArrayJsonConverter))]
        public byte[] UnknownBytes5 = new byte[0x26];

        private byte[] rawData;
        public void FromBytes(byte[] packet)
        {
            rawData = packet;
            using (BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
            {
                bin.BaseStream.Seek(0x0B, SeekOrigin.Begin);
                DeviceName = bin.ReadBytes(0x14);
                Unknown1 = bin.ReadByte();
                ChannelID = bin.ReadByte();
                ChannelDestID = bin.ReadByte();
                Length = BitConverter.ToUInt16(Utils.SwapEndianesss(bin.ReadBytes(2)), 0);
                UnknownBytes = bin.ReadBytes(0x8);
                OnAirDisplay = bin.ReadByte();
                LCDBrightness = bin.ReadByte();
                QuantizeMode = bin.ReadByte();
                AutoCueLevel = bin.ReadByte();
                Language = bin.ReadByte();
                SubCategory = bin.ReadByte();
                JogBrightness = bin.ReadByte();
                JogLedEndTrackBehaviour = bin.ReadByte();
                SlipModeJogLed = bin.ReadByte();
                UnknownBytes2 = bin.ReadBytes(0x3);
                DiscSlotLedBehaviour = bin.ReadByte();
                LockEjectLoadTracks = bin.ReadByte();
                SyncModeActivate = bin.ReadByte();
                AutoPlayMode = bin.ReadByte();
                QuantizeModeValue = bin.ReadByte();
                HotCueAutoLoad = bin.ReadByte();
                HotCueColorMode = bin.ReadByte();
                UnknownBytes3 = bin.ReadBytes(0x02);
                NeedleLockMode = bin.ReadByte();
                UnknownBytes4 = bin.ReadBytes(0x02);
                TimeDisplayMode = bin.ReadByte();
                JogMode = bin.ReadByte();
                AutoCueMode = bin.ReadByte();
                MasterTempoMode = bin.ReadByte();
                RangeTempo = bin.ReadByte();
                PhaseMeter = bin.ReadByte();
                VinylSpeedAdjust = bin.ReadByte();
                JogLCDContent = bin.ReadByte();
                ButtonBrightness = bin.ReadByte();
                JogLCDBrightness = bin.ReadByte();
                UnknownBytes5 = bin.ReadBytes(0x26);
            }
        }

        public byte[] GetRawData()
        {
            return rawData;
        }

        public int GetSize()
        {
            return 0x74;
        }

        public void PrintCommand()
        {
            Console.WriteLine("LoadSettingsCommand");
            Console.WriteLine(Hex.Dump(rawData));
        }

        public byte[] ToBytes()
        {
            MemoryStream stream = new MemoryStream();
            using(BinaryWriter bin = new BinaryWriter(stream))
            {
                bin.Write(ID);
                bin.Write(DeviceName);
                bin.Write(Unknown1);
                bin.Write(ChannelID);
                bin.Write(ChannelDestID);
                bin.Write(Utils.SwapEndianesss(BitConverter.GetBytes(Length)));
                bin.Write(UnknownBytes);
                bin.Write(OnAirDisplay);
                bin.Write(LCDBrightness);
                bin.Write(QuantizeMode);
                bin.Write(AutoCueLevel);
                bin.Write(Language);
                bin.Write(SubCategory);
                bin.Write(JogBrightness);
                bin.Write(JogLedEndTrackBehaviour);
                bin.Write(SlipModeJogLed);
                bin.Write(UnknownBytes2);
                bin.Write(DiscSlotLedBehaviour);
                bin.Write(LockEjectLoadTracks);
                bin.Write(SyncModeActivate);
                bin.Write(AutoPlayMode);
                bin.Write(QuantizeModeValue);
                bin.Write(HotCueAutoLoad);
                bin.Write(HotCueColorMode);
                bin.Write(UnknownBytes3);
                bin.Write(NeedleLockMode);
                bin.Write(UnknownBytes4);
                bin.Write(TimeDisplayMode);
                bin.Write(JogMode);
                bin.Write(AutoCueMode);
                bin.Write(MasterTempoMode);
                bin.Write(RangeTempo);
                bin.Write(PhaseMeter);
                bin.Write(VinylSpeedAdjust);
                bin.Write(JogLCDContent);
                bin.Write(ButtonBrightness);
                bin.Write(JogLCDBrightness);
                bin.Write(UnknownBytes5);

            }

            return stream.ToArray();
        }
    }
}
