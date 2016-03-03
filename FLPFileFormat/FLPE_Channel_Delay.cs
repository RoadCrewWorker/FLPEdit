using System;
using System.ComponentModel;
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
    public class FLPE_Channel_Delay : FLPE_Data
    {
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int DelayFeedback { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(6400)]
        public int DelayPan { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(4)]
        public int DelayEchos { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(144)]
        public int DelayTime { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int DelayPitchShift { get; set; }

        public FLPE_Channel_Delay(FLP_File.EventID type) : base(type) { }
        public FLPE_Channel_Delay() : base(0) { }

        public override string ToString()
        {
            return Id + " : Feedback:" + this.DelayFeedback + ", Pan:" + this.DelayPan + ", Time: " + this.DelayTime + ", Echos: " + this.DelayEchos + " Pitchshift:" + this.DelayPitchShift;
        }

        public override void DeserializeData(int len, BinaryReader r)
        {
            this.DelayFeedback = r.ReadInt32();
            this.DelayPan = r.ReadInt32();
            this.DelayPitchShift = r.ReadInt32();
            this.DelayEchos = r.ReadInt32();
            this.DelayTime = r.ReadInt32();
        }

        public override void SerializeData(BinaryWriter w)
        {
            w.Write(this.DelayFeedback);
            w.Write(this.DelayPan);
            w.Write(this.DelayPitchShift);
            w.Write(this.DelayEchos);
            w.Write(this.DelayTime);
        }
        public override bool IsDefault()
        {
            return DelayFeedback == 0 && DelayPan == 6400 && DelayEchos == 4 && DelayTime == 144 && DelayPitchShift == 0;
        }
    }
}
