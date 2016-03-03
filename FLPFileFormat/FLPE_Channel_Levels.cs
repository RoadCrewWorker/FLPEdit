using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace FLPFileFormat
{
    /*  
    ##############################################################################################################
    Channel levels      
    ##############################################################################################################
    */
    [Serializable]
    public class FLPE_Channel_Levels : FLPE_Data
    {
        [XmlAttribute]
        [DefaultValueAttribute(6400)]
        public int Pan { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(10000)]
        public int Volume { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int PitchShiftInCents { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(256)]
        public int U1 { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int U2 { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int U3 { get; set; }

        public FLPE_Channel_Levels(FLP_File.EventID type) : base(type) { }
        public FLPE_Channel_Levels() : base(0) { }

        public override string ToString()
        {
            return Id + " : Channel Pan:" + this.Pan + ", Volume:" + this.Volume + ", Pitch Shift (cents): " + this.PitchShiftInCents + ", ?: " + this.U1 + "," + this.U2 + "," + this.U3;
        }

        public override void DeserializeData(int len, BinaryReader r)
        {
            this.Pan = r.ReadInt32();
            this.Volume = r.ReadInt32();
            this.PitchShiftInCents = r.ReadInt32();
            this.U1 = r.ReadInt32();
            this.U2 = r.ReadInt32();
            this.U3 = r.ReadInt32();
        }

        public override void SerializeData(BinaryWriter w)
        {
            w.Write(this.Pan);
            w.Write(this.Volume);
            w.Write(this.PitchShiftInCents);
            w.Write(this.U1);
            w.Write(this.U2);
            w.Write(this.U3);
        }
        public override bool IsDefault()
        {
            return Pan == 6400 && Volume == 10000 && PitchShiftInCents == 0 && U1 == 256 && U2 == 0 && U3 == 0;
        }
    }
}
