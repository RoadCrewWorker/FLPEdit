using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace FLPFileFormat
{
    /* 
    ##############################################################################################################
    Mixer Routing - Contains list of target mixer tracks. Serialized as 105 byte array of fixed length.
    ##############################################################################################################
    */
    [Serializable]
    public class FLPE_MixerTrack_Routing : FLPE_Data
    {
        [XmlElement("Track", Type = typeof(byte))]
        private List<byte> _routedTracks = new List<byte>();
        
        [XmlAttribute]
        public int LinkedTracks
        {
            get
            {
                return _routedTracks.Count;
            }

            set
            {
                //Empty Setter to have this value show up during xml serizalization
            }
        }

        public byte[] RoutedTracks
        {
            get
            {
                return _routedTracks.ToArray();
            }

            set
            {
                _routedTracks = new List<byte>(value);
            }
        }

        public FLPE_MixerTrack_Routing(FLP_File.EventID type) : base(type) { }
        public FLPE_MixerTrack_Routing() : base(0) { }

        public override string ToString()
        {
            return Id + " = " + this._routedTracks.Count + " connected Tracks";
        }

        public override void DeserializeData(int len, BinaryReader r)
        {
            byte[] data = r.ReadBytes(len);
            for (byte i = 0; i < this.ParentProject.MAX_MIXERTRACKS; i++)
            {
                if (data[i] > 0) this._routedTracks.Add(i);
            }
        }

        public override void SerializeData(BinaryWriter w)
        {
            byte[] d = new byte[this.ParentProject.MAX_MIXERTRACKS];
            for (byte i = 0; i < this.ParentProject.MAX_MIXERTRACKS; i++) d[i] = 0;
            foreach (byte track in this._routedTracks) d[track] = 1;
            w.Write(d);
        }
        public override bool IsDefault()
        {
            return false;
        }
    }
}
