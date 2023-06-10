pioneer_status = Proto("PROLINK_STATUS_PROTOCOL", "ProLink Status Protocol")
--- Common for every Packet

header = ProtoField.bytes("ProLink.Header", "Header", base.SPACE)
ID = ProtoField.uint8("ProLink.ID", "ID", base.HEX)
DeviceName = ProtoField.string("ProLink.DeviceName", "DeviceName", base.UNICODE)
Unknown = ProtoField.uint8("ProLink.Unknown", "Unknown", base.HEX)
SubCategory = ProtoField.uint8("ProLink.SubCategory", "SubCategory", base.HEX)
ChannelID = ProtoField.uint8("ProLink.ChannelID", "ChannelID", base.HEX)
Length = ProtoField.uint16("ProLink.Length", "Length", base.DEC)

-- Media Query Request --
IPAddress = ProtoField.ipv4("ProLink.IP", "IP")
ChannelIDDeviceOwner = ProtoField.uint8("ProLink.ChannelIDDeviceOwner", "ChannelID Device Owner", base.HEX)
SlotRequest = ProtoField.uint8("ProLink.SlotRequest", "Slot Request", base.HEX)

-- Media Query Response --
MediaName = ProtoField.string("ProLink.MediaName", "Media Name", base.UNICODE)
CreationDate = ProtoField.string("ProLink.CreationDate", "Creation Date", base.UNICODE)
NumbTracks = ProtoField.uint16("ProLink.NumbTracks", "Number of Tracks", base.DEC)
USBColor = ProtoField.string("ProLink.USBColor", "USB Color", base.ASCII)
PlayListNumb = ProtoField.uint16("ProLink.PlayListNumber", "PlayList Number", base.DEC)
TotalSpace = ProtoField.uint64("ProLink.TotalSpace", "Device Total Space (Bytes)", base.DEC)
FreeSpace = ProtoField.uint64("ProLink.FreeSpace", "Device Remaining Space (Bytes)", base.DEC)

-- Load Track Command --
ChannelSecondID = ProtoField.uint8("ProLink.ChannelSecondID", "Channel Second ID", base.HEX)
TrackType = ProtoField.string("ProLink.TrackType", "Track Type", base.ASCII)
RekordboxID = ProtoField.uint32("ProLink.RekordboxID", "Track Rekordbox ID", base.HEX)
DstChannelID = ProtoField.uint8("ProLink.DstChannelID", "Destination Channel ID (-1)", base.HEX)

-- Load Settings Command --
UnknownBytes = ProtoField.bytes("ProLink.UnknownBytes", "Unknown Bytes", base.SPACE)

OnAirDisplay = ProtoField.string("ProLink.OnAirDisplay", "On Air Display", base.ASCII)
ScreenBrightness = ProtoField.string("ProLink.ScreenBrightness", "Screen Brightness", base.ASCII)
QuantizeMode = ProtoField.string("ProLink.QuantizeMode", "Quantize Mode", base.ASCII)
AutoCueLevel = ProtoField.string("ProLink.AutoCueLevel", "Auto Cue Level", base.ASCII)
Language = ProtoField.string("ProLink.Language", "Language", base.ASCII)

UnknownByte = ProtoField.uint8("ProLink.UnknownByte", "UnknownByte", base.HEX)

JogRingBrightness = ProtoField.string("ProLink.JogRingBrightness", "Jog Ring Brightness", base.ASCII)
JogRingIndicator = ProtoField.string("ProLink.JogRingIndicator", "Jog Ring Indicator", base.ASCII)
SlipFlashing = ProtoField.string("ProLink.SlipFlashing", "Slip Flashing", base.ASCII)

UnknownBytes2 = ProtoField.bytes("ProLink.UnknownBytes2", "Unknown Bytes 2", base.SPACE)

