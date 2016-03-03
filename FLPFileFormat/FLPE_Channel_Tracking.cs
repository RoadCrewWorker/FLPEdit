using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace FLPFileFormat
{
    /*  
    ##############################################################################################################
    Channel Polyphony class.        
    ##############################################################################################################
    */
    [Serializable]
    public class FLPE_Channel_Tracking : FLPE_Data
    {
        [XmlAttribute]
        public int MiddleValue { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int Pan { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int ModX { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int ModY { get; set; }

        public FLPE_Channel_Tracking(FLP_File.EventID type) : base(type) { }
        public FLPE_Channel_Tracking() : base(0) { }

        public override string ToString()
        {
            return Id + " : Middle Value:" + this.MiddleValue + " => Pan:" + this.Pan + ", ModX: " + this.ModX + ", ModY: " + this.ModY;
        }

        public override void DeserializeData(int len, BinaryReader r)
        {
            this.MiddleValue = r.ReadInt32();
            this.Pan = r.ReadInt32();
            this.ModX = r.ReadInt32();
            this.ModY = r.ReadInt32();
        }

        public override void SerializeData(BinaryWriter w)
        {
            w.Write(this.MiddleValue);
            w.Write(this.Pan);
            w.Write(this.ModX);
            w.Write(this.ModY);
        }
        public override bool IsDefault()
        {
            return false;
        }
    }
}
