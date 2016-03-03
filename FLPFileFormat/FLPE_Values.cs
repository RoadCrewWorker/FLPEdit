using System;
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
    public class FLPE_Values : FLPE_Data
    {
        //Enum members just used for ID matching, labels are not used.
        public const byte FLP_Text = 192;
        public enum ValidIDs : byte
        {
            //ID_Plugin_New = FLP_Text + 20, // new VST or DirectX plugin
            ID_Channel_Parameters = FLP_Text + 23, // block of various channel params (can grow)
            ID_Channel_Envelope = FLP_Text + 26,
            FLP_Ctrl_Remote_MIDI = FLP_Text + 34, // remote control entry (MIDI)
            FLP_Ctrl_Remote_Internal = FLP_Text + 35 // remote control entry (internal)
        }

        [XmlAttribute]
        public int Count
        {
            get
            {
                return Values.Length;
            }

            set
            {
                //Empty Setter to have this value show up during xml serizalization
            }
        }

        [XmlElement("DWORD", Type = typeof(int))]
        public int[] Values { get; set; }

        public FLPE_Values(FLP_File.EventID type) : base(type) { }
        public FLPE_Values() : base(0) { }


        public override string ToString()
        {
            return Id + " Structure [" + this.Values.Length + " DWORDs]";
        }

        public override void DeserializeData(int len, BinaryReader r)
        {
            if (len % 4 != 0) throw new InvalidDataException("Invalid Length for Values Event:" + this.Id + " " + len);
            this.Values = new int[len / 4];
            for (int i = 0; i * 4 < len; i++)
            {
                this.Values[i] = r.ReadInt32();
            }
        }

        public override void SerializeData(BinaryWriter w)
        {
            for (int i = 0; i < this.Values.Length; i++)
            {
                w.Write(this.Values[i]);
            }
        }
        public override bool IsDefault()
        {
            return false;
        }
    }
}
