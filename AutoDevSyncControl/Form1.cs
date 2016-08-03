using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutoDevSync
{
    public partial class FrmAutoDevSync : Form
    {
        protected const String SRegistryPath = @"Software\AutoDevSync";
        protected const String SWinMergeRegistryPath = @"Software\wow6432node\Thingamahoochie\WinMerge";
        protected const String SWinMergeRegistryValue = "Executable";
        public const String SSynchronisedFilesFile = "FilesSynchronized.txt";

        public const String SRegSource = "source";
        public const String SRegDestination = "destination";
        public const String SRegFilter = "filter";
        public const String SRegExclude = "exclude";
        public const String SRegLogFile = "logfile";
        public const String SRegBackupPath = "7zipOutDir";
        public const String SRegClientRegistry = "registryLeftovers";

        private bool _showBaloon = true;
        private bool _closeCommandPressed = false;
        private bool _shouldClose = false;
        private List<String> _mFilesModified;
        private string mSourceCompareDistPath;
        private string mDestCompareDistPath;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetScrollPos(int hWnd, int nBar);

        [DllImport("user32.dll")]
        static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        //private const int SbHorz = 0x0;
        //private const int SbVert = 0x1;
        public FrmAutoDevSync(string[] args)
        {

            InitializeComponent();
            Process[] remoteByName = Process.GetProcesses();

            foreach (Process item in remoteByName)
            {
                if (
                    (item.ProcessName == Process.GetCurrentProcess().ProcessName) &&
                    (Process.GetCurrentProcess().Id != item.Id)
                    )
                {
                    _shouldClose = true;
                }
            }
            if (args.Length > 0)
            {
                if (args[0].Equals("/w"))
                {
                    _shouldClose = true;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (_shouldClose)
            {
                _closeCommandPressed = true;
                Close();
            }
            else
            {
                //treeView1.CheckBoxes = true;
                RefreshData();
            }
        }

        private void RefreshData()
        {
            int s = GetScrollPos((int)treeView1.Handle, 1);
            txtDestination.ResetBackColor();
            RegistryKey reg = Registry.LocalMachine.OpenSubKey(SRegistryPath, false);
            treeView1.Nodes.Clear();
            if (reg != null)
            {
                try
                {
                    txtSource.Text = reg.GetValue(SRegSource, "").ToString();
                }
                catch (Exception)
                {
                }
                try
                {
                    txtDestination.Text = reg.GetValue(SRegDestination, "").ToString();
                }
                catch (Exception)
                {
                }
                try
                {
                    txtFilters.Text = reg.GetValue(SRegFilter, "").ToString();
                }
                catch (Exception)
                {
                }
                try
                {
                    txtExclude.Text = reg.GetValue(SRegExclude, "").ToString();
                }
                catch (Exception)
                {
                }
                try
                {
                    txtBackup.Text = reg.GetValue(SRegBackupPath, "").ToString();
                }
                catch (Exception)
                {
                }

            }
            OrganizeAfterRezise();
            treeView1.AllowDrop = false;
            if (txtDestination.Text.Length > 0)
            {
                FilesTreeScan(txtDestination.Text);
            }
            treeView1.ExpandAll();

            ServiceRefresh();
            SetScrollPos(treeView1.Handle, 1, s, false);
            if (_mFilesModified != null)
            {
                _mFilesModified.Clear();
                _mFilesModified = null;
            }
            mSourceCompareDistPath = "";
            mDestCompareDistPath = "";
        }

        private void ServiceRefresh()
        {
            sc.Refresh();
            lblerviceStatus.Text = sc.Status.ToString();
            if (sc.Status != ServiceControllerStatus.Running)
            {
                lblerviceStatus.BackColor = Color.OrangeRed;
            }
            else
            {
                lblerviceStatus.BackColor = Color.LightGreen;
            }
        }

        void FilesTreeScan(string source, int depth, TreeNode n)
        {
            DirectoryInfo dir = new DirectoryInfo(source);
            int i = 0;
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                n.Nodes.Add(subDir.Name, subDir.Name).Tag = subDir.FullName;
                FilesTreeScan(subDir.FullName, depth + 1, n.Nodes[i++]);
            }

            foreach (FileInfo file in dir.GetFiles())
            {
                n.Nodes.Add(file.Name, file.Name + " (" + file.LastWriteTime.ToString("dd/MM/yy HH:mm") + ")").Tag = file.FullName;
            }
        }

        void FilesTreeScan(string source)
        {
            DirectoryInfo dir = new DirectoryInfo(source);
            if (!dir.Exists)
            {
                return;
            }
            treeView1.Nodes.Add(source).Tag = source;
            FilesTreeScan(source, 1, treeView1.Nodes[0]);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            RegistryKey reg = Registry.LocalMachine.OpenSubKey(SRegistryPath, true);

            try
            {
                reg.SetValue(SRegSource, txtSource.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                reg.SetValue(SRegDestination, txtDestination.Text);
                if (txtDestination.Text.Length > 0)
                {
                    FilesTreeScan(txtDestination.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                reg.SetValue(SRegFilter, txtFilters.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                reg.SetValue(SRegExclude, txtExclude.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                reg.SetValue(SRegBackupPath, txtBackup.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if ((sc.Status.Equals(ServiceControllerStatus.Stopped)) ||
                 (sc.Status.Equals(ServiceControllerStatus.StopPending)))
            {
                // Start the service if the current status is stopped.

                Console.WriteLine("Starting the Telnet service...");
                sc.Start();
            }
            else
            {
                // Stop the service if its status is not set to "Stopped".

                Console.WriteLine("Stopping the Telnet service...");
                sc.Stop();
                try
                {
                    sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(2));
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(2));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // Refresh and display the current service status.
            sc.Refresh();
            Console.WriteLine("The service status is now set to {0}.", sc.Status);
            RefreshData();
        }

        private void btnStopService_Click(object sender, EventArgs e)
        {
            try
            {
                sc.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Thread.Sleep(3000);
            RefreshData();
        }

        private void btnStartService_Click(object sender, EventArgs e)
        {
            try
            {
                sc.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Thread.Sleep(3000);
            RefreshData();
        }

        private void notity_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = true;
            this.Show();
            RefreshData();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_closeCommandPressed)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                notify.Visible = true;
                if (_showBaloon)
                {
                    _showBaloon = false;
                    notify.ShowBalloonTip(1000, "Still running", "The application is running", ToolTipIcon.Info);
                }
                this.Hide();
            }
        }

        private void startServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                sc.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Thread.Sleep(3000);
            RefreshData();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _closeCommandPressed = true;
            Close();
        }

        private void stopServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                sc.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Thread.Sleep(3000);
            RefreshData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ServiceRefresh();
        }

        private void treeView1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    DeleteFile();
                    break;
                default:
                    break;
            }
        }

        private void DeleteFile()
        {
            if (MessageBox.Show("Are you sure you'd like to delete the file?",
                "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    if (Directory.Exists(treeView1.SelectedNode.Tag.ToString()))
                    {
                        DirectoryInfo s = new DirectoryInfo(treeView1.SelectedNode.Tag.ToString());
                        if ((s.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                        {
                            s.Attributes = s.Attributes & (FileAttributes)(0xff - ((int)FileAttributes.ReadOnly));
                        }
                        Directory.Delete(treeView1.SelectedNode.Tag.ToString(), true);
                    }
                    else if (File.Exists(treeView1.SelectedNode.Tag.ToString()))
                    {
                        FileInfo s = new FileInfo(treeView1.SelectedNode.Tag.ToString());
                        if ((s.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                        {
                            s.Attributes = s.Attributes & (FileAttributes)(0xff - ((int)FileAttributes.ReadOnly));
                        }
                        File.Delete(treeView1.SelectedNode.Tag.ToString());
                    }
                    RefreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnZip_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(BackupData));
            thread.Start();
            MessageBox.Show("Please wait...");
            while (thread.IsAlive)
            {
                Cursor.Current = Cursors.WaitCursor;
                Thread.Sleep(200);
            }
            Cursor.Current = Cursors.Default;

        }

        private void BackupData()
        {
            try
            {
                RegistryKey reg = Registry.LocalMachine.OpenSubKey("Software\\7-zip");
                if (reg != null)
                {
                    String path = reg.GetValue("path", "").ToString();
                    reg = Registry.LocalMachine.OpenSubKey(SRegistryPath);
                    String backupPath = reg.GetValue(SRegBackupPath, "").ToString();
                    if (path.Length == 0)
                    {
                        MessageBox.Show("7-zip file path error");
                        return;
                    }
                    if (backupPath.Length == 0)
                    {
                        MessageBox.Show("No backup path found");
                        return;
                    }

                    if (!backupPath.EndsWith("\\"))
                    {
                        backupPath = backupPath + "\\";
                    }
                    backupPath = backupPath + DateTime.Now.ToShortDateString().Replace('/', '-');
                    path = path + "7z.exe";

                    Process p = new Process
                    {
                        StartInfo =
                        {
                            FileName = path,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            Arguments = String.Format("a \"{0}\" {1}", backupPath,
                                txtDestination.Text)
                        }
                    };
                    p.Start();
                    p.WaitForExit();
                    if (p.ExitCode != 0)
                    {
                        MessageBox.Show(String.Format("Couldn't backup directry. exitcode {0}", p.ExitCode));
                    }
                    else
                    {
                        MessageBox.Show("Work backed-up");
                    }
                }
                else
                {
                    MessageBox.Show("Please install 7-zip", "7-Zip", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CompareWithWinMerge(string sourcePath, string destPath)
        {

            try
            {
                RegistryKey reg = Registry.LocalMachine.OpenSubKey(SWinMergeRegistryPath);
                if (reg != null)
                {
                    String path = reg.GetValue(SWinMergeRegistryValue, "").ToString();

                    Process p = new Process
                    {
                        StartInfo =
                        {
                            FileName = path,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            Arguments = String.Format("/r /maximize /e /f \"{0}\" \"{1}\" \"{2}\"",
                                txtFilters.Text, sourcePath, destPath)
                        }
                    };
                    p.Start();
                    p.WaitForExit();
                    if (p.ExitCode != 0)
                    {
                        MessageBox.Show(String.Format("Couldn't open compare software. exitcode {0}", p.ExitCode));
                    }
                }
                else
                {
                    MessageBox.Show("Please install WinMerge", "WinMerge", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OrganizeAfterRezise()
        {
            int diff = txtDestination.Top - txtSource.Bottom;

            btnDepends.Top = btnLogFile.Top = btnRegistryFinder.Top = btnZip.Top = btnUpdate.Top = btnRefresh.Top = Height - 50 - btnRefresh.Height - diff;
            btnOpenReformatted.Top = btnChooseLog.Top = lblLogFile.Top = txtLogFile.Top = btnUpdate.Top - btnChooseLog.Height - diff;
            label3.Top = dateTimePicker2.Top = btnCopyFilesModified.Top = btnChooseLog.Top - dateTimePicker2.Height - diff;
            chkReadOnly.Top = label2.Top = dateTimePicker1.Top = btnListModifiedFiles.Top = btnCopyFilesModified.Top - dateTimePicker1.Height - diff;
            btnOpenBackup.Top = lblBackup.Top = txtBackup.Top = dateTimePicker1.Top - txtBackup.Height - diff;
            lblExclude.Top = txtExclude.Top = txtBackup.Top - txtExclude.Height - diff;
            lblFilters.Top = txtFilters.Top = txtExclude.Top - txtFilters.Height - diff;
            btnOpenDest.Top = lblDest.Top = txtDestination.Top = txtFilters.Top - txtDestination.Height - diff;
            btnOpenSrc.Top = lblSource.Top = txtSource.Top = txtDestination.Top - txtSource.Height - diff;

            treeView1.Height = txtSource.Top - treeView1.Top - diff;

        }

        private void openSourceMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo procInfo = new ProcessStartInfo();
            procInfo.FileName = "explorer";
            procInfo.Arguments = txtSource.Text;
            Process.Start(procInfo);
        }

        private void openDestMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo procInfo = new ProcessStartInfo();
            procInfo.FileName = "explorer";
            procInfo.Arguments = txtDestination.Text;
            Process.Start(procInfo);
        }

        private void btnSourceOpen_Click(object sender, EventArgs e)
        {
            openSourceMenuItem_Click(sender, e);
        }

        private void btnOpenDest_Click(object sender, EventArgs e)
        {
            openDestMenuItem_Click(sender, e);
        }

        private bool GetDateFormat(string str, out DateTime date)
        {
            return (DateTime.TryParseExact(str, "d/M/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out date) ||
                    DateTime.TryParseExact(str, "d/MM/yy HH:mm", null, System.Globalization.DateTimeStyles.None, out date) ||
                    DateTime.TryParseExact(str, "dd/MM/yy HH:mm", null, System.Globalization.DateTimeStyles.None, out date) ||
                    DateTime.TryParseExact(str, "d/M/yy HH:mm", null, System.Globalization.DateTimeStyles.None, out date) ||
                    DateTime.TryParseExact(str, "dd/MM/yy HH:mm", null, System.Globalization.DateTimeStyles.None, out date));
        }

        private void btnCopyFilesModified_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you'd like to copy the files modified between to dates " +
                    dateTimePicker1.Text + " and " + dateTimePicker2.Text + " to " + txtDestination.Text + "?",
                        "Copy modified files", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    GetFilesModifiedThreaded();

                    foreach (String item in _mFilesModified)
                    {
                        string temp = txtDestination.Text;
                        string src = txtSource.Text;
                        if (!temp.EndsWith("\\"))
                        {
                            temp = temp + "\\";
                        }
                        if (!src.EndsWith("\\"))
                        {
                            src = src + "\\";
                        }

                        temp = temp + item.Substring(src.Length);
                        FileInfo file = new FileInfo(item);
                        try
                        {
                            Directory.CreateDirectory(temp.Substring(0, temp.LastIndexOf('\\')));
                        }
                        catch (Exception)
                        {

                        }
                        File.Copy(file.FullName, temp, true);
                    }
                    RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteFile();
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            string src = "", dest = "", origDest = "", origSource = "";
            bool isModifiedFilesView = false;
            int srcIndx = 0;
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("You have to select a node first");
                return;
            }
            if (txtSource.Text.Length * txtDestination.Text.Length == 0)
            {
                MessageBox.Show("No source or destination directory given");
                return;
            }
            isModifiedFilesView = !IsViewListOriginalDestination();
            //if ((mSourceCompareDistPath.Length == 0) &&
            //    (mDestCompareDistPath.Length == 0))
            //{
            //    RegistryKey reg = Registry.LocalMachine.OpenSubKey(SRegistryPath, false);
            //    if (reg != null)
            //    {
            //        try
            //        {
            //            origSource = reg.GetValue(SRegSource, "").ToString();
            //        }
            //        catch (Exception)
            //        {
            //        }
            //        try
            //        {
            //            origDest = reg.GetValue(SRegDestination, "").ToString();
            //        }
            //        catch (Exception)
            //        {
            //        }
            //        reg.Close();
            //    }
            //}
            //else
            //{

            //}
            if (isModifiedFilesView)
            {
                origSource = mSourceCompareDistPath;
            }
            else
            {
                origSource = txtSource.Text;
            }
            origDest = txtDestination.Text;

            if (!origSource.EndsWith("\\"))
            {
                src = origSource + "\\";
            }
            else
            {
                src = origSource;
            }
            if (!origDest.EndsWith("\\"))
            {
                dest = origDest + "\\";
            }
            else
            {
                dest = origDest;
            }

            if (!isModifiedFilesView)
            {
                srcIndx = origDest.Length;
                dest = treeView1.SelectedNode.Tag.ToString();
                src = src + dest.Substring(srcIndx);
            }
            else
            {
                srcIndx = origSource.Length;
                src = treeView1.SelectedNode.Tag.ToString();
                dest = dest + src.Substring(srcIndx);
            }

            CompareWithWinMerge(src, dest);
        }

        private bool IsViewListOriginalDestination()
        {
            RegistryKey reg = Registry.LocalMachine.OpenSubKey(SRegistryPath, false);
            string origDest = "";
            if (reg != null)
            {
                try
                {
                    origDest = reg.GetValue(SRegDestination, "").ToString();
                }
                catch (Exception)
                {
                }
                reg.Close();
            }
            if (treeView1.Nodes.Count > 0)
            {
                return treeView1.Nodes[0].Text.StartsWith(origDest);
            }
            return false;
        }

        private void GetFilesModifiedThreaded()
        {
            DateTime from = new DateTime();
            DateTime to = new DateTime();
            _mFilesModified = new List<string>();
            if (
                GetDateFormat(dateTimePicker1.Text, out from) &&
                GetDateFormat(dateTimePicker2.Text, out to)
                )
            {
                to = to.AddDays(1);
                to = to.AddMilliseconds(-1);
                if ((to.CompareTo(from) >= 0))
                {
                    DsFileManager dsFm = new DsFileManager(txtSource.Text, txtDestination.Text);
                    dsFm.ScanFrom = from;
                    dsFm.ScanTo = to;
                    dsFm.Filters = txtFilters.Text;
                    dsFm.Excludes = txtExclude.Text;
                    dsFm.ScanReadOnly = chkReadOnly.Checked;
                    Thread thread = new Thread(dsFm.listModifiedFiles);
                    thread.Start();

                    while (thread.IsAlive)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Thread.Sleep(200);
                    }
                    Cursor.Current = Cursors.Default;
                    _mFilesModified = dsFm.FilesModified;
                    if (_mFilesModified.Count > 100)
                    {
                        DateTime earliestTime = DateTime.Now;
                        long time = 0;
                        int filesCount = 0;

                        foreach (string file in _mFilesModified)
                        {
                            FileInfo info = new FileInfo(file);

                            time += info.LastWriteTime.Ticks / _mFilesModified.Count;
                            if (earliestTime.CompareTo(info.LastWriteTime) > 0)
                            {
                                earliestTime = info.LastWriteTime;
                            }
                            filesCount++;

                        }
                        DateTime avgDateTime = new DateTime().AddTicks(time);
                        if (MessageBox.Show(
                            String.Format("There are many changes detected ({0}). " +
                                          "The earliest time of the files is {1} ", _mFilesModified.Count,
                                          earliestTime.ToShortDateString()) + avgDateTime.ToShortDateString(), "",
                                          MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
                        {
                            _mFilesModified.Clear();
                        }
                    }
                }
            }
        }

        private void btnListModifiedFiles_Click(object sender, EventArgs e)
        {
            mSourceCompareDistPath = txtSource.Text;
            mDestCompareDistPath = txtDestination.Text;
            GetFilesModifiedThreaded();
            if (_mFilesModified.Count > 0)
            {
                ViewChangedFiles();
            }
            else if ((_mFilesModified.Count == 0) && (!chkReadOnly.Checked))
            {
                if (MessageBox.Show("No modified files found. Read only files were not included in the search." +
                    " Whould you like to search for read only files as well?", "No modified files", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    chkReadOnly.Checked = true;
                    chkReadOnly.Refresh();
                    GetFilesModifiedThreaded();
                    ViewChangedFiles();
                }
            }
        }

        private void ViewChangedFiles()
        {
            treeView1.Nodes.Clear();
            foreach (String item in _mFilesModified)
            {
                FileInfo info = new FileInfo(item);
                treeView1.Nodes.Add(item + " (" + info.LastWriteTime.ToString("dd/MM/yy HH:mm") + ")").Tag = item;
            }
            toolTip1.Show("This text field is used for compare destination.", txtDestination, new Point(0, txtDestination.Height), 6 * 1000);
            txtDestination.BackColor = Color.LimeGreen;
            txtDestination.Refresh();

        }

        private void frmAutoDevSync_Resize(object sender, EventArgs e)
        {
            OrganizeAfterRezise();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;
            }
        }

        private void stripMenuCompare_Click(object sender, EventArgs e)
        {
            btnCompare_Click(sender, e);
        }

        private void stripMenuDelete_Click(object sender, EventArgs e)
        {
            DeleteFile();
        }

        private void btnRegistryFinder_Click(object sender, EventArgs e)
        {
            String str = "arnativ;arxml;sapicrypt;arosig;arinfo;MyRSAPKCS1SHA512SignatureDescription;pdfcreator;armgrsig;XpdfViewerCtrl";
            try
            {
                str = Registry.LocalMachine.OpenSubKey(SRegistryPath)
                    .GetValue(SRegClientRegistry,
                        str).ToString();
            }
            catch (Exception)
            {
                try
                {
                    Registry.LocalMachine.OpenSubKey(SRegistryPath, true).SetValue(SRegClientRegistry, str);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            DsFileManager dsFm = new DsFileManager(txtSource.Text, txtDestination.Text)
            {
                Filters = str
            };
            if (txtBackup.Text.Length > 0)
            {
                dsFm.RegistryFilePath = txtBackup.Text.EndsWith("\\") ? txtBackup.Text : txtBackup.Text + "\\";
                dsFm.RegistryFilePath = dsFm.RegistryFilePath + "ScanClientRegistry.txt";
            }
            else
            {
                MessageBox.Show("Please set the output path in the backup path directory. Output file name will be 'ScanClientRegistry.txt'");
                return;
            }

            Thread thread = new Thread(dsFm.ScanClientRegistry);
            thread.Start();

            while (thread.IsAlive)
            {
                Cursor.Current = Cursors.WaitCursor;
                Thread.Sleep(200);
            }
            Cursor.Current = Cursors.Default;
            DialogResult result = MessageBox.Show("Registry scan done. Output in " + dsFm.RegistryFilePath + ". Do you like to open it now?",
                                                "Registry leftovers",
                                                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Process.Start(dsFm.RegistryFilePath);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (sc.Status == ServiceControllerStatus.Running)
            {
                startServiceToolStripMenuItem.Enabled = false;
                stopServiceToolStripMenuItem.Enabled = true;
            }
            else if (sc.Status == ServiceControllerStatus.Stopped)
            {
                startServiceToolStripMenuItem.Enabled = true;
                stopServiceToolStripMenuItem.Enabled = false;
            }
            else
            {
                startServiceToolStripMenuItem.Enabled = false;
                stopServiceToolStripMenuItem.Enabled = false;
            }
        }

        private void btnLogFile_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtLogFile.Text))
            {
                StreamReader reader = new StreamReader(new FileStream(txtLogFile.Text, FileMode.Open));

                try
                {
                    using (StreamWriter writer = new StreamWriter(new FileStream(txtLogFile.Text + ".reformated.log", FileMode.Create)))
                    {
                        String line = reader.ReadLine();
                        while (!String.IsNullOrEmpty(line))
                        {
                            int ind = line.LastIndexOf('|') + 1;
                            if (ind > 0)
                            {
                                string newline = line.Substring(ind);
                                writer.WriteLine(newline);
                            }
                            line = reader.ReadLine();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                reader.Close();
            }
        }

        private void btnChooseLog_Click(object sender, EventArgs e)
        {
            if (dlgLogFile.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo dir = new FileInfo(dlgLogFile.FileName).Directory;
                if (dir != null)
                {
                    dlgLogFile.InitialDirectory = dir.FullName;
                }

                txtLogFile.Text = dlgLogFile.FileName;
            }
        }

        private void btnOpenReformatted_Click(object sender, EventArgs e)
        {
            string log = txtLogFile.Text + ".reformated.log";
            if (File.Exists(log))
                Process.Start(log);
        }

        private void btnOpenBackup_Click(object sender, EventArgs e)
        {
            ProcessStartInfo procInfo = new ProcessStartInfo();
            procInfo.FileName = "explorer";
            procInfo.Arguments = txtBackup.Text;
            Process.Start(procInfo);
        }

        private void btnDepends_Click(object sender, EventArgs e)
        {
            var dialog = new DependencyViewer();
            dialog.InitialDirectory = txtSource.Text;
            dialog.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(openFileDialog1.FileName);
                String dataString = "";
                int i = 1;
                foreach (byte item in data)
                {
                    dataString = dataString + "0x" + item.ToString("X2") + ", ";
                    if (i % 8 == 0)
                    {
                        dataString = dataString + "\n";
                        i = 0;
                    }
                    i++;
                }
                MessageBox.Show(dataString);
            }
        }
    }
}
