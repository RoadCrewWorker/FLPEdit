using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace FLPFileFormat
{
    /* 
    ##############################################################################################################
    Pattern Control Events: contains all midi events associated with a pattern
    ##############################################################################################################
    */
    [Serializable]
    public class FLPE_Ctrl_Init_RecChan : FLPE_Data
    {
        [XmlElement("InitEvent", Type = typeof(InitEvent))]
        private List<InitEvent> _initEvents = new List<InitEvent>();

        [XmlAttribute]
        public int EventCount
        {
            get
            {
                return _initEvents.Count;
            }

            set
            {
                //Empty Setter to have this value show up during xml serizalization
            }
        }

        public InitEvent[] InitEvents
        {
            get
            {
                return _initEvents.ToArray();
            }

            set
            {
                _initEvents = new List<InitEvent>(value);
            }
        }

        public FLPE_Ctrl_Init_RecChan(FLP_File.EventID type) : base(type) { }
        public FLPE_Ctrl_Init_RecChan() : base(0) { }

        public override string ToString()
        {
            return Id + " = " + this._initEvents.Count + " Init Events";
        }

        public override void DeserializeData(int len, BinaryReader r)
        {
            int bytes_per_item = 12; // COULD BE OUTDATED
            if (len % bytes_per_item != 0) throw new InvalidDataException("Invalid Length for Event:" + this.Id + " " + len);
            int notecount = len / bytes_per_item;
            for (int note = 0; note < notecount; note++)
            {
                InitEvent n = new InitEvent();
                n.Deserialize(r, this.ParentProject.MAX_MIXERTRACKS);
                this._initEvents.Add(n);
            }
        }

        public override void SerializeData(BinaryWriter w)
        {
            for (int i = 0; i < this._initEvents.Count; i++)
            {
                this._initEvents[i].Serialize(w);
            }
        }
        public override bool IsDefault()
        {
            //Filters only the events that arent redundant.
            _initEvents=_initEvents.FindAll(delegate (InitEvent e) { return !e.IsDefault(); });

            return this._initEvents.Count == 0;
        }
    }

    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    [DescriptionAttribute("Event initializing a parameter.")]
    public class InitEvent
    {
        public enum InitParameter : byte
        {
            Enabled = 0x00,
			Mix = 0x01,
					
			SendLevelToTrack = 0x40, //Reminder: Master=0, Insert1=1...., Insert103=103 //C(urrent) Mixer Track=104
					
			Mixer_Volume = 0xC0,
			Mixer_Pan = 0xC1,
			Mixer_StereoSeperation = 0xC2,
					
			Mixer_EQ_Low_Level = 0xD0,
			Mixer_EQ_Mid_Level = 0xD1,
			Mixer_EQ_High_Level = 0xD2,
					
			Mixer_EQ_Low_Freq= 0xD8,
			Mixer_EQ_Mid_Freq = 0xD9,
			Mixer_EQ_High_Freq = 0xDA,
					
			Mixer_EQ_Low_Q = 0xE0,
			Mixer_EQ_Mid_Q = 0xE1,
			Mixer_EQ_High_Q = 0xE2
        }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public uint U1_0 { get; set; }

        [XmlAttribute]
        public InitParameter Par { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public byte SendOffset { get; set; }

        [XmlAttribute]
        [DefaultValueAttribute(31)]
        public byte C31{ get; set; } //Could also be 0
        [XmlAttribute]
        public byte EffectNr { get; set; }
        [XmlAttribute]
        public byte TrackNr { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(1)]
        public byte U2 { get; set; }
        const int FxSlotBits = 6; // gol why
        const int TrackNrBits = 7; // no gol dont
        [XmlAttribute]
        public int V { get; set; }

        public void Deserialize(BinaryReader r, int count_mixertracks)
        {
            this.U1_0 = r.ReadUInt32();
            //if (U1_0 != 0) throw new InvalidDataException(this.U1_0 + " should be 0 but isn't.");
            byte p= r.ReadByte();
            if (Enum.IsDefined(typeof(InitEvent.InitParameter), p))
            {
                this.Par = (InitEvent.InitParameter)p;
                this.SendOffset = 0;
            }
            else if (p >= (byte)InitParameter.SendLevelToTrack && p <= (byte)InitParameter.SendLevelToTrack + count_mixertracks)
            {
                this.Par = InitParameter.SendLevelToTrack;
                this.SendOffset = (byte)(p - (byte)InitParameter.SendLevelToTrack);
            }
            else
            {
                throw new InvalidDataException(p + " ??");
            }
            this.C31 = r.ReadByte();
            //if (Const1F != 0x1f) throw new InvalidDataException(this.Const1F + " should be 31 but isn't.");
            ushort t= r.ReadUInt16();

            int fxmask = (2 << (FxSlotBits-1)) - 1, trackmask = (2 << (TrackNrBits-1)) - 1;
            this.EffectNr = (byte)(t & fxmask); //6 bits: 0-63
            this.TrackNr = (byte)((t >> FxSlotBits) & trackmask); // 7 bits: 0-127
            this.U2 = (byte)(t >> (TrackNrBits + FxSlotBits));

            this.V = r.ReadInt32();
        }

        public void Serialize(BinaryWriter w)
        {
            w.Write(this.U1_0);
            w.Write((byte)((byte)this.Par+this.SendOffset));
            w.Write(this.C31);
            w.Write((ushort)( (this.U2 << (TrackNrBits + FxSlotBits)) | (this.TrackNr << FxSlotBits) | this.EffectNr));
            w.Write(this.V);
        }

        public override string ToString()
        {
            //" (High " + (Ohlordy >> 22) + " |Low " + (Ohlordy - ((Ohlordy >> 22) << 22)).ToString("X4") +
            return "InitEvent: Track #" + this.TrackNr + " Effect #" + this.EffectNr + " ?=" + this.U2 + " -> " + this.Par + " " + (this.SendOffset > 0 ? "ST:" + this.SendOffset : "") + " = " + this.V;
        }
        public bool IsDefault()
        {
            return
                (Par == InitParameter.Enabled && V == 1) ||
                (Par == InitParameter.Mix && V == 12800) ||
                (Par == InitParameter.Mixer_Volume && V == 12800) ||
                (Par == InitParameter.Mixer_Pan && V == 0) ||
                (Par == InitParameter.Mixer_StereoSeperation && V == 0) ||
                (Par == InitParameter.Mixer_EQ_Low_Level && V == 0) ||
                (Par == InitParameter.Mixer_EQ_Mid_Level && V == 0) ||
                (Par == InitParameter.Mixer_EQ_High_Level && V == 0) ||
                (Par == InitParameter.Mixer_EQ_Low_Q && V == 17500) ||
                (Par == InitParameter.Mixer_EQ_Mid_Q && V == 17500) ||
                (Par == InitParameter.Mixer_EQ_High_Q && V == 17500) ||
                (Par == InitParameter.Mixer_EQ_Low_Freq && V == 5777) ||
                (Par == InitParameter.Mixer_EQ_Mid_Freq && V == 33145) ||
                (Par == InitParameter.Mixer_EQ_High_Freq && V == 55825) ||
                false;
        }
    }
}
