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
    public class FLPE_Channel_Poly : FLPE_Data
    {
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public uint MaxPolyphony { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(820)]
        public uint Slide { get; set; }
        [Flags]
        public enum PolyFlags : byte
        {
            None = 0x00,
            Mono = 0x01,
            Porta = 0x02,
            U1 = 0x04,
            U2 = 0x08,
            U3 = 0x10,
            U4 = 0x20,
            U5 = 0x40,
            U6 = 0x80
        }
        [XmlAttribute]
        [DefaultValueAttribute(PolyFlags.None)]
        public PolyFlags Flags { get; set; }

        public FLPE_Channel_Poly(FLP_File.EventID type) : base(type) { }
        public FLPE_Channel_Poly() : base(0) { }
        
        public override string ToString()
        {
            return Id + " : Max Polyphony:" + this.MaxPolyphony + ", Slide:" + this.Slide + ", Flags: " + this.Flags;
        }

        public override void DeserializeData(int len, BinaryReader r)
        {
            this.MaxPolyphony = r.ReadUInt32();
            this.Slide = r.ReadUInt32();
            this.Flags = (PolyFlags)r.ReadByte();
        }

        public override void SerializeData(BinaryWriter w)
        {
            w.Write(this.MaxPolyphony);
            w.Write(this.Slide);
            w.Write((byte)this.Flags);
        }
        public override bool IsDefault()
        {
            return MaxPolyphony == 0 && Slide == 820 && Flags == PolyFlags.None;
        }
    }
}
