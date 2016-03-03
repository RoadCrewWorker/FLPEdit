using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace FLPFileFormat
{
    /* 
   ##############################################################################################################
   FLPE_DelayFRes - Stores modx, mody change for channel settings delay
   ##############################################################################################################
   */
    [Serializable]
    public class FLPE_DelayFRes : FLP_Event
    {
        [XmlAttribute]
        [DefaultValueAttribute(128)]
        public short DelayModX { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(128)]
        public short DelayModY { get; set; }

        public FLPE_DelayFRes(FLP_File.EventID type) : base(type) { }
        public FLPE_DelayFRes() : base(0) { }

        public override void Deserialize(BinaryReader r)
        {
            this.DelayModY = r.ReadInt16();
            this.DelayModX = r.ReadInt16();
        }

        public override void Serialize(BinaryWriter w)
        {
            w.Write((byte)this.Id);
            w.Write(this.DelayModY);
            w.Write(this.DelayModX);
        }

        public override string ToString()
        {
            return Id + " = DelayModX:" + this.DelayModX + ", DelayModY:" + this.DelayModY;
        }
        public override bool IsDefault()
        {
            return DelayModX == 128 && DelayModY == 128;
        }
    }
}
