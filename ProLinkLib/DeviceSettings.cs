using Newtonsoft.Json;
using ProLinkLib.Commands.StatusCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib
{
    public class DeviceSettings
    {
        public byte OnAirDisplay = 0x81;                                                           // 0x81: ON 0x82: OFF
        public byte LCDBrightness = 0x83;                                                          // 0x81: Min Brightness to 0x85 (Max Brightness)
        public byte QuantizeMode = 0x81;                                                           // 0x81: ON 0x82: OFF
        public byte AutoCueLevel = 0x88;
        public byte Language = 0x81;
        public byte JogBrightness = 0x82;                                                          // 0x81: Min Bright 0x82: Full Bright
        public byte JogLedEndTrackBehaviour = 0x80;                                                // 0x80: Turn ON Led 0x82: Turn Off
        public byte SlipModeJogLed = 0x80;                                                         // 0x80: ON 0x82: OFF
        public byte DiscSlotLedBehaviour = 0x82;                                                   // 0x80 OFF 0x81 Min Led 0x82 Max Led
        public byte LockEjectLoadTracks = 0x80;                                                    // 0x80 False 0x81 True
        public byte SyncModeActivate = 0x80;                                                       // 0x80 False 0x81 True
        public byte AutoPlayMode = 0x81;                                                           // 0x80 True 0x81 False
        public byte QuantizeModeValue = 0x80;                                                      // 0x80 1 beat 0x81 1/2 beat 0x82 1/4 beat 0x83 8 beats
        public byte HotCueAutoLoad = 0x81;                                                         // 0x80 OFF 0x81 Enabled 0x82 Rekordbox owner
        public byte HotCueColorMode = 0x80;                                                        // 0x80 Disabled 0x81 Enabled
        public byte NeedleLockMode = 0x81;                                                         // 0x80 Unlocked 0x81 Locked
        public byte TimeDisplayMode = 0x81;                                                        // 0x80 Elapsed 0x81 Remaining
        public byte JogMode = 0x80;                                                                // 0x80 CDJ 0x81 Vinyl
        public byte AutoCueMode = 0x81;                                                            // 0x80 Disabled 0x81 Enabled
        public byte MasterTempoMode = 0x81;                                                        // 0x80 Enabled 0x81 Disabled
        public byte RangeTempo = 0x81;
        public byte PhaseMeter = 0x81;                                                             // 0x80 |===||===||===||===|  0x81 ---|---|---|---|---
        public byte VinylSpeedAdjust = 0x80;                                                       // 0x80 Touch & Release 0x81 Touch Only 0x82 Release Only
        public byte JogLCDContent = 0x80;                                                          // 0x80 Auto 0x81 Simple 0x82 Artwork
        public byte ButtonBrightness = 0x84;                                                       // 0x81 Min Brightness to 0x84 Max Brightness
        public byte JogLCDBrightness = 0x84;                                                       // 0x81 Min Brightness to 0x84 Max Brightness

        public LoadSettingsCommand GetSettingsPacket(byte destination_device)
        {
            LoadSettingsCommand ld_cmd = new LoadSettingsCommand();
            
            ld_cmd.ChannelDestID = destination_device;
            ld_cmd.OnAirDisplay = OnAirDisplay;
            ld_cmd.LCDBrightness = LCDBrightness;
            ld_cmd.QuantizeMode = QuantizeMode;
            ld_cmd.AutoCueLevel = AutoCueLevel;
            ld_cmd.Language = Language;
            ld_cmd.JogBrightness = JogBrightness;
            ld_cmd.JogLedEndTrackBehaviour = JogLedEndTrackBehaviour;
            ld_cmd.SlipModeJogLed = SlipModeJogLed;
            ld_cmd.DiscSlotLedBehaviour = DiscSlotLedBehaviour;
            ld_cmd.LockEjectLoadTracks = LockEjectLoadTracks;
            ld_cmd.SyncModeActivate = SyncModeActivate;
            ld_cmd.AutoPlayMode = AutoPlayMode;
            ld_cmd.QuantizeModeValue = QuantizeModeValue;
            ld_cmd.HotCueAutoLoad = HotCueAutoLoad;
            ld_cmd.HotCueColorMode = HotCueColorMode;
            ld_cmd.NeedleLockMode = NeedleLockMode;
            ld_cmd.TimeDisplayMode = TimeDisplayMode;
            ld_cmd.JogMode = JogMode;
            ld_cmd.MasterTempoMode = MasterTempoMode;
            ld_cmd.RangeTempo = RangeTempo;
            ld_cmd.PhaseMeter = PhaseMeter;
            ld_cmd.VinylSpeedAdjust = VinylSpeedAdjust;
            ld_cmd.JogLCDContent = JogLCDContent;
            ld_cmd.ButtonBrightness = ButtonBrightness;
            ld_cmd.JogLCDBrightness = JogLCDBrightness;

            return ld_cmd;
        }

    }
}