PlayerDiscSlotBrightness = ProtoField.string("ProLink.PlDiscSlotBrightness", "Player Disc Slot Brightness", base.ASCII)
LockFeature = ProtoField.string("ProLink.LockFeature", "Lock Feature", base.ASCII)
SyncMode = ProtoField.string("ProLink.SyncMode", "Sync Mode", base.ASCII)
AutoPlayMode = ProtoField.string("ProLink.AutoPlayMode", "Auto Play Mode", base.ASCII)
QuantizeBeatValue = ProtoField.string("ProLink.QuantizeBeatValue", "Quantize Value", base.ASCII)
HotCueAutoLoad = ProtoField.string("ProLink.HotCueAutoLoad", "Hot Cue Auto Load", base.ASCII)
HotCueColor = ProtoField.string("ProLink.HotCueColor", "Hot Cue Color", base.ASCII)
NeedleLockFeature = ProtoField.string("ProLink.NeedleLock", "Needle Lock Mode", base.ASCII)
TimeMode = ProtoField.string("ProLink.TimeMode", "Time Mode", base.ASCII)
JogMode = ProtoField.string("ProLink.JogMode", "Jog Mode", base.ASCII)
AutoCue = ProtoField.string("ProLink.AutoCue", "Auto Cue", base.ASCII)
MasterTempo = ProtoField.string("ProLink.MasterTempo", "Master Tempo", base.ASCII)
RangeTempo = ProtoField.string("ProLink.RangeTempo", "Range Tempo", base.ASCII)
PhaseMeter = ProtoField.string("ProLink.PhaseMeter", "Phase Meter", base.ASCII)

UnknownBytes3 = ProtoField.bytes("ProLink.UnknownBytes3", "Unknown Bytes 3", base.SPACE)

VinylSpeedAdjust = ProtoField.string("ProLink.VinylSpeedAdjust", "Vinyl Speed Adjust", base.ASCII)
JogDisplayMode = ProtoField.string("ProLink.JogDisplayMode", "Jog Display Mode", base.ASCII)
PadsBrightness = ProtoField.string("ProLink.PadsBrightness", "Pads Brightness", base.ASCII)
JogWheelLCDBright = ProtoField.string("ProLink.JogWheelLCDBright", "Jog Wheel LCD Brightness", base.ASCII)

-- Mixer Status --
StatusFlag_Play = ProtoField.uint8("ProLink.StatusFlagPlay", "Status Flag Play", base.DEC, NULL, 0x40)
StatusFlag_Master = ProtoField.uint8("ProLink.StatusFlagMaster", "Status Flag Master", base.DEC, NULL, 0x20)
StatusFlag_Sync = ProtoField.uint8("ProLink.StatusFlagSync", "Status Flag Sync", base.DEC, NULL, 0x10)
StatusFlag_OnAir = ProtoField.uint8("ProLink.StatusFlagOnAir", "Status Flag OnAir", base.DEC, NULL, 0x08)
StatusFlag_BPM = ProtoField.uint8("ProLink.StatusFlagBPM", "Status Flag BPM", base.DEC, NULL, 0x02)


pioneer_status.fields = {header, ID, DeviceName, Unknown, SubCategory, ChannelID, 
    Length, IPAddress, ChannelIDDeviceOwner, SlotRequest, MediaName, CreationDate, NumbTracks, 
    USBColor, PlayListNumb, TotalSpace, FreeSpace, ChannelSecondID, TrackType, 
    RekordboxID, DstChannelID, UnknownBytes, OnAirDisplay, ScreenBrightness, QuantizeMode, AutoCueLevel,
    Language, UnknownByte, JogRingBrightness, JogRingIndicator, SlipFlashing, UnknownBytes2, PlayerDiscSlotBrightness, LockFeature,
    AutoPlayMode, QuantizeBeatValue, HotCueAutoLoad, HotCueColor, NeedleLockFeature, TimeMode, JogMode, AutoCue, 
    MasterTempo, RangeTempo, PhaseMeter, UnknownBytes3, VinylSpeedAdjust, JogDisplayMode, JogWheelLCDBright, StatusFlag_Play, StatusFlag_Master, StatusFlag_Sync,
    StatusFlag_OnAir, StatusFlag_BPM
}

