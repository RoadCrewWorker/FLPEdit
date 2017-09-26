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
            this.scriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playlistToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mixerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.patternsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeDefaultEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFL123EventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFL125EventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetEmptyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reorderByToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seperateOverlappingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patternsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteUnusedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mixerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patternToRackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inspectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compatibilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aSCIIOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allowUnknownIDsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dBGUnlockUnfixedOperationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFLPFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveXMLFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.flpPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.LogOutput = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mainMenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.scriptsToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.inspectorToolStripMenuItem,
            this.compatibilityToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(753, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadNewToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveOverToolStripMenuItem,
            this.exportXMLToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadNewToolStripMenuItem
            // 
            this.loadNewToolStripMenuItem.Name = "loadNewToolStripMenuItem";
            this.loadNewToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.loadNewToolStripMenuItem.Text = "Open";
            this.loadNewToolStripMenuItem.Click += new System.EventHandler(this.loadNewToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // saveOverToolStripMenuItem
            // 
            this.saveOverToolStripMenuItem.Enabled = false;
            this.saveOverToolStripMenuItem.Name = "saveOverToolStripMenuItem";
            this.saveOverToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.saveOverToolStripMenuItem.Text = "Save Over";
            this.saveOverToolStripMenuItem.Click += new System.EventHandler(this.saveOverToolStripMenuItem_Click);
            // 
            // exportXMLToolStripMenuItem
            // 
            this.exportXMLToolStripMenuItem.Name = "exportXMLToolStripMenuItem";
            this.exportXMLToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.exportXMLToolStripMenuItem.Text = "Export xml";
            this.exportXMLToolStripMenuItem.Click += new System.EventHandler(this.exportXMLToolStripMenuItem_Click);
            // 
            // scriptsToolStripMenuItem
            // 
            this.scriptsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showStatisticsToolStripMenuItem,
            this.fileToolStripMenuItem1,
            this.channelsToolStripMenuItem,
            this.playlistToolStripMenuItem,
            this.patternsToolStripMenuItem,
            this.mixerToolStripMenuItem,
            this.normalizeToolStripMenuItem});
            this.scriptsToolStripMenuItem.Enabled = false;
            this.scriptsToolStripMenuItem.Name = "scriptsToolStripMenuItem";
            this.scriptsToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.scriptsToolStripMenuItem.Text = "Operations";
            // 
            // showStatisticsToolStripMenuItem
            // 
            this.showStatisticsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eventsToolStripMenuItem,
            this.playlistToolStripMenuItem1,
            this.mixerToolStripMenuItem1,
            this.patternsToolStripMenuItem1});
            this.showStatisticsToolStripMenuItem.Name = "showStatisticsToolStripMenuItem";
            this.showStatisticsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showStatisticsToolStripMenuItem.Text = "Show Statistics";
            // 
            // eventsToolStripMenuItem
            // 
            this.eventsToolStripMenuItem.Name = "eventsToolStripMenuItem";
            this.eventsToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eventsToolStripMenuItem.Text = "Events";
            this.eventsToolStripMenuItem.Click += new System.EventHandler(this.showEventStatisticsToolStripMenuItem_Click);
            // 
            // playlistToolStripMenuItem1
            // 
            this.playlistToolStripMenuItem1.Name = "playlistToolStripMenuItem1";
            this.playlistToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.playlistToolStripMenuItem1.Text = "Playlist";
            this.playlistToolStripMenuItem1.Click += new System.EventHandler(this.showPlaylistStatisticsToolStripMenuItem1_Click);
            // 
            // mixerToolStripMenuItem1
            // 
            this.mixerToolStripMenuItem1.Enabled = false;
            this.mixerToolStripMenuItem1.Name = "mixerToolStripMenuItem1";
            this.mixerToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.mixerToolStripMenuItem1.Text = "Mixer";
            this.mixerToolStripMenuItem1.Click += new System.EventHandler(this.showMixerStatisticsToolStripMenuItem1_Click);
            // 
            // patternsToolStripMenuItem1
            // 
            this.patternsToolStripMenuItem1.Name = "patternsToolStripMenuItem1";
            this.patternsToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.patternsToolStripMenuItem1.Text = "Patterns";
            this.patternsToolStripMenuItem1.Click += new System.EventHandler(this.showPatternsStatisticsToolStripMenuItem1_Click);
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeDefaultEntriesToolStripMenuItem,
            this.removeFL123EventsToolStripMenuItem,
            this.removeFL125EventsToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // removeDefaultEntriesToolStripMenuItem
            // 
            this.removeDefaultEntriesToolStripMenuItem.Enabled = false;
            this.removeDefaultEntriesToolStripMenuItem.Name = "removeDefaultEntriesToolStripMenuItem";
            this.removeDefaultEntriesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.removeDefaultEntriesToolStripMenuItem.Text = "Remove Default Values";
            this.removeDefaultEntriesToolStripMenuItem.Click += new System.EventHandler(this.removeDefaultEntriesToolStripMenuItem_Click);
            // 
            // removeFL123EventsToolStripMenuItem
            // 
            this.removeFL123EventsToolStripMenuItem.Enabled = false;
            this.removeFL123EventsToolStripMenuItem.Name = "removeFL123EventsToolStripMenuItem";
            this.removeFL123EventsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.removeFL123EventsToolStripMenuItem.Text = "Remove FL12.3 Events";
            this.removeFL123EventsToolStripMenuItem.Click += new System.EventHandler(this.removeFL123EventsToolStripMenuItem_Click);
            // 
            // removeFL125EventsToolStripMenuItem
            // 
            this.removeFL125EventsToolStripMenuItem.Enabled = false;
            this.removeFL125EventsToolStripMenuItem.Name = "removeFL125EventsToolStripMenuItem";
            this.removeFL125EventsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.removeFL125EventsToolStripMenuItem.Text = "Remove FL12.5 Events";
            this.removeFL125EventsToolStripMenuItem.Click += new System.EventHandler(this.removeFL125EventsToolStripMenuItem_Click);
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
            this.reorderByToolStripMenuItem1,
            this.seperateOverlappingToolStripMenuItem});
            this.playlistToolStripMenuItem.Name = "playlistToolStripMenuItem";
            this.playlistToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.playlistToolStripMenuItem.Text = "Playlist";
            // 
            // resetEmptyToolStripMenuItem
            // 
            this.resetEmptyToolStripMenuItem.Enabled = false;
            this.resetEmptyToolStripMenuItem.Name = "resetEmptyToolStripMenuItem";
            this.resetEmptyToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.resetEmptyToolStripMenuItem.Text = "Reset Empty";
            // 
            // reorderByToolStripMenuItem1
            // 
            this.reorderByToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourceChannelToolStripMenuItem});
            this.reorderByToolStripMenuItem1.Name = "reorderByToolStripMenuItem1";
            this.reorderByToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.reorderByToolStripMenuItem1.Text = "Reorder by";
            // 
            // sourceChannelToolStripMenuItem
            // 
            this.sourceChannelToolStripMenuItem.Name = "sourceChannelToolStripMenuItem";
            this.sourceChannelToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.sourceChannelToolStripMenuItem.Text = "Source/Channel";
            this.sourceChannelToolStripMenuItem.Click += new System.EventHandler(this.sourceChannelToolStripMenuItem_Click);
            // 
            // seperateOverlappingToolStripMenuItem
            // 
            this.seperateOverlappingToolStripMenuItem.Enabled = false;
            this.seperateOverlappingToolStripMenuItem.Name = "seperateOverlappingToolStripMenuItem";
            this.seperateOverlappingToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.seperateOverlappingToolStripMenuItem.Text = "Seperate Overlapping";
            // 
            // patternsToolStripMenuItem
            // 
            this.patternsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteUnusedToolStripMenuItem});
            this.patternsToolStripMenuItem.Name = "patternsToolStripMenuItem";
            this.patternsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.patternsToolStripMenuItem.Text = "Patterns";
            // 
            // deleteUnusedToolStripMenuItem
            // 
            this.deleteUnusedToolStripMenuItem.Enabled = false;
            this.deleteUnusedToolStripMenuItem.Name = "deleteUnusedToolStripMenuItem";
            this.deleteUnusedToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.deleteUnusedToolStripMenuItem.Text = "Delete Unused";
            this.deleteUnusedToolStripMenuItem.Click += new System.EventHandler(this.deleteUnusedToolStripMenuItem_Click);
            // 
            // mixerToolStripMenuItem
            // 
            this.mixerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importFromProjectToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.mixerToolStripMenuItem.Name = "mixerToolStripMenuItem";
            this.mixerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mixerToolStripMenuItem.Text = "Mixer";
            // 
            // importFromProjectToolStripMenuItem
            // 
            this.importFromProjectToolStripMenuItem.Name = "importFromProjectToolStripMenuItem";
            this.importFromProjectToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.importFromProjectToolStripMenuItem.Text = "Import From Project";
            this.importFromProjectToolStripMenuItem.Click += new System.EventHandler(this.importMixerFromProjectToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.deleteToolStripMenuItem.Text = "Clear";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteMixerToolStripMenuItem_Click);
            // 
            // normalizeToolStripMenuItem
            // 
            this.normalizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patternToRackToolStripMenuItem});
            this.normalizeToolStripMenuItem.Enabled = false;
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
            // inspectorToolStripMenuItem
            // 
            this.inspectorToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.inspectorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showCurrentToolStripMenuItem,
            this.hideToolStripMenuItem});
            this.inspectorToolStripMenuItem.Name = "inspectorToolStripMenuItem";
            this.inspectorToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.inspectorToolStripMenuItem.Text = "Inspector";
            // 
            // showCurrentToolStripMenuItem
            // 
            this.showCurrentToolStripMenuItem.Name = "showCurrentToolStripMenuItem";
            this.showCurrentToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.showCurrentToolStripMenuItem.Text = "Show Current";
            this.showCurrentToolStripMenuItem.Click += new System.EventHandler(this.showInInspectorToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // compatibilityToolStripMenuItem
            // 
            this.compatibilityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aSCIIOnlyToolStripMenuItem,
            this.allowUnknownIDsToolStripMenuItem,
            this.dBGUnlockUnfixedOperationsToolStripMenuItem});
            this.compatibilityToolStripMenuItem.Name = "compatibilityToolStripMenuItem";
            this.compatibilityToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.compatibilityToolStripMenuItem.Text = "Compatibility";
            // 
            // aSCIIOnlyToolStripMenuItem
            // 
            this.aSCIIOnlyToolStripMenuItem.Enabled = false;
            this.aSCIIOnlyToolStripMenuItem.Name = "aSCIIOnlyToolStripMenuItem";
            this.aSCIIOnlyToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.aSCIIOnlyToolStripMenuItem.Text = "ASCII only";
            this.aSCIIOnlyToolStripMenuItem.Click += new System.EventHandler(this.aSCIIOnlyToolStripMenuItem_Click);
            // 
            // allowUnknownIDsToolStripMenuItem
            // 
            this.allowUnknownIDsToolStripMenuItem.Name = "allowUnknownIDsToolStripMenuItem";
            this.allowUnknownIDsToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.allowUnknownIDsToolStripMenuItem.Text = "DBG: Allow unknown IDs";
            this.allowUnknownIDsToolStripMenuItem.Click += new System.EventHandler(this.allowUnknownIDsToolStripMenuItem_Click);
            // 
            // dBGUnlockUnfixedOperationsToolStripMenuItem
            // 
            this.dBGUnlockUnfixedOperationsToolStripMenuItem.Name = "dBGUnlockUnfixedOperationsToolStripMenuItem";
            this.dBGUnlockUnfixedOperationsToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.dBGUnlockUnfixedOperationsToolStripMenuItem.Text = "DBG: Unlock unfixed Operations";
            this.dBGUnlockUnfixedOperationsToolStripMenuItem.Click += new System.EventHandler(this.dBGUnlockUnfixedOperationsToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 338);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(753, 22);
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
            this.flpPropertyGrid.HelpVisible = false;
            this.flpPropertyGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.flpPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.flpPropertyGrid.Name = "flpPropertyGrid";
            this.flpPropertyGrid.Size = new System.Drawing.Size(753, 250);
            this.flpPropertyGrid.TabIndex = 2;
            this.flpPropertyGrid.ToolbarVisible = false;
            this.flpPropertyGrid.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.flpPropertyGrid_PreviewKeyDown);
            // 
            // LogOutput
            // 
            this.LogOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogOutput.Location = new System.Drawing.Point(0, 0);
            this.LogOutput.Name = "LogOutput";
            this.LogOutput.Size = new System.Drawing.Size(753, 60);
            this.LogOutput.TabIndex = 3;
            this.LogOutput.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flpPropertyGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.LogOutput);
            this.splitContainer1.Size = new System.Drawing.Size(753, 314);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 4;
            // 
            // FLPEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 360);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.statusStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "FLPEditForm";
            this.Text = "FLP Edit";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeDefaultEntriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteUnusedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetEmptyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reorderByToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sourceChannelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFL123EventsToolStripMenuItem;
        private System.Windows.Forms.RichTextBox LogOutput;
        private System.Windows.Forms.ToolStripMenuItem normalizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patternToRackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFL125EventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFromProjectToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem seperateOverlappingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showStatisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playlistToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mixerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem patternsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem inspectorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showCurrentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compatibilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aSCIIOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allowUnknownIDsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dBGUnlockUnfixedOperationsToolStripMenuItem;
    }
}

