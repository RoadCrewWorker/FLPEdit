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
        public uint Channel { get; set; }
        [XmlAttribute]
        public uint Value { get; set; }

        public void Deserialize(BinaryReader r)
        {
            this.Position = r.ReadUInt32();
            this.Channel = r.ReadUInt32();
            this.Value = r.ReadUInt32();
        }

        public void Serialize(BinaryWriter w)
        {
            w.Write(this.Position);
            w.Write(this.Channel);
            w.Write(this.Value);
        }

        public override string ToString()
        {
            return "ControlEvent [Channel: " + this.Channel + " Pos:" + this.Position + " Value:" + this.Value + "]";
        }
    }
}
