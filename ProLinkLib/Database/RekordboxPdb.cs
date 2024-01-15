using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

namespace ProLinkLib.Database
{

    /// <summary>
    /// This is a relational database format designed to be efficiently used
    /// by very low power devices (there were deployments on 16 bit devices
    /// with 32K of RAM). Today you are most likely to encounter it within
    /// the Pioneer Professional DJ ecosystem, because it is the format that
    /// their rekordbox software uses to write USB and SD media which can be
    /// mounted in DJ controllers and used to play and mix music.
    /// 
    /// It has been reverse-engineered to facilitate sophisticated
    /// integrations with light and laser shows, videos, and other musical
    /// instruments, by supporting deep knowledge of what is playing and
    /// what is coming next through monitoring the network communications of
    /// the players.
    /// 
    /// The file is divided into fixed-size blocks. The first block has a
    /// header that establishes the block size, and lists the tables
    /// available in the database, identifying their types and the index of
    /// the first of the series of linked pages that make up that table.
    /// 
    /// Each table is made up of a series of rows which may be spread across
    /// any number of pages. The pages start with a header describing the
    /// page and linking to the next page. The rest of the page is used as a
    /// heap: rows are scattered around it, and located using an index
    /// structure that builds backwards from the end of the page. Each row
    /// of a given type has a fixed size structure which links to any
    /// variable-sized strings by their offsets within the page.
    /// 
    /// As changes are made to the table, some records may become unused,
    /// and there may be gaps within the heap that are too small to be used
    /// by other data. There is a bit map in the row index that identifies
    /// which rows are actually present. Rows that are not present must be
    /// ignored: they do not contain valid (or even necessarily well-formed)
    /// data.
    /// 
    /// The majority of the work in reverse-engineering this format was
    /// performed by @henrybetts and @flesniak, for which I am hugely
    /// grateful. @GreyCat helped me learn the intricacies (and best
    /// practices) of Kaitai far faster than I would have managed on my own.
    /// </summary>
    /// <remarks>
    /// Reference: <a href="https://github.com/Deep-Symmetry/crate-digger/blob/master/doc/Analysis.pdf">Source</a>
    /// </remarks>
    public partial class RekordboxPdb : KaitaiStruct
    {
        public static RekordboxPdb FromFile(string fileName)
        {
            return new RekordboxPdb(new KaitaiStream(fileName));
        }


        public enum PageType
        {
            Tracks = 0,
            Genres = 1,
            Artists = 2,
            Albums = 3,
            Labels = 4,
            Keys = 5,
            Colors = 6,
            PlaylistTree = 7,
            PlaylistEntries = 8,
            Unknown9 = 9,
            Unknown10 = 10,
            HistoryPlaylists = 11,
            HistoryEntries = 12,
            Artwork = 13,
            Unknown14 = 14,
            Unknown15 = 15,
            Columns = 16,
            Unknown17 = 17,
            Unknown18 = 18,
            History = 19,
        }
        public RekordboxPdb(KaitaiStream p__io, KaitaiStruct p__parent = null, RekordboxPdb p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }
        private void _read()
        {
            __unnamed0 = m_io.ReadU4le();
            _lenPage = m_io.ReadU4le();
            _numTables = m_io.ReadU4le();
            _nextUnusedPage = m_io.ReadU4le();
            __unnamed4 = m_io.ReadU4le();
            _sequence = m_io.ReadU4le();
            _gap = m_io.ReadBytes(4);
            if (!((KaitaiStream.ByteArrayCompare(Gap, new byte[] { 0, 0, 0, 0 }) == 0)))
            {
                throw new ValidationNotEqualError(new byte[] { 0, 0, 0, 0 }, Gap, M_Io, "/seq/6");
            }
            _tables = new List<Table>();
            for (var i = 0; i < NumTables; i++)
            {
                _tables.Add(new Table(m_io, this, m_root));
            }
        }

        /// <summary>
        /// A variable length string which can be stored in a variety of
        /// different encodings.
        /// </summary>
        public partial class DeviceSqlString : KaitaiStruct
        {
            public static DeviceSqlString FromFile(string fileName)
            {
                return new DeviceSqlString(new KaitaiStream(fileName));
            }

            public DeviceSqlString(KaitaiStream p__io, KaitaiStruct p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _lengthAndKind = m_io.ReadU1();
                switch (LengthAndKind)
                {
                    case 64:
                        {
                            _body = new DeviceSqlLongAscii(m_io, this, m_root);
                            break;
                        }
                    case 144:
                        {
                            _body = new DeviceSqlLongUtf16le(m_io, this, m_root);
                            break;
                        }
                    default:
                        {
                            _body = new DeviceSqlShortAscii(LengthAndKind, m_io, this, m_root);
                            break;
                        }
                }
            }
            private byte _lengthAndKind;
            private KaitaiStruct _body;
            private RekordboxPdb m_root;
            private KaitaiStruct m_parent;

