using System;
using System.IO;

namespace FLPFileFormat
{
    /* 
    ##############################################################################################################
    Abstract class for variable length events. Implements length encoding in serialization.
    ##############################################################################################################
    */
    [Serializable]
    public abstract class FLPE_Data : FLP_Event
    {
        protected int my_data_len;
        //Rekursive Factory Method, propagates to further subclasses depending on ID
        public static new FLP_Event FromEventID(FLP_File.EventID id,bool use_fallback=false)
        {
            //FLP_Ctrl_Init_RecChan - Initial ctrl values
            //ID_Playlist_Events - Arrangement!
            //Special classes...
            if (!use_fallback)
            {
                if (id == FLP_File.EventID.ID_MixerTrack_Parameters) return new FLPE_MixerTrack_Parameters(id);
                if (id == FLP_File.EventID.FLP_Ctrl_Init_RecChan) return new FLPE_Ctrl_Init_RecChan(id);
                if (id == FLP_File.EventID.ID_Playlist_Track_Info) return new ID_Playlist_Track_Info(id);
                if (id == FLP_File.EventID.ID_Channel_Delay) return new FLPE_Channel_Delay(id);
                if (id == FLP_File.EventID.ID_Channel_Levels) return new FLPE_Channel_Levels(id);
                if (id == FLP_File.EventID.ID_Channel_LevelOffsets) return new FLPE_Channel_LevelOffsets(id);
                if (id == FLP_File.EventID.ID_Channel_Tracking) return new FLPE_Channel_Tracking(id);
                if (id == FLP_File.EventID.ID_Channel_Poly) return new FLPE_Channel_Poly(id);

                if (id == FLP_File.EventID.ID_Pattern_Ctrl_Events) return new FLPE_Pattern_Ctrl_Events(id);
                if (id == FLP_File.EventID.ID_Pattern_Note_Events) return new FLPE_Pattern_Note_Events(id);
                if (id == FLP_File.EventID.ID_MixerTrack_Routing) return new FLPE_MixerTrack_Routing(id);
                if (id == FLP_File.EventID.ID_Project_Time) return new FLPE_Project_Time(id);
                if (id == FLP_File.EventID.ID_Playlist_Events) return new FLPE_Playlist_Events(id);
                //Typed generic classes...
                if (Enum.IsDefined(typeof(FLPE_Unicode.ValidIDs), (byte)id)) return new FLPE_Unicode(id);
                if (Enum.IsDefined(typeof(FLPE_Values.ValidIDs), (byte)id)) return new FLPE_Values(id);
            }

            //Default fallback class
            if (FLP_File.Dbg_AllowUnknownIDs || Enum.IsDefined(typeof(FLP_File.EventID), (byte)id))
                return new FLPE_Bytes(id);
            else
            {
                throw new InvalidDataException(id + " is not a known FL event ID. Possibly incompatible or corrupt file.");
            }
        }

        public FLPE_Data(FLP_File.EventID type) : base(type) { }
        public FLPE_Data() : base(0) { }

        public abstract void DeserializeData(int DataLength, BinaryReader r);
        public override void Deserialize(BinaryReader r)
        {
            int data_len = 0, shift = 0;
            byte b;
            do
            {
                b = r.ReadByte();
                data_len = data_len | ((b & 0x7F) << shift);
                shift += 7;
            }
            while ((b & 0x80) != 0);
            long pos_r1 = r.BaseStream.Position;
            this.my_data_len = data_len;
            this.DeserializeData(data_len, r);
            //Confirm we fully deserialized the data:
            long pos_r2 = r.BaseStream.Position;
            if (pos_r2 - pos_r1 != data_len)
            {
                throw new InvalidDataException("FLPE_Data structure '"+this.ToString()+"' was deserialized incorrectly: Supposed length: " + data_len + ", Deseralized: " + (pos_r2 - pos_r1));
            }
        }

        public abstract void SerializeData(BinaryWriter w);
        public override void Serialize(BinaryWriter w)
        {
            w.Write((byte)this.Id);

            MemoryStream temp_stream = new MemoryStream();
            BinaryWriter temp_writer = new BinaryWriter(temp_stream);
            this.SerializeData(temp_writer);
            temp_writer.Close();
            byte[] data = temp_stream.ToArray();

            int data_len = data.Length;
            do
            {
                int towrite = data_len & 0x7F;
                data_len = data_len >> 7;
                if (data_len > 0) towrite = (towrite | 0x80);
                w.Write((byte)towrite);
            } while (data_len > 0);

            w.Write(data);
        }
    }
}
