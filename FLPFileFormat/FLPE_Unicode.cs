using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace FLPFileFormat
{
    /*
    ##############################################################################################################
    Unicode String Class - holds variable length Unicode string
    ##############################################################################################################
    */
    [Serializable]
    public class FLPE_Unicode : FLPE_Data
    {
        //Enum members just used for ID matching, labels are not used.
        public enum ValidIDs : byte
        {
            // Variable size (192..255)
            FLP_Undef = 192,               // +Length (VarLengthInt)
            FLP_Text = FLP_Undef,  // +Length (VarLengthInt) +Text (Null Term. AnsiString)
            FLP_Text_ChanName = FLP_Text,    // obsolete
            FLP_Text_PatName = FLP_Text + 1,
            FLP_Text_Title = FLP_Text + 2,
            FLP_Text_Comment = FLP_Text + 3,
            FLP_Text_SampleFileName = FLP_Text + 4,
            FLP_Text_URL = FLP_Text + 5,
            FLP_Text_CommentRTF = FLP_Text + 6,  // comments in Rich Text format
            //FLP_Version = FLP_Text + 7, //this one is using ANSI
            FLP_RegName = FLP_Text + 8,  // since 1.3.9 the (scrambled) reg name is stored in the FLP
            FLP_Text_DefPluginName = FLP_Text + 9,
            FLP_Text_ProjDataPath = FLP_Text + 10,
            FLP_Text_PluginName = FLP_Text + 11, // plugin's name
            FLP_Text_FXName = FLP_Text + 12, // FX track name
            FLP_Text_TimeMarker = FLP_Text + 13, // time marker name
            FLP_Text_Genre = FLP_Text + 14,
            FLP_Text_Author = FLP_Text + 15,
            FLP_Text_ChanFilter = FLP_Text + 39,
            FLP_Text_RemoteCtrlFormula = FLP_Text + 38, // remote control entry formula
            FLP_Text_PLTrackName = FLP_Text + 47
        }


        [XmlAttribute]
        public string Text { get; set; }

        public FLPE_Unicode(FLP_File.EventID type) : base(type) { }
        public FLPE_Unicode() : base(0) { }

        public override string ToString()
        {
            return Id + " = \"" + this.Text + "\" [" + this.Text.Length + " chars]";
        }

        public override void DeserializeData(int len, BinaryReader r)
        {
            this.Text = Encoding.Unicode.GetString(r.ReadBytes(len)).Trim('\0');
        }

        public override void SerializeData(BinaryWriter w)
        {
            w.Write(Encoding.Unicode.GetBytes(this.Text + '\0'));
        }
        public override bool IsDefault()
        {
            return
                (Id == FLP_File.EventID.ID_Project_Title && Text == "") ||
                (Id == FLP_File.EventID.ID_Project_Genre && Text == "") ||
                (Id == FLP_File.EventID.ID_Project_Author && Text == "") ||
                (Id == FLP_File.EventID.ID_Project_DataPath && Text == "") ||
                (Id == FLP_File.EventID.ID_Project_Comment && Text == "") ||
                false;
        }
    }
}