            /// <summary>
            /// Mangled length of an ordinary ASCII string if odd, or a flag
            /// indicating another encoding with a longer length value to
            /// follow.
            /// </summary>
            public byte LengthAndKind { get { return _lengthAndKind; } }
            public KaitaiStruct Body { get { return _body; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// A row that holds a history playlist ID and name, linking to
        /// the track IDs captured during a performance on the player.
        /// </summary>
        public partial class HistoryPlaylistRow : KaitaiStruct
        {
            public static HistoryPlaylistRow FromFile(string fileName)
            {
                return new HistoryPlaylistRow(new KaitaiStream(fileName));
            }

            public HistoryPlaylistRow(KaitaiStream p__io, RekordboxPdb.RowRef p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _id = m_io.ReadU4le();
                _name = new DeviceSqlString(m_io, this, m_root);
            }
            private uint _id;
            private DeviceSqlString _name;
            private RekordboxPdb m_root;
            private RekordboxPdb.RowRef m_parent;

            /// <summary>
            /// The unique identifier by which this history playlist can
            /// be requested.
            /// </summary>
            public uint Id { get { return _id; } }

            /// <summary>
            /// The variable-length string naming the playlist.
            /// </summary>
            public DeviceSqlString Name { get { return _name; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.RowRef M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// A row that holds a playlist name, ID, indication of whether it
        /// is an ordinary playlist or a folder of other playlists, a link
        /// to its parent folder, and its sort order.
        /// </summary>
        public partial class PlaylistTreeRow : KaitaiStruct
        {
            public static PlaylistTreeRow FromFile(string fileName)
            {
                return new PlaylistTreeRow(new KaitaiStream(fileName));
            }

            public PlaylistTreeRow(KaitaiStream p__io, RekordboxPdb.RowRef p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_isFolder = false;
                _read();
            }
            private void _read()
            {
                _parentId = m_io.ReadU4le();
                __unnamed1 = m_io.ReadBytes(4);
                _sortOrder = m_io.ReadU4le();
                _id = m_io.ReadU4le();
                _rawIsFolder = m_io.ReadU4le();
                _name = new DeviceSqlString(m_io, this, m_root);
            }
            private bool f_isFolder;
            private bool _isFolder;
            public bool IsFolder
            {
                get
                {
                    if (f_isFolder)
                        return _isFolder;
                    _isFolder = (bool)(RawIsFolder != 0);
                    f_isFolder = true;
                    return _isFolder;
                }
            }
            private uint _parentId;
            private byte[] __unnamed1;
            private uint _sortOrder;
            private uint _id;
            private uint _rawIsFolder;
            private DeviceSqlString _name;
            private RekordboxPdb m_root;
            private RekordboxPdb.RowRef m_parent;

            /// <summary>
            /// The ID of the `playlist_tree_row` in which this one can be
            /// found, or `0` if this playlist exists at the root level.
            /// </summary>
            public uint ParentId { get { return _parentId; } }
            public byte[] Unnamed_1 { get { return __unnamed1; } }

            /// <summary>
            /// The order in which the entries of this playlist are sorted.
            /// </summary>
            public uint SortOrder { get { return _sortOrder; } }

            /// <summary>
            /// The unique identifier by which this playlist or folder can
            /// be requested and linked from other rows.
            /// </summary>
            public uint Id { get { return _id; } }

            /// <summary>
            /// Has a non-zero value if this is actually a folder rather
            /// than a playlist.
            /// </summary>
            public uint RawIsFolder { get { return _rawIsFolder; } }

            /// <summary>
            /// The variable-length string naming the playlist.
            /// </summary>
            public DeviceSqlString Name { get { return _name; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.RowRef M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// A row that holds a color name and the associated ID.
        /// </summary>
        public partial class ColorRow : KaitaiStruct
        {
            public static ColorRow FromFile(string fileName)
            {
                return new ColorRow(new KaitaiStream(fileName));
            }

            public ColorRow(KaitaiStream p__io, RekordboxPdb.RowRef p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                __unnamed0 = m_io.ReadBytes(5);
                _id = m_io.ReadU2le();
                __unnamed2 = m_io.ReadU1();
                _name = new DeviceSqlString(m_io, this, m_root);
            }
            private byte[] __unnamed0;
            private ushort _id;
            private byte __unnamed2;
            private DeviceSqlString _name;
            private RekordboxPdb m_root;
            private RekordboxPdb.RowRef m_parent;
            public byte[] Unnamed_0 { get { return __unnamed0; } }

            /// <summary>
            /// The unique identifier by which this color can be requested
            /// and linked from other rows (such as tracks).
            /// </summary>
            public ushort Id { get { return _id; } }
            public byte Unnamed_2 { get { return __unnamed2; } }

            /// <summary>
            /// The variable-length string naming the color.
            /// </summary>
            public DeviceSqlString Name { get { return _name; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.RowRef M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// An ASCII-encoded string up to 127 bytes long.
        /// </summary>
        public partial class DeviceSqlShortAscii : KaitaiStruct
        {
            public DeviceSqlShortAscii(byte p_lengthAndKind, KaitaiStream p__io, RekordboxPdb.DeviceSqlString p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _lengthAndKind = p_lengthAndKind;
                f_length = false;
                _read();
            }
            private void _read()
            {
                _text = System.Text.Encoding.GetEncoding("ascii").GetString(m_io.ReadBytes((Length - 1)));
            }
            private bool f_length;
            private int _length;

            /// <summary>
            /// the length extracted of the entire device_sql_short_ascii type
            /// </summary>
            public int Length
            {
                get
                {
                    if (f_length)
                        return _length;
                    _length = (int)((LengthAndKind >> 1));
                    f_length = true;
                    return _length;
                }
            }
            private string _text;
            private byte _lengthAndKind;
            private RekordboxPdb m_root;
            private RekordboxPdb.DeviceSqlString m_parent;

            /// <summary>
            /// The content of the string.
            /// </summary>
            public string Text { get { return _text; } }

            /// <summary>
            /// Contains the actual length, incremented, doubled, and
            /// incremented again. Go figure.
            /// </summary>
            public byte LengthAndKind { get { return _lengthAndKind; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.DeviceSqlString M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// A row that holds an album name and ID.
        /// </summary>
        public partial class AlbumRow : KaitaiStruct
        {
            public static AlbumRow FromFile(string fileName)
            {
                return new AlbumRow(new KaitaiStream(fileName));
            }

            public AlbumRow(KaitaiStream p__io, RekordboxPdb.RowRef p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_name = false;
                _read();
            }
            private void _read()
            {
                __unnamed0 = m_io.ReadU2le();
                _indexShift = m_io.ReadU2le();
                __unnamed2 = m_io.ReadU4le();
                _artistId = m_io.ReadU4le();
                _id = m_io.ReadU4le();
                __unnamed5 = m_io.ReadU4le();
                __unnamed6 = m_io.ReadU1();
                _ofsName = m_io.ReadU1();
            }
            private bool f_name;
            private DeviceSqlString _name;

            /// <summary>
            /// The name of this album.
            /// </summary>
            public DeviceSqlString Name
            {
                get
                {
                    if (f_name)
                        return _name;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsName));
                    _name = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_name = true;
                    return _name;
                }
            }
            private ushort __unnamed0;
            private ushort _indexShift;
            private uint __unnamed2;
            private uint _artistId;
            private uint _id;
            private uint __unnamed5;
            private byte __unnamed6;
            private byte _ofsName;
            private RekordboxPdb m_root;
            private RekordboxPdb.RowRef m_parent;

            /// <summary>
            /// Some kind of magic word? Usually 0x80, 0x00.
            /// </summary>
            public ushort Unnamed_0 { get { return __unnamed0; } }

            /// <summary>
            /// TODO name from @flesniak, but what does it mean?
            /// </summary>
            public ushort IndexShift { get { return _indexShift; } }
            public uint Unnamed_2 { get { return __unnamed2; } }

            /// <summary>
            /// Identifies the artist associated with the album.
            /// </summary>
            public uint ArtistId { get { return _artistId; } }

            /// <summary>
            /// The unique identifier by which this album can be requested
            /// and linked from other rows (such as tracks).
            /// </summary>
            public uint Id { get { return _id; } }
            public uint Unnamed_5 { get { return __unnamed5; } }

            /// <summary>
            /// @flesniak says: &quot;alwayx 0x03, maybe an unindexed empty string&quot;
            /// </summary>
            public byte Unnamed_6 { get { return __unnamed6; } }

            /// <summary>
            /// The location of the variable-length name string, relative to
            /// the start of this row.
            /// </summary>
            public byte OfsName { get { return _ofsName; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.RowRef M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// A table page, consisting of a short header describing the
        /// content of the page and linking to the next page, followed by a
        /// heap in which row data is found. At the end of the page there is
        /// an index which locates all rows present in the heap via their
        /// offsets past the end of the page header.
        /// </summary>
        public partial class Page : KaitaiStruct
        {
            public static Page FromFile(string fileName)
            {
                return new Page(new KaitaiStream(fileName));
            }

            public Page(KaitaiStream p__io, RekordboxPdb.PageRef p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_numRows = false;
                f_numGroups = false;
                f_rowGroups = false;
                f_heapPos = false;
                f_isDataPage = false;
                _read();
            }
            private void _read()
            {
                _gap = m_io.ReadBytes(4);
                if (!((KaitaiStream.ByteArrayCompare(Gap, new byte[] { 0, 0, 0, 0 }) == 0)))
                {
                    throw new ValidationNotEqualError(new byte[] { 0, 0, 0, 0 }, Gap, M_Io, "/types/page/seq/0");
                }
                _pageIndex = m_io.ReadU4le();
                _type = ((RekordboxPdb.PageType)m_io.ReadU4le());
                _nextPage = new PageRef(m_io, this, m_root);
                __unnamed4 = m_io.ReadU4le();
                __unnamed5 = m_io.ReadBytes(4);
                _numRowsSmall = m_io.ReadU1();
                __unnamed7 = m_io.ReadU1();
                __unnamed8 = m_io.ReadU1();
                _pageFlags = m_io.ReadU1();
                _freeSize = m_io.ReadU2le();
                _usedSize = m_io.ReadU2le();
                __unnamed12 = m_io.ReadU2le();
                _numRowsLarge = m_io.ReadU2le();
                __unnamed14 = m_io.ReadU2le();
                __unnamed15 = m_io.ReadU2le();
                if (false)
                {
                    _heap = m_io.ReadBytesFull();
                }
            }
            private bool f_numRows;
            private ushort _numRows;

            /// <summary>
            /// The number of rows on this page (controls the number of row
            /// index entries there are, but some of those may not be marked
            /// as present in the table due to deletion).
            /// </summary>
            public ushort NumRows
            {
                get
                {
                    if (f_numRows)
                        return _numRows;
                    _numRows = (ushort)((((NumRowsLarge > NumRowsSmall) && (NumRowsLarge != 8191)) ? NumRowsLarge : NumRowsSmall));
                    f_numRows = true;
                    return _numRows;
                }
            }
            private bool f_numGroups;
            private int _numGroups;

            /// <summary>
            /// The number of row groups that are present in the index. Each
            /// group can hold up to sixteen rows. All but the final one
            /// will hold sixteen rows.
            /// </summary>
            public int NumGroups
            {
                get
                {
                    if (f_numGroups)
                        return _numGroups;
                    _numGroups = (int)((((NumRows - 1) / 16) + 1));
                    f_numGroups = true;
                    return _numGroups;
                }
            }
            private bool f_rowGroups;
            private List<RowGroup> _rowGroups;

            /// <summary>
            /// The actual row groups making up the row index. Each group
            /// can hold up to sixteen rows. Non-data pages do not have
            /// actual rows, and attempting to parse them can crash.
            /// </summary>
            public List<RowGroup> RowGroups
            {
                get
                {
                    if (f_rowGroups)
                        return _rowGroups;
                    if (IsDataPage)
                    {
                        _rowGroups = new List<RowGroup>();
                        for (ushort i = 0; i < NumGroups; i++)
                        {
                            _rowGroups.Add(new RowGroup(i, m_io, this, m_root));
                        }
                        f_rowGroups = true;
                    }
                    return _rowGroups;
                }
            }
            private bool f_heapPos;
            private int _heapPos;
            public int HeapPos
            {
                get
                {
                    if (f_heapPos)
                        return _heapPos;
                    _heapPos = (int)(M_Io.Pos);
                    f_heapPos = true;
                    return _heapPos;
                }
            }
            private bool f_isDataPage;
            private bool _isDataPage;
            public bool IsDataPage
            {
                get
                {
                    if (f_isDataPage)
                        return _isDataPage;
                    _isDataPage = (bool)((PageFlags & 64) == 0);
                    f_isDataPage = true;
                    return _isDataPage;
                }
            }
            private byte[] _gap;
            private uint _pageIndex;
            private PageType _type;
            private PageRef _nextPage;
            private uint __unnamed4;
            private byte[] __unnamed5;
            private byte _numRowsSmall;
            private byte __unnamed7;
            private byte __unnamed8;
            private byte _pageFlags;
            private ushort _freeSize;
            private ushort _usedSize;
            private ushort __unnamed12;
            private ushort _numRowsLarge;
            private ushort __unnamed14;
            private ushort __unnamed15;
            private byte[] _heap;
            private RekordboxPdb m_root;
            private RekordboxPdb.PageRef m_parent;

            /// <summary>
            /// Only exposed until
            /// https://github.com/kaitai-io/kaitai_struct/issues/825 can be
            /// fixed.
            /// </summary>
            public byte[] Gap { get { return _gap; } }

            /// <summary>
            /// Matches the index we used to look up the page, sanity check?
            /// </summary>
            public uint PageIndex { get { return _pageIndex; } }

            /// <summary>
            /// Identifies the type of information stored in the rows of this page.
            /// </summary>
            public PageType Type { get { return _type; } }

            /// <summary>
            /// Index of the next page containing this type of rows. Points past
            /// the end of the file if there are no more.
            /// </summary>
            public PageRef NextPage { get { return _nextPage; } }

            /// <summary>
            /// @flesniak said: &quot;sequence number (0-&gt;1: 8-&gt;13, 1-&gt;2: 22, 2-&gt;3: 27)&quot;
            /// </summary>
            public uint Unnamed_4 { get { return __unnamed4; } }
            public byte[] Unnamed_5 { get { return __unnamed5; } }

            /// <summary>
            /// Holds the value used for `num_rows` (see below) unless
            /// `num_rows_large` is larger (but not equal to `0x1fff`). This
            /// seems like some strange mechanism to deal with the fact that
            /// lots of tiny entries, such as are found in the
            /// `playlist_entries` table, are too big to count with a single
            /// byte. But why not just always use `num_rows_large`, then?
            /// </summary>
            public byte NumRowsSmall { get { return _numRowsSmall; } }

            /// <summary>
            /// @flesniak said: &quot;a bitmask (1st track: 32)&quot;
            /// </summary>
            public byte Unnamed_7 { get { return __unnamed7; } }

            /// <summary>
            /// @flesniak said: &quot;often 0, sometimes larger, esp. for pages
            /// with high real_entry_count (e.g. 12 for 101 entries)&quot;
            /// </summary>
            public byte Unnamed_8 { get { return __unnamed8; } }

            /// <summary>
            /// @flesniak said: &quot;strange pages: 0x44, 0x64; otherwise seen: 0x24, 0x34&quot;
            /// </summary>
            public byte PageFlags { get { return _pageFlags; } }

            /// <summary>
            /// Unused space (in bytes) in the page heap, excluding the row
            /// index at end of page.
            /// </summary>
            public ushort FreeSize { get { return _freeSize; } }

            /// <summary>
            /// The number of bytes that are in use in the page heap.
            /// </summary>
            public ushort UsedSize { get { return _usedSize; } }

            /// <summary>
            /// @flesniak said: &quot;(0-&gt;1: 2)&quot;
            /// </summary>
            public ushort Unnamed_12 { get { return __unnamed12; } }

            /// <summary>
            /// Holds the value used for `num_rows` (as described above)
            /// when that is too large to fit into `num_rows_small`, and
            /// that situation seems to be indicated when this value is
            /// larger than `num_rows_small`, but not equal to `0x1fff`.
            /// This seems like some strange mechanism to deal with the fact
            /// that lots of tiny entries, such as are found in the
            /// `playlist_entries` table, are too big to count with a single
            /// byte. But why not just always use this value, then?
            /// </summary>
            public ushort NumRowsLarge { get { return _numRowsLarge; } }

            /// <summary>
            /// @flesniak said: &quot;1004 for strange blocks, 0 otherwise&quot;
            /// </summary>
            public ushort Unnamed_14 { get { return __unnamed14; } }

            /// <summary>
            /// @flesniak said: &quot;always 0 except 1 for history pages, num
            /// entries for strange pages?&quot;
            /// </summary>
            public ushort Unnamed_15 { get { return __unnamed15; } }
            public byte[] Heap { get { return _heap; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.PageRef M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// A group of row indices, which are built backwards from the end
        /// of the page. Holds up to sixteen row offsets, along with a bit
        /// mask that indicates whether each row is actually present in the
        /// table.
        /// </summary>
        public partial class RowGroup : KaitaiStruct
        {
            public RowGroup(ushort p_groupIndex, KaitaiStream p__io, RekordboxPdb.Page p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _groupIndex = p_groupIndex;
                f_base = false;
                f_rowPresentFlags = false;
                f_rows = false;
                _read();
            }
            private void _read()
            {
            }
            private bool f_base;
            private int _base;

            /// <summary>
            /// The starting point of this group of row indices.
            /// </summary>
            public int Base
            {
                get
                {
                    if (f_base)
                        return _base;
                    _base = (int)((M_Root.LenPage - (GroupIndex * 36)));
                    f_base = true;
                    return _base;
                }
            }
            private bool f_rowPresentFlags;
            private ushort _rowPresentFlags;

            /// <summary>
            /// Each bit specifies whether a particular row is present. The
            /// low order bit corresponds to the first row in this index,
            /// whose offset immediately precedes these flag bits. The
            /// second bit corresponds to the row whose offset precedes
            /// that, and so on.
            /// </summary>
            public ushort RowPresentFlags
            {
                get
                {
                    if (f_rowPresentFlags)
                        return _rowPresentFlags;
                    long _pos = m_io.Pos;
                    m_io.Seek((Base - 4));
                    _rowPresentFlags = m_io.ReadU2le();
                    m_io.Seek(_pos);
                    f_rowPresentFlags = true;
                    return _rowPresentFlags;
                }
            }
            private bool f_rows;
            private List<RowRef> _rows;

            /// <summary>
            /// The row offsets in this group.
            /// </summary>
            public List<RowRef> Rows
            {
                get
                {
                    if (f_rows)
                        return _rows;
                    _rows = new List<RowRef>();
                    for (ushort i = 0; i < (GroupIndex < (M_Parent.NumGroups - 1) ? 16 : (KaitaiStream.Mod((M_Parent.NumRows - 1), 16) + 1)); i++)
                    {
                        _rows.Add(new RowRef(i, m_io, this, m_root));
                    }
                    f_rows = true;
                    return _rows;
                }
            }
            private ushort _groupIndex;
            private RekordboxPdb m_root;
            private RekordboxPdb.Page m_parent;

            /// <summary>
            /// Identifies which group is being generated. They build backwards
            /// from the end of the page.
            /// </summary>
            public ushort GroupIndex { get { return _groupIndex; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.Page M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// A row that holds a genre name and the associated ID.
        /// </summary>
        public partial class GenreRow : KaitaiStruct
        {
            public static GenreRow FromFile(string fileName)
            {
                return new GenreRow(new KaitaiStream(fileName));
            }

            public GenreRow(KaitaiStream p__io, RekordboxPdb.RowRef p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _id = m_io.ReadU4le();
                _name = new DeviceSqlString(m_io, this, m_root);
            }
            private uint _id;
            private DeviceSqlString _name;
            private RekordboxPdb m_root;
            private RekordboxPdb.RowRef m_parent;

            /// <summary>
            /// The unique identifier by which this genre can be requested
            /// and linked from other rows (such as tracks).
            /// </summary>
            public uint Id { get { return _id; } }

            /// <summary>
            /// The variable-length string naming the genre.
            /// </summary>
            public DeviceSqlString Name { get { return _name; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.RowRef M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// A row that associates a track with a position in a history playlist.
        /// </summary>
        public partial class HistoryEntryRow : KaitaiStruct
        {
            public static HistoryEntryRow FromFile(string fileName)
            {
                return new HistoryEntryRow(new KaitaiStream(fileName));
            }

            public HistoryEntryRow(KaitaiStream p__io, RekordboxPdb.RowRef p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _trackId = m_io.ReadU4le();
                _playlistId = m_io.ReadU4le();
                _entryIndex = m_io.ReadU4le();
            }
            private uint _trackId;
            private uint _playlistId;
            private uint _entryIndex;
            private RekordboxPdb m_root;
            private RekordboxPdb.RowRef m_parent;

            /// <summary>
            /// The track found at this position in the playlist.
            /// </summary>
            public uint TrackId { get { return _trackId; } }

            /// <summary>
            /// The history playlist to which this entry belongs.
            /// </summary>
            public uint PlaylistId { get { return _playlistId; } }

            /// <summary>
            /// The position within the playlist represented by this entry.
            /// </summary>
            public uint EntryIndex { get { return _entryIndex; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.RowRef M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// A row that holds the path to an album art image file and the
        /// associated artwork ID.
        /// </summary>
        public partial class ArtworkRow : KaitaiStruct
        {
            public static ArtworkRow FromFile(string fileName)
            {
                return new ArtworkRow(new KaitaiStream(fileName));
            }

            public ArtworkRow(KaitaiStream p__io, RekordboxPdb.RowRef p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _id = m_io.ReadU4le();
                _path = new DeviceSqlString(m_io, this, m_root);
            }
            private uint _id;
            private DeviceSqlString _path;
            private RekordboxPdb m_root;
            private RekordboxPdb.RowRef m_parent;

            /// <summary>
            /// The unique identifier by which this art can be requested
            /// and linked from other rows (such as tracks).
            /// </summary>
            public uint Id { get { return _id; } }

            /// <summary>
            /// The variable-length file path string at which the art file
            /// can be found.
            /// </summary>
            public DeviceSqlString Path { get { return _path; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.RowRef M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// An ASCII-encoded string preceded by a two-byte length field in a four-byte header.
        /// </summary>
        public partial class DeviceSqlLongAscii : KaitaiStruct
        {
            public static DeviceSqlLongAscii FromFile(string fileName)
            {
                return new DeviceSqlLongAscii(new KaitaiStream(fileName));
            }

            public DeviceSqlLongAscii(KaitaiStream p__io, RekordboxPdb.DeviceSqlString p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _length = m_io.ReadU2le();
                __unnamed1 = m_io.ReadU1();
                _text = System.Text.Encoding.GetEncoding("ascii").GetString(m_io.ReadBytes((Length - 4)));
            }
            private ushort _length;
            private byte __unnamed1;
            private string _text;
            private RekordboxPdb m_root;
            private RekordboxPdb.DeviceSqlString m_parent;

            /// <summary>
            /// Contains the length of the string in bytes.
            /// </summary>
            public ushort Length { get { return _length; } }
            public byte Unnamed_1 { get { return __unnamed1; } }

            /// <summary>
            /// The content of the string.
            /// </summary>
            public string Text { get { return _text; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.DeviceSqlString M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// A row that holds an artist name and ID.
        /// </summary>
        public partial class ArtistRow : KaitaiStruct
        {
            public static ArtistRow FromFile(string fileName)
            {
                return new ArtistRow(new KaitaiStream(fileName));
            }

            public ArtistRow(KaitaiStream p__io, RekordboxPdb.RowRef p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_ofsNameFar = false;
                f_name = false;
                _read();
            }
            private void _read()
            {
                _subtype = m_io.ReadU2le();
                _indexShift = m_io.ReadU2le();
                _id = m_io.ReadU4le();
                __unnamed3 = m_io.ReadU1();
                _ofsNameNear = m_io.ReadU1();
            }
            private bool f_ofsNameFar;
            private ushort? _ofsNameFar;

            /// <summary>
            /// For names that might be further than 0xff bytes from the
            /// start of this row, this holds a two-byte offset, and is
            /// signalled by the subtype value.
            /// </summary>
            public ushort? OfsNameFar
            {
                get
                {
                    if (f_ofsNameFar)
                        return _ofsNameFar;
                    if (Subtype == 100)
                    {
                        long _pos = m_io.Pos;
                        m_io.Seek((M_Parent.RowBase + 10));
                        _ofsNameFar = m_io.ReadU2le();
                        m_io.Seek(_pos);
                        f_ofsNameFar = true;
                    }
                    return _ofsNameFar;
                }
            }
            private bool f_name;
            private DeviceSqlString _name;

            /// <summary>
            /// The name of this artist.
            /// </summary>
            public DeviceSqlString Name
            {
                get
                {
                    if (f_name)
                        return _name;
                    long _pos = m_io.Pos;
                    m_io.Seek((long)(M_Parent.RowBase + (Subtype == 100 ? OfsNameFar : OfsNameNear)));
                    _name = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_name = true;
                    return _name;
                }
            }
            private ushort _subtype;
            private ushort _indexShift;
            private uint _id;
            private byte __unnamed3;
            private byte _ofsNameNear;
            private RekordboxPdb m_root;
            private RekordboxPdb.RowRef m_parent;

            /// <summary>
            /// Usually 0x60, but 0x64 means we have a long name offset
            /// embedded in the row.
            /// </summary>
            public ushort Subtype { get { return _subtype; } }

            /// <summary>
            /// TODO name from @flesniak, but what does it mean?
            /// </summary>
            public ushort IndexShift { get { return _indexShift; } }

            /// <summary>
            /// The unique identifier by which this artist can be requested
            /// and linked from other rows (such as tracks).
            /// </summary>
            public uint Id { get { return _id; } }

            /// <summary>
            /// @flesniak says: &quot;always 0x03, maybe an unindexed empty string&quot;
            /// </summary>
            public byte Unnamed_3 { get { return __unnamed3; } }

            /// <summary>
            /// The location of the variable-length name string, relative to
            /// the start of this row, unless subtype is 0x64.
            /// </summary>
            public byte OfsNameNear { get { return _ofsNameNear; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.RowRef M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// An index which points to a table page (its offset can be found
        /// by multiplying the index by the `page_len` value in the file
        /// header). This type allows the linked page to be lazy loaded.
        /// </summary>
        public partial class PageRef : KaitaiStruct
        {
            public static PageRef FromFile(string fileName)
            {
                return new PageRef(new KaitaiStream(fileName));
            }

            public PageRef(KaitaiStream p__io, KaitaiStruct p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_body = false;
                _read();
            }
            private void _read()
            {
                _index = m_io.ReadU4le();
            }
            private bool f_body;
            private Page _body;

            /// <summary>
            /// When referenced, loads the specified page and parses its
            /// contents appropriately for the type of data it contains.
            /// </summary>
            public Page Body
            {
                get
                {
                    if (f_body)
                        return _body;
                    KaitaiStream io = M_Root.M_Io;
                    long _pos = io.Pos;
                    io.Seek((M_Root.LenPage * Index));
                    __raw_body = io.ReadBytes(M_Root.LenPage);
                    var io___raw_body = new KaitaiStream(__raw_body);
                    _body = new Page(io___raw_body, this, m_root);
                    io.Seek(_pos);
                    f_body = true;
                    return _body;
                }
            }
            private uint _index;
            private RekordboxPdb m_root;
            private KaitaiStruct m_parent;
            private byte[] __raw_body;

            /// <summary>
            /// Identifies the desired page number.
            /// </summary>
            public uint Index { get { return _index; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
            public byte[] M_RawBody { get { return __raw_body; } }
        }

        /// <summary>
        /// A row that describes a track that can be played, with many
        /// details about the music, and links to other tables like artists,
        /// albums, keys, etc.
        /// </summary>
        public partial class TrackRow : KaitaiStruct
        {
            public static TrackRow FromFile(string fileName)
            {
                return new TrackRow(new KaitaiStream(fileName));
            }

            public TrackRow(KaitaiStream p__io, RekordboxPdb.RowRef p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_unknownString8 = false;
                f_unknownString6 = false;
                f_analyzeDate = false;
                f_filePath = false;
                f_autoloadHotcues = false;
                f_dateAdded = false;
                f_unknownString3 = false;
                f_texter = false;
                f_kuvoPublic = false;
                f_mixName = false;
                f_unknownString5 = false;
                f_unknownString4 = false;
                f_message = false;
                f_unknownString2 = false;
                f_isrc = false;
                f_unknownString7 = false;
                f_filename = false;
                f_analyzePath = false;
                f_comment = false;
                f_releaseDate = false;
                f_title = false;
                _read();
            }
            private void _read()
            {
                __unnamed0 = m_io.ReadU2le();
                _indexShift = m_io.ReadU2le();
                _bitmask = m_io.ReadU4le();
                _sampleRate = m_io.ReadU4le();
                _composerId = m_io.ReadU4le();
                _fileSize = m_io.ReadU4le();
                __unnamed6 = m_io.ReadU4le();
                __unnamed7 = m_io.ReadU2le();
                __unnamed8 = m_io.ReadU2le();
                _artworkId = m_io.ReadU4le();
                _keyId = m_io.ReadU4le();
                _originalArtistId = m_io.ReadU4le();
                _labelId = m_io.ReadU4le();
                _remixerId = m_io.ReadU4le();
                _bitrate = m_io.ReadU4le();
                _trackNumber = m_io.ReadU4le();
                _tempo = m_io.ReadU4le();
                _genreId = m_io.ReadU4le();
                _albumId = m_io.ReadU4le();
                _artistId = m_io.ReadU4le();
                _id = m_io.ReadU4le();
                _discNumber = m_io.ReadU2le();
                _playCount = m_io.ReadU2le();
                _year = m_io.ReadU2le();
                _sampleDepth = m_io.ReadU2le();
                _duration = m_io.ReadU2le();
                __unnamed26 = m_io.ReadU2le();
                _colorId = m_io.ReadU1();
                _rating = m_io.ReadU1();
                __unnamed29 = m_io.ReadU2le();
                __unnamed30 = m_io.ReadU2le();
                _ofsStrings = new List<ushort>();
                for (var i = 0; i < 21; i++)
                {
                    _ofsStrings.Add(m_io.ReadU2le());
                }
            }
            private bool f_unknownString8;
            private DeviceSqlString _unknownString8;

            /// <summary>
            /// A string of unknown purpose, usually empty.
            /// </summary>
            public DeviceSqlString UnknownString8
            {
                get
                {
                    if (f_unknownString8)
                        return _unknownString8;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[18]));
                    _unknownString8 = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_unknownString8 = true;
                    return _unknownString8;
                }
            }
            private bool f_unknownString6;
            private DeviceSqlString _unknownString6;

            /// <summary>
            /// A string of unknown purpose, usually empty.
            /// </summary>
            public DeviceSqlString UnknownString6
            {
                get
                {
                    if (f_unknownString6)
                        return _unknownString6;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[9]));
                    _unknownString6 = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_unknownString6 = true;
                    return _unknownString6;
                }
            }
            private bool f_analyzeDate;
            private DeviceSqlString _analyzeDate;

            /// <summary>
            /// A string containing the date this track was analyzed by rekordbox.
            /// </summary>
            public DeviceSqlString AnalyzeDate
            {
                get
                {
                    if (f_analyzeDate)
                        return _analyzeDate;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[15]));
                    _analyzeDate = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_analyzeDate = true;
                    return _analyzeDate;
                }
            }
            private bool f_filePath;
            private DeviceSqlString _filePath;

            /// <summary>
            /// The file path of the track audio file.
            /// </summary>
            public DeviceSqlString FilePath
            {
                get
                {
                    if (f_filePath)
                        return _filePath;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[20]));
                    _filePath = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_filePath = true;
                    return _filePath;
                }
            }
            private bool f_autoloadHotcues;
            private DeviceSqlString _autoloadHotcues;

            /// <summary>
            /// A string whose value is always either empty or &quot;ON&quot;, and
            /// which apparently for some insane reason is used, rather than
            /// a single bit somewhere, to control whether hot-cues are
            /// auto-loaded for the track.
            /// </summary>
            public DeviceSqlString AutoloadHotcues
            {
                get
                {
                    if (f_autoloadHotcues)
                        return _autoloadHotcues;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[7]));
                    _autoloadHotcues = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_autoloadHotcues = true;
                    return _autoloadHotcues;
                }
            }
            private bool f_dateAdded;
            private DeviceSqlString _dateAdded;

            /// <summary>
            /// A string containing the date this track was added to the collection.
            /// </summary>
            public DeviceSqlString DateAdded
            {
                get
                {
                    if (f_dateAdded)
                        return _dateAdded;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[10]));
                    _dateAdded = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_dateAdded = true;
                    return _dateAdded;
                }
            }
            private bool f_unknownString3;
            private DeviceSqlString _unknownString3;

            /// <summary>
            /// A string of unknown purpose; @flesniak said &quot;strange
            /// strings, often zero length, sometimes low binary values
            /// 0x01/0x02 as content&quot;
            /// </summary>
            public DeviceSqlString UnknownString3
            {
                get
                {
                    if (f_unknownString3)
                        return _unknownString3;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[3]));
                    _unknownString3 = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_unknownString3 = true;
                    return _unknownString3;
                }
            }
            private bool f_texter;
            private DeviceSqlString _texter;

            /// <summary>
            /// A string of unknown purpose, which @flesnik named.
            /// </summary>
            public DeviceSqlString Texter
            {
                get
                {
                    if (f_texter)
                        return _texter;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[1]));
                    _texter = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_texter = true;
                    return _texter;
                }
            }
            private bool f_kuvoPublic;
            private DeviceSqlString _kuvoPublic;

            /// <summary>
            /// A string whose value is always either empty or &quot;ON&quot;, and
            /// which apparently for some insane reason is used, rather than
            /// a single bit somewhere, to control whether the track
            /// information is visible on Kuvo.
            /// </summary>
            public DeviceSqlString KuvoPublic
            {
                get
                {
                    if (f_kuvoPublic)
                        return _kuvoPublic;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[6]));
                    _kuvoPublic = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_kuvoPublic = true;
                    return _kuvoPublic;
                }
            }
            private bool f_mixName;
            private DeviceSqlString _mixName;

            /// <summary>
            /// A string naming the remix of the track, if known.
            /// </summary>
            public DeviceSqlString MixName
            {
                get
                {
                    if (f_mixName)
                        return _mixName;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[12]));
                    _mixName = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_mixName = true;
                    return _mixName;
                }
            }
            private bool f_unknownString5;
            private DeviceSqlString _unknownString5;

            /// <summary>
            /// A string of unknown purpose.
            /// </summary>
            public DeviceSqlString UnknownString5
            {
                get
                {
                    if (f_unknownString5)
                        return _unknownString5;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[8]));
                    _unknownString5 = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_unknownString5 = true;
                    return _unknownString5;
                }
            }
            private bool f_unknownString4;
            private DeviceSqlString _unknownString4;

            /// <summary>
            /// A string of unknown purpose; @flesniak said &quot;strange
            /// strings, often zero length, sometimes low binary values
            /// 0x01/0x02 as content&quot;
            /// </summary>
            public DeviceSqlString UnknownString4
            {
                get
                {
                    if (f_unknownString4)
                        return _unknownString4;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[4]));
                    _unknownString4 = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_unknownString4 = true;
                    return _unknownString4;
                }
            }
            private bool f_message;
            private DeviceSqlString _message;

            /// <summary>
            /// A string of unknown purpose, which @flesnik named.
            /// </summary>
            public DeviceSqlString Message
            {
                get
                {
                    if (f_message)
                        return _message;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[5]));
                    _message = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_message = true;
                    return _message;
                }
            }
            private bool f_unknownString2;
            private DeviceSqlString _unknownString2;

            /// <summary>
            /// A string of unknown purpose; @flesniak said &quot;thought
            /// tracknumber -&gt; wrong!&quot;
            /// </summary>
            public DeviceSqlString UnknownString2
            {
                get
                {
                    if (f_unknownString2)
                        return _unknownString2;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[2]));
                    _unknownString2 = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_unknownString2 = true;
                    return _unknownString2;
                }
            }
            private bool f_isrc;
            private DeviceSqlString _isrc;

            /// <summary>
            /// International Standard Recording Code of track
            /// when known (in mangled format).
            /// </summary>
            public DeviceSqlString Isrc
            {
                get
                {
                    if (f_isrc)
                        return _isrc;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[0]));
                    _isrc = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_isrc = true;
                    return _isrc;
                }
            }
            private bool f_unknownString7;
            private DeviceSqlString _unknownString7;

            /// <summary>
            /// A string of unknown purpose, usually empty.
            /// </summary>
            public DeviceSqlString UnknownString7
            {
                get
                {
                    if (f_unknownString7)
                        return _unknownString7;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[13]));
                    _unknownString7 = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_unknownString7 = true;
                    return _unknownString7;
                }
            }
            private bool f_filename;
            private DeviceSqlString _filename;

            /// <summary>
            /// The file name of the track audio file.
            /// </summary>
            public DeviceSqlString Filename
            {
                get
                {
                    if (f_filename)
                        return _filename;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[19]));
                    _filename = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_filename = true;
                    return _filename;
                }
            }
            private bool f_analyzePath;
            private DeviceSqlString _analyzePath;

            /// <summary>
            /// The file path of the track analysis, which allows rapid
            /// seeking to particular times in variable bit-rate files,
            /// jumping to particular beats, visual waveform previews, and
            /// stores cue points and loops.
            /// </summary>
            public DeviceSqlString AnalyzePath
            {
                get
                {
                    if (f_analyzePath)
                        return _analyzePath;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[14]));
                    _analyzePath = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_analyzePath = true;
                    return _analyzePath;
                }
            }
            private bool f_comment;
            private DeviceSqlString _comment;

            /// <summary>
            /// The comment assigned to the track by the DJ, if any.
            /// </summary>
            public DeviceSqlString Comment
            {
                get
                {
                    if (f_comment)
                        return _comment;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[16]));
                    _comment = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_comment = true;
                    return _comment;
                }
            }
            private bool f_releaseDate;
            private DeviceSqlString _releaseDate;

            /// <summary>
            /// A string containing the date this track was released, if known.
            /// </summary>
            public DeviceSqlString ReleaseDate
            {
                get
                {
                    if (f_releaseDate)
                        return _releaseDate;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[11]));
                    _releaseDate = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_releaseDate = true;
                    return _releaseDate;
                }
            }
            private bool f_title;
            private DeviceSqlString _title;

            /// <summary>
            /// The title of the track.
            /// </summary>
            public DeviceSqlString Title
            {
                get
                {
                    if (f_title)
                        return _title;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.RowBase + OfsStrings[17]));
                    _title = new DeviceSqlString(m_io, this, m_root);
                    m_io.Seek(_pos);
                    f_title = true;
                    return _title;
                }
            }
            private ushort __unnamed0;
            private ushort _indexShift;
            private uint _bitmask;
            private uint _sampleRate;
            private uint _composerId;
            private uint _fileSize;
            private uint __unnamed6;
            private ushort __unnamed7;
            private ushort __unnamed8;
            private uint _artworkId;
            private uint _keyId;
            private uint _originalArtistId;
            private uint _labelId;
            private uint _remixerId;
            private uint _bitrate;
            private uint _trackNumber;
            private uint _tempo;
            private uint _genreId;
            private uint _albumId;
            private uint _artistId;
            private uint _id;
            private ushort _discNumber;
            private ushort _playCount;
            private ushort _year;
            private ushort _sampleDepth;
            private ushort _duration;
            private ushort __unnamed26;
            private byte _colorId;
            private byte _rating;
            private ushort __unnamed29;
            private ushort __unnamed30;
            private List<ushort> _ofsStrings;
            private RekordboxPdb m_root;
            private RekordboxPdb.RowRef m_parent;

            /// <summary>
            /// Some kind of magic word? Usually 0x24, 0x00.
            /// </summary>
            public ushort Unnamed_0 { get { return __unnamed0; } }

            /// <summary>
            /// TODO name from @flesniak, but what does it mean?
            /// </summary>
            public ushort IndexShift { get { return _indexShift; } }

            /// <summary>
            /// TODO what do the bits mean?
            /// </summary>
            public uint Bitmask { get { return _bitmask; } }

            /// <summary>
            /// Playback sample rate of the audio file.
            /// </summary>
            public uint SampleRate { get { return _sampleRate; } }

            /// <summary>
            /// References a row in the artist table if the composer is
            /// known.
            /// </summary>
            public uint ComposerId { get { return _composerId; } }

            /// <summary>
            /// The length of the audio file, in bytes.
            /// </summary>
            public uint FileSize { get { return _fileSize; } }

            /// <summary>
            /// Some ID? Purpose as yet unknown.
            /// </summary>
            public uint Unnamed_6 { get { return __unnamed6; } }

            /// <summary>
            /// From @flesniak: &quot;always 19048?&quot;
            /// </summary>
            public ushort Unnamed_7 { get { return __unnamed7; } }

            /// <summary>
            /// From @flesniak: &quot;always 30967?&quot;
            /// </summary>
            public ushort Unnamed_8 { get { return __unnamed8; } }

            /// <summary>
            /// References a row in the artwork table if there is album art.
            /// </summary>
            public uint ArtworkId { get { return _artworkId; } }

            /// <summary>
            /// References a row in the keys table if the track has a known
            /// main musical key.
            /// </summary>
            public uint KeyId { get { return _keyId; } }

            /// <summary>
            /// References a row in the artwork table if this is a cover
            /// performance and the original artist is known.
            /// </summary>
            public uint OriginalArtistId { get { return _originalArtistId; } }

            /// <summary>
            /// References a row in the labels table if the track has a
            /// known record label.
            /// </summary>
            public uint LabelId { get { return _labelId; } }

            /// <summary>
            /// References a row in the artists table if the track has a
            /// known remixer.
            /// </summary>
            public uint RemixerId { get { return _remixerId; } }

            /// <summary>
            /// Playback bit rate of the audio file.
            /// </summary>
            public uint Bitrate { get { return _bitrate; } }

            /// <summary>
            /// The position of the track within an album.
            /// </summary>
            public uint TrackNumber { get { return _trackNumber; } }

            /// <summary>
            /// The tempo at the start of the track in beats per minute,
            /// multiplied by 100.
            /// </summary>
            public uint Tempo { get { return _tempo; } }

            /// <summary>
            /// References a row in the genres table if the track has a
            /// known musical genre.
            /// </summary>
            public uint GenreId { get { return _genreId; } }

            /// <summary>
            /// References a row in the albums table if the track has a
            /// known album.
            /// </summary>
            public uint AlbumId { get { return _albumId; } }

            /// <summary>
            /// References a row in the artists table if the track has a
            /// known performer.
            /// </summary>
            public uint ArtistId { get { return _artistId; } }

            /// <summary>
            /// The id by which this track can be looked up; players will
            /// report this value in their status packets when they are
            /// playing the track.
            /// </summary>
            public uint Id { get { return _id; } }

            /// <summary>
            /// The number of the disc on which this track is found, if it
            /// is known to be part of a multi-disc album.
            /// </summary>
            public ushort DiscNumber { get { return _discNumber; } }

            /// <summary>
            /// The number of times this track has been played.
            /// </summary>
            public ushort PlayCount { get { return _playCount; } }

            /// <summary>
            /// The year in which this track was released.
            /// </summary>
            public ushort Year { get { return _year; } }

            /// <summary>
            /// The number of bits per sample of the audio file.
            /// </summary>
            public ushort SampleDepth { get { return _sampleDepth; } }

            /// <summary>
            /// The length, in seconds, of the track when played at normal
            /// speed.
            /// </summary>
            public ushort Duration { get { return _duration; } }

            /// <summary>
            /// From @flesniak: &quot;always 41?&quot;
            /// </summary>
            public ushort Unnamed_26 { get { return __unnamed26; } }

            /// <summary>
            /// References a row in the colors table if the track has been
            /// assigned a color.
            /// </summary>
            public byte ColorId { get { return _colorId; } }

            /// <summary>
            /// The number of stars to display for the track, 0 to 5.
            /// </summary>
            public byte Rating { get { return _rating; } }

            /// <summary>
            /// From @flesniak: &quot;always 1?&quot;
            /// </summary>
            public ushort Unnamed_29 { get { return __unnamed29; } }

            /// <summary>
            /// From @flesniak: &quot;alternating 2 or 3&quot;
            /// </summary>
            public ushort Unnamed_30 { get { return __unnamed30; } }

            /// <summary>
            /// The location, relative to the start of this row, of a
            /// variety of variable-length strings.
            /// </summary>
            public List<ushort> OfsStrings { get { return _ofsStrings; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.RowRef M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// A row that holds a musical key and the associated ID.
        /// </summary>
        public partial class KeyRow : KaitaiStruct
        {
            public static KeyRow FromFile(string fileName)
            {
                return new KeyRow(new KaitaiStream(fileName));
            }

            public KeyRow(KaitaiStream p__io, RekordboxPdb.RowRef p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _id = m_io.ReadU4le();
                _id2 = m_io.ReadU4le();
                _name = new DeviceSqlString(m_io, this, m_root);
            }
            private uint _id;
            private uint _id2;
            private DeviceSqlString _name;
            private RekordboxPdb m_root;
            private RekordboxPdb.RowRef m_parent;

            /// <summary>
            /// The unique identifier by which this key can be requested
            /// and linked from other rows (such as tracks).
            /// </summary>
            public uint Id { get { return _id; } }

            /// <summary>
            /// Seems to be a second copy of the ID?
            /// </summary>
            public uint Id2 { get { return _id2; } }

            /// <summary>
            /// The variable-length string naming the key.
            /// </summary>
            public DeviceSqlString Name { get { return _name; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.RowRef M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// A row that associates a track with a position in a playlist.
        /// </summary>
        public partial class PlaylistEntryRow : KaitaiStruct
        {
            public static PlaylistEntryRow FromFile(string fileName)
            {
                return new PlaylistEntryRow(new KaitaiStream(fileName));
            }

            public PlaylistEntryRow(KaitaiStream p__io, RekordboxPdb.RowRef p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _entryIndex = m_io.ReadU4le();
                _trackId = m_io.ReadU4le();
                _playlistId = m_io.ReadU4le();
            }
            private uint _entryIndex;
            private uint _trackId;
            private uint _playlistId;
            private RekordboxPdb m_root;
            private RekordboxPdb.RowRef m_parent;

            /// <summary>
            /// The position within the playlist represented by this entry.
            /// </summary>
            public uint EntryIndex { get { return _entryIndex; } }

            /// <summary>
            /// The track found at this position in the playlist.
            /// </summary>
            public uint TrackId { get { return _trackId; } }

            /// <summary>
            /// The playlist to which this entry belongs.
            /// </summary>
            public uint PlaylistId { get { return _playlistId; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.RowRef M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// A row that holds a label name and the associated ID.
        /// </summary>
        public partial class LabelRow : KaitaiStruct
        {
            public static LabelRow FromFile(string fileName)
            {
                return new LabelRow(new KaitaiStream(fileName));
            }

            public LabelRow(KaitaiStream p__io, RekordboxPdb.RowRef p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _id = m_io.ReadU4le();
                _name = new DeviceSqlString(m_io, this, m_root);
            }
            private uint _id;
            private DeviceSqlString _name;
            private RekordboxPdb m_root;
            private RekordboxPdb.RowRef m_parent;

            /// <summary>
            /// The unique identifier by which this label can be requested
            /// and linked from other rows (such as tracks).
            /// </summary>
            public uint Id { get { return _id; } }

            /// <summary>
            /// The variable-length string naming the label.
            /// </summary>
            public DeviceSqlString Name { get { return _name; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.RowRef M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// A UTF-16LE-encoded string preceded by a two-byte length field in a four-byte header.
        /// </summary>
        public partial class DeviceSqlLongUtf16le : KaitaiStruct
        {
            public static DeviceSqlLongUtf16le FromFile(string fileName)
            {
                return new DeviceSqlLongUtf16le(new KaitaiStream(fileName));
            }

            public DeviceSqlLongUtf16le(KaitaiStream p__io, RekordboxPdb.DeviceSqlString p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _length = m_io.ReadU2le();
                __unnamed1 = m_io.ReadU1();
                _text = System.Text.Encoding.GetEncoding("utf-16le").GetString(m_io.ReadBytes((Length - 4)));
            }
            private ushort _length;
            private byte __unnamed1;
            private string _text;
            private RekordboxPdb m_root;
            private RekordboxPdb.DeviceSqlString m_parent;

            /// <summary>
            /// Contains the length of the string in bytes, plus four trailing bytes that must be ignored.
            /// </summary>
            public ushort Length { get { return _length; } }
            public byte Unnamed_1 { get { return __unnamed1; } }

            /// <summary>
            /// The content of the string.
            /// </summary>
            public string Text { get { return _text; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.DeviceSqlString M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// Each table is a linked list of pages containing rows of a single
        /// type. This header describes the nature of the table and links to
        /// its pages by index.
        /// </summary>
        public partial class Table : KaitaiStruct
        {
            public static Table FromFile(string fileName)
            {
                return new Table(new KaitaiStream(fileName));
            }

            public Table(KaitaiStream p__io, RekordboxPdb p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _type = ((RekordboxPdb.PageType)m_io.ReadU4le());
                _emptyCandidate = m_io.ReadU4le();
                _firstPage = new PageRef(m_io, this, m_root);
                _lastPage = new PageRef(m_io, this, m_root);
            }
            private PageType _type;
            private uint _emptyCandidate;
            private PageRef _firstPage;
            private PageRef _lastPage;
            private RekordboxPdb m_root;
            private RekordboxPdb m_parent;

            /// <summary>
            /// Identifies the kind of rows that are found in this table.
            /// </summary>
            public PageType Type { get { return _type; } }
            public uint EmptyCandidate { get { return _emptyCandidate; } }

            /// <summary>
            /// Links to the chain of pages making up that table. The first
            /// page seems to always contain similar garbage patterns and
            /// zero rows, but the next page it links to contains the start
            /// of the meaningful data rows.
            /// </summary>
            public PageRef FirstPage { get { return _firstPage; } }

            /// <summary>
            /// Holds the index of the last page that makes up this table.
            /// When following the linked list of pages of the table, you
            /// either need to stop when you reach this page, or when you
            /// notice that the `next_page` link you followed took you to a
            /// page of a different `type`.
            /// </summary>
            public PageRef LastPage { get { return _lastPage; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// An offset which points to a row in the table, whose actual
        /// presence is controlled by one of the bits in
        /// `row_present_flags`. This instance allows the row itself to be
        /// lazily loaded, unless it is not present, in which case there is
        /// no content to be loaded.
        /// </summary>
        public partial class RowRef : KaitaiStruct
        {
            public RowRef(ushort p_rowIndex, KaitaiStream p__io, RekordboxPdb.RowGroup p__parent = null, RekordboxPdb p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _rowIndex = p_rowIndex;
                f_ofsRow = false;
                f_rowBase = false;
                f_present = false;
                f_body = false;
                _read();
            }
            private void _read()
            {
            }
            private bool f_ofsRow;
            private ushort _ofsRow;

            /// <summary>
            /// The offset of the start of the row (in bytes past the end of
            /// the page header).
            /// </summary>
            public ushort OfsRow
            {
                get
                {
                    if (f_ofsRow)
                        return _ofsRow;
                    long _pos = m_io.Pos;
                    m_io.Seek((M_Parent.Base - (6 + (2 * RowIndex))));
                    _ofsRow = m_io.ReadU2le();
                    m_io.Seek(_pos);
                    f_ofsRow = true;
                    return _ofsRow;
                }
            }
            private bool f_rowBase;
            private int _rowBase;

            /// <summary>
            /// The location of this row relative to the start of the page.
            /// A variety of pointers (such as all device_sql_string values)
            /// are calculated with respect to this position.
            /// </summary>
            public int RowBase
            {
                get
                {
                    if (f_rowBase)
                        return _rowBase;
                    _rowBase = (int)((OfsRow + M_Parent.M_Parent.HeapPos));
                    f_rowBase = true;
                    return _rowBase;
                }
            }
            private bool f_present;
            private bool _present;

            /// <summary>
            /// Indicates whether the row index considers this row to be
            /// present in the table. Will be `false` if the row has been
            /// deleted.
            /// </summary>
            public bool Present
            {
                get
                {
                    if (f_present)
                        return _present;
                    _present = (bool)((((M_Parent.RowPresentFlags >> RowIndex) & 1) != 0 ? true : false));
                    f_present = true;
                    return _present;
                }
            }
            private bool f_body;
            private KaitaiStruct _body;

            /// <summary>
            /// The actual content of the row, as long as it is present.
            /// </summary>
            public KaitaiStruct Body
            {
                get
                {
                    if (f_body)
                        return _body;
                    if (Present)
                    {
                        long _pos = m_io.Pos;
                        m_io.Seek(RowBase);
                        switch (M_Parent.M_Parent.Type)
                        {
                            case RekordboxPdb.PageType.PlaylistTree:
                                {
                                    _body = new PlaylistTreeRow(m_io, this, m_root);
                                    break;
                                }
                            case RekordboxPdb.PageType.Keys:
                                {
                                    _body = new KeyRow(m_io, this, m_root);
                                    break;
                                }
                            case RekordboxPdb.PageType.Artists:
                                {
                                    _body = new ArtistRow(m_io, this, m_root);
                                    break;
                                }
                            case RekordboxPdb.PageType.Albums:
                                {
                                    _body = new AlbumRow(m_io, this, m_root);
                                    break;
                                }
                            case RekordboxPdb.PageType.Genres:
                                {
                                    _body = new GenreRow(m_io, this, m_root);
                                    break;
                                }
                            case RekordboxPdb.PageType.HistoryPlaylists:
                                {
                                    _body = new HistoryPlaylistRow(m_io, this, m_root);
                                    break;
                                }
                            case RekordboxPdb.PageType.Artwork:
                                {
                                    _body = new ArtworkRow(m_io, this, m_root);
                                    break;
                                }
                            case RekordboxPdb.PageType.PlaylistEntries:
                                {
                                    _body = new PlaylistEntryRow(m_io, this, m_root);
                                    break;
                                }
                            case RekordboxPdb.PageType.Labels:
                                {
                                    _body = new LabelRow(m_io, this, m_root);
                                    break;
                                }
                            case RekordboxPdb.PageType.Tracks:
                                {
                                    _body = new TrackRow(m_io, this, m_root);
                                    break;
                                }
                            case RekordboxPdb.PageType.HistoryEntries:
                                {
                                    _body = new HistoryEntryRow(m_io, this, m_root);
                                    break;
                                }
                            case RekordboxPdb.PageType.Colors:
                                {
                                    _body = new ColorRow(m_io, this, m_root);
                                    break;
                                }
                        }
                        m_io.Seek(_pos);
                        f_body = true;
                    }
                    return _body;
                }
            }
            private ushort _rowIndex;
            private RekordboxPdb m_root;
            private RekordboxPdb.RowGroup m_parent;

            /// <summary>
            /// Identifies which row within the row index this reference
            /// came from, so the correct flag can be checked for the row
            /// presence and the correct row offset can be found.
            /// </summary>
            public ushort RowIndex { get { return _rowIndex; } }
            public RekordboxPdb M_Root { get { return m_root; } }
            public RekordboxPdb.RowGroup M_Parent { get { return m_parent; } }
        }
        private uint __unnamed0;
        private uint _lenPage;
        private uint _numTables;
        private uint _nextUnusedPage;
        private uint __unnamed4;
        private uint _sequence;
        private byte[] _gap;
        private List<Table> _tables;
        private RekordboxPdb m_root;
        private KaitaiStruct m_parent;

        /// <summary>
        /// Unknown purpose, perhaps an unoriginal signature, seems to
        /// always have the value 0.
        /// </summary>
        public uint Unnamed_0 { get { return __unnamed0; } }

        /// <summary>
        /// The database page size, in bytes. Pages are referred to by
        /// index, so this size is needed to calculate their offset, and
        /// table pages have a row index structure which is built from the
        /// end of the page backwards, so finding that also requires this
        /// value.
        /// </summary>
        public uint LenPage { get { return _lenPage; } }

        /// <summary>
        /// Determines the number of table entries that are present. Each
        /// table is a linked list of pages containing rows of a particular
        /// type.
        /// </summary>
        public uint NumTables { get { return _numTables; } }

        /// <summary>
        /// @flesinak said: &quot;Not used as any `empty_candidate`, points
        /// past the end of the file.&quot;
        /// </summary>
        public uint NextUnusedPage { get { return _nextUnusedPage; } }
        public uint Unnamed_4 { get { return __unnamed4; } }

        /// <summary>
        /// @flesniak said: &quot;Always incremented by at least one,
        /// sometimes by two or three.&quot;
        /// </summary>
        public uint Sequence { get { return _sequence; } }

        /// <summary>
        /// Only exposed until
        /// https://github.com/kaitai-io/kaitai_struct/issues/825 can be
        /// fixed.
        /// </summary>
        public byte[] Gap { get { return _gap; } }

        /// <summary>
        /// Describes and links to the tables present in the database.
        /// </summary>
        public List<Table> Tables { get { return _tables; } }
        public RekordboxPdb M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}
