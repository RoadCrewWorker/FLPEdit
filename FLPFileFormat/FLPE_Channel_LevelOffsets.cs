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
    public class FLPE_Channel_LevelOffsets : FLPE_Data
    {
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int Pan { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(12800)]
        public int Volume { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int ModX { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int ModY { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int U1 { get; set; }

        public FLPE_Channel_LevelOffsets(FLP_File.EventID type) : base(type) { }
        public FLPE_Channel_LevelOffsets() : base(0) { }

        public override string ToString()
        {
            return Id + " : Volume:" + this.Volume + ", Pan:" + this.Pan + ", ModX: " + this.ModX + ", ModY: " + this.ModY + " ?:" + this.U1;
        }

        public override void DeserializeData(int len, BinaryReader r)
        {
            this.Pan = r.ReadInt32();
            this.Volume = r.ReadInt32();
            this.U1 = r.ReadInt32();
            this.ModX = r.ReadInt32();
            this.ModY = r.ReadInt32();
        }

        public override void SerializeData(BinaryWriter w)
        {
            w.Write(this.Pan);
            w.Write(this.Volume);
            w.Write(this.U1);
            w.Write(this.ModX);
            w.Write(this.ModY);
        }
        public override bool IsDefault()
        {
            return Pan == 0 && Volume == 12800 && ModX == 0 && ModY == 0 && U1 == 0;
        }
    }
}
