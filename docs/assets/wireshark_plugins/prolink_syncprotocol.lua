pioneer_sync = Proto("ProLink_Sync_Protocol", "ProLink Sync Protocol")
--- Common for every Packet

header = ProtoField.bytes("ProLink.Header", "Header", base.SPACE)
ID = ProtoField.uint8("ProLink.ID", "ID", base.HEX)
DeviceName = ProtoField.string("ProLink.DeviceName", "DeviceName", base.UNICODE)
Unknown = ProtoField.uint8("ProLink.Unknown", "Unknown", base.HEX)
SubCategory = ProtoField.uint8("ProLink.SubCategory", "SubCategory", base.HEX)
ChannelID = ProtoField.uint8("ProLink.ChannelID", "ChannelID", base.HEX)
Length = ProtoField.uint16("ProLink.Length", "Length", base.DEC)

-- Fader start command --
C1 = ProtoField.uint8("ProLink.C1", "C1", base.HEX)
C2 = ProtoField.uint8("ProLink.C2", "C2", base.HEX)
C3 = ProtoField.uint8("ProLink.C3", "C3", base.HEX)
C4 = ProtoField.uint8("ProLink.C4", "C4", base.HEX)

-- Channels On Air --
F1 = ProtoField.uint8("ProLink.F1", "F1", base.HEX)
F2 = ProtoField.uint8("ProLink.F2", "F2", base.HEX)
F3 = ProtoField.uint8("ProLink.F3", "F3", base.HEX)
F4 = ProtoField.uint8("ProLink.F4", "F4", base.HEX)
F5 = ProtoField.uint8("ProLink.F5", "F5", base.HEX)
F6 = ProtoField.uint8("ProLink.F6", "F6", base.HEX)

-- Sync Control --
DstChannelID = ProtoField.uint8("ProLink.DstChannelID", "DstChannelID", base.HEX)
SyncMode = ProtoField.uint8("ProLink.SyncMode", "SyncMode", base.HEX)

-- Master Tempo takeover
NewMTChannelID = ProtoField.uint8("ProLink.NewMTChannelID", "NewMTChannelID", base.HEX)

-- Absolute Position --
TrackLength = ProtoField.uint32("ProLink.TrackLength", "TrackLength", base.DEC)
PlayHead = ProtoField.uint32("ProLink.CurrentPosition", "Current Position", base.DEC)
Pitch = ProtoField.float("ProLink.Pitch", "Pitch")
BPM = ProtoField.float("ProLink.BPM", "BPM")

-- Beat Packet --
NextBeat = ProtoField.uint32("ProLink.NextBeat", "NextBeat", base.DEC)
SecondBeat = ProtoField.uint32("ProLink.SecondBeat", "SecondBeat", base.DEC)
NextBar = ProtoField.uint32("ProLink.NextBar", "NextBar", base.DEC)
FourthBar = ProtoField.uint32("ProLink.FourthBar", "FourthBar", base.DEC)
SecondBar = ProtoField.uint32("ProLink.SecondBar", "SecondBar", base.DEC)
EighthBar = ProtoField.uint32("ProLink.EighthBar", "EighthBar", base.DEC)
BeatCounter = ProtoField.uint8("ProLink.BeatCounter", "BeatCounter", base.HEX)


pioneer_sync.fields = {header, ID, DeviceName, Unknown, SubCategory, ChannelID, Length, 
    C1, C2, C3, C4, F1, F2, F3, F4, F5, F6, 
    DstChannelID, SyncMode, NewMTChannelID, TrackLength, 
    PlayHead, Pitch, BPM, NextBeat, SecondBeat, NextBar, FourthBar, SecondBar, EighthBar, BeatCounter}

