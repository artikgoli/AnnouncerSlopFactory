namespace AnnouncerSlopFactory
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            pnlTop = new Panel();
            lblModFolder = new Label();
            txtModFolder = new TextBox();
            btnBrowseModFolder = new Button();
            pnlBottom = new Panel();
            btnReset = new Button();
            lblStatus = new Label();
            btnExport = new Button();
            pnlLeft = new Panel();
            grpModInfo = new GroupBox();
            lblModName = new Label();
            txtModName = new TextBox();
            lblAnnouncerId = new Label();
            numAnnouncerId = new NumericUpDown();
            lblBaseId = new Label();
            numBaseId = new NumericUpDown();
            lblVolume = new Label();
            lblVolumeValue = new Label();
            trackBarVolume = new TrackBar();
            chkNeglectWaiting = new CheckBox();
            numNeglectWaiting = new NumericUpDown();
            chkNeglectRandomize = new CheckBox();
            numNeglectRandomize = new NumericUpDown();
            lblDisplayName = new Label();
            txtDisplayName = new TextBox();
            lblIcon = new Label();
            picIcon = new PictureBox();
            lblWalpurgis = new Label();
            cmbWalpurgis = new ComboBox();
            lblFrame = new Label();
            btnSelectFrame = new Button();
            btnSelectIcon = new Button();
            lblLivePreview = new Label();
            picPreview = new PictureBox();
            pnlRight = new Panel();
            grpDialogue = new GroupBox();
            dgvEntries = new DataGridView();
            colCategory = new DataGridViewButtonColumn();
            colVariant = new DataGridViewTextBoxColumn();
            colAudio = new DataGridViewTextBoxColumn();
            colDialogue = new DataGridViewTextBoxColumn();
            colDelete = new DataGridViewButtonColumn();
            pnlToolbar = new Panel();
            btnAddDialogue = new Button();
            lblHint = new Label();
            contextMenuCategories = new ContextMenuStrip(components);
            _toolTip = new ToolTip(components);
            pnlTop.SuspendLayout();
            pnlBottom.SuspendLayout();
            pnlLeft.SuspendLayout();
            grpModInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAnnouncerId).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numBaseId).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numNeglectWaiting).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numNeglectRandomize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
            pnlRight.SuspendLayout();
            grpDialogue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEntries).BeginInit();
            pnlToolbar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(lblModFolder);
            pnlTop.Controls.Add(txtModFolder);
            pnlTop.Controls.Add(btnBrowseModFolder);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(10);
            pnlTop.Size = new Size(1100, 48);
            pnlTop.TabIndex = 4;
            // 
            // lblModFolder
            // 
            lblModFolder.AutoSize = true;
            lblModFolder.Location = new Point(10, 17);
            lblModFolder.Name = "lblModFolder";
            lblModFolder.Size = new Size(71, 15);
            lblModFolder.TabIndex = 0;
            lblModFolder.Text = "Mod Folder:";
            // 
            // txtModFolder
            // 
            txtModFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtModFolder.Location = new Point(95, 14);
            txtModFolder.Name = "txtModFolder";
            txtModFolder.Size = new Size(906, 23);
            txtModFolder.TabIndex = 1;
            // 
            // btnBrowseModFolder
            // 
            btnBrowseModFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBrowseModFolder.Location = new Point(1007, 12);
            btnBrowseModFolder.Name = "btnBrowseModFolder";
            btnBrowseModFolder.Size = new Size(80, 25);
            btnBrowseModFolder.TabIndex = 2;
            btnBrowseModFolder.Text = "Browse...";
            btnBrowseModFolder.Click += BtnBrowseModFolder_Click;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnReset);
            pnlBottom.Controls.Add(lblStatus);
            pnlBottom.Controls.Add(btnExport);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 828);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Padding = new Padding(10);
            pnlBottom.Size = new Size(1100, 52);
            pnlBottom.TabIndex = 3;
            // 
            // btnReset
            // 
            btnReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnReset.Location = new Point(10, 10);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(100, 32);
            btnReset.TabIndex = 0;
            btnReset.Text = "Reset";
            btnReset.Click += BtnReset_Click;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.Gray;
            lblStatus.Location = new Point(125, 19);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(39, 15);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "Ready";
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnExport.Location = new Point(967, 10);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(120, 32);
            btnExport.TabIndex = 2;
            btnExport.Text = "Export Mod";
            btnExport.Click += BtnExport_Click;
            // 
            // pnlLeft
            // 
            pnlLeft.AutoScroll = true;
            pnlLeft.Controls.Add(grpModInfo);
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Location = new Point(0, 48);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Padding = new Padding(5);
            pnlLeft.Size = new Size(420, 780);
            pnlLeft.TabIndex = 2;
            // 
            // grpModInfo
            // 
            grpModInfo.Controls.Add(lblModName);
            grpModInfo.Controls.Add(txtModName);
            grpModInfo.Controls.Add(lblAnnouncerId);
            grpModInfo.Controls.Add(numAnnouncerId);
            grpModInfo.Controls.Add(lblBaseId);
            grpModInfo.Controls.Add(numBaseId);
            grpModInfo.Controls.Add(lblVolume);
            grpModInfo.Controls.Add(lblVolumeValue);
            grpModInfo.Controls.Add(trackBarVolume);
            grpModInfo.Controls.Add(chkNeglectWaiting);
            grpModInfo.Controls.Add(numNeglectWaiting);
            grpModInfo.Controls.Add(chkNeglectRandomize);
            grpModInfo.Controls.Add(numNeglectRandomize);
            grpModInfo.Controls.Add(lblDisplayName);
            grpModInfo.Controls.Add(txtDisplayName);
            grpModInfo.Controls.Add(lblIcon);
            grpModInfo.Controls.Add(picIcon);
            grpModInfo.Controls.Add(lblWalpurgis);
            grpModInfo.Controls.Add(cmbWalpurgis);
            grpModInfo.Controls.Add(lblFrame);
            grpModInfo.Controls.Add(btnSelectFrame);
            grpModInfo.Controls.Add(btnSelectIcon);
            grpModInfo.Controls.Add(lblLivePreview);
            grpModInfo.Controls.Add(picPreview);
            grpModInfo.Location = new Point(5, 5);
            grpModInfo.Name = "grpModInfo";
            grpModInfo.Size = new Size(395, 775);
            grpModInfo.TabIndex = 0;
            grpModInfo.TabStop = false;
            grpModInfo.Text = "Mod Information";
            // 
            // lblModName
            // 
            lblModName.AutoSize = true;
            lblModName.Location = new Point(15, 22);
            lblModName.Name = "lblModName";
            lblModName.Size = new Size(145, 15);
            lblModName.TabIndex = 0;
            lblModName.Text = "Mod Name (folder name):";
            // 
            // txtModName
            // 
            txtModName.Location = new Point(15, 42);
            txtModName.Name = "txtModName";
            txtModName.Size = new Size(350, 23);
            txtModName.TabIndex = 1;
            // 
            // lblAnnouncerId
            // 
            lblAnnouncerId.AutoSize = true;
            lblAnnouncerId.Location = new Point(15, 75);
            lblAnnouncerId.Name = "lblAnnouncerId";
            lblAnnouncerId.Size = new Size(147, 15);
            lblAnnouncerId.TabIndex = 2;
            lblAnnouncerId.Text = "Announcer ID (100 - 9999):";
            // 
            // numAnnouncerId
            // 
            numAnnouncerId.Location = new Point(15, 95);
            numAnnouncerId.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numAnnouncerId.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            numAnnouncerId.Name = "numAnnouncerId";
            numAnnouncerId.Size = new Size(120, 23);
            numAnnouncerId.TabIndex = 3;
            numAnnouncerId.Value = new decimal(new int[] { 9901, 0, 0, 0 });
            // 
            // lblBaseId
            // 
            lblBaseId.AutoSize = true;
            lblBaseId.Location = new Point(192, 75);
            lblBaseId.Name = "lblBaseId";
            lblBaseId.Size = new Size(148, 15);
            lblBaseId.TabIndex = 8;
            lblBaseId.Text = "Base ID (0 = none/default):";
            // 
            // numBaseId
            // 
            numBaseId.Location = new Point(192, 95);
            numBaseId.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            numBaseId.Name = "numBaseId";
            numBaseId.Size = new Size(120, 23);
            numBaseId.TabIndex = 9;
            // 
            // lblVolume
            // 
            lblVolume.AutoSize = true;
            lblVolume.Location = new Point(15, 123);
            lblVolume.Name = "lblVolume";
            lblVolume.Size = new Size(50, 15);
            lblVolume.TabIndex = 10;
            lblVolume.Text = "Volume:";
            // 
            // lblVolumeValue
            // 
            lblVolumeValue.ForeColor = Color.FromArgb(0, 120, 215);
            lblVolumeValue.Location = new Point(300, 123);
            lblVolumeValue.Name = "lblVolumeValue";
            lblVolumeValue.Size = new Size(65, 15);
            lblVolumeValue.TabIndex = 11;
            lblVolumeValue.Text = "100";
            lblVolumeValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // trackBarVolume
            // 
            trackBarVolume.LargeChange = 10;
            trackBarVolume.Location = new Point(15, 139);
            trackBarVolume.Maximum = 100;
            trackBarVolume.Name = "trackBarVolume";
            trackBarVolume.Size = new Size(350, 45);
            trackBarVolume.TabIndex = 12;
            trackBarVolume.TickFrequency = 10;
            trackBarVolume.Value = 100;
            // 
            // chkNeglectWaiting
            // 
            chkNeglectWaiting.AutoSize = true;
            chkNeglectWaiting.Location = new Point(15, 192);
            chkNeglectWaiting.Name = "chkNeglectWaiting";
            chkNeglectWaiting.Size = new Size(137, 19);
            chkNeglectWaiting.TabIndex = 13;
            chkNeglectWaiting.Text = "neglectWaiting (sec):";
            // 
            // numNeglectWaiting
            // 
            numNeglectWaiting.Enabled = false;
            numNeglectWaiting.Location = new Point(260, 190);
            numNeglectWaiting.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            numNeglectWaiting.Name = "numNeglectWaiting";
            numNeglectWaiting.Size = new Size(105, 23);
            numNeglectWaiting.TabIndex = 14;
            numNeglectWaiting.Value = new decimal(new int[] { 180, 0, 0, 0 });
            // 
            // chkNeglectRandomize
            // 
            chkNeglectRandomize.AutoSize = true;
            chkNeglectRandomize.Location = new Point(15, 220);
            chkNeglectRandomize.Name = "chkNeglectRandomize";
            chkNeglectRandomize.Size = new Size(148, 19);
            chkNeglectRandomize.TabIndex = 15;
            chkNeglectRandomize.Text = "neglectRandomize (%):";
            // 
            // numNeglectRandomize
            // 
            numNeglectRandomize.Enabled = false;
            numNeglectRandomize.Location = new Point(260, 220);
            numNeglectRandomize.Name = "numNeglectRandomize";
            numNeglectRandomize.Size = new Size(105, 23);
            numNeglectRandomize.TabIndex = 16;
            numNeglectRandomize.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // lblDisplayName
            // 
            lblDisplayName.AutoSize = true;
            lblDisplayName.Location = new Point(15, 281);
            lblDisplayName.Name = "lblDisplayName";
            lblDisplayName.Size = new Size(83, 15);
            lblDisplayName.TabIndex = 4;
            lblDisplayName.Text = "Display Name:";
            // 
            // txtDisplayName
            // 
            txtDisplayName.Location = new Point(15, 301);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Size = new Size(350, 23);
            txtDisplayName.TabIndex = 5;
            // 
            // lblIcon
            // 
            lblIcon.AutoSize = true;
            lblIcon.Location = new Point(15, 336);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(33, 15);
            lblIcon.TabIndex = 17;
            lblIcon.Text = "Icon:";
            // 
            // picIcon
            // 
            picIcon.BackColor = Color.FromArgb(240, 240, 240);
            picIcon.BorderStyle = BorderStyle.FixedSingle;
            picIcon.Location = new Point(26, 354);
            picIcon.Name = "picIcon";
            picIcon.Size = new Size(100, 100);
            picIcon.SizeMode = PictureBoxSizeMode.Zoom;
            picIcon.TabIndex = 18;
            picIcon.TabStop = false;
            // 
            // lblWalpurgis
            // 
            lblWalpurgis.AutoSize = true;
            lblWalpurgis.Location = new Point(159, 336);
            lblWalpurgis.Name = "lblWalpurgis";
            lblWalpurgis.Size = new Size(75, 15);
            lblWalpurgis.TabIndex = 6;
            lblWalpurgis.Text = "Tape Overlay";
            // 
            // cmbWalpurgis
            // 
            cmbWalpurgis.Items.AddRange(new object[] { "NONE", "LC", "LOR", "LC_QOH", "PILGRIMAGE", "Custom" });
            cmbWalpurgis.Location = new Point(159, 354);
            cmbWalpurgis.Name = "cmbWalpurgis";
            cmbWalpurgis.Size = new Size(217, 23);
            cmbWalpurgis.TabIndex = 7;
            cmbWalpurgis.Text = "LOR";
            // 
            // lblFrame
            // 
            lblFrame.AutoSize = true;
            lblFrame.Location = new Point(159, 439);
            lblFrame.Name = "lblFrame";
            lblFrame.Size = new Size(166, 15);
            lblFrame.TabIndex = 20;
            lblFrame.Text = "Overlay Frame PNG (Custom):";
            // 
            // btnSelectFrame
            // 
            btnSelectFrame.Location = new Point(159, 460);
            btnSelectFrame.Name = "btnSelectFrame";
            btnSelectFrame.Size = new Size(130, 30);
            btnSelectFrame.TabIndex = 21;
            btnSelectFrame.Text = "Select Frame...";
            btnSelectFrame.Click += BtnSelectFrame_Click;
            // 
            // btnSelectIcon
            // 
            btnSelectIcon.Location = new Point(15, 460);
            btnSelectIcon.Name = "btnSelectIcon";
            btnSelectIcon.Size = new Size(130, 30);
            btnSelectIcon.TabIndex = 19;
            btnSelectIcon.Text = "Select Icon...";
            btnSelectIcon.Click += BtnSelectIcon_Click;
            // 
            // lblLivePreview
            // 
            lblLivePreview.AutoSize = true;
            lblLivePreview.Location = new Point(15, 512);
            lblLivePreview.Name = "lblLivePreview";
            lblLivePreview.Size = new Size(51, 15);
            lblLivePreview.TabIndex = 22;
            lblLivePreview.Text = "Preview:";
            // 
            // picPreview
            // 
            picPreview.BackColor = Color.FromArgb(224, 224, 224);
            picPreview.BorderStyle = BorderStyle.FixedSingle;
            picPreview.Location = new Point(15, 532);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(360, 240);
            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.TabIndex = 23;
            picPreview.TabStop = false;
            // 
            // pnlRight
            // 
            pnlRight.Controls.Add(grpDialogue);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Location = new Point(420, 48);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(5);
            pnlRight.Size = new Size(680, 780);
            pnlRight.TabIndex = 1;
            // 
            // grpDialogue
            // 
            grpDialogue.Controls.Add(dgvEntries);
            grpDialogue.Controls.Add(pnlToolbar);
            grpDialogue.Dock = DockStyle.Fill;
            grpDialogue.Location = new Point(5, 5);
            grpDialogue.Name = "grpDialogue";
            grpDialogue.Size = new Size(670, 770);
            grpDialogue.TabIndex = 0;
            grpDialogue.TabStop = false;
            grpDialogue.Text = "Dialogue Entries";
            // 
            // dgvEntries
            // 
            dgvEntries.AllowUserToAddRows = false;
            dgvEntries.AllowUserToDeleteRows = false;
            dgvEntries.AllowUserToResizeRows = false;
            dgvEntries.BackgroundColor = Color.White;
            dgvEntries.BorderStyle = BorderStyle.None;
            dgvEntries.ColumnHeadersHeight = 28;
            dgvEntries.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvEntries.Columns.AddRange(new DataGridViewColumn[] { colCategory, colVariant, colAudio, colDialogue, colDelete });
            dgvEntries.Dock = DockStyle.Fill;
            dgvEntries.GridColor = Color.LightGray;
            dgvEntries.Location = new Point(3, 57);
            dgvEntries.MultiSelect = false;
            dgvEntries.Name = "dgvEntries";
            dgvEntries.ReadOnly = true;
            dgvEntries.RowHeadersVisible = false;
            dgvEntries.RowTemplate.Height = 26;
            dgvEntries.ScrollBars = ScrollBars.Vertical;
            dgvEntries.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEntries.Size = new Size(664, 710);
            dgvEntries.TabIndex = 0;
            dgvEntries.CellContentClick += DgvEntries_CellContentClick;
            // 
            // colCategory
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colCategory.DefaultCellStyle = dataGridViewCellStyle1;
            colCategory.FlatStyle = FlatStyle.Flat;
            colCategory.HeaderText = "Category";
            colCategory.Name = "colCategory";
            colCategory.ReadOnly = true;
            colCategory.Width = 200;
            // 
            // colVariant
            // 
            colVariant.HeaderText = "Var";
            colVariant.Name = "colVariant";
            colVariant.ReadOnly = true;
            colVariant.Width = 45;
            // 
            // colAudio
            // 
            colAudio.HeaderText = "Audio";
            colAudio.Name = "colAudio";
            colAudio.ReadOnly = true;
            colAudio.Width = 160;
            // 
            // colDialogue
            // 
            colDialogue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDialogue.HeaderText = "Dialogue";
            colDialogue.Name = "colDialogue";
            colDialogue.ReadOnly = true;
            // 
            // colDelete
            // 
            colDelete.FlatStyle = FlatStyle.Flat;
            colDelete.HeaderText = "";
            colDelete.Name = "colDelete";
            colDelete.ReadOnly = true;
            colDelete.Width = 32;
            // 
            // pnlToolbar
            // 
            pnlToolbar.Controls.Add(btnAddDialogue);
            pnlToolbar.Controls.Add(lblHint);
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Location = new Point(3, 19);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Size = new Size(664, 38);
            pnlToolbar.TabIndex = 1;
            // 
            // btnAddDialogue
            // 
            btnAddDialogue.Location = new Point(10, 6);
            btnAddDialogue.Name = "btnAddDialogue";
            btnAddDialogue.Size = new Size(150, 28);
            btnAddDialogue.TabIndex = 0;
            btnAddDialogue.Text = "Add Dialogue ▼";
            btnAddDialogue.Click += BtnAddDialogue_Click;
            // 
            // lblHint
            // 
            lblHint.AutoSize = true;
            lblHint.ForeColor = Color.Gray;
            lblHint.Location = new Point(170, 14);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(358, 15);
            lblHint.TabIndex = 1;
            lblHint.Text = "Click a category name in the grid to set its audio and dialogue text.";
            // 
            // contextMenuCategories
            // 
            contextMenuCategories.Name = "contextMenuCategories";
            contextMenuCategories.ShowImageMargin = false;
            contextMenuCategories.Size = new Size(36, 4);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 880);
            Controls.Add(pnlRight);
            Controls.Add(pnlLeft);
            Controls.Add(pnlBottom);
            Controls.Add(pnlTop);
            MinimumSize = new Size(950, 700);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Announcer Slop Factory";
            Load += Form1_Load;
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlBottom.ResumeLayout(false);
            pnlBottom.PerformLayout();
            pnlLeft.ResumeLayout(false);
            grpModInfo.ResumeLayout(false);
            grpModInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAnnouncerId).EndInit();
            ((System.ComponentModel.ISupportInitialize)numBaseId).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).EndInit();
            ((System.ComponentModel.ISupportInitialize)numNeglectWaiting).EndInit();
            ((System.ComponentModel.ISupportInitialize)numNeglectRandomize).EndInit();
            ((System.ComponentModel.ISupportInitialize)picIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
            pnlRight.ResumeLayout(false);
            grpDialogue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvEntries).EndInit();
            pnlToolbar.ResumeLayout(false);
            pnlToolbar.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblModFolder;
        private System.Windows.Forms.TextBox txtModFolder;
        private System.Windows.Forms.Button btnBrowseModFolder;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.GroupBox grpModInfo;
        private System.Windows.Forms.Label lblModName;
        private System.Windows.Forms.TextBox txtModName;
        private System.Windows.Forms.Label lblAnnouncerId;
        private System.Windows.Forms.NumericUpDown numAnnouncerId;
        private System.Windows.Forms.Label lblDisplayName;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Label lblWalpurgis;
        private System.Windows.Forms.ComboBox cmbWalpurgis;
        private System.Windows.Forms.Label lblBaseId;
        private System.Windows.Forms.NumericUpDown numBaseId;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.Label lblVolumeValue;
        private System.Windows.Forms.TrackBar trackBarVolume;
        private System.Windows.Forms.CheckBox chkNeglectWaiting;
        private System.Windows.Forms.NumericUpDown numNeglectWaiting;
        private System.Windows.Forms.CheckBox chkNeglectRandomize;
        private System.Windows.Forms.NumericUpDown numNeglectRandomize;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Button btnSelectIcon;
        private System.Windows.Forms.Label lblFrame;
        private System.Windows.Forms.Button btnSelectFrame;
        private System.Windows.Forms.Label lblLivePreview;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.GroupBox grpDialogue;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Button btnAddDialogue;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.DataGridView dgvEntries;
        private System.Windows.Forms.DataGridViewButtonColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVariant;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAudio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDialogue;
        private System.Windows.Forms.DataGridViewButtonColumn colDelete;
        private System.Windows.Forms.ContextMenuStrip contextMenuCategories;
        private System.Windows.Forms.ToolTip _toolTip;
    }
}