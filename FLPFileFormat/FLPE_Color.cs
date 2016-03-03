using System;
using System.ComponentModel;
using System.Drawing;
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
    public class FLPE_Color : FLP_Event
    {
        public const byte FLP_Int = 128;
        public enum ValidIDs : byte
        {
            ID_Plugin_Color = FLP_Int + 0,
            ID_Pattern_Color = FLP_Int + 22,
            ID_MixerTrack_Color = FLP_Int + 21
        }
        
        private byte Alpha;
        private byte Red;
        private byte Green;
        private byte Blue;

        [XmlAttribute]
        [DefaultValueAttribute("#485156")]
        public string HexColor
        {
            get
            {
                return ColorTranslator.ToHtml(Color.FromArgb(255, this.Red, this.Green, this.Blue));
            }
            set
            {
                Color v = ColorTranslator.FromHtml(value);
                this.Alpha = 0;
                this.Red = v.R;
                this.Green = v.G;
                this.Blue = v.B;
            }
        }

        public FLPE_Color(FLP_File.EventID type) : base(type) { }
        public FLPE_Color() : base(0) { }

        public override void Deserialize(BinaryReader r)
        {
            this.Red = r.ReadByte();
            this.Green = r.ReadByte();
            this.Blue = r.ReadByte();
            this.Alpha = r.ReadByte();
        }

        public override void Serialize(BinaryWriter w)
        {
            w.Write((byte)this.Id);
            w.Write(this.Red);
            w.Write(this.Green);
            w.Write(this.Blue);
            w.Write(this.Alpha);
        }

        public override string ToString()
        {
            return Id + " = " + this.HexColor;
        }
        public override bool IsDefault()
        {
            return HexColor == "#485156";
        }
    }
}
