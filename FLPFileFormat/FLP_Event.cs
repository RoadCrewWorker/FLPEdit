using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace FLPFileFormat
{
    /* 
   ##############################################################################################################
   Abstract base class for events in the loading script identified by an ID byte from the FLP_File.EventID enum.
   ##############################################################################################################
   */
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    [DescriptionAttribute("Abstract base class for events in the loading script identified by an ID byte from the FLP_File.EventID enum.")]
    public abstract class FLP_Event
    {
        [XmlAttribute]
        [ReadOnlyAttribute(true)]
        public FLP_File.EventID Id { get; set; }

        public FLP_File ParentProject { get; set; }
        //Rekursive Factory Method
        public static FLP_Event FromEventID(FLP_File.EventID id, bool use_fallback = false)
        {
            FLP_Event ev = null;
            if (id < (FLP_File.EventID)FLP_File.FLP_Text) ev = FLPE_Val.FromEventID(id, use_fallback);
            else ev = FLPE_Data.FromEventID(id, use_fallback);
            return ev;
        }

        public FLP_Event(FLP_File.EventID type) { this.Id = type; }
        public abstract void Deserialize(BinaryReader r);
        public abstract void Serialize(BinaryWriter w);

        public abstract bool IsDefault();
    }
}
