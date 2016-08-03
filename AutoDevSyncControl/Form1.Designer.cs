namespace AutoDevSync
{
    partial class FrmAutoDevSync
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAutoDevSync));
            this.notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSourceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDestMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuTreeViewer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stripMenuCompare = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.txtFilters = new System.Windows.Forms.TextBox();
            this.txtExclude = new System.Windows.Forms.TextBox();
            this.lblSource = new System.Windows.Forms.Label();
            this.lblDest = new System.Windows.Forms.Label();
            this.lblFilters = new System.Windows.Forms.Label();
            this.lblExclude = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblerviceStatus = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.sc = new System.ServiceProcess.ServiceController();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnZip = new System.Windows.Forms.Button();
            this.txtBackup = new System.Windows.Forms.TextBox();
            this.lblBackup = new System.Windows.Forms.Label();
            this.btnOpenSrc = new System.Windows.Forms.Button();
            this.btnOpenDest = new System.Windows.Forms.Button();
            this.btnCopyFilesModified = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.btnddelete = new System.Windows.Forms.Button();
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnListModifiedFiles = new System.Windows.Forms.Button();
            this.btnRegistryFinder = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblLogFile = new System.Windows.Forms.Label();
            this.txtLogFile = new System.Windows.Forms.TextBox();
            this.btnLogFile = new System.Windows.Forms.Button();
            this.btnChooseLog = new System.Windows.Forms.Button();
            this.dlgLogFile = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenBackup = new System.Windows.Forms.Button();
            this.btnOpenReformatted = new System.Windows.Forms.Button();
            this.chkReadOnly = new System.Windows.Forms.CheckBox();
            this.btnDepends = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1.SuspendLayout();
            this.menuTreeViewer.SuspendLayout();
            this.SuspendLayout();
            // 
            // notify
            // 
            this.notify.ContextMenuStrip = this.contextMenuStrip1;
            this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
            this.notify.Text = "AutoDevSync";
            this.notify.Visible = true;
            this.notify.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notity_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startServiceToolStripMenuItem,
            this.stopServiceToolStripMenuItem,
            this.openSourceMenuItem,
            this.openDestMenuItem,
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(217, 114);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // startServiceToolStripMenuItem
            // 
            this.startServiceToolStripMenuItem.Name = "startServiceToolStripMenuItem";
            this.startServiceToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.startServiceToolStripMenuItem.Text = "&Start service";
            this.startServiceToolStripMenuItem.Click += new System.EventHandler(this.startServiceToolStripMenuItem_Click);
            // 
            // stopServiceToolStripMenuItem
            // 
            this.stopServiceToolStripMenuItem.Name = "stopServiceToolStripMenuItem";
            this.stopServiceToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.stopServiceToolStripMenuItem.Text = "S&top service";
            this.stopServiceToolStripMenuItem.Click += new System.EventHandler(this.stopServiceToolStripMenuItem_Click);
            // 
            // openSourceMenuItem
            // 
            this.openSourceMenuItem.Name = "openSourceMenuItem";
            this.openSourceMenuItem.Size = new System.Drawing.Size(216, 22);
            this.openSourceMenuItem.Text = "&Open source directory";
            this.openSourceMenuItem.Click += new System.EventHandler(this.openSourceMenuItem_Click);
            // 
            // openDestMenuItem
            // 
            this.openDestMenuItem.Name = "openDestMenuItem";
            this.openDestMenuItem.Size = new System.Drawing.Size(216, 22);
            this.openDestMenuItem.Text = "Open &destination Directory";
            this.openDestMenuItem.Click += new System.EventHandler(this.openDestMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(216, 22);
            this.toolStripMenuItem1.Text = "&Close";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.menuTreeViewer;
            this.treeView1.Location = new System.Drawing.Point(12, 40);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(657, 303);
            this.treeView1.TabIndex = 20;
            this.treeView1.TabStop = false;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyUp);
            // 
            // menuTreeViewer
            // 
            this.menuTreeViewer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripMenuCompare,
            this.stripMenuDelete});
            this.menuTreeViewer.Name = "menuTreeViewer";
            this.menuTreeViewer.Size = new System.Drawing.Size(124, 48);
            // 
            // stripMenuCompare
            // 
            this.stripMenuCompare.Name = "stripMenuCompare";
            this.stripMenuCompare.Size = new System.Drawing.Size(123, 22);
            this.stripMenuCompare.Text = "&Compare";
            this.stripMenuCompare.Click += new System.EventHandler(this.stripMenuCompare_Click);
            // 
            // stripMenuDelete
            // 
            this.stripMenuDelete.Name = "stripMenuDelete";
            this.stripMenuDelete.Size = new System.Drawing.Size(123, 22);
            this.stripMenuDelete.Text = "&Delete";
            this.stripMenuDelete.Click += new System.EventHandler(this.stripMenuDelete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(194, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Files synchronized";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(26, 557);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh list";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(86, 349);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(583, 20);
            this.txtSource.TabIndex = 0;
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(86, 375);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(583, 20);
            this.txtDestination.TabIndex = 1;
            // 
            // txtFilters
            // 
            this.txtFilters.Location = new System.Drawing.Point(86, 401);
            this.txtFilters.Name = "txtFilters";
            this.txtFilters.Size = new System.Drawing.Size(583, 20);
            this.txtFilters.TabIndex = 2;
            // 
            // txtExclude
            // 
            this.txtExclude.Location = new System.Drawing.Point(86, 427);
            this.txtExclude.Name = "txtExclude";
            this.txtExclude.Size = new System.Drawing.Size(583, 20);
            this.txtExclude.TabIndex = 3;
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(9, 349);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(41, 13);
            this.lblSource.TabIndex = 4;
            this.lblSource.Text = "Source";
            // 
            // lblDest
            // 
            this.lblDest.AutoSize = true;
            this.lblDest.Location = new System.Drawing.Point(9, 375);
            this.lblDest.Name = "lblDest";
            this.lblDest.Size = new System.Drawing.Size(60, 13);
            this.lblDest.TabIndex = 4;
            this.lblDest.Text = "Destination";
            // 
            // lblFilters
            // 
            this.lblFilters.AutoSize = true;
            this.lblFilters.Location = new System.Drawing.Point(9, 401);
            this.lblFilters.Name = "lblFilters";
            this.lblFilters.Size = new System.Drawing.Size(66, 13);
            this.lblFilters.TabIndex = 4;
            this.lblFilters.Text = "Files to copy";
            // 
            // lblExclude
            // 
            this.lblExclude.AutoSize = true;
            this.lblExclude.Location = new System.Drawing.Point(9, 427);
            this.lblExclude.Name = "lblExclude";
            this.lblExclude.Size = new System.Drawing.Size(45, 13);
            this.lblExclude.TabIndex = 4;
            this.lblExclude.Text = "Exclude";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(107, 557);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(102, 23);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Update service";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(715, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Service status";
            // 
            // lblerviceStatus
            // 
            this.lblerviceStatus.AutoSize = true;
            this.lblerviceStatus.BackColor = System.Drawing.Color.Lime;
            this.lblerviceStatus.Location = new System.Drawing.Point(739, 63);
            this.lblerviceStatus.Name = "lblerviceStatus";
            this.lblerviceStatus.Size = new System.Drawing.Size(21, 13);
            this.lblerviceStatus.TabIndex = 7;
            this.lblerviceStatus.Text = "On";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(685, 98);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Stop Service";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnStopService_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(776, 98);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "Start service";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnStartService_Click);
            // 
            // sc
            // 
            this.sc.ServiceName = "AutoDevSync";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnZip
            // 
            this.btnZip.Location = new System.Drawing.Point(215, 557);
            this.btnZip.Name = "btnZip";
            this.btnZip.Size = new System.Drawing.Size(75, 23);
            this.btnZip.TabIndex = 21;
            this.btnZip.Text = "Zip data";
            this.btnZip.UseVisualStyleBackColor = true;
            this.btnZip.Click += new System.EventHandler(this.btnZip_Click);
            // 
            // txtBackup
            // 
            this.txtBackup.Location = new System.Drawing.Point(86, 453);
            this.txtBackup.Name = "txtBackup";
            this.txtBackup.Size = new System.Drawing.Size(583, 20);
            this.txtBackup.TabIndex = 3;
            // 
            // lblBackup
            // 
            this.lblBackup.AutoSize = true;
            this.lblBackup.Location = new System.Drawing.Point(9, 453);
            this.lblBackup.Name = "lblBackup";
            this.lblBackup.Size = new System.Drawing.Size(68, 13);
            this.lblBackup.TabIndex = 4;
            this.lblBackup.Text = "Backup path";
            // 
            // btnOpenSrc
            // 
            this.btnOpenSrc.Location = new System.Drawing.Point(675, 348);
            this.btnOpenSrc.Name = "btnOpenSrc";
            this.btnOpenSrc.Size = new System.Drawing.Size(75, 23);
            this.btnOpenSrc.TabIndex = 22;
            this.btnOpenSrc.Text = "Open";
            this.btnOpenSrc.UseVisualStyleBackColor = true;
            this.btnOpenSrc.Click += new System.EventHandler(this.btnSourceOpen_Click);
            // 
            // btnOpenDest
            // 
            this.btnOpenDest.Location = new System.Drawing.Point(675, 374);
            this.btnOpenDest.Name = "btnOpenDest";
            this.btnOpenDest.Size = new System.Drawing.Size(75, 23);
            this.btnOpenDest.TabIndex = 22;
            this.btnOpenDest.Text = "Open";
            this.btnOpenDest.UseVisualStyleBackColor = true;
            this.btnOpenDest.Click += new System.EventHandler(this.btnOpenDest_Click);
            // 
            // btnCopyFilesModified
            // 
            this.btnCopyFilesModified.Location = new System.Drawing.Point(346, 505);
            this.btnCopyFilesModified.Name = "btnCopyFilesModified";
            this.btnCopyFilesModified.Size = new System.Drawing.Size(126, 23);
            this.btnCopyFilesModified.TabIndex = 23;
            this.btnCopyFilesModified.Text = "Copy modified files";
            this.btnCopyFilesModified.UseVisualStyleBackColor = true;
            this.btnCopyFilesModified.Click += new System.EventHandler(this.btnCopyFilesModified_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 479);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Modified from";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 505);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Modified to";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(86, 479);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(231, 20);
            this.dateTimePicker1.TabIndex = 24;
            this.dateTimePicker1.Value = new System.DateTime(2016, 4, 4, 0, 0, 0, 0);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dateTimePicker2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(86, 505);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(231, 20);
            this.dateTimePicker2.TabIndex = 24;
            // 
            // btnddelete
            // 
            this.btnddelete.Location = new System.Drawing.Point(685, 150);
            this.btnddelete.Name = "btnddelete";
            this.btnddelete.Size = new System.Drawing.Size(85, 42);
            this.btnddelete.TabIndex = 6;
            this.btnddelete.Text = "Delete selected";
            this.btnddelete.UseVisualStyleBackColor = true;
            this.btnddelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(685, 216);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(85, 42);
            this.btnCompare.TabIndex = 25;
            this.btnCompare.Text = "Compare file or folder";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // btnListModifiedFiles
            // 
            this.btnListModifiedFiles.Location = new System.Drawing.Point(346, 479);
            this.btnListModifiedFiles.Name = "btnListModifiedFiles";
            this.btnListModifiedFiles.Size = new System.Drawing.Size(126, 23);
            this.btnListModifiedFiles.TabIndex = 26;
            this.btnListModifiedFiles.Text = "View modified files";
            this.btnListModifiedFiles.UseVisualStyleBackColor = true;
            this.btnListModifiedFiles.Click += new System.EventHandler(this.btnListModifiedFiles_Click);
            // 
            // btnRegistryFinder
            // 
            this.btnRegistryFinder.Location = new System.Drawing.Point(297, 557);
            this.btnRegistryFinder.Name = "btnRegistryFinder";
            this.btnRegistryFinder.Size = new System.Drawing.Size(114, 23);
            this.btnRegistryFinder.TabIndex = 27;
            this.btnRegistryFinder.Text = "Scan client registry ";
            this.btnRegistryFinder.UseVisualStyleBackColor = true;
            this.btnRegistryFinder.Click += new System.EventHandler(this.btnRegistryFinder_Click);
            // 
            // lblLogFile
            // 
            this.lblLogFile.AutoSize = true;
            this.lblLogFile.Location = new System.Drawing.Point(9, 531);
            this.lblLogFile.Name = "lblLogFile";
            this.lblLogFile.Size = new System.Drawing.Size(49, 13);
            this.lblLogFile.TabIndex = 29;
            this.lblLogFile.Text = "Log path";
            // 
            // txtLogFile
            // 
            this.txtLogFile.Location = new System.Drawing.Point(86, 531);
            this.txtLogFile.Name = "txtLogFile";
            this.txtLogFile.Size = new System.Drawing.Size(583, 20);
            this.txtLogFile.TabIndex = 28;
            // 
            // btnLogFile
            // 
            this.btnLogFile.Location = new System.Drawing.Point(417, 557);
            this.btnLogFile.Name = "btnLogFile";
            this.btnLogFile.Size = new System.Drawing.Size(75, 23);
            this.btnLogFile.TabIndex = 30;
            this.btnLogFile.Text = "Clean log file";
            this.btnLogFile.UseVisualStyleBackColor = true;
            this.btnLogFile.Click += new System.EventHandler(this.btnLogFile_Click);
            // 
            // btnChooseLog
            // 
            this.btnChooseLog.Location = new System.Drawing.Point(675, 527);
            this.btnChooseLog.Name = "btnChooseLog";
            this.btnChooseLog.Size = new System.Drawing.Size(75, 23);
            this.btnChooseLog.TabIndex = 31;
            this.btnChooseLog.Text = "Choose log";
            this.btnChooseLog.UseVisualStyleBackColor = true;
            this.btnChooseLog.Click += new System.EventHandler(this.btnChooseLog_Click);
            // 
            // dlgLogFile
            // 
            this.dlgLogFile.FileName = "client.log";
            this.dlgLogFile.Filter = "Text files|*.txt;*.log|All files|*.*";
            // 
            // btnOpenBackup
            // 
            this.btnOpenBackup.Location = new System.Drawing.Point(675, 452);
            this.btnOpenBackup.Name = "btnOpenBackup";
            this.btnOpenBackup.Size = new System.Drawing.Size(75, 23);
            this.btnOpenBackup.TabIndex = 22;
            this.btnOpenBackup.Text = "Open";
            this.btnOpenBackup.UseVisualStyleBackColor = true;
            this.btnOpenBackup.Click += new System.EventHandler(this.btnOpenBackup_Click);
            // 
            // btnOpenReformatted
            // 
            this.btnOpenReformatted.Location = new System.Drawing.Point(756, 527);
            this.btnOpenReformatted.Name = "btnOpenReformatted";
            this.btnOpenReformatted.Size = new System.Drawing.Size(95, 23);
            this.btnOpenReformatted.TabIndex = 22;
            this.btnOpenReformatted.Text = "Open new log";
            this.btnOpenReformatted.UseVisualStyleBackColor = true;
            this.btnOpenReformatted.Click += new System.EventHandler(this.btnOpenReformatted_Click);
            // 
            // chkReadOnly
            // 
            this.chkReadOnly.AutoSize = true;
            this.chkReadOnly.Location = new System.Drawing.Point(478, 482);
            this.chkReadOnly.Name = "chkReadOnly";
            this.chkReadOnly.Size = new System.Drawing.Size(133, 17);
            this.chkReadOnly.TabIndex = 32;
            this.chkReadOnly.Text = "Scan for read-only files";
            this.chkReadOnly.UseVisualStyleBackColor = true;
            // 
            // btnDepends
            // 
            this.btnDepends.Location = new System.Drawing.Point(514, 557);
            this.btnDepends.Name = "btnDepends";
            this.btnDepends.Size = new System.Drawing.Size(97, 23);
            this.btnDepends.TabIndex = 33;
            this.btnDepends.Text = "Dependency";
            this.btnDepends.UseVisualStyleBackColor = true;
            this.btnDepends.Click += new System.EventHandler(this.btnDepends_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(728, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 34;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FrmAutoDevSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 591);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDepends);
            this.Controls.Add(this.chkReadOnly);
            this.Controls.Add(this.btnChooseLog);
            this.Controls.Add(this.btnLogFile);
            this.Controls.Add(this.lblLogFile);
            this.Controls.Add(this.txtLogFile);
            this.Controls.Add(this.btnRegistryFinder);
            this.Controls.Add(this.btnListModifiedFiles);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btnCopyFilesModified);
            this.Controls.Add(this.btnOpenReformatted);
            this.Controls.Add(this.btnOpenBackup);
            this.Controls.Add(this.btnOpenDest);
            this.Controls.Add(this.btnOpenSrc);
            this.Controls.Add(this.btnZip);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnddelete);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lblerviceStatus);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lblBackup);
            this.Controls.Add(this.lblExclude);
            this.Controls.Add(this.lblFilters);
            this.Controls.Add(this.lblDest);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.txtBackup);
            this.Controls.Add(this.txtExclude);
            this.Controls.Add(this.txtFilters);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeView1);
            this.MinimumSize = new System.Drawing.Size(891, 630);
            this.Name = "FrmAutoDevSync";
            this.Text = "Developer synchronizer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.frmAutoDevSync_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuTreeViewer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notify;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.TextBox txtFilters;
        private System.Windows.Forms.TextBox txtExclude;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.Label lblDest;
        private System.Windows.Forms.Label lblFilters;
        private System.Windows.Forms.Label lblExclude;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblerviceStatus;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.ServiceProcess.ServiceController sc;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem startServiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopServiceToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnZip;
        private System.Windows.Forms.TextBox txtBackup;
        private System.Windows.Forms.Label lblBackup;
        private System.Windows.Forms.ToolStripMenuItem openSourceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDestMenuItem;
        private System.Windows.Forms.Button btnOpenSrc;
        private System.Windows.Forms.Button btnOpenDest;
        private System.Windows.Forms.Button btnCopyFilesModified;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button btnddelete;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Button btnListModifiedFiles;
        private System.Windows.Forms.ContextMenuStrip menuTreeViewer;
        private System.Windows.Forms.ToolStripMenuItem stripMenuCompare;
        private System.Windows.Forms.ToolStripMenuItem stripMenuDelete;
        private System.Windows.Forms.Button btnRegistryFinder;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblLogFile;
        private System.Windows.Forms.TextBox txtLogFile;
        private System.Windows.Forms.Button btnLogFile;
        private System.Windows.Forms.Button btnChooseLog;
        private System.Windows.Forms.OpenFileDialog dlgLogFile;
        private System.Windows.Forms.Button btnOpenBackup;
        private System.Windows.Forms.Button btnOpenReformatted;
        private System.Windows.Forms.CheckBox chkReadOnly;
        private System.Windows.Forms.Button btnDepends;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

