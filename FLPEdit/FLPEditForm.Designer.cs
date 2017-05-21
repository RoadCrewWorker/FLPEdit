namespace FLPEdit
{
    partial class FLPEditForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveOverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInInspectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patternsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteUnusedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reorderByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rackChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetEmptyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reorderByToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mixerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsFstToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeDefaultEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFL123EventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showEventStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patternToRackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFLPFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveXMLFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.flpPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.LogOutput = new System.Windows.Forms.RichTextBox();
            this.removeFL125EventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.scriptsToolStripMenuItem,
            this.reloadToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(668, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadNewToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveOverToolStripMenuItem,
            this.exportXMLToolStripMenuItem,
            this.showInInspectorToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadNewToolStripMenuItem
            // 
            this.loadNewToolStripMenuItem.Name = "loadNewToolStripMenuItem";
            this.loadNewToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.loadNewToolStripMenuItem.Text = "Load New";
            this.loadNewToolStripMenuItem.Click += new System.EventHandler(this.loadNewToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // saveOverToolStripMenuItem
            // 
            this.saveOverToolStripMenuItem.Name = "saveOverToolStripMenuItem";
            this.saveOverToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.saveOverToolStripMenuItem.Text = "Save Over";
            this.saveOverToolStripMenuItem.Click += new System.EventHandler(this.saveOverToolStripMenuItem_Click);
            // 
            // exportXMLToolStripMenuItem
            // 
            this.exportXMLToolStripMenuItem.Name = "exportXMLToolStripMenuItem";
            this.exportXMLToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.exportXMLToolStripMenuItem.Text = "Export XML";
            this.exportXMLToolStripMenuItem.Click += new System.EventHandler(this.exportXMLToolStripMenuItem_Click);
            // 
            // showInInspectorToolStripMenuItem
            // 
            this.showInInspectorToolStripMenuItem.Name = "showInInspectorToolStripMenuItem";
            this.showInInspectorToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.showInInspectorToolStripMenuItem.Text = "Show in Inspector";
            this.showInInspectorToolStripMenuItem.Click += new System.EventHandler(this.showInInspectorToolStripMenuItem_Click);
            // 
            // scriptsToolStripMenuItem
            // 
            this.scriptsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patternsToolStripMenuItem,
            this.channelsToolStripMenuItem,
            this.playlistToolStripMenuItem,
            this.mixerToolStripMenuItem,
            this.fileToolStripMenuItem1,
            this.normalizeToolStripMenuItem});
            this.scriptsToolStripMenuItem.Name = "scriptsToolStripMenuItem";
            this.scriptsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.scriptsToolStripMenuItem.Text = "Scripts";
            // 
            // patternsToolStripMenuItem
            // 
            this.patternsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteUnusedToolStripMenuItem,
            this.reorderByToolStripMenuItem});
            this.patternsToolStripMenuItem.Name = "patternsToolStripMenuItem";
            this.patternsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.patternsToolStripMenuItem.Text = "Patterns";
            // 
            // deleteUnusedToolStripMenuItem
            // 
            this.deleteUnusedToolStripMenuItem.Name = "deleteUnusedToolStripMenuItem";
            this.deleteUnusedToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.deleteUnusedToolStripMenuItem.Text = "Delete Unused";
            this.deleteUnusedToolStripMenuItem.Click += new System.EventHandler(this.deleteUnusedToolStripMenuItem_Click);
            // 
            // reorderByToolStripMenuItem
            // 
            this.reorderByToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rackChannelToolStripMenuItem,
            this.nameToolStripMenuItem,
            this.usesToolStripMenuItem});
            this.reorderByToolStripMenuItem.Enabled = false;
            this.reorderByToolStripMenuItem.Name = "reorderByToolStripMenuItem";
            this.reorderByToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.reorderByToolStripMenuItem.Text = "Reorder by";
            // 
            // rackChannelToolStripMenuItem
            // 
            this.rackChannelToolStripMenuItem.Name = "rackChannelToolStripMenuItem";
            this.rackChannelToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.rackChannelToolStripMenuItem.Text = "RackChannel";
            // 
            // nameToolStripMenuItem
            // 
            this.nameToolStripMenuItem.Name = "nameToolStripMenuItem";
            this.nameToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.nameToolStripMenuItem.Text = "Name";
            // 
            // usesToolStripMenuItem
            // 
            this.usesToolStripMenuItem.Name = "usesToolStripMenuItem";
            this.usesToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.usesToolStripMenuItem.Text = "Uses";
            // 
            // channelsToolStripMenuItem
            // 
            this.channelsToolStripMenuItem.Enabled = false;
            this.channelsToolStripMenuItem.Name = "channelsToolStripMenuItem";
            this.channelsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.channelsToolStripMenuItem.Text = "Channels";
            // 
            // playlistToolStripMenuItem
            // 
            this.playlistToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetEmptyToolStripMenuItem,
            this.reorderByToolStripMenuItem1});
            this.playlistToolStripMenuItem.Name = "playlistToolStripMenuItem";
            this.playlistToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.playlistToolStripMenuItem.Text = "Playlist";
            // 
            // resetEmptyToolStripMenuItem
            // 
            this.resetEmptyToolStripMenuItem.Enabled = false;
            this.resetEmptyToolStripMenuItem.Name = "resetEmptyToolStripMenuItem";
            this.resetEmptyToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.resetEmptyToolStripMenuItem.Text = "Reset Empty";
            this.resetEmptyToolStripMenuItem.Click += new System.EventHandler(this.resetEmptyToolStripMenuItem_Click);
            // 
            // reorderByToolStripMenuItem1
            // 
            this.reorderByToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourceChannelToolStripMenuItem});
            this.reorderByToolStripMenuItem1.Enabled = false;
            this.reorderByToolStripMenuItem1.Name = "reorderByToolStripMenuItem1";
            this.reorderByToolStripMenuItem1.Size = new System.Drawing.Size(139, 22);
            this.reorderByToolStripMenuItem1.Text = "Reorder by";
            // 
            // sourceChannelToolStripMenuItem
            // 
            this.sourceChannelToolStripMenuItem.Name = "sourceChannelToolStripMenuItem";
            this.sourceChannelToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.sourceChannelToolStripMenuItem.Text = "Source/Channel";
            // 
            // mixerToolStripMenuItem
            // 
            this.mixerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAsFstToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.mixerToolStripMenuItem.Name = "mixerToolStripMenuItem";
            this.mixerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mixerToolStripMenuItem.Text = "Mixer";
            // 
            // exportAsFstToolStripMenuItem
            // 
            this.exportAsFstToolStripMenuItem.Enabled = false;
            this.exportAsFstToolStripMenuItem.Name = "exportAsFstToolStripMenuItem";
            this.exportAsFstToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.exportAsFstToolStripMenuItem.Text = "Export as fst";
            this.exportAsFstToolStripMenuItem.Click += new System.EventHandler(this.exportAsFstToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeDefaultEntriesToolStripMenuItem,
            this.removeFL123EventsToolStripMenuItem,
            this.showEventStatisticsToolStripMenuItem,
            this.removeFL125EventsToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // removeDefaultEntriesToolStripMenuItem
            // 
            this.removeDefaultEntriesToolStripMenuItem.Name = "removeDefaultEntriesToolStripMenuItem";
            this.removeDefaultEntriesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.removeDefaultEntriesToolStripMenuItem.Text = "Remove Default Entries";
            this.removeDefaultEntriesToolStripMenuItem.Click += new System.EventHandler(this.removeDefaultEntriesToolStripMenuItem_Click);
            // 
            // removeFL123EventsToolStripMenuItem
            // 
            this.removeFL123EventsToolStripMenuItem.Name = "removeFL123EventsToolStripMenuItem";
            this.removeFL123EventsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.removeFL123EventsToolStripMenuItem.Text = "Remove FL12.3 Events";
            this.removeFL123EventsToolStripMenuItem.Click += new System.EventHandler(this.removeFL123EventsToolStripMenuItem_Click);
            // 
            // showEventStatisticsToolStripMenuItem
            // 
            this.showEventStatisticsToolStripMenuItem.Name = "showEventStatisticsToolStripMenuItem";
            this.showEventStatisticsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.showEventStatisticsToolStripMenuItem.Text = "Show Event Statistics";
            this.showEventStatisticsToolStripMenuItem.Click += new System.EventHandler(this.showEventStatisticsToolStripMenuItem_Click);
            // 
            // normalizeToolStripMenuItem
            // 
            this.normalizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patternToRackToolStripMenuItem});
            this.normalizeToolStripMenuItem.Name = "normalizeToolStripMenuItem";
            this.normalizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.normalizeToolStripMenuItem.Text = "Normalize";
            // 
            // patternToRackToolStripMenuItem
            // 
            this.patternToRackToolStripMenuItem.Name = "patternToRackToolStripMenuItem";
            this.patternToRackToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.patternToRackToolStripMenuItem.Text = "Pattern to Rack";
            this.patternToRackToolStripMenuItem.Click += new System.EventHandler(this.patternToRackToolStripMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadLastToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 641);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(668, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(95, 17);
            this.toolStripStatusLabel1.Text = "Program loaded.";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "*.flp";
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "FLP Projects|*.flp|XML files|*.xml|Any|*.*";
            // 
            // saveFLPFileDialog
            // 
            this.saveFLPFileDialog.Filter = "FLP Project|*.flp";
            // 
            // saveXMLFileDialog
            // 
            this.saveXMLFileDialog.Filter = "XML File|*.xml";
            // 
            // flpPropertyGrid
            // 
            this.flpPropertyGrid.AllowDrop = true;
            this.flpPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPropertyGrid.Enabled = false;
            this.flpPropertyGrid.Location = new System.Drawing.Point(0, 24);
            this.flpPropertyGrid.Name = "flpPropertyGrid";
            this.flpPropertyGrid.Size = new System.Drawing.Size(668, 521);
            this.flpPropertyGrid.TabIndex = 2;
            this.flpPropertyGrid.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.flpPropertyGrid_PreviewKeyDown);
            // 
            // LogOutput
            // 
            this.LogOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LogOutput.Location = new System.Drawing.Point(0, 545);
            this.LogOutput.Name = "LogOutput";
            this.LogOutput.Size = new System.Drawing.Size(668, 96);
            this.LogOutput.TabIndex = 3;
            this.LogOutput.Text = "";
            // 
            // removeFL125EventsToolStripMenuItem
            // 
            this.removeFL125EventsToolStripMenuItem.Name = "removeFL125EventsToolStripMenuItem";
            this.removeFL125EventsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.removeFL125EventsToolStripMenuItem.Text = "Remove FL 12.5 Events";
            this.removeFL125EventsToolStripMenuItem.Click += new System.EventHandler(this.removeFL125EventsToolStripMenuItem_Click);
            // 
            // FLPEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 663);
            this.Controls.Add(this.flpPropertyGrid);
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.LogOutput);
            this.Controls.Add(this.statusStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "FLPEditForm";
            this.Text = "FLP Edit";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patternsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playlistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mixerToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveOverToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFLPFileDialog;
        private System.Windows.Forms.ToolStripMenuItem exportXMLToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveXMLFileDialog;
        private System.Windows.Forms.PropertyGrid flpPropertyGrid;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAsFstToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeDefaultEntriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteUnusedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reorderByToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rackChannelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetEmptyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reorderByToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sourceChannelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showInInspectorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFL123EventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showEventStatisticsToolStripMenuItem;
        private System.Windows.Forms.RichTextBox LogOutput;
        private System.Windows.Forms.ToolStripMenuItem normalizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patternToRackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFL125EventsToolStripMenuItem;
    }
}

