using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace FLPFileFormat
{
    /* 
   ##############################################################################################################
   Color Attribute Class - Stores alpha,red,green,blu as 32 bit ARGB value
   ##############################################################################################################
   */
    [Serializable]
    public class FLPE_CutCutBy : FLP_Event
    {
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public ushort Cuts { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public ushort CutBy { get; set; }

        public FLPE_CutCutBy(FLP_File.EventID type) : base(type) { }
        public FLPE_CutCutBy() : base(0) { }

        public override void Deserialize(BinaryReader r)
        {
            this.Cuts = r.ReadUInt16();
            this.CutBy = r.ReadUInt16();
        }

        public override void Serialize(BinaryWriter w)
        {
            w.Write((byte)this.Id);
            w.Write(this.Cuts);
            w.Write(this.CutBy);
        }

        public override string ToString()
        {
            return Id + " = Cuts:" + this.Cuts + ", CutBy:" + this.CutBy;
        }
        public override bool IsDefault()
        {
            return Cuts==0 && CutBy==0;
        }
    }
}
