using System;
using System.IO;
using System.Xml.Serialization;

namespace FLPFileFormat
{
    /* 
    ##############################################################################################################
    Byte Array Class - holds variable length byte array for events with arbitrary data.
    ##############################################################################################################
    */
    [Serializable]
    public class FLPE_Bytes : FLPE_Data
    {
        [XmlAttribute]
        public int Bytes
        {
            get
            {
                return Data.Length;
            }

            set
            {
                //Empty Setter to have this value show up during xml serizalization
            }
        }

        //Remove Datatype specification for default bin64 encoding (smaller xml files but less readable data)
        //[XmlElement(DataType = "hexBinary")]
        [XmlElement]
        public byte[] Data { get; set; }

        public FLPE_Bytes(FLP_File.EventID type) : base(type) { }
        public FLPE_Bytes() : base(0) { }


        public override string ToString()
        {
            return Id + " Byte Array [" + this.Data.Length + "b]";
        }

        public override void DeserializeData(int len, BinaryReader r)
        {
            this.Data = r.ReadBytes(len);
        }

        public override void SerializeData(BinaryWriter w)
        {
            w.Write(this.Data);
        }

        public override bool IsDefault()
        {
            return false;
        }
    }
}
