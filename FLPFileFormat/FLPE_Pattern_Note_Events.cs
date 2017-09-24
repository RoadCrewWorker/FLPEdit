using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace FLPFileFormat
{
    /* 
    ##############################################################################################################
    Pattern Notes - Contains all notes in a particular pattern (for all rack channels)
    ##############################################################################################################
    */
    [Serializable]
    public class FLPE_Pattern_Note_Events : FLPE_Data
    {
        [XmlElement("Note", Type = typeof(PatternNote))]
        private List<PatternNote> _notes = new List<PatternNote>();

        [XmlAttribute]
        public int NoteCount
        {
            get
            {
                return this._notes.Count;
            }

            set
            {
                //Empty Setter to have this value show up during xml serizalization
            }
        }

        public PatternNote[] Notes
        {
            get
            {
                return _notes.ToArray();
            }

            set
            {
                _notes = new List<PatternNote>(value);
            }
        }

        public FLPE_Pattern_Note_Events(FLP_File.EventID type) : base(type) { }
        public FLPE_Pattern_Note_Events() : base(0) { }

        public override string ToString()
        {
            return Id + " = " + this._notes.Count + " Notes" + String.Join<ushort>(",", this.GetRackChannels());
        }

        public override void DeserializeData(int len, BinaryReader r)
        {
            int bytes_per_note = 24; //
            if (len % bytes_per_note != 0) throw new InvalidDataException("Invalid Length for Pattern Notes Event:" + this.Id + " " + len);
            int notecount = len / bytes_per_note;
            for (int note = 0; note < notecount; note++)
            {
                PatternNote n = new PatternNote();
                n.Deserialize(r);
                this._notes.Add(n);
            }
        }
        public ushort[] GetRackChannels()
        {
            HashSet<ushort> channels = new HashSet<ushort>();
            foreach(PatternNote n in this._notes)
            {
                if (!channels.Contains(n.RackChannel)) channels.Add(n.RackChannel);
            }
            ushort[] r = new ushort[channels.Count];
            channels.CopyTo(r);
            return r;
        }

        public override void SerializeData(BinaryWriter w)
        {
            for (int note = 0; note < this._notes.Count; note++)
            {
                this._notes[note].Serialize(w);
            }
        }
        public override bool IsDefault()
        {
            return this.NoteCount == 0;
        }
    }

    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    [DescriptionAttribute("Note in a pattern.")]
    public class PatternNote
    {
        private uint intKey; //Possibly just ushort? FL Only shows 0-131 (C0 to B10)
        private static string[] note = new string[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        [XmlAttribute]
        public string Key
        {
            get
            {
                return PatternNote.note[intKey % 12] + (intKey / 12).ToString();
            }
            set
            {
                string n = value.Contains("#") ? value.Substring(0, 2) : value.Substring(0, 1);
                uint sk = (uint)Array.FindIndex(PatternNote.note, v => v == n);
                uint octave = uint.Parse(value.Substring(n.Length));
                this.intKey = octave * 12 + sk;
            }
        }

        [XmlAttribute]
        public ushort RackChannel { get; set; } //Possibly just byte?

        //These depend on the BeatDivision of project, eg ppq=96, then 4 bars=96
        [XmlAttribute]
        public uint Position { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)] //Infinite
        public uint Duration { get; set; }

        [XmlAttribute]
        [DefaultValueAttribute(16384)]
        public ushort Flags { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public byte MidiChannel { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(100)]
        public byte Velocity { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(64)]
        public byte Pan { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(64)]
        public byte Release { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public byte UnUsed { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(120)]
        public byte FinePitch { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(128)]
        public byte ModX { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(128)]
        public byte ModY { get; set; }

        public void Deserialize(BinaryReader r)
        {
            this.Position = r.ReadUInt32(); //4b
            this.Flags = r.ReadUInt16(); //2b
            this.RackChannel = r.ReadUInt16(); //2b
            this.Duration = r.ReadUInt32(); //4b
            this.intKey = r.ReadUInt32(); //4b

            this.FinePitch = r.ReadByte();
            this.UnUsed = r.ReadByte();
            this.Release = r.ReadByte();
            this.MidiChannel = r.ReadByte();
            this.Pan = r.ReadByte();
            this.Velocity = r.ReadByte();
            this.ModX = r.ReadByte();
            this.ModY = r.ReadByte();
        }

        public void Serialize(BinaryWriter w)
        {
            w.Write(this.Position);
            w.Write(this.Flags);
            w.Write(this.RackChannel);
            w.Write(this.Duration);
            w.Write(this.intKey);
            w.Write(this.FinePitch);
            w.Write(this.UnUsed);
            w.Write(this.Release);
            w.Write(this.MidiChannel);
            w.Write(this.Pan);
            w.Write(this.Velocity);
            w.Write(this.ModX);
            w.Write(this.ModY);
        }
        public override string ToString()
        {
            return "[CH:" + this.RackChannel + " @" + this.Position + " ~" + this.Duration + "] Note " + this.Key + " (CH:" + this.MidiChannel + ",V:" + this.Velocity + ",P:" + this.Pan + ",X:" + this.ModX + ",Y:" + this.ModY + ",R:" + this.Release + ",Fine:" + this.FinePitch + ")";
        }
    }
}
