using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace FLPFileFormat
{
    /* 
   ##############################################################################################################
   Fixed length class - holds single numeric value of 1,2 or 4 bytes of length.
   ##############################################################################################################
   */
    [Serializable]
    public class FLPE_Val : FLP_Event
    {
        [XmlAttribute]
        [ReadOnlyAttribute(true)]
        public int B { get; set; }

        private int _value;
        [XmlAttribute]
        public int V
        {
            get
            {
                return _value;
            }

            set
            {
                long max_value = ((long)2 << this.B * 8) - 1;
                if (value > max_value)
                {
                    throw new InvalidDataException("Value " + value + " exceeds " + max_value);
                }
                _value = value;
            }
        }

        //Rekursive Factory Method for fixed length values (1,2,4 bytes)
        public static new FLP_Event FromEventID(FLP_File.EventID id)
        {
            if (id == FLP_File.EventID.ID_Channel_CutCutBy) return new FLPE_CutCutBy(id);
            if (id == FLP_File.EventID.ID_Channel_Delay_ModXYChange) return new FLPE_DelayFRes(id);
            if (Enum.IsDefined(typeof(FLPE_Color.ValidIDs), (byte)id)) return new FLPE_Color(id);
            int bytelength = id < (FLP_File.EventID)FLP_File.FLP_Word ? 1 : id < (FLP_File.EventID)FLP_File.FLP_Int ? 2 : 4;
           //if (Enum.IsDefined(typeof(FLPE_Val.ValidIDs), (byte)id))
                return new FLPE_Val(id, bytelength);
           //else
           //   throw new InvalidDataException(id + " is not a valid ID.");
        }

        public FLPE_Val(FLP_File.EventID type, int bytelength) : base(type) { this.B = bytelength; }
        public FLPE_Val() : base(0) { }


        public override void Deserialize(BinaryReader r)
        {
            if (this.B == 1) this._value = r.ReadByte();
            else if (this.B == 2) this._value = r.ReadUInt16();
            else if (this.B == 4) this._value = r.ReadInt32(); //Assuming all 4 byte values are signed ints?
        }

        public override void Serialize(BinaryWriter w)
        {
            w.Write((byte)this.Id);
            if (this.B == 1) w.Write((byte)this.V);
            else if (this.B == 2) w.Write((UInt16)this.V);
            else if (this.B == 4) w.Write((Int32)this.V);
        }

        public override string ToString()
        {
            return Id + " = " + this.V + " [" + B + "b]";
        }
        public override bool IsDefault()
        {
            return
                (Id == FLP_File.EventID.ID_Project_LoopActive && V == 0) ||
                (Id == FLP_File.EventID.ID_Project_Shuffle && V == 0) ||
                (Id == FLP_File.EventID.ID_Project_MainPitch && V == 0) ||
                (Id == FLP_File.EventID.ID_Project_TimeSig_Num && V == 4) ||
                (Id == FLP_File.EventID.ID_Project_TimeSig_Beat && V == 4) ||
                (Id == FLP_File.EventID.ID_Project_PanVolTab && V == 0) ||
                (Id == FLP_File.EventID.ID_Project_TruncateClipNotes && V == 1) ||
                (Id == FLP_File.EventID.ID_Project_ShowInfo && V == 0) ||
                (Id == FLP_File.EventID.ID_Project_Registered && V == 3) ||
                (Id == FLP_File.EventID.ID_ChannelFilter_CurrentNum && V == -1) ||
                (Id == FLP_File.EventID.ID_Plugin_Icon && V == 0) ||
                (Id == FLP_File.EventID.ID_Channel_Enabled && V == 1) ||
                (Id == FLP_File.EventID.FLP_Reverb && V == 65536) ||
                (Id == FLP_File.EventID.ID_Channel_ShiftTime && V == 0) ||
                (Id == FLP_File.EventID.ID_Channel_SwingMix && V == 128) ||
                (Id == FLP_File.EventID.FLP_FX && V == 128) ||
                (Id == FLP_File.EventID.FLP_FX3 && V == 256) ||
                (Id == FLP_File.EventID.FLP_FXCut && V == 1024) ||
                (Id == FLP_File.EventID.FLP_FXRes && V == 0) ||
                (Id == FLP_File.EventID.FLP_FXPreamp && V == 0) ||
                (Id == FLP_File.EventID.FLP_FXDecay && V == 0) ||
                (Id == FLP_File.EventID.FLP_FXAttack && V == 0) ||
                (Id == FLP_File.EventID.FLP_FXStDel && V == 2048) ||
                (Id == FLP_File.EventID.FLP_FXFlags && V == 0) ||
                (Id == FLP_File.EventID.FLP_LayerFlags && V == 0) ||
                (Id == FLP_File.EventID.ID_ChannelFilter_Num && V == 0) ||
                (Id == FLP_File.EventID.FLP_SampleFlags && V == 3) ||
                (Id == FLP_File.EventID.FLP_LoopType && V == 0) ||
                (Id == FLP_File.EventID.ID_Project_APDC && V == 1) ||
                (Id == FLP_File.EventID.ID_Project_EEAutoMode && V == 0) ||
                //(Id == FLP_File.EventID.ID_MixerTrack_Input_Channel && V == -1) ||
                //(Id == FLP_File.EventID.ID_MixerTrack_Output_Channel && V == -1) ||
                false;
        }
    }
}
