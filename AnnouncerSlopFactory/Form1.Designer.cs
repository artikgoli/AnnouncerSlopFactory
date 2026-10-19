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
            lblDisplayName = new Label();
            txtDisplayName = new TextBox();
            lblWalpurgis = new Label();
            cmbWalpurgis = new ComboBox();
            lblBaseId = new Label();
            numBaseId = new NumericUpDown();
            lblIcon = new Label();
            picIcon = new PictureBox();
            btnSelectIcon = new Button();
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
            ((System.ComponentModel.ISupportInitialize)picIcon).BeginInit();
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
            txtModFolder.PlaceholderText = "e.g. ...\\mods\\ExampleMod";
            txtModFolder.Size = new Size(997, 23);
            txtModFolder.TabIndex = 1;
            // 
            // btnBrowseModFolder
            // 
            btnBrowseModFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBrowseModFolder.Location = new Point(1530, 13);
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
            pnlBottom.Location = new Point(0, 648);
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
            btnExport.Location = new Point(950, 10);
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
            pnlLeft.Size = new Size(380, 600);
            pnlLeft.TabIndex = 2;
            // 
            // grpModInfo
            // 
            grpModInfo.Controls.Add(lblModName);
            grpModInfo.Controls.Add(txtModName);
            grpModInfo.Controls.Add(lblAnnouncerId);
            grpModInfo.Controls.Add(numAnnouncerId);
            grpModInfo.Controls.Add(lblDisplayName);
            grpModInfo.Controls.Add(txtDisplayName);
            grpModInfo.Controls.Add(lblWalpurgis);
            grpModInfo.Controls.Add(cmbWalpurgis);
            grpModInfo.Controls.Add(lblBaseId);
            grpModInfo.Controls.Add(numBaseId);
            grpModInfo.Controls.Add(lblIcon);
            grpModInfo.Controls.Add(picIcon);
            grpModInfo.Controls.Add(btnSelectIcon);
            grpModInfo.Location = new Point(5, 5);
            grpModInfo.Name = "grpModInfo";
            grpModInfo.Size = new Size(360, 570);
            grpModInfo.TabIndex = 0;
            grpModInfo.TabStop = false;
            grpModInfo.Text = "Mod Information";
            // 
            // lblModName
            // 
            lblModName.AutoSize = true;
            lblModName.Location = new Point(15, 22);
            lblModName.Name = "lblModName";
            lblModName.Size = new Size(173, 15);
            lblModName.TabIndex = 0;
            lblModName.Text = "Mod Name (Announcer name):";
            lblModName.Click += lblModName_Click;
            // 
            // txtModName
            // 
            txtModName.Location = new Point(15, 42);
            txtModName.Name = "txtModName";
            txtModName.Size = new Size(320, 23);
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
            // lblDisplayName
            // 
            lblDisplayName.AutoSize = true;
            lblDisplayName.Location = new Point(15, 128);
            lblDisplayName.Name = "lblDisplayName";
            lblDisplayName.Size = new Size(83, 15);
            lblDisplayName.TabIndex = 4;
            lblDisplayName.Text = "Display Name:";
            // 
            // txtDisplayName
            // 
            txtDisplayName.Location = new Point(15, 148);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Size = new Size(320, 23);
            txtDisplayName.TabIndex = 5;
            // 
            // lblWalpurgis
            // 
            lblWalpurgis.AutoSize = true;
            lblWalpurgis.Location = new Point(15, 181);
            lblWalpurgis.Name = "lblWalpurgis";
            lblWalpurgis.Size = new Size(91, 15);
            lblWalpurgis.TabIndex = 6;
            lblWalpurgis.Text = "Walpurgis Type:";
            // 
            // cmbWalpurgis
            // 
            cmbWalpurgis.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbWalpurgis.Items.AddRange(new object[] { "NONE", "LC", "LOR", "LC_QOH", "PILGRIMAGE" });
            cmbWalpurgis.Location = new Point(15, 201);
            cmbWalpurgis.Name = "cmbWalpurgis";
            cmbWalpurgis.Size = new Size(200, 23);
            cmbWalpurgis.TabIndex = 7;
            // 
            // lblBaseId
            // 
            lblBaseId.AutoSize = true;
            lblBaseId.Location = new Point(15, 234);
            lblBaseId.Name = "lblBaseId";
            lblBaseId.Size = new Size(148, 15);
            lblBaseId.TabIndex = 8;
            lblBaseId.Text = "Base ID (0 = none/default):";
            // 
            // numBaseId
            // 
            numBaseId.Location = new Point(15, 254);
            numBaseId.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            numBaseId.Name = "numBaseId";
            numBaseId.Size = new Size(120, 23);
            numBaseId.TabIndex = 9;
            // 
            // lblIcon
            // 
            lblIcon.AutoSize = true;
            lblIcon.Location = new Point(15, 287);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(33, 15);
            lblIcon.TabIndex = 10;
            lblIcon.Text = "Icon:";
            // 
            // picIcon
            // 
            picIcon.BackColor = Color.FromArgb(240, 240, 240);
            picIcon.BorderStyle = BorderStyle.FixedSingle;
            picIcon.Location = new Point(15, 307);
            picIcon.Name = "picIcon";
            picIcon.Size = new Size(100, 100);
            picIcon.SizeMode = PictureBoxSizeMode.Zoom;
            picIcon.TabIndex = 11;
            picIcon.TabStop = false;
            // 
            // btnSelectIcon
            // 
            btnSelectIcon.Location = new Point(125, 307);
            btnSelectIcon.Name = "btnSelectIcon";
            btnSelectIcon.Size = new Size(130, 30);
            btnSelectIcon.TabIndex = 12;
            btnSelectIcon.Text = "Select Icon...";
            btnSelectIcon.Click += BtnSelectIcon_Click;
            // 
            // pnlRight
            // 
            pnlRight.Controls.Add(grpDialogue);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Location = new Point(380, 48);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(5);
            pnlRight.Size = new Size(720, 600);
            pnlRight.TabIndex = 1;
            // 
            // grpDialogue
            // 
            grpDialogue.Controls.Add(dgvEntries);
            grpDialogue.Controls.Add(pnlToolbar);
            grpDialogue.Dock = DockStyle.Fill;
            grpDialogue.Location = new Point(5, 5);
            grpDialogue.Name = "grpDialogue";
            grpDialogue.Size = new Size(710, 590);
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
            dgvEntries.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEntries.Size = new Size(704, 530);
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
            colCategory.Width = 220;
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
            colAudio.Width = 180;
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
            pnlToolbar.Size = new Size(704, 38);
            pnlToolbar.TabIndex = 1;
            // 
            // btnAddDialogue
            // 
            btnAddDialogue.Location = new Point(0, 4);
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
            lblHint.Location = new Point(156, 11);
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
            ClientSize = new Size(1100, 700);
            Controls.Add(pnlRight);
            Controls.Add(pnlLeft);
            Controls.Add(pnlBottom);
            Controls.Add(pnlTop);
            MinimumSize = new Size(900, 600);
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
            ((System.ComponentModel.ISupportInitialize)picIcon).EndInit();
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
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Button btnSelectIcon;
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