using FLPFileFormat;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace FLPXML
{
    class Program
    {
        public static void Log(string f)
        {
            Console.WriteLine(DateTime.Now + ": " + f);
        }

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: FLP-XML.exe <FLP, XML or Dir> [action, action, action ...] \nwhere action:"
                    + "\n__________________________________"
                    + "\n-info : list a few properties"
                    + "\n__________________________________"
                    + "\n-rup : Remove Unusued Patterns"
                    + "\n-rdv : Remove redundant default values"
                    + "\n-r123 : Remove 12.3 events"
                    + "\n-r125 : Remove 12.5 events"
                    + "\n__________________________________"
                    + "\n-store : Save as FLP"
                    + "\n-xml : Save as XML"
                    + "\n__________________________________"
                    + "\nExample: -rdv -r123 -store"
                    + "\nExample: -rup -store -rdv -xml");
                return;
            }
            string filename = args[0];

            if (filename.EndsWith(".flp"))
            {
                //Log("Opening "+filename+" as a FL Studio project...");
                FLP_File flp = new FLP_File(filename);
               // Log(filename + " opened. " + flp.FLPFormat + " file with " + flp.Events.Length + " events.");
                ProcessFLPFile(flp, filename, args);
            }
            else if (Directory.Exists(filename))
            {
                Log("Opening " + filename + " as directory...");
                foreach (string fn in Directory.EnumerateFiles(filename))
                {
                    if (fn.EndsWith(".flp"))
                    {
                        //Log("Opening " + filename + " as a FL Studio project...");
                        FLP_File flp = new FLP_File(fn);
                        //Log(filename + " opened. " + flp.FLPFormat + " file with " + flp.Events.Length + " events.");
                        ProcessFLPFile(flp, fn, args);
                    }
                    else
                    {
                        Log(fn + " has incorrect extension. Skipping...");
                    }
                }
            }
            else if (filename.EndsWith(".xml"))
            {
                using (FileStream stream = File.OpenRead(filename))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(FLP_File));
                    FLP_File loaded = (FLP_File)serializer.Deserialize(stream);
                    BinaryWriter w = new BinaryWriter(File.OpenWrite(filename + "_fromXML.flp"));
                    loaded.Serialize(w);
                    w.Close();
                }
            }

        }

        private static void ProcessFLPFile(FLP_File f, string filename, string[] cmds)
        {
            foreach (string cmd in cmds)
            {
                if (cmd == "-info")
                {
                    /*
                        path author title bpm started worktime flversion 
                    */
                    FLPE_Unicode flversion = ((FLPE_Unicode)f.Seek(null, FLP_File.EventID.ID_Project_Version));
                    FLPE_Unicode author = ((FLPE_Unicode)f.Seek(null, FLP_File.EventID.ID_Project_Author));
                    FLPE_Unicode regname = ((FLPE_Unicode)f.Seek(null, FLP_File.EventID.ID_Project_RegName));
                    FLPE_Unicode title = ((FLPE_Unicode)f.Seek(null, FLP_File.EventID.ID_Project_Title));
                    FLPE_Val bpm = ((FLPE_Val)f.Seek(null, FLP_File.EventID.ID_Project_FineTempo));
                    FLPE_Project_Time ptime = (FLPE_Project_Time)f.Seek(null, FLP_File.EventID.ID_Project_Time);

                    Log(Path.GetFileNameWithoutExtension(filename) + " | " 
                        + (flversion==null?"":flversion.Text) + " | "
                        + (author == null ? "Missing" : author.Text) + " | "
                        + (regname == null ? "Missing" : regname.Text) + " | "
                        + (title == null ? "Missing" : title.Text) + " | "
                        + (bpm == null ? "Missing" : bpm.V+"Bpm") + " | "
                        + (ptime == null ? "Missing | Missing" : ptime.StartDate+" | "+ptime.WorkTime) + " | "
                        + f.EventCount);
                }
                if (cmd == "-rup")
                {
                    Log("Removing unused patterns from " + filename);
                    int c_pre = f.Events.Length;
                    f.RemoveUnusuedPatterns(true);
                    Log("Done. " + c_pre + "->" + f.Events.Length);
                }
                if (cmd == "-rdv")
                {
                    Log("Removing redundant events from " + filename);
                    int c_pre = f.Events.Length;
                    f.RemoveRedundantEvents();
                    Log("Done. " + c_pre + "->" + f.Events.Length);
                }
                if (cmd == "-r123")
                {
                    Log("Removing 12.3 exclusive events from " + filename);
                    int c_pre = f.Events.Length;
                    f.RemoveFL123Events(true);
                    Log("Done. " + c_pre + "->" + f.Events.Length);
                }
                if (cmd == "-r125")
                {
                    Log("Removing 12.5 exclusive events from " + filename);
                    int c_pre = f.Events.Length;
                    f.RemoveFL125Events(true);
                    Log("Done. " + c_pre + "->" + f.Events.Length);
                }

                if (cmd == "-store")
                {
                    Log("Storing " + filename);
                    BinaryWriter w = new BinaryWriter(File.OpenWrite(filename + "_out.flp"));
                    f.Serialize(w);
                    w.Close();
                    Log("Stored to: " + filename + "_out.flp");
                }
                if (cmd == "-xml")
                {
                    XmlSerializer xmlserializer = new XmlSerializer(typeof(FLP_File));

                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Encoding = new UnicodeEncoding(false, false);
                    settings.NewLineChars = "\n";
                    settings.Indent = true;
                    settings.OmitXmlDeclaration = false;

                    using (StringWriter textWriter = new StringWriter())
                    {
                        using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                        {
                            xmlserializer.Serialize(xmlWriter, f);
                            File.WriteAllText(filename + ".xml", textWriter.ToString(), Encoding.Unicode);
                        }
                    }
                }
            }
        }
    }
}
