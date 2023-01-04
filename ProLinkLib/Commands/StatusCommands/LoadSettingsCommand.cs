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
        public byte ID;
        public byte[] DeviceName = new byte[0x14];
        private byte Unknown1 = 0x02;
        public byte ChannelID;
        public byte ChannelDestID;
        public ushort Length = 0x50;
        public byte[] UnknownBytes = { 0x12, 0x34, 0x56, 0x78, 0x00, 0x00, 0x00, 0x03 };
        public byte OnAirDisplay;                                                           // 0x81: ON 0x82: OFF
        public byte LCDBrightness;                                                          // 0x81: Min Brightness to 0x85 (Max Brightness)
        public byte QuantizeMode;                                                           // 0x81: ON 0x82: OFF
        public byte AutoCueLevel;
        public byte Language;
        private byte Unknown2 = 0x01;
        public byte JogBrightness;                                                          // 0x81: Min Bright 0x82: Full Bright
        public byte JogLedEndTrackBehaviour;                                                // 0x80: Turn ON Led 0x82: Turn Off
        public byte SlipModeJogLed;                                                         // 0x80: ON 0x82: OFF
        private byte[] UnknownBytes2 = { 0x01, 0x01, 0x01 };
        public byte DiscSlotLedBehaviour;                                                   // 0x80 OFF 0x81 Min Led 0x82 Max Led
        public byte LockEjectLoadTracks;                                                    // 0x80 False 0x81 True
        public byte SyncModeActivate;                                                       // 0x80 False 0x81 True
        public byte AutoPlayMode;                                                           // 0x80 True 0x81 False
        public byte QuantizeModeValue;                                                      // 0x80 1 beat 0x81 1/2 beat 0x82 1/4 beat 0x83 8 beats
        public byte HotCueAutoLoad;                                                         // 0x80 OFF 0x81 Enabled 0x82 Rekordbox owner
        public byte HotCueColorMode;                                                        // 0x80 Disabled 0x81 Enabled
        private byte[] UnknownBytes3 = { 0x00, 0x00 };
        public byte NeedleLockMode;                                                         // 0x80 Unlocked 0x81 Locked
        private byte[] UnknownBytes4 = { 0x00, 0x00 };
        public byte TimeDisplayMode;                                                        // 0x80 Elapsed 0x81 Remaining
        public byte JogMode;                                                                // 0x80 CDJ 0x81 Vinyl
        public byte AutoCueMode;                                                            // 0x80 Disabled 0x81 Enabled
        public byte MasterTempoMode;                                                        // 0x80 Enabled 0x81 Disabled
        public byte RangeTempo;
        public byte PhaseMeter;                                                             // 0x80 |===||===||===||===|  0x81 ---|---|---|---|---
        public byte VinylSpeedAdjust;                                                       // 0x80 Touch & Release 0x81 Touch Only 0x82 Release Only
        public byte JogLCDContent;                                                          // 0x80 Auto 0x81 Simple 0x82 Artwork
        public byte ButtonBrightness;                                                       // 0x81 Min Brightness to 0x84 Max Brightness
        public byte JogLCDBrightness;                                                       // 0x81 Min Brightness to 0x84 Max Brightness
        public byte[] UnknownBytes5 = new byte[0x24];

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
                Unknown2 = bin.ReadByte();
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
                UnknownBytes5 = bin.ReadBytes(0x24);
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
                bin.Write(Unknown2);
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
