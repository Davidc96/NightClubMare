using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.Commands.MetadataCommands.Messages
{
    public class RenderMenuResponseMessage : MessageStructure
    {
        private byte Separator1 = 0x11;
        public uint ArtistID;
        private byte Separator2 = 0x11;
        public uint RekordboxTrackID;
        private byte Separator3 = 0x11;
        public uint TrackTitleLength;
        private byte Separator4 = 0x11;
        private uint StrLength1;
        public string TitleName;
        private byte Separator5 = 0x11;
        private uint StrLength2;
        public uint ArtistNameLength;
        private byte Separator6 = 0x11;
        public string ArtistName;
        private byte Separator7 = 0x11;
        public uint TypeOfItem;                 //0x0704: Title And Artist
        private byte Separator8 = 0x11;
        public uint Flags;
        private byte Separator9 = 0x11;
        public uint UnknownNumber1;
        private byte Separator10 = 0x11;
        public uint UnknownNumber2;
        private byte Separator11 = 0x11;
        public uint UnknownNumber3 ;
        private byte Separator12 = 0x11;
        public uint UnknownNumber4;
        private byte Separator13 = 0x11;
        private byte[] BlankBytes;


        public void ParseBytes(byte[] packet)
        {
           this.FromBytes(packet);
           using(BinaryReader bin = new BinaryReader(new MemoryStream(packet)))
           {
                bin.BaseStream.Seek(0x20, SeekOrigin.Begin);
                Separator1 = bin.ReadByte();
                ArtistID = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x4)), 0);
                Separator2 = bin.ReadByte();
                RekordboxTrackID = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x4)), 0);
                Separator3 = bin.ReadByte();
                TrackTitleLength = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x4)), 0);
                Separator4 = bin.ReadByte();
                StrLength1 = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x4)), 0);
                if (TrackTitleLength > 0)
                {
                    TitleName = Utils.BytesToName(bin.ReadBytes((int)TrackTitleLength));
                }
                Separator5 = bin.ReadByte();
                ArtistNameLength = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x4)), 0);
                Separator6 = bin.ReadByte();
                StrLength1 = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x4)), 0);
                if (ArtistNameLength > 0)
                {
                    ArtistName = Utils.BytesToName(bin.ReadBytes((int)ArtistNameLength));
                }
                Separator7 = bin.ReadByte();
                TypeOfItem = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x4)), 0);
                Separator8 = bin.ReadByte();
                Flags = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x4)), 0);
                Separator9 = bin.ReadByte();
                UnknownNumber1 = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x4)), 0);
                Separator10 = bin.ReadByte();
                UnknownNumber2 = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x4)), 0);
                Separator11 = bin.ReadByte();
                UnknownNumber3 = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x4)), 0);
                Separator12 = bin.ReadByte();
                UnknownNumber4 = BitConverter.ToUInt32(Utils.SwapEndianesss(bin.ReadBytes(0x4)), 0);
                Separator13 = bin.ReadByte();
                BlankBytes = bin.ReadBytes(0x4);
           }
        }
    }
}
