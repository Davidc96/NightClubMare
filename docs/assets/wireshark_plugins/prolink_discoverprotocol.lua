pioneer_discover = Proto("ProLink_Discover_Protocol", "ProLink Discover Protocol")
--- Common for every Packet

header = ProtoField.bytes("ProLink.Header", "Header", base.SPACE)
ID = ProtoField.uint8("ProLink.ID", "ID", base.HEX)
ToMixer = ProtoField.bool("ProLink.ToMixer", "ToMixer")
DeviceName = ProtoField.string("ProLink.DeviceName", "DeviceName", base.UNICODE)
Unknown = ProtoField.uint8("ProLink.Unknown", "Unknown", base.HEX)
SubCategory = ProtoField.uint8("ProLink.SubCategory", "SubCategory", base.HEX)
Length = ProtoField.uint16("ProLink.Length", "Length", base.DEC)
------ Custom for packet ------

-- Device Init --
Payload = ProtoField.uint8("ProLinkDI.Payload", "Payload", base.HEX)

-- First Stage --
PacketCounter = ProtoField.uint8("ProLink.PacketCounter", "PacketCounter", base.HEX)
DeviceType = ProtoField.string("ProLink.DeviceType", "DeviceType", base.ASCII)
MacAddress = ProtoField.ether("ProLink.MACAddress", "MACAddress")

-- Second Stage --
IPAddress = ProtoField.ipv4("ProLink.IP", "IP")
ChannelID = ProtoField.uint8("ProLink.ChannelID", "ChannelID", base.HEX)
AutoAssign = ProtoField.uint8("ProLink.AutoAssigned", "AutoAssigned")

pioneer_discover.fields = {header, ID, ToMixer, DeviceName, Unknown, SubCategory, Length, Payload, PacketCounter, DeviceType, 
    MacAddress, IPAddress, ChannelID, AutoAssign}

function pioneer_discover.dissector(buffer, pinfo, tree)
    ---- Function to dissect the buffer ---

    --- Add to the columns
    pinfo.cols.protocol = pioneer_discover.name
    ---- Add the info using subtree
    local subtree = tree:add(pioneer_discover, buffer(), "Pro Link Discover Protocol")
    
    subtree:add(header, buffer(0, 0xA)) -- buffer(start_offset, number_bytes)
    subtree:add(ID, buffer(0xA, 1))
    subtree:add(ToMixer, buffer(0xB, 1))
    subtree:add(DeviceName, buffer(0xC, 0x14))
    subtree:add(Unknown, buffer(0x20, 1))
    subtree:add(SubCategory, buffer(0x21, 1))
    subtree:add(Length, buffer(0x22, 2))

    local packet_type = buffer(0xA, 1):uint()
    local payload = subtree:add(pioneer_discover, buffer(), "Pro Link Packet Payload")
    -- Device Init --
    if packet_type == 0x0A then
        subtree:set_text("Pro Link Discover Device Init")
        pinfo.cols.protocol = "PRO_LINK_DEVICE_INIT"

        payload:add(Payload, buffer(0x24, 1))
    end

    -- First Attempt --
    if packet_type == 0x00 then
        subtree:set_text("Pro Link Discover First Stage")
        pinfo.cols.protocol = "PRO_LINK_DISCOVER_FIRST_STAGE"

        payload:add(PacketCounter, buffer(0x24, 1))
        payload:add(DeviceType, buffer(0x25, 1), get_devicetype(buffer(0x25, 1):uint()))
        payload:add(MacAddress, buffer(0x26, 6))
    end

    -- Second Attempt --
    if packet_type == 0x02 then
        subtree:set_text("Pro Link Discover Second Stage")
        pinfo.cols.protocol = "PRO_LINK_DISCOVER_SECOND_STAGE"

        payload:add(IPAddress, buffer(0x24, 4))
        payload:add(MacAddress, buffer(0x28, 6))
        payload:add(ChannelID, buffer(0x2E, 1))
        payload:add(PacketCounter, buffer(0x2F, 1))
        payload:add(DeviceType, buffer(0x30, 1), get_devicetype(buffer(0x30, 1):uint()))
        payload:add(AutoAssign, buffer(0x31, 1))
    end

    -- Final Stage --
    if packet_type == 0x04 then
        subtree:set_text("Pro Link Discover Final Stage")
        pinfo.cols.protocol = "PRO_LINK_DISCOVER_FINAL_STAGE"

        payload:add(ChannelID, buffer(0x24, 1))
        payload:add(PacketCounter, buffer(0x25, 1))
    end

    -- Keep Alive Packet --
    if packet_type == 0x06 then
        subtree:set_text("Pro Link Discover Keep Alive")
        pinfo.cols.protocol = "PRO_LINK_DISCOVER_KEEP_ALIVE"

        payload:add(ChannelID, buffer(0x24, 1))
        payload:add(DeviceType, buffer(0x25, 1), get_devicetype(buffer(0x25, 1):uint()))
        payload:add(MacAddress, buffer(0x26, 6))
        payload:add(IPAddress, buffer(0x2C, 4))
    end

    -- Mixer Device Number Assigment Intention --
    if packet_type == 0x01 then
        subtree:set_text("Pro Link Mixer Device Number Assigment")
        pinfo.cols.protocol = "PRO_LINK_DISCOVER_MIXER_DEV_NUMB_ASSIGNMENT"

        payload:add(IPAddress, buffer(0x24, 4))
        payload:add(MACAddress, buffer(0X28, 6))
    end

    -- CDJ Device Number Assigment --
    if packet_type == 0x03 then
        subtree:set_text("Pro Link CDJ Device Number Assigment")
        pinfo.cols.protocol = "PRO_LINK_DISCOVER_CDJ_DEV_NUMB_ASSIGMENT"

        payload:add(ChannelID, buffer(0x24, 1))
        payload:add(PacketCounter, buffer(0x25, 1))
    end

    -- CDJ Device Number Assigment Finished --
    if packet_type == 0x05 then
        subtree:set_text("Pro Link CDJ Device Number Assigment Finished")
        pinfo.cols.protocol = "PRO_LINK_DISCOVER_CDJ_DEV_NUMB_ASSIGMENT_FINISHED"

        payload:add(ChannelID, buffer(0x24, 1))
    end

    -- Channel Conflicts --
    if packet_type == 0x08 then
        subtree:set_text("Pro Link Channel Conflict")
        pinfo.cols.protocol = "PRO_LINK_DISCOVER_CHANNEL_CONFLICT"

        payload:add(ChannelID, buffer(0x24, 1))
        payload:add(IPAddress, buffer(0x25, 4))
    end
end

function get_devicetype(device_type)
    if device_type == 0x01 then
        return "CDJ (0x01)"
    end

    if device_type == 0x02 then
        return "Mixer (0x02)"
    end

    if device_type == 0x04 then
        return "PC (0x04)"
    end

    return "Unknown"
end

function get_autoassigned(is_autoassigned)
    
end

udp_protocol = DissectorTable.get("udp.port"):add(50000, pioneer_discover)