function pioneer_status.dissector(buffer, pinfo, tree)
    ---- Function to dissect the buffer ---

    --- Add to the columns
    pinfo.cols.protocol = pioneer_status.name
    ---- Add the info using subtree
    local packet_type = buffer(0xA, 1):uint()
    local subtree = tree:add(pioneer_status, buffer(), "Pro Link Status Protocol")
    subtree:add(header, buffer(0, 0xA)) -- buffer(start_offset, number_bytes)
    subtree:add(ID, buffer(0xA, 1))
    subtree:add(DeviceName, buffer(0xB, 0x14))
    subtree:add(Unknown, buffer(0x1F, 1))

    -- Settings packet has a different structure, let's take into consideration
    if packet_type == 0x34 then
        subtree:add(ChannelID, buffer(0x20,1))
        subtree:add(DstChannelID, buffer(0x21, 1))
    else
        subtree:add(SubCategory, buffer(0x20, 1))
        subtree:add(ChannelID, buffer(0x21, 1))
    end
    subtree:add(Length, buffer(0x22, 2))
    local payload = subtree:add(pioneer_status, buffer(), "Pro Link Packet Payload")

    -- Media Query Request --
    if packet_type == 0x05 then
        subtree:set_text("Pro Link Media Query Request")
        pinfo.cols.protocol = "PRO_LINK_STATUS_MEDIA_QUERY_REQUEST"

        payload:add(IPAddress, buffer(0x24, 4))
        payload:add(ChannelIDDeviceOwner, buffer(0x2B, 1))
        payload:add(SlotRequest, buffer(0x2F, 1))
    end

    -- Media Query Response --
    if packet_type == 0x06 then
        subtree:set_text("Pro Link Media Query Response")
        pinfo.cols.protocol = "PRO_LINK_STATUS_MEDIA_QUERY_RESPONSE"

        payload:add(ChannelIDDeviceOwner, buffer(0x27, 1))
        payload:add(SlotRequest, buffer(0x2b, 1))
        payload:add(MediaName, buffer(0x2C, 40), buffer(0x2C, 40):ustring())
        payload:add(CreationDate, buffer(0x6B, 28), buffer(0x6B, 28):ustring())
        payload:add(NumbTracks, buffer(0xA6, 2))
        payload:add(USBColor, buffer(0xA8, 1), get_usb_color(buffer(0xA8,1):uint()))
        payload:add(PlayListNumb, buffer(0xAE, 2))
        payload:add(TotalSpace, buffer(0xB0, 8))
        payload:add(FreeSpace, buffer(0xB8, 8))
    end

    -- Load Track Command --
    if packet_type == 0x19 then
        subtree:set_text("Pro Link Load Track Command")
        pinfo.cols.protocol = "PRO_LINK_STATUS_LOAD_TRACK_COMMAND"

        payload:add(ChannelSecondID, buffer(0x24, 1))
        payload:add(ChannelIDDeviceOwner, buffer(0x28, 1))
        payload:add(SlotRequest, buffer(0x29, 1))
        payload:add(TrackType, buffer(0x2A, 1), get_track_type(base(0x2A, 1):uint()))
        payload:add(RekordboxID, buffer(0x2C, 4))
        payload:add(DstChannelID, buffer(0x40, 1))
    end

    -- Load Track Ack Command --
    if packet_type == 0x1A then
        subtree:set_text("Pro Link Load Track ACK Command")
        pinfo.cols.protocol = "PRO_LINK_STATUS_LOAD_TRACK_COMMAND_ACK"
    end

    -- Mixer Status Command --
    if packet_type == 0x29 then
        subtree:set_text("Pro Link Mixer Status Command")
        pinfo.cols.protocol = "PRO_LINK_STATUS_MIXER_STATUS"
        
        payload:add(ChannelSecondID, buffer(0x24,1))
        payload:add(StatusFlag_Play, buffer(0x27, 1), buffer(0x27, 1):uint())
        payload:add(StatusFlag_Master, buffer(0x27, 1), buffer(0x27, 1):uint())
        payload:add(StatusFlag_Sync, buffer(0x27, 1), buffer(0x27, 1):uint())
        payload:add(StatusFlag_OnAir, buffer(0x27, 1), buffer(0x27, 1):uint())
        payload:add(StatusFlag_BPM, buffer(0x27, 1), buffer(0x27, 1):uint())
    end

    -- Load Settings Command --
    if packet_type == 0x34 then
        subtree:set_text("Pro Link Load Settings Command")
        pinfo.cols.protocol = "PRO_LINK_STATUS_LOAD_SETTINGS_COMMAND"

        payload:add(UnknownBytes, buffer(0x24, 0x08))
        payload:add(OnAirDisplay, buffer(0x2C, 1), get_ONOFF(buffer(0x2C, 1):uint()))
        payload:add(ScreenBrightness, buffer(0x2D, 1), get_brightness(buffer(0x2D, 1):uint()))
        payload:add(QuantizeMode, buffer(0x2E, 1), get_ONOFF(buffer(0x2E, 1):uint()))
        payload:add(AutoCueLevel, buffer(0x2F, 1), get_autocue_level(buffer(0x2F, 1):uint()))
        payload:add(Language, buffer(0x30, 1), get_language(buffer(0x30, 1):uint()))
        payload:add(UnknownByte, buffer(0x31, 1))
        payload:add(JogRingBrightness, buffer(0x32, 1), get_brightness(buffer(0x32, 1):uint()))
        payload:add(JogRingIndicator, buffer(0x33, 1), get_ONOFF(buffer(0x33, 1):uint()))
        payload:add(SlipFlashing, buffer(0x34, 1), get_ONOFF(buffer(0x34, 1):uint()))
        payload:add(UnknownBytes2, buffer(0x35, 3))
        payload:add(PlayerDiscSlotBrightness, buffer(0x38, 1), get_brightness(buffer(0x38, 1):uint()))
        payload:add(LockFeature, buffer(0x39, 1), get_ONOFF(buffer(0x39, 1):uint()))
        payload:add(SyncMode, buffer(0x3A, 1), get_ONOFF(buffer(0x3A, 1):uint()))
        payload:add(AutoPlayMode, buffer(0x3B, 1), get_autoplaymode(buffer(0x3B, 1):uint()))
        payload:add(QuantizeBeatValue, buffer(0x3C, 1), get_quantizevalue(buffer(0x3C, 1):uint()))
        payload:add(HotCueAutoLoad, buffer(0x3D, 1), get_ONOFF(buffer(0x3D, 1):uint()))
        payload:add(HotCueColor, buffer(0x3E, 1), get_ONOFF(buffer(0x3E, 1):uint()))
        payload:add(NeedleLockFeature, buffer(0x41, 1), get_ONOFF(buffer(0x41, 1):uint()))
        payload:add(TimeMode, buffer(0x44, 1), get_timemode(buffer(0x44, 1):uint()))
        payload:add(JogMode, buffer(0x45, 1), get_jogmode(buffer(0x45, 1):uint()))
        payload:add(AutoCue, buffer(0x46, 1), get_ONOFF(buffer(0x46, 1):uint()))
        payload:add(MasterTempo, buffer(0x47, 1), get_ONOFF(buffer(0x47, 1):uint()))
        payload:add(RangeTempo, buffer(0x48, 1), get_rangetempo(buffer(0x48, 1):uint()))
        payload:add(PhaseMeter, buffer(0x49, 1), get_phasemeter_type(buffer(0x49, 1):uint()))
        payload:add(UnknownBytes3, buffer(0x4A, 2))
        payload:add(VinylSpeedAdjust, buffer(0x4C, 1), get_vsa(buffer(0x4C, 1):uint()))
        payload:add(JogDisplayMode, buffer(0x4D, 1), get_jogdisplaymode(buffer(0x4D, 1):uint()))
        payload:add(PadsBrightness, buffer(0x4E, 1), get_brightness(buffer(0x4E, 1):uint()))
        payload:add(JogWheelLCDBright, buffer(0x4F, 1), get_brightness(buffer(0x4F, 1):uint()))
    end
