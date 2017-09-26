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
    public class FLPE_Pattern_Ctrl_Events : FLPE_Data
    {
        [XmlElement("CtrlEvent", Type = typeof(ControlEvent))]
        private List<ControlEvent> _controlEvents = new List<ControlEvent>();

        [XmlAttribute]
        public int EventCount
        {
            get
            {
                return _controlEvents.Count;
            }

            set
            {
                //Empty Setter to have this value show up during xml serizalization
            }
        }

        public ControlEvent[] ControlEvents
        {
            get
            {
                return _controlEvents.ToArray();
            }

            set
            {
                _controlEvents = new List<ControlEvent>(value);
            }
        }

        public FLPE_Pattern_Ctrl_Events(FLP_File.EventID type) : base(type) { }
        public FLPE_Pattern_Ctrl_Events() : base(0) { }

        public override string ToString()
        {
            return Id + " = " + this._controlEvents.Count + " Control Events";
        }

        public override void DeserializeData(int len, BinaryReader r)
        {
            int bytes_per_item = 12; //
            if (len % bytes_per_item != 0) throw new InvalidDataException("Invalid Length for Event:" + this.Id + " " + len);
            int notecount = len / bytes_per_item;
            for (int note = 0; note < notecount; note++)
            {
                ControlEvent n = new ControlEvent();
                n.Deserialize(r);
                this._controlEvents.Add(n);
            }
        }

        public override void SerializeData(BinaryWriter w)
        {
            for (int i = 0; i < this._controlEvents.Count; i++)
            {
                this._controlEvents[i].Serialize(w);
            }
        }
        public override bool IsDefault()
        {
            return this.EventCount == 0;
        }
    }
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    [DescriptionAttribute("Midi event controlling a parameter.")]
    public class ControlEvent
    {
        [XmlAttribute]
        public uint Position { get; set; }
        [XmlAttribute]
        public byte Parameter_U1 { get; set; }
        [XmlAttribute]
        public byte Parameter_U2 { get; set; }
        [XmlAttribute]
        public byte TargetChannel { get; set; }
        [XmlAttribute]
        public byte TargetFlags { get; set; }

        //Value is either a normalized 0.0-1.0 4 byte float for vst parameters (- sticking with this for now)
        //or a 4 byte integer (in the range of 0 to 12800) for FLs internal parameters.
        //oh boy
        [XmlAttribute]
        public float Value { get; set; } 

        public void Deserialize(BinaryReader r)
        {
            this.Position = r.ReadUInt32();
            this.Parameter_U1 = r.ReadByte();
            this.Parameter_U2 = r.ReadByte();
            this.TargetChannel = r.ReadByte();
            this.TargetFlags = r.ReadByte();
            this.Value = r.ReadSingle();
        }

        public void Serialize(BinaryWriter w)
        {
            w.Write(this.Position);
            w.Write(this.Parameter_U1);
            w.Write(this.Parameter_U2);
            w.Write(this.TargetChannel);
            w.Write(this.TargetFlags);
            w.Write(this.Value);
        }

        public override string ToString()
        {
            return "ControlEvent [Type: " + this.Parameter_U1 + ", "+this.Parameter_U2 + ", Channel:" + this.TargetChannel + ", Flags:" + this.TargetFlags + " Pos:" + this.Position + " Value:" + this.Value + "]";
        }
    }
}
