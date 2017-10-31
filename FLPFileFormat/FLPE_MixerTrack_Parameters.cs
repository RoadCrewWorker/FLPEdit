using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace FLPFileFormat
{
    /*  
    ##############################################################################################################
    DWORD Array Class - holds variable count of DWORDs for event structs with fixed length.
    Stored and displayed as Int32s without semantic interpretation.        
    ##############################################################################################################
    */
    [Serializable]
    public class FLPE_MixerTrack_Parameters : FLPE_Data
    {
        [Flags]
        public enum MixerTrackFlags : uint
        {
            None = 0x00,
            ReversePolarity = 0x01,
            SwapLeftRight = 0x02,
            U1 = 0x04,
            Enabled = 0x08,
            AllowThreadedProcessing = 0x10,
            U2 = 0x20,
            DockMiddle = 0x40,
            DockRight = 0x80,
            ShowSeperator = 0x0400
        }
        public FLPE_MixerTrack_Parameters(FLP_File.EventID type) : base(type) { }
        public FLPE_MixerTrack_Parameters() : base(0) { }

        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int U1 { get; set; }
        [XmlAttribute]
        [DefaultValueAttribute(0)]
        public int U2 { get; set; }
        [XmlAttribute]
        public MixerTrackFlags Flags { get; set; }

        public override string ToString()
        {
            return Id + " " + this.Flags;
        }

        public override void DeserializeData(int len, BinaryReader r)
        {
            if (this.my_data_len >= 8)
            {
                this.U1 = r.ReadInt32();
                this.Flags = (MixerTrackFlags)r.ReadUInt32();
                if (this.my_data_len >= 12)
                {
                    this.U2 = r.ReadInt32();
                }
            }
        }

        public override void SerializeData(BinaryWriter w)
        {
            if (this.my_data_len >= 8)
            {
                w.Write(this.U1);
                w.Write((uint)this.Flags);
                if (this.my_data_len >= 12)
                {
                    w.Write(this.U2);
                }
            }
        }
        public override bool IsDefault()
        {
            return false; //Probably want to keep this as a structual FLP element
        }
    }
}
