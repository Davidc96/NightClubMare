﻿using ConsoleTables;
using ProLinkLib;
using ProLinkLib.Network.UDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.Commands
{
    public class SettingsCommand : ICommand
    {
        public void BuildCustom(ProLinkController plc, CommandLineController clc, string args)
        {
            throw new NotImplementedException();
        }

        public void Run(ProLinkController plc, CommandLineController clc, string args)
        {
            string[] args_splitted = args.Split(' ');
            string main_arg = args_splitted[0].ToLower();
            string param1 = "";
            VirtualCDJ virtual_cdj = plc.GetVirtualCDJ();
            DeviceSettings device_settings = plc.GetVirtualCDJ().GetDeviceSettings();
            
            if (args_splitted.Length > 1)
            {
               param1 = args_splitted[1].ToLower();
            }

            // This is built in case there is a path which contains folders with whitespaces
            if(args_splitted.Length > 2)
            {
                param1 = "";
                for(int i = 1; i < args_splitted.Length - 1; i++)
                {
                    param1 += args_splitted[i] + " ";
                }
                param1 = param1.Remove(param1.Length - 1);
            }

            switch(main_arg)
            {
                case "onair_display":
                    device_settings.OnAirDisplay = (byte)((param1 == "on") ? 0x81 : 0x82);
                    break;
                case "lcd_brightness":
                    device_settings.LCDBrightness = (byte)(0x80 + Convert.ToByte(param1));
                    break;
                case "quantize":
                    device_settings.QuantizeMode = (byte)((param1 == "on") ? 0x81 : 0x82);
                    break;
                case "autocue_level":
                    device_settings.AutoCueLevel = Utils.GetAutoCueLevel(param1);
                    break;
                case "language":
                    device_settings.Language = Utils.GetLanguage(param1);
                    break;
                case "jog_ring_bright":
                    device_settings.JogBrightness = (byte)((param1 == "min") ? 0x81 : 0x82);
                    break;
                case "endtrack_light":
                    device_settings.JogLedEndTrackBehaviour = (byte)((param1 == "on") ? 0x80 : 0x81);
                    break;
                case "slipmode_light":
                    device_settings.SlipModeJogLed = (byte)((param1 == "on") ? 0x80 : 0x81);
                    break;
                case "discslot_light":
                    device_settings.DiscSlotLedBehaviour = Utils.GetDiscSlotBrightnessMode(param1);
                    break;
                case "lock_ejectload":
                    device_settings.LockEjectLoadTracks = (byte)((param1 == "off") ? 0x80 : 0x81);
                    break;
                case "sync_mode":
                    device_settings.SyncModeActivate = (byte)((param1 == "off") ? 0x80 : 0x81);
                    break;
                case "autoplay_mode":
                    device_settings.AutoPlayMode = (byte)((param1 == "on") ? 0x80 : 0x81);
                    break;
                case "quantize_value":
                    device_settings.QuantizeModeValue = (byte)(0x79 + Convert.ToByte(param1));
                    break;
                case "hotcue_autoload":
                    device_settings.HotCueAutoLoad = (byte)((param1 == "off") ? 0x80 : 0x81);
                    break;
                case "hotcue_color":
                    device_settings.HotCueColorMode = (byte)((param1 == "disable") ? 0x80 : 0x81);
                    break;
                case "needle_lock":
                    device_settings.NeedleLockMode = (byte)((param1 == "off") ? 0x80 : 0x81);
                    break;
                case "time_display_mode":
                    device_settings.TimeDisplayMode = (byte)((param1 == "elapsed") ? 0x80 : 0x81);
                    break;
                case "jog_mode":
                    device_settings.JogMode = (byte)((param1 == "cdj") ? 0x80 : 0x81);
                    break;
                case "autocue_mode":
                    device_settings.AutoCueMode = (byte)((param1 == "off") ? 0x80 : 0x81);
                    break;
                case "mastertempo_mode":
                    device_settings.MasterTempoMode = (byte)((param1 == "on") ? 0x80 : 0x81);
                    break;
                case "rangetempo_mode":
                    device_settings.RangeTempo = Utils.GetRangeTempo(param1);
                    break;
                case "phasemeter_mode":
                    device_settings.PhaseMeter = (byte)((param1 == "squares") ? 0x80 : 0x81);
                    break;
                case "vinylspeed_adjust":
                    device_settings.VinylSpeedAdjust = Utils.GetVinylSpeedAdjust(param1);
                    break;
                case "joglcd_display":
                    device_settings.JogLCDContent = Utils.GetJogLCDContent(param1);
                    break;
                case "button_brightness":
                    device_settings.ButtonBrightness = (byte)(0x80 + Convert.ToByte(param1));
                    break;
                case "joglcd_brightness":
                    device_settings.JogLCDBrightness = (byte)(0x80 + Convert.ToByte(param1));
                    break;
                case "import":
                    virtual_cdj.LoadSettingsFromDisk(param1);
                    Console.WriteLine("Settings imported successfully. Use settings send <device_id> command to send it to the CDJ");
                    break;
                case "export":
                    virtual_cdj.SaveSettingsToDisk(param1);
                    Console.WriteLine("Settings exported successfully as " + param1 + ".json");
                    break;
                case "send":
                    int device_id = 0;
                    //Console.WriteLine(Hex.Dump(PacketBuilder.PACKET_HEADER.Concat(device_settings.GetSettingsPacket(virtual_cdj, 1).ToBytes()).ToArray()));
                    if (param1 == "")
                    {
                        // If we are in a selected device and no param1 is provided get it from it.
                        if(clc.GetSelectedDevice() != null)
                        {
                            device_id = clc.GetSelectedDevice().ChannelID;
                        }
                        else
                        {
                            Console.WriteLine("ID not provided and no device is selected! Please provide a device ID using the command settings send <device-ID> or select one device!");
                        }
                    }
                    else
                    {
                        device_id = Convert.ToInt32(param1);
                    }
                    
                    var device_list = plc.GetDevices();
                    if (!device_list.ContainsKey(device_id))
                    {
                        Console.WriteLine("ID not found! Use devices command to see the current devices on network");
                        return;
                    }
             
                    virtual_cdj.GetStatusServer().SendPacketToClient(device_list[device_id].IpAddress, device_settings.GetSettingsPacket(virtual_cdj, device_list[device_id].ChannelID));
                    break;
                case "show":
                    PrintCurrentSettings(device_settings);
                    break;
                case "help":
                default:
                    PrintHelpCommand();
                    break;
            }

        }

        private void PrintCurrentSettings(DeviceSettings settings)
        {
            ConsoleTable table = new ConsoleTable("Settings", "Value");
            
            table.AddRow("onair_display", (settings.OnAirDisplay == 0x81) ? "on" : "off");
            table.AddRow("lcd_brightness", settings.LCDBrightness - 0x80);
            table.AddRow("quantize", (settings.QuantizeMode == 0x81) ? "on" : "off");
            table.AddRow("autocue_level", Utils.GetAutoCueLevel(settings.AutoCueLevel) + " dB " + $"(0x{settings.AutoCueLevel:X})");
            table.AddRow("language", Utils.GetLanguage(settings.Language) + $" (0x{settings.Language:X})");
            table.AddRow("jog_ring_bright", (settings.JogBrightness == 0x81) ? "min" : "max");
            table.AddRow("endtrack_light", (settings.JogLedEndTrackBehaviour == 0x80) ? "on" : "off");
            table.AddRow("slipmode_light", (settings.SlipModeJogLed == 0x80) ? "on" : "off");
            table.AddRow("discslot_light", Utils.GetDiscSlotBrightnessMode(settings.DiscSlotLedBehaviour));
            table.AddRow("lock_ejectload", (settings.LockEjectLoadTracks == 0x80) ? "off" : "on");
            table.AddRow("sync_mode", (settings.SyncModeActivate == 0x80) ? "off" : "on");
            table.AddRow("autoplay_mode", (settings.AutoPlayMode == 0x81) ? "on" : "off");
            table.AddRow("quantize_value", settings.QuantizeModeValue);
            table.AddRow("hotcue_autoload", (settings.AutoPlayMode == 0x80) ? "on" : "off");
            table.AddRow("needle_lock", (settings.NeedleLockMode == 0x80) ? "off" : "on");
            table.AddRow("time_display_mode", (settings.TimeDisplayMode == 0x80) ? "Elapsed" : "Remaining");
            table.AddRow("jog_mode", (settings.JogMode == 0x80) ? "CDJ" : "Vinyl");
            table.AddRow("master_tempo", (settings.QuantizeMode == 0x80) ? "on" : "off");
            table.AddRow("rangetempo_mode", Utils.GetRangeTempo(settings.RangeTempo));
            table.AddRow("phasemode_meter", (settings.PhaseMeter == 0x80) ? "squares" : "line");
            table.AddRow("vinylspeed_adjust", Utils.GetVinylSpeedAdjust(settings.VinylSpeedAdjust));
            table.AddRow("joglcd_display", Utils.GetJogLCDContent(settings.JogLCDContent));
            table.AddRow("button_brightness", settings.ButtonBrightness - 0x80);
            table.AddRow("joglcd_brightness", settings.JogLCDBrightness - 0x80);

            table.Write();
        }

        private void PrintHelpCommand()
        {
            ConsoleTable table = new ConsoleTable("Description");
            table.AddRow("settings onair_display <on|off>");
            table.AddRow("settings lcd_brightness <1-5>");
            table.AddRow("settings quantize <on|off>");
            table.AddRow("settings autocue_level [-36,-42,-48,-54,-60,-66,-72,-78,memory]");
            table.AddRow("settings language [english,french,german,italian,dutch,spanish,russian,korean,chinese-simp,chinese-trad,japanese,portugese,swedish,czech,magyar,danish,greek,turkish]");
            table.AddRow("settings jog_ring_bright <min|max>");
            table.AddRow("settings endtrack_light <on|off>");
            table.AddRow("settings slipmode_light <on|off>");
            table.AddRow("settings discslot_light <off|min|max>");
            table.AddRow("settings lock_ejectload <on|off>");
            table.AddRow("settings sync_mode <on|off>");
            table.AddRow("settings autoplay_mode <on|off>");
            table.AddRow("settings quantize_value (1: 1 beat, 2: 1/2 beat, 3: 1/4 beat, 4: 8 beat)");
            table.AddRow("settings hotcue_autoload <on|off>");
            table.AddRow("settings hotcue_color <enable|disable>");
            table.AddRow("settings needle_lock <on|off>");
            table.AddRow("settings time_display_mode <elapsed|remaining>");
            table.AddRow("settings jog_mode <cdj|vinyl>");
            table.AddRow("settings master_tempo <on|off>");
            table.AddRow("settings rangetempo_mode [+-6,+-10,+-16,wide]");
            table.AddRow("settings phasemode_meter <squares|line>");
            table.AddRow("settings vinylspeed_adjust <touch_release|touch|release>");
            table.AddRow("settings joglcd_display <auto|simple|artwork>");
            table.AddRow("settings button_brightness <1-4>");
            table.AddRow("settings joglcd_brightness <1-4>");
            table.AddRow("--------- Other commands -----------");
            table.AddRow("settings import <path config json location>");
            table.AddRow("settings export <file_name>");
            table.AddRow("settings show");
            table.AddRow("settings send <Device ID>");

            table.Write();

        }
    }
}