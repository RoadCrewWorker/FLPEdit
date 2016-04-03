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
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                //TODO: behavior not yet implemented.
                Console.WriteLine("Usage: FLP-XML.exe <FLP, XML or Dir> [action, action, action ...] \nwhere action:"
                    + "\n-rup : Remove Unusued Patterns"
                    + "\n-clean : Remove redundant default values"
                    + "\n-xml : Export to XML");
                return;
            }
            string filename = args[0];

            if (filename.EndsWith(".flp"))
            {
                FLP_File flp = new FLP_File(filename, null);

                // Perform various options here:


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
                        xmlserializer.Serialize(xmlWriter, flp);
                        File.WriteAllText(filename + ".xml", textWriter.ToString(), Encoding.Unicode);
                    }
                }
            }
            else if (Directory.Exists(filename))
            {

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

        private static void ProcessFLPFile(FLP_File f, string[] cmds)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