function pioneer_sync.dissector(buffer, pinfo, tree)
    ---- Function to dissect the buffer ---

    --- Add to the columns
    pinfo.cols.protocol = pioneer_sync.name
    ---- Add the info using subtree
    local subtree = tree:add(pioneer_sync, buffer(), "Pro Link Sync Protocol")
    subtree:add(header, buffer(0, 0xA)) -- buffer(start_offset, number_bytes)
    subtree:add(ID, buffer(0xA, 1))
    subtree:add(DeviceName, buffer(0xB, 0x14))
    subtree:add(Unknown, buffer(0x1F, 1))
    subtree:add(SubCategory, buffer(0x20, 1))
    subtree:add(ChannelID, buffer(0x21, 1))
    subtree:add(Length, buffer(0x22, 2))

    local packet_type = buffer(0xA, 1):uint()
    local payload = subtree:add(pioneer_sync, buffer(), "Pro Link Packet Payload")

    -- Fader start command --
    if packet_type == 0x02 then
        subtree:set_text("Pro Link Fader Start")
        pinfo.cols.protocol = "PRO_LINK_SYNC_FADER_START"

        payload:add(C1, buffer(0x24, 1))
        payload:add(C2, buffer(0x25, 1))
        payload:add(C3, buffer(0x26, 1))
        payload:add(C4, buffer(0x27, 1))
    end

    -- Channels On Air --
    if packet_type == 0x03 then
        subtree:set_text("Pro Link Channels On Air")
        pinfo.cols.protocol = "PRO_LINK_SYNC_CHANNELS_ONAIR"

        local length = buffer(0x22, 2):uint()
        payload:add(F1, buffer(0x24, 1))
        payload:add(F2, buffer(0x25, 1))
        payload:add(F3, buffer(0x26, 1))
        payload:add(F4, buffer(0x27, 1))

        if length == 0x11 then -- Six Channel support
            payload:add(F5, buffer(0x2D, 1))
            payload:add(F6, buffer(0x2E, 1))
        end
    end

    -- Sync Control --
    if packet_type == 0x2A then
        subtree:set_text("Pro Link Sync Control")
        pinfo.cols.protocol = "PRO_LINK_SYNC_SYNCMODE_CONTROL"

        payload:add(DstChannelID, buffer(0x27, 1))
        payload:add(SyncMode, buffer(0x2B, 1))
    end

    -- Master Tempo Takeover Request --
    if packet_type == 0x26 then
        subtree:set_text("Pro Link Master Tempo Takeover Request")
        pinfo.cols.protocol = "PRO_LINK_SYNC_MT_TAKEOVER_REQUEST"

        payload:add(NewMTChannelID, buffer(0x27, 1))
    end

    -- Master tempo Takeover Response --
    if packet_type == 0x27 then
        subtree:set_text("Pro Link Master Tempo Takeover Response")
        pinfo.cols.protocol = "PRO_LINK_SYNC_MT_TAKEOVER_RESPONSE"

        payload:add(DstChannelID, buffer(0x27, 1))
    end

    -- Absolute Position --
    if packet_type == 0x0B then
        subtree:set_text("Pro Link Absolute Position")
        pinfo.cols.protocol = "PRO_LINK_SYNC_ABSOLUTE_POSITION"

        payload:add(TrackLength, buffer(0x24, 4))
        payload:add(PlayHead, buffer(0x28, 4))
        payload:add(Pitch, buffer(0x2c, 4):uint() / 10.0)
        payload:add(BPM, buffer(0x38, 4):uint() / 10.0)
    end

    -- Beat Packet --
    if packet_type == 0x28 then
        subtree:set_text("Pro Link Beat Packet")
        pinfo.cols.protocol = "PRO_LINK_SYNC_BEAT_PACKET"

        payload:add(NextBeat, buffer(0x24, 4))
        payload:add(SecondBeat, buffer(0x28, 4))
        payload:add(NextBar, buffer(0x2C, 4))
        payload:add(FourthBar, buffer(0x30, 4))
        payload:add(SecondBar, buffer(0x34, 4))
        payload:add(EighthBar, buffer(0x38, 4))

        local byte_1 = buffer(0x54, 1):uint()
        local byte_2 = buffer(0x55, 1):uint()
        local byte_3 = buffer(0x56, 1):uint()
        local byte_4 = buffer(0x57, 1):uint()

        local bpm_1 = buffer(0x5A, 1):uint()
        local bpm_2 = buffer(0x5B, 1):uint()

        local pitch = ((byte_2 * 10000 + byte_3 * 100 + byte_4) - 10000) / 10000
        local bpm = (bpm_1 * 256 + bpm_2) / 100.0

        payload:add(Pitch, buffer(0x54, 4), pitch)
        payload:add(BPM, buffer(0x5A, 2), bpm)
        payload:add(BeatCounter, buffer(0x5C, 1))
    end
end

udp_protocol = DissectorTable.get("udp.port"):add(50001, pioneer_sync)