end

function get_usb_color(usb_color)
    if usb_color == 0x00 then
        return "Default"
    end
    
    if usb_color == 0x01 then
        return "Pink"
    end

    if usb_color == 0x02 then
        return "Red"
    end

    if usb_color == 0x03 then
        return "Orange"
    end

    if usb_color == 0x04 then
        return "Yellow"
    end

    if usb_color == 0x05 then
        return "Green"
    end

    if usb_color == 0x06 then
        return "Aqua"
    end

    if usb_color == 0x07 then
        return "Blue"
    end

    if usb_color == 0x08 then
        return "Purple"
    end

    return "Unknown Color"
end

function get_track_type(track_type)
    if track_type == 0x00 then
        return "No track loaded"
    end

    if track_type == 0x01 then
        return "Rekordbox Track"
    end

    if track_type == 0x02 then
        return "Unanalized track"
    end

    if track_type == 0x05 then
        return "CD Track"
    end

    return "Unknown Track Type"
end

function get_ONOFF(boolean_value)
    if boolean_value == 0x80 then
        return "DEACTIVATE (0x80)"
    end

    if boolean_value == 0x81 then
        return "ACTIVATE (0x81)"
    end

    if boolean_value == 0x82 then
        return "REKORDBOX CONTROL (0x82)"
    end
end

function get_quantizevalue(quantize_value)
    if quantize_value == 0x80 then
        return "1 BEAT (0x80)"
    end
    if quantize_value == 0x81 then
        return "1/2 BEAT (0x81)"
    end
    if quantize_value == 0x82 then
        return "1/4 BEAT (0x82)"
    end
    if quantize_value == 0x83 then
        return "8 BEATS (0x83)"
    end

    return "UNKNOWN"
end

function get_autoplaymode(autoplay_mode)
    if autoplay_mode == 0x80 then
        return "Auto Play Mode Enabled (0x80)"
    end

    if autoplay_mode == 0x81 then
        return "Single Mode (0x81)"
    end

    return "UNKNOWN"
end

