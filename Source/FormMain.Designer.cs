namespace RegRipperRunner
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.listPlugins = new BrightIdeasSoftware.ObjectListView();
            this.olvcName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcHive = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcOs = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcVersion = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcShortDesc = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.context = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.contextFilterAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.contextFilterDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextFilterSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextFilterNew = new System.Windows.Forms.ToolStripMenuItem();
            this.contextFilterSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.contextFilterRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextPlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsRunPlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsRunHive = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsRunFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuToolsAutoRip = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuToolsOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.lblMode = new System.Windows.Forms.ToolStripLabel();
            this.cboMode = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblFilter = new System.Windows.Forms.ToolStripLabel();
            this.cboFilter = new System.Windows.Forms.ToolStripComboBox();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPagePlugins = new System.Windows.Forms.TabPage();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.listPlugins)).BeginInit();
            this.context.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPagePlugins.SuspendLayout();
            this.tabPageOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // listPlugins
            // 
            this.listPlugins.AllColumns.Add(this.olvcName);
            this.listPlugins.AllColumns.Add(this.olvcHive);
            this.listPlugins.AllColumns.Add(this.olvcOs);
            this.listPlugins.AllColumns.Add(this.olvcVersion);
            this.listPlugins.AllColumns.Add(this.olvcShortDesc);
            this.listPlugins.CheckBoxes = true;
            this.listPlugins.CheckedAspectName = "Active";
            this.listPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcName,
            this.olvcHive,
            this.olvcOs,
            this.olvcVersion,
            this.olvcShortDesc});
            this.listPlugins.ContextMenuStrip = this.context;
            this.listPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listPlugins.FullRowSelect = true;
            this.listPlugins.HideSelection = false;
            this.listPlugins.Location = new System.Drawing.Point(3, 3);
            this.listPlugins.Name = "listPlugins";
            this.listPlugins.ShowGroups = false;
            this.listPlugins.ShowImagesOnSubItems = true;
            this.listPlugins.Size = new System.Drawing.Size(504, 311);
            this.listPlugins.TabIndex = 0;
            this.listPlugins.UseCompatibleStateImageBehavior = false;
            this.listPlugins.UseFiltering = true;
            this.listPlugins.View = System.Windows.Forms.View.Details;
            this.listPlugins.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listPlugins_MouseDoubleClick);
            // 
            // olvcName
            // 
            this.olvcName.AspectName = "Name";
            this.olvcName.CellPadding = null;
            this.olvcName.IsEditable = false;
            this.olvcName.Text = "Name";
            // 
            // olvcHive
            // 
            this.olvcHive.AspectName = "Hive";
            this.olvcHive.CellPadding = null;
            this.olvcHive.IsEditable = false;
            this.olvcHive.Text = "Hive";
            // 
            // olvcOs
            // 
            this.olvcOs.AspectName = "Os";
            this.olvcOs.CellPadding = null;
            this.olvcOs.IsEditable = false;
            this.olvcOs.Text = "OS";
            // 
            // olvcVersion
            // 
            this.olvcVersion.AspectName = "Version";
            this.olvcVersion.CellPadding = null;
            this.olvcVersion.IsEditable = false;
            this.olvcVersion.Text = "Version";
            // 
            // olvcShortDesc
            // 
            this.olvcShortDesc.AspectName = "ShortDesc";
            this.olvcShortDesc.CellPadding = null;
            this.olvcShortDesc.IsEditable = false;
            this.olvcShortDesc.Text = "Desc";
            // 
            // context
            // 
            this.context.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextFilter,
            this.contextSep1,
            this.contextPlugin});
            this.context.Name = "context";
            this.context.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.context.Size = new System.Drawing.Size(109, 54);
            this.context.Opening += new System.ComponentModel.CancelEventHandler(this.context_Opening);
            // 
            // contextFilter
            // 
            this.contextFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextFilterAdd,
            this.contextFilterDelete,
            this.contextFilterSep1,
            this.contextFilterNew,
            this.contextFilterSep2,
            this.contextFilterRefresh});
            this.contextFilter.Name = "contextFilter";
            this.contextFilter.Size = new System.Drawing.Size(108, 22);
            this.contextFilter.Text = "Filter";
            // 
            // contextFilterAdd
            // 
            this.contextFilterAdd.Name = "contextFilterAdd";
            this.contextFilterAdd.Size = new System.Drawing.Size(113, 22);
            this.contextFilterAdd.Text = "Add";
            this.contextFilterAdd.Click += new System.EventHandler(this.contextFilterAdd_Click);
            // 
            // contextFilterDelete
            // 
            this.contextFilterDelete.Name = "contextFilterDelete";
            this.contextFilterDelete.Size = new System.Drawing.Size(113, 22);
            this.contextFilterDelete.Text = "Delete";
            this.contextFilterDelete.Click += new System.EventHandler(this.contextFilterDelete_Click);
            // 
            // contextFilterSep1
            // 
            this.contextFilterSep1.Name = "contextFilterSep1";
            this.contextFilterSep1.Size = new System.Drawing.Size(110, 6);
            // 
            // contextFilterNew
            // 
            this.contextFilterNew.Name = "contextFilterNew";
            this.contextFilterNew.Size = new System.Drawing.Size(113, 22);
            this.contextFilterNew.Text = "New";
            this.contextFilterNew.Click += new System.EventHandler(this.contextFilterNew_Click);
            // 
            // contextFilterSep2
            // 
            this.contextFilterSep2.Name = "contextFilterSep2";
            this.contextFilterSep2.Size = new System.Drawing.Size(110, 6);
            // 
            // contextFilterRefresh
            // 
            this.contextFilterRefresh.Name = "contextFilterRefresh";
            this.contextFilterRefresh.Size = new System.Drawing.Size(113, 22);
            this.contextFilterRefresh.Text = "Refresh";
            this.contextFilterRefresh.Click += new System.EventHandler(this.contextFilterRefresh_Click);
            // 
            // contextSep1
            // 
            this.contextSep1.Name = "contextSep1";
            this.contextSep1.Size = new System.Drawing.Size(105, 6);
            // 
            // contextPlugin
            // 
            this.contextPlugin.Name = "contextPlugin";
            this.contextPlugin.Size = new System.Drawing.Size(108, 22);
            this.contextPlugin.Text = "Plugin";
            this.contextPlugin.Click += new System.EventHandler(this.contextPlugin_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuTools,
            this.menuHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip.Size = new System.Drawing.Size(518, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "&File";
            // 
            // menuFileExit
            // 
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.Size = new System.Drawing.Size(92, 22);
            this.menuFileExit.Text = "Exit";
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // menuTools
            // 
            this.menuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolsRunPlugin,
            this.menuToolsRunHive,
            this.menuToolsRunFolder,
            this.toolStripMenuItem2,
            this.menuToolsAutoRip,
            this.menuToolsSep1,
            this.menuToolsOptions});
            this.menuTools.Name = "menuTools";
            this.menuTools.Size = new System.Drawing.Size(48, 20);
            this.menuTools.Text = "&Tools";
            // 
            // menuToolsRunPlugin
            // 
            this.menuToolsRunPlugin.Name = "menuToolsRunPlugin";
            this.menuToolsRunPlugin.Size = new System.Drawing.Size(132, 22);
            this.menuToolsRunPlugin.Text = "Run Plugin";
            this.menuToolsRunPlugin.Click += new System.EventHandler(this.menuToolsRunPlugin_Click);
            // 
            // menuToolsRunHive
            // 
            this.menuToolsRunHive.Name = "menuToolsRunHive";
            this.menuToolsRunHive.Size = new System.Drawing.Size(132, 22);
            this.menuToolsRunHive.Text = "Run Hive";
            this.menuToolsRunHive.Click += new System.EventHandler(this.menuToolsRunHive_Click);
            // 
            // menuToolsRunFolder
            // 
            this.menuToolsRunFolder.Name = "menuToolsRunFolder";
            this.menuToolsRunFolder.Size = new System.Drawing.Size(132, 22);
            this.menuToolsRunFolder.Text = "Run Folder";
            this.menuToolsRunFolder.Click += new System.EventHandler(this.menuToolsRunFolder_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(129, 6);
            // 
            // menuToolsAutoRip
            // 
            this.menuToolsAutoRip.Name = "menuToolsAutoRip";
            this.menuToolsAutoRip.Size = new System.Drawing.Size(132, 22);
            this.menuToolsAutoRip.Text = "Auto Rip";
            this.menuToolsAutoRip.Click += new System.EventHandler(this.menuToolsAutoRip_Click);
            // 
            // menuToolsSep1
            // 
            this.menuToolsSep1.Name = "menuToolsSep1";
            this.menuToolsSep1.Size = new System.Drawing.Size(129, 6);
            // 
            // menuToolsOptions
            // 
            this.menuToolsOptions.Name = "menuToolsOptions";
            this.menuToolsOptions.Size = new System.Drawing.Size(132, 22);
            this.menuToolsOptions.Text = "Options";
            this.menuToolsOptions.Click += new System.EventHandler(this.menuToolsOptions_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpHelp,
            this.toolStripMenuItem1,
            this.menuHelpAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(44, 20);
            this.menuHelp.Text = "&Help";
            // 
            // menuHelpHelp
            // 
            this.menuHelpHelp.Name = "menuHelpHelp";
            this.menuHelpHelp.Size = new System.Drawing.Size(107, 22);
            this.menuHelpHelp.Text = "Help";
            this.menuHelpHelp.Click += new System.EventHandler(this.menuHelpHelp_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(104, 6);
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.Size = new System.Drawing.Size(107, 22);
            this.menuHelpAbout.Text = "&About";
            this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 394);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(518, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMode,
            this.cboMode,
            this.toolStripSeparator1,
            this.lblFilter,
            this.cboFilter});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(518, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip1";
            // 
            // lblMode
            // 
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(38, 22);
            this.lblMode.Text = "Mode";
            // 
            // cboMode
            // 
            this.cboMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMode.Items.AddRange(new object[] {
            "Single Plugin",
            "Multiple Plugins",
            "All Plugins",
            "Filter"});
            this.cboMode.Name = "cboMode";
            this.cboMode.Size = new System.Drawing.Size(121, 25);
            this.cboMode.SelectedIndexChanged += new System.EventHandler(this.cboMode_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // lblFilter
            // 
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(33, 22);
            this.lblFilter.Text = "Filter";
            // 
            // cboFilter
            // 
            this.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(121, 25);
            this.cboFilter.SelectedIndexChanged += new System.EventHandler(this.cboFilter_SelectedIndexChanged);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPagePlugins);
            this.tabMain.Controls.Add(this.tabPageOutput);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 49);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(518, 345);
            this.tabMain.TabIndex = 4;
            // 
            // tabPagePlugins
            // 
            this.tabPagePlugins.Controls.Add(this.listPlugins);
            this.tabPagePlugins.Location = new System.Drawing.Point(4, 24);
            this.tabPagePlugins.Name = "tabPagePlugins";
            this.tabPagePlugins.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlugins.Size = new System.Drawing.Size(510, 317);
            this.tabPagePlugins.TabIndex = 0;
            this.tabPagePlugins.Text = "Plugins";
            this.tabPagePlugins.UseVisualStyleBackColor = true;
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.Controls.Add(this.txtOutput);
            this.tabPageOutput.Location = new System.Drawing.Point(4, 24);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutput.Size = new System.Drawing.Size(510, 317);
            this.tabPageOutput.TabIndex = 1;
            this.tabPageOutput.Text = "Output";
            this.tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // txtOutput
            // 
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(3, 3);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.txtOutput.Size = new System.Drawing.Size(504, 311);
            this.txtOutput.TabIndex = 2;
            this.txtOutput.Text = "";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 416);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegRipperRunner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listPlugins)).EndInit();
            this.context.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabPagePlugins.ResumeLayout(false);
            this.tabPageOutput.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView listPlugins;
        private BrightIdeasSoftware.OLVColumn olvcHive;
        private BrightIdeasSoftware.OLVColumn olvcOs;
        private BrightIdeasSoftware.OLVColumn olvcVersion;
        private BrightIdeasSoftware.OLVColumn olvcShortDesc;
        private BrightIdeasSoftware.OLVColumn olvcName;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripLabel lblMode;
        private System.Windows.Forms.ToolStripComboBox cboMode;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuTools;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPagePlugins;
        private System.Windows.Forms.TabPage tabPageOutput;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem menuToolsRunPlugin;
        private System.Windows.Forms.ToolStripMenuItem menuToolsRunHive;
        private System.Windows.Forms.ToolStripMenuItem menuToolsRunFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lblFilter;
        private System.Windows.Forms.ToolStripComboBox cboFilter;
        private System.Windows.Forms.ToolStripSeparator menuToolsSep1;
        private System.Windows.Forms.ToolStripMenuItem menuToolsOptions;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ContextMenuStrip context;
        private System.Windows.Forms.ToolStripMenuItem contextFilter;
        private System.Windows.Forms.ToolStripMenuItem contextFilterAdd;
        private System.Windows.Forms.ToolStripMenuItem contextFilterDelete;
        private System.Windows.Forms.ToolStripSeparator contextFilterSep1;
        private System.Windows.Forms.ToolStripMenuItem contextFilterRefresh;
        private System.Windows.Forms.ToolStripMenuItem contextFilterNew;
        private System.Windows.Forms.ToolStripSeparator contextFilterSep2;
        private System.Windows.Forms.ToolStripMenuItem menuHelpHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator contextSep1;
        private System.Windows.Forms.ToolStripMenuItem contextPlugin;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuToolsAutoRip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}

