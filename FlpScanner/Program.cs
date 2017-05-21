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
                    + "\n-rup : Remove Unusued Patterns"
                    + "\n-clean : Remove redundant default values"
                    + "\n-12.2 : Remove 12.3 beta data"
                    + "\n-store : Store as FLP"
                    + "\n-xml : Export to XML"
                    + "\nExample: -clean -12.2 -store"
                    + "\nExample: -rup -store -clean -xml");
                return;
            }
            string filename = args[0];

            if (filename.EndsWith(".flp"))
            {
                Log("Opening "+filename+" as a FL Studio project...");
                FLP_File flp = new FLP_File(filename, null);
                Log(filename + " opened. " + flp.FLPFormat + " file with " + flp.Events.Length + " events.");
                ProcessFLPFile(flp, filename, args);
            }
            else if (Directory.Exists(filename))
            {
                Log("Opening " + filename + " as directory...");
                foreach (string fn in Directory.EnumerateFiles(filename))
                {
                    if (fn.EndsWith(".flp"))
                    {
                        Log("Opening " + filename + " as a FL Studio project...");
                        FLP_File flp = new FLP_File(fn, null);
                        Log(filename + " opened. " + flp.FLPFormat + " file with " + flp.Events.Length + " events.");
                        ProcessFLPFile(flp, fn, args);
                    }
                    else
                    {
                        Log(filename + " has incorrect extension. Skipping...");
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
                if (cmd == "-rup")
                {
                    Log("Removing unused patterns from " + filename);
                    int c_pre = f.Events.Length;
                    f.RemoveUnusuedPatterns(true);
                    Log("Done. " + c_pre + "->" + f.Events.Length);
                }
                if (cmd == "-clean")
                {
                    Log("Removing redundant events from " + filename);
                    int c_pre = f.Events.Length;
                    f.RemoveRedundantEvents();
                    Log("Done. " + c_pre + "->" + f.Events.Length);
                }
                if (cmd == "-12.2")
                {
                    Log("Removing 12.3 exclusive events from " + filename);
                    int c_pre = f.Events.Length;
                    f.RemoveFL123Events(true);
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