function get_autocue_level(autocue_level)
    if autocue_level == 0x80 then
        return "-36 dB(0x80)"
    end
    if autocue_level == 0x81 then
        return "-42 dB(0x81)"
    end
    if autocue_level == 0x82 then
        return "-48 dB(0x82)"
    end
    if autocue_level == 0x83 then
        return "-54 dB(0x83)"
    end
    if autocue_level == 0x84 then
        return "-60 dB(0x84)"
    end
    if autocue_level == 0x85 then
        return "-66 dB(0x85)"
    end
    if autocue_level == 0x86 then
        return "-72 dB(0x86)"
    end
    if autocue_level == 0x87 then
        return "-78 dB(0x87)"
    end
    if autocue_level == 0x88 then
        return "Memory(0x88)"
    end

    return "UNKNOWN"
end

function get_language(language)
    if language == 0x81 then
        return "English (0x81)"
    end
    if language == 0x82 then
        return "French (0x82)"
    end
    if language == 0x83 then
        return "German (0x83)"
    end
    if language == 0x84 then
        return "Italian (0x84)"
    end
    if language == 0x85 then
        return "Dutch (0x85)"
    end
    if language == 0x86 then
        return "Spanish (0x86)"
    end
    if language == 0x87 then
        return "Russian (0x87)"
    end
    if language == 0x88 then
        return "Korean (0x88)"
    end
    if language == 0x89 then
        return "Simplified Chinese (0x89)"
    end
    if language == 0x8A then
        return "Traditional Chinese (0x8A)"
    end
    if language == 0x8B then
        return "Japanese (0x8B)"
    end
    if language == 0x8C then
        return "Portuguese (0x8C)"
    end
    if language == 0x8D then
        return "Swedish (0x8D)"
    end
    if language == 0x8E then
        return "Czech (0x8E)"
    end
    if language == 0x8F then
        return "Magyar (0x8F)"
    end
    if language == 0x90 then
        return "Danish (0x90)"
    end
    if language == 0x91 then
        return "Greek (0x91)"
    end
    if language == 0x92 then
        return "Turkish (0x92)"
    end

    return "UNKNOWN"
end

function get_timemode(time_mode)
    if time_mode == 0x80 then
        return "ELAPSED (0x80)"
    end

    if time_mode == 0x81 then
        return "REMAINING (0x81)"
    end

    return "UNKNOWN"
end

function get_brightness(brightness_level)
    if brightness_level == 0x80 then
        return "OFF (0x80)"
    end

    if brightness_level == 0x81 then
        return "MINIMUM (0x81)"
    end

    if brightness_level == 0x82 then
        return "HALF (0x82)"
    end
    
    if brightness_level == 0x83 then
        return "HALF (0x83)"
    end
    
    if brightness_level == 0x84 then
        return "FULL (0x84)"
    end

    if brightness_level == 0x85 then
        return "FULL (0x85)"
    end
     
    return "UNKNOWN"
end

function get_jogmode(jog_mode)
    if jog_mode == 0x80 then
        return "CDJ (0x80)"
    end

    if jog_mode == 0x81 then
        return "Vinyl (0x81)"
    end

    return "UNKNOWN"
end

function get_rangetempo(range_tempo)
    if range_tempo == 0x80 then
        return "±6 (0x80)"
    end

    if range_tempo == 0x81 then
        return "±10 (0x81)"
    end

    if range_tempo == 0x82 then
        return "±16 (0x82)"
    end

    if range_tempo == 0x83 then
        return "WIDE (0x83)"
    end

    return "UNKNOWN"
end

function get_phasemeter_type(phasemeter_type)
    if phasemeter_type == 0x80 then
        return "Type 1 (0x80)"
    end

    if phasemeter_type == 0x81 then
        return "Type 2 (0x81)"
    end

    return "UNKNOWN"
end

function get_vsa(vinyl_speedadjust)
    if vinyl_speedadjust == 0x80 then
        return "Touch & Release (0x80)"
    end

    if vinyl_speedadjust == 0x81 then
        return "Touch (0x81)"
    end

    if vinyl_speedadjust == 0x82 then
        return "Release (0x82)"
    end

    return "UKNOWN"
end

function get_jogdisplaymode(jog_displaymode)
    if jog_displaymode == 0x80 then
        return "Auto (0x80)"
    end

    if jog_displaymode == 0x81 then
        return "Info (0x81)"
    end

    if jog_displaymode == 0x82 then
        return "Simple (0x82)"
    end

    if jog_displaymode == 0x83 then
        return "Artwork (0x83)"
    end

    return "UNKNOWN"
end

udp_protocol = DissectorTable.get("udp.port"):add(50002, pioneer_status)