using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace FLPFileFormat
{
    /*  
    ##############################################################################################################
    Playlist Events - Contains all items (clips) on the playlist
    ##############################################################################################################
    */
    [Serializable]
    public class FLPE_Playlist_Events : FLPE_Data
    {
        [XmlElement("PlaylistItem", Type = typeof(PlaylistClip))]
        private List<PlaylistClip> _playlistItems = new List<PlaylistClip>();

        [XmlAttribute]
        public int ItemCount
        {
            get
            {
                return this._playlistItems.Count;
            }

            set
            {
                //Empty Setter to have this value show up during xml serizalization
            }
        }

        public PlaylistClip[] PlaylistItems
        {
            get
            {
                return _playlistItems.ToArray();
            }

            set
            {
                _playlistItems = new List<PlaylistClip>(value);
            }
        }

        public FLPE_Playlist_Events(FLP_File.EventID type) : base(type) { }
        public FLPE_Playlist_Events() : base(0) { }

        public override string ToString()
        {
            return "Event '" + this.Id + "' = " + this._playlistItems.Count + " Playlist Items";
        }

        public override void DeserializeData(int len, BinaryReader r)
        {
            int bytes_per_item = 32; //8x DWORD
            if (len % bytes_per_item != 0) throw new InvalidDataException("Invalid Length for Event:" + this.Id + " " + len);
            int notecount = len / bytes_per_item;
            for (int note = 0; note < notecount; note++)
            {
                PlaylistClip n = new PlaylistClip();
                n.Deserialize(r);
                this._playlistItems.Add(n);
            }
        }

        public override void SerializeData(BinaryWriter w)
        {
            for (int item = 0; item < this._playlistItems.Count; item++)
            {
                this._playlistItems[item].Serialize(w);
            }
        }
        public override bool IsDefault()
        {
            return this.ItemCount == 0;
        }
    }

    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    [DescriptionAttribute("Clip in the playlist.")]
    public class PlaylistClip
    {
        [XmlAttribute]
        public uint Position { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(20480)]
        public ushort PatternClipOffset { get; set; } // Always seems to be 20480
        [XmlAttribute]
        public ushort ClipSource { get; set; } // For Patterns: 20480 + Pat# , For Audio and Automation Clips: 0,1,2,3....
        [XmlAttribute]
        public uint Duration { get; set; }
        [XmlAttribute]
        public ushort PlaylistTrack { get; set; } //Counts up from the bottom: so Track 199 = 0, Track 1 is 198
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public ushort Group { get; set; } //0 for no group, otherwise same value for same group

        [XmlAttribute]
        [DefaultValueAttribute(120)]
        public ushort U1 { get; set; }
        [XmlAttribute]
        public ushort Flags { get; set; } // Possibly Flags? includes "muted"
        [XmlAttribute]
        [DefaultValueAttribute(64)]
        public byte U3 { get; set; } // 40
        [XmlAttribute]
        [DefaultValueAttribute(100)]
        public byte U4 { get; set; } // 64
        [XmlAttribute]
        [DefaultValueAttribute(128)]
        public byte U5 { get; set; } // 80
        [XmlAttribute]
        [DefaultValueAttribute(128)]
        public byte U6 { get; set; } // 80

        [XmlAttribute]
        [DefaultValueAttribute(4294967295)]
        public uint WindowStart { get; set; } //000080BF for full range
        [XmlAttribute]
        [DefaultValueAttribute(4294967295)]
        public uint WindowEnd { get; set; } //000080BF for full range


        public void Deserialize(BinaryReader r)
        {
            this.Position = r.ReadUInt32();
            this.PatternClipOffset = r.ReadUInt16();
            this.ClipSource = r.ReadUInt16();
            this.Duration = r.ReadUInt32();
            this.PlaylistTrack = r.ReadUInt16();
            this.Group = r.ReadUInt16();
            this.U1 = r.ReadUInt16();
            this.Flags = r.ReadUInt16();
            this.U3 = r.ReadByte();
            this.U4 = r.ReadByte();
            this.U5 = r.ReadByte();
            this.U6 = r.ReadByte();
            this.WindowStart = r.ReadUInt32();
            this.WindowEnd = r.ReadUInt32();
        }

        public void Serialize(BinaryWriter w)
        {
            w.Write(this.Position);
            w.Write(this.PatternClipOffset);
            w.Write(this.ClipSource);
            w.Write(this.Duration);
            w.Write(this.PlaylistTrack);
            w.Write(this.Group);
            w.Write(this.U1);
            w.Write(this.Flags);
            w.Write(this.U3);
            w.Write(this.U4);
            w.Write(this.U5);
            w.Write(this.U6);
            w.Write(this.WindowStart);
            w.Write(this.WindowEnd);
        }
    }
}
