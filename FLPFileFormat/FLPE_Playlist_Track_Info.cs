using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace FLPFileFormat
{
    /*  
    ##############################################################################################################
    Channel Offsets for various levels      
    ##############################################################################################################
    */
    [Serializable]
    public class ID_Playlist_Track_Info : FLPE_Data
    {
        [XmlAttribute]
        public int TrackNumber { get; set; }
        private int rgba;

        [XmlAttribute]
        [DefaultValueAttribute("#565148")]
        public string HexColor
        {
            get
            {
                return ColorTranslator.ToHtml(Color.FromArgb(rgba));
            }
            set
            {
                this.rgba = ColorTranslator.FromHtml(value).ToArgb();
            }
        }

        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int Icon { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(1)]
        public byte Enabled { get; set; }

        [XmlAttribute]
        [DefaultValueAttribute(1)]
        public float Height { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(4294967280)]
        public uint LockedHeight { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public byte LockedToContent { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public byte GroupedWithAbove { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int pMotion { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int pPress { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(5)]
        public int pTriggerSync { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int pQueued { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(1)]
        public int pTolerant { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int pPositionSync { get; set; }

        public ID_Playlist_Track_Info(FLP_File.EventID type) : base(type) { }
        public ID_Playlist_Track_Info() : base(0) { }

        public override string ToString()
        {
            return Id + " : #" + this.TrackNumber + " Icon:" + this.Icon + ", Color: " + this.HexColor + ", Height: " + this.Height + " Grouped:" + this.GroupedWithAbove;
        }

        public override void DeserializeData(int len, BinaryReader r)
        {
            this.TrackNumber = r.ReadInt32();
            this.rgba = r.ReadInt32();
            this.Icon = r.ReadInt32();
            this.Enabled = r.ReadByte();
            this.Height = r.ReadSingle();
            this.LockedHeight = r.ReadUInt32();
            this.LockedToContent = r.ReadByte();
            this.pMotion = r.ReadInt32();
            this.pPress = r.ReadInt32();
            this.pTriggerSync = r.ReadInt32();
            this.pQueued = r.ReadInt32();
            this.pTolerant = r.ReadInt32();
            this.pPositionSync = r.ReadInt32();
            this.GroupedWithAbove = r.ReadByte();
        }

        public override void SerializeData(BinaryWriter w)
        {
            w.Write(this.TrackNumber);
            w.Write(this.rgba);
            w.Write(this.Icon);
            w.Write(this.Enabled);
            w.Write(this.Height);
            w.Write(this.LockedHeight);
            w.Write(this.LockedToContent);
            w.Write(this.pMotion);
            w.Write(this.pPress);
            w.Write(this.pTriggerSync);
            w.Write(this.pQueued);
            w.Write(this.pTolerant);
            w.Write(this.pPositionSync);
            w.Write(this.GroupedWithAbove);
        }
        public override bool IsDefault()
        { //TODO: Default Color!
            return Icon == 0 && Enabled == 1 && Height == 1 && LockedHeight== 4294967280 && LockedToContent == 0 && pMotion== 0 && pPress == 0 && pTriggerSync == 5 && pQueued == 0 && pTolerant == 1 && pPositionSync == 0;
        }
    }
}
