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

pioneer_sync.fields = {header, ID, ToMixer, DeviceName, Unknown, SubCategory, ChannelID, Length, C1, C2, C3, C4, F1, F2, F3, F4, F5, F6 }

function pioneer_sync.dissector(buffer, pinfo, tree)
    ---- Function to dissect the buffer ---

    --- Add to the columns
    pinfo.cols.protocol = pioneer_discover.name
    ---- Add the info using subtree
    local subtree = tree:add(pioneer_discover, buffer(), "Pro Link Sync Protocol")
    subtree:add(header, buffer(0, 0xA)) -- buffer(start_offset, number_bytes)
    subtree:add(ID, buffer(0xA, 1))
    subtree:add(DeviceName, buffer(0xB, 0x14))
    subtree:add(Unknown, buffer(0x1F, 1))
    subtree:add(SubCategory, buffer(0x20, 1))
    subtree:add(ChannelID, buffer(0x21, 1))
    subtree:add(Length, buffer(0x22, 2))

    local packet_type = buffer(0xA, 1):uint()

    -- Fader start command --
    if packet_type == 0x02 then
        subtree:set_text("Pro Link Fader Start")

        subtree:add(C1, base(0x24, 1))
        subtree:add(C2, base(0x25, 1))
        subtree:add(C3, base(0x26, 1))
        subtree:add(C4, base(0x27, 1))
    end

    -- Channels On Air --
    if packet_type == 0x03 then
        subtree:set_text("Pro Link Channels On Air")

        local length = buffer(0x22, 2):uint()
        subtree:add(F1, base(0x24, 1))
        subtree:add(F2, base(0x25, 1))
        subtree:add(F3, base(0x26, 1))
        subtree:add(F4, base(0x27, 1))

        if length == 0x11 then -- Six Channel support
            subtree:add(F5, base(0x2D, 1))
            subtree:add(F6, base(0x2E, 1))
        end
    end

    -- Sync Control --
    if packet_type == 0x2A then
        subtree:set_text("Pro Link Sync Control")

end