using FLPFileFormat;
using System;
using System.Collections.Generic;
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
        private FLP_File _currentFLPFile = null;
        private FLP_File CurrentFLPFile
        {
            get
            {
                return _currentFLPFile;
            }
            set
            {
                if (value != null)
                {
                    this._currentFLPFile = value;
                    this.scriptsToolStripMenuItem.Enabled = true;
                }
                else
                {
                    this._currentFLPFile = null;
                    this.scriptsToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void LogStatusMessage(string message)
        {
            this.LogOutput.Text += "\n" + DateTime.Now.ToString() + " " + message;
        }

        public FLPEditForm()
        {
            InitializeComponent();
        }

        #region Loading
        private FLP_File LoadFLPFile(string filename, TextWriter loggy)
        {

            if (filename.EndsWith(".flp"))
            {
                return new FLP_File(filename, loggy);
            }
            else if (filename.EndsWith(".fst"))
            {
                return new FLP_File(filename, loggy);
            }
            else if (filename.EndsWith(".xml"))
            {
                FLP_File xml_file;
                using (FileStream stream = File.OpenRead(filename))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(FLP_File));
                    xml_file = (FLP_File)serializer.Deserialize(stream);
                }
                return xml_file;
            }
            return null;
        }

        private void LoadAndShowFLPFile(string filename)
        {
            TextWriter loggy = File.CreateText(filename + ".log");
            DateTime t1 = DateTime.Now;
            this.CurrentFLPFile = this.LoadFLPFile(filename, loggy);
            this.Text = "FLP Edit ["+Path.GetFileName(filename)+"]";
            loggy.Close();
            DateTime t2 = DateTime.Now;
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
        
        #endregion

        #region Saving
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.saveFLPFileDialog.ShowDialog() == DialogResult.OK)
            {
                LogStatusMessage("Saving to: " + this.saveFLPFileDialog.FileName);
                if (File.Exists(this.saveFLPFileDialog.FileName))
                {
                    if (MessageBox.Show("This project file already exists.\nEdited projects may contain subtle errors or corruption that may be hard to detect. Overwriting existing projects is extremely risky.\n\nProceed at your own risk?", "Disclaimer Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        SaveFLPFileTo(this.saveFLPFileDialog.FileName);
                        LogStatusMessage("Completed saving: " + this.saveFLPFileDialog.FileName);
                    }
                }
                else {
                    SaveFLPFileTo(this.saveFLPFileDialog.FileName);
                    LogStatusMessage("Completed saving: " + this.saveFLPFileDialog.FileName);
                }
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
        #endregion 

        #region Display
        private void showInInspectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.flpPropertyGrid.SelectedObject = this.CurrentFLPFile;
            this.flpPropertyGrid.Enabled = true;
        }
        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.flpPropertyGrid.SelectedObject = null;
            this.flpPropertyGrid.Enabled = false;
        }

        private void flpPropertyGrid_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                reloadLastToolStripMenuItem_Click(sender, e);
            }
        }
        #endregion 

        #region Project Cleanup
        private void deleteUnusedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int prev = this.CurrentFLPFile.EventCount;
            this.CurrentFLPFile.RemoveUnusuedPatterns();
            int removed = prev - this.CurrentFLPFile.EventCount;
            LogStatusMessage("Removed " + removed + " unused pattern events.");
        }

        private void removeDefaultEntriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int prev = this.CurrentFLPFile.EventCount;
            this.CurrentFLPFile.RemoveRedundantEvents();
            int removed = prev - this.CurrentFLPFile.EventCount;
            LogStatusMessage("Removed "+removed+" redundant default events.");
        }

        private void removeFL123EventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int prev = this.CurrentFLPFile.EventCount;
            CurrentFLPFile.RemoveFL123Events(true);
            int removed = prev - this.CurrentFLPFile.EventCount;
            LogStatusMessage("Removed " + removed + " v12.3 specific events.");
        }

        private void removeFL125EventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int prev = this.CurrentFLPFile.EventCount;
            CurrentFLPFile.RemoveFL125Events(true);
            int removed = prev - this.CurrentFLPFile.EventCount;
            LogStatusMessage("Removed " + removed + " v12.5 specific events.");
        }
        #endregion

        #region Mixer Manipulation
        private void deleteMixerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int prev = this.CurrentFLPFile.EventCount;
            CurrentFLPFile.DeleteMixerItems(true);
            int removed = prev - this.CurrentFLPFile.EventCount;
            LogStatusMessage("Removed " + removed + " mixer events.");
        }

        private void importMixerFromProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string remote_project = this.openFileDialog.FileName;
                LogStatusMessage("Loading: " + remote_project);
                FLP_File r = this.LoadFLPFile(remote_project, null);
                LogStatusMessage("Completed Loading: " + remote_project + " -> " + r.EventCount + " events.");
                IEnumerable<FLP_Event> remote_mixer = r.GetMixerEvents();
                int c = 0; foreach (FLP_Event eve in remote_mixer) c++;
                LogStatusMessage("Extracted " + c + " remote mixer events.");

                if (c > 2)
                {
                    int prev = this.CurrentFLPFile.EventCount;
                    this.CurrentFLPFile.SetMixerEvents(remote_mixer);
                    LogStatusMessage("Replace mixer: " + prev + " -> " + this.CurrentFLPFile.EventCount + " events.");
                }
                else
                {
                    LogStatusMessage("Insufficient Mixer data extracted, invalid or corrupted file?");
                }

            }
        }

        #endregion
   
        #region Statistics
        private void showEventStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LogOutput.Text += "\n" + CurrentFLPFile.GetEventStatistics();
        }

        private void showPlaylistStatisticsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.LogOutput.Text += "\n" + CurrentFLPFile.GetPlaylistStatistics();
        }

        private void showMixerStatisticsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.LogOutput.Text += "\n" + CurrentFLPFile.GetMixerEvents();
        }

        private void showPatternsStatisticsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.LogOutput.Text += "\n" + CurrentFLPFile.GetPatternStatistics();
        }
        #endregion

        #region etc
        private void patternToRackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentFLPFile.PropagatePatternsToRackChannels();
            LogStatusMessage("Propagated Pattern Names/Colors to unique Rack Channels.");
        }
        #endregion

    }
}
