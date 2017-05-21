﻿using FLPFileFormat;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace FLPEdit
{
    public partial class FLPEditForm : Form
    {
        private string CurrentFilename = null;
        private FLP_File CurrentFLPFile = null;

        private void LogStatusMessage(string message)
        {
            this.LogOutput.Text += "\n" + DateTime.Now.ToString() + " " + message;
        }

        public FLPEditForm()
        {
            InitializeComponent();
        }

        private void loadNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.CurrentFilename = this.openFileDialog.FileName;
                LogStatusMessage("Loading: " + this.CurrentFilename);
                LoadAndShowFLPFile(this.CurrentFilename);
                LogStatusMessage("Completed Loading: " + this.CurrentFilename);
            }
        }

        private void reloadLastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentFilename == null)
            {
                MessageBox.Show("Invalid or no file loaded previously.");
                return;
            }
            LogStatusMessage("Reloading: " + this.CurrentFilename);
            LoadAndShowFLPFile(this.CurrentFilename);
            LogStatusMessage("Completed Loading: " + this.CurrentFilename);
        }

        private void LoadAndShowFLPFile(string filename)
        {
            //Load
            TextWriter loggy = File.CreateText(filename + ".log");
            DateTime t1 = DateTime.Now;
            if (filename.EndsWith(".flp"))
            {
                this.CurrentFLPFile = new FLP_File(filename,loggy);
            }
            else if(filename.EndsWith(".fst"))
            {
                this.CurrentFLPFile = new FLP_File(filename, loggy);
            }
            else if (filename.EndsWith(".xml"))
            {
                using (FileStream stream = File.OpenRead(filename))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(FLP_File));
                    this.CurrentFLPFile = (FLP_File)serializer.Deserialize(stream);
                }
            }
            loggy.Close();
            DateTime t2 = DateTime.Now;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.saveFLPFileDialog.ShowDialog() == DialogResult.OK)
            {
                LogStatusMessage("Saving to: " + this.saveFLPFileDialog.FileName);
                SaveFLPFileTo(this.saveFLPFileDialog.FileName);
                LogStatusMessage("Completed saving: " + this.saveFLPFileDialog.FileName);
            }
        }

        private void saveOverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentFilename == null)
            {
                MessageBox.Show("Invalid or no file loaded previously.");
                return;
            }
            LogStatusMessage("Overwriting: " + this.saveFLPFileDialog.FileName);
            SaveFLPFileTo(this.CurrentFilename);
            LogStatusMessage("Completed saving: " + this.saveFLPFileDialog.FileName);
        }

        private void SaveFLPFileTo(string filename)
        {
            BinaryWriter w = new BinaryWriter(File.OpenWrite(filename));
            this.CurrentFLPFile.Serialize(w);
            w.Close();
        }

        private void exportXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (this.saveXMLFileDialog.ShowDialog() == DialogResult.OK)
            {
                LogStatusMessage("Exporting: " + this.saveXMLFileDialog.FileName);
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
                        xmlserializer.Serialize(xmlWriter, this.CurrentFLPFile);
                        File.WriteAllText(this.saveXMLFileDialog.FileName, textWriter.ToString(), Encoding.Unicode);
                    }
                }
                LogStatusMessage("Completed exporting: " + this.saveXMLFileDialog.FileName);
            }
        }

        private void flpPropertyGrid_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                reloadLastToolStripMenuItem_Click(sender, e);
            }
        }

        private void exportAsFstToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CurrentFLPFile.ExtractMixer();

            FLP_Event[] source = this.CurrentFLPFile.Events;
            for(int i = 0; i < source.Length; i++)
            {

            }
            
        }

        private void removeDefaultEntriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int prev = this.CurrentFLPFile.EventCount;
            this.CurrentFLPFile.RemoveRedundantEvents();
            int removed = prev - this.CurrentFLPFile.EventCount;
            LogStatusMessage("Removed "+removed+" redundant default events.");
        }

        private void deleteUnusedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int prev = this.CurrentFLPFile.EventCount;
            this.CurrentFLPFile.RemoveUnusuedPatterns();
            int removed = prev - this.CurrentFLPFile.EventCount;
            LogStatusMessage("Removed " + removed + " unused pattern events.");
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CurrentFLPFile.RemoveMixer();
            LogStatusMessage("Removed mixer entities.");
        }

        private void showInInspectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.flpPropertyGrid.SelectedObject = this.CurrentFLPFile;
            this.flpPropertyGrid.Enabled = true;
        }

        private void resetEmptyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void removeFL123EventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int prev = this.CurrentFLPFile.EventCount;
            CurrentFLPFile.RemoveFL123Events();
            int removed = prev - this.CurrentFLPFile.EventCount;
            LogStatusMessage("Removed " + removed + " v12.3+ specific events.");
        }

        private void showEventStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string info = CurrentFLPFile.GetEventStatistics();
            this.LogOutput.Text += "\n" + info;
       
        }

        private void patternToRackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentFLPFile.PropagatePatternsToRackChannels();
            LogStatusMessage("Propagated Pattern Names/Colors to unique Rack Channels.");
        }

        private void removeFL125EventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int prev = this.CurrentFLPFile.EventCount;
            CurrentFLPFile.RemoveFL125Events();
            int removed = prev - this.CurrentFLPFile.EventCount;
            LogStatusMessage("Removed " + removed + " v12.5 + specific events.");
        }
    }
}
