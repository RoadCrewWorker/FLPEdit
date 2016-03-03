using System;
using System.IO;
using System.Xml.Serialization;

namespace FLPFileFormat
{
    /* 
    ##############################################################################################################
    Time Class - only used once for project, stores 2 dates in Delphi double format?
    ##############################################################################################################
    */
    [Serializable]
    public class FLPE_Project_Time : FLPE_Data
    {
        private double startdouble;
        private double worktimedouble;

        private DateTime delphiorigin = new DateTime(1899, 12, 30);
        [XmlAttribute]
        public string StartDate
        {
            get
            {
                return (delphiorigin + TimeSpan.FromDays(startdouble)).ToString();
            }

            set
            {
                startdouble = (DateTime.Parse(value) - delphiorigin).TotalDays;
            }
        }

        [XmlAttribute]
        public string WorkTime
        {
            get
            {
                return TimeSpan.FromDays(worktimedouble).ToString();
            }

            set
            {
                worktimedouble = TimeSpan.Parse(value).TotalDays;
            }
        }

        public FLPE_Project_Time(FLP_File.EventID type) : base(type) { }
        public FLPE_Project_Time() : base(0) { }

        public override void DeserializeData(int len, BinaryReader r)
        {
            this.startdouble = r.ReadDouble();
            this.worktimedouble = r.ReadDouble();
        }

        public override void SerializeData(BinaryWriter w)
        {
            w.Write(this.startdouble);
            w.Write(this.worktimedouble);
        }

        public override string ToString()
        {
            return Id + " Start Time:" + this.StartDate.ToString() + " , Work Time:" + this.WorkTime.ToString();
        }
        public override bool IsDefault()
        {
            return false;
        }
    }
}
