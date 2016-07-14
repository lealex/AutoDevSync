using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Threading;

namespace AutoDevSync
{
    public partial class AutoDevSync : ServiceBase
    {
        public  const String sSynchronisedFilesFile = "FilesSynchronized.txt";

        public const String sRegSource = "source";
        public const String sRegDestination = "destination";
        public const String sRegFilter = "filter";
        public const String sRegExclude = "exclude";
        public const String sRegLogFile = "logfile";

        private FileSystemWatcher watcher;
        private static Object lockLogFile = new Object();
        private static Object lockFileSync = new Object();
        private static Object syncLocker = new Object();
        private static string mSource;
        private static string mDestination;
        private static string mFilter;
        private static bool stopped;
        private static string mExclude;
        private static string mLogFile;
        private static LinkedList<String> mSyncedFiles;


        public AutoDevSync()
        {
            InitializeComponent();
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            bool moveFile = false;
            foreach (string item in mExclude.Split(';'))
            {
                if (e.FullPath.ToLower().Contains(item))
                {
                    return;
                }
            }
            foreach (string item in mFilter.Split(';'))
            {
                if (e.FullPath.ToLower().EndsWith(item))
                {
                    moveFile = true;
                    break;
                }
            }
            lock (syncLocker)
            {
                try
                {
                    if (moveFile &&
                           !stopped &&
                           mDestination.Length > 0 &&
                           mSource.Length > 0)
                    {
                        string newpath = e.FullPath.Substring(e.FullPath.IndexOf(mSource) + mSource.Length);
                        if (!mDestination.EndsWith("\\") &&
                            !newpath.StartsWith("\\"))
                        {

                            newpath = "\\" + newpath;
                        }

                        string destpath = mDestination + newpath;

                        if (File.Exists(destpath))
                        {
                            WriteToLog("Deleting old file " + mSource);
                            File.Delete(destpath);
                        }
                        try
                        {
                            string path = destpath.Substring(0, destpath.LastIndexOf('\\'));
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                                WriteToLog("Directory path created " + path);
                            }
                        }
                        catch (Exception ex)
                        {
                            WriteToLog("EXCEPTION: " + ex.Message + ";  " + ex.Data + ";  " + ex.StackTrace);
                        }
                        try
                        {
                            File.Copy(e.FullPath, destpath);
                            WriteToLog(e.FullPath + " copied to " + destpath);
                            mSyncedFiles.AddLast(newpath);
                        }
                        catch (Exception ex)
                        {
                            WriteToLog("EXCEPTION: " + ex.Message + ";  " + ex.Data + ";  " + ex.StackTrace);
                        }
                    }
                }
                catch (Exception ex)
                {
                    WriteToLog("EXCEPTION: " + ex.Message + ";  " + ex.Data + ";  " + ex.StackTrace);
                }
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                RegistryKey sub = Registry.LocalMachine.OpenSubKey("Software\\AutoDevSync", false);
                
                mSource = sub.GetValue(sRegSource, "").ToString();
                mDestination = sub.GetValue(sRegDestination, "").ToString();
                mExclude = sub.GetValue(sRegExclude, "").ToString();
                mFilter = sub.GetValue(sRegFilter, "").ToString();
                mLogFile = sub.GetValue(sRegLogFile, "").ToString();

                mSyncedFiles = new LinkedList<string>();

                if (String.IsNullOrWhiteSpace(mSource) ||
                    String.IsNullOrWhiteSpace(mDestination))
                {
                    throw new IOException("OnStart: Must set source and destination paths");
                }

                if (!Directory.Exists(mDestination))
                {
                    try
                    {
                        WriteToLog("Destination directory is " + mDestination);
                        Directory.CreateDirectory(mDestination);

                    }
                    catch (Exception ex)
                    {
                        WriteToLog("EXCEPTION: " + ex.Message + ";  " + ex.Data + ";  " + ex.StackTrace);
                        throw new IOException("OnStart: Must set an accessable source and destination paths.\n" + ex.Message);
                    }
                }

                //ReadFromFilesLog();

                StartFileWatcher();
            }
            catch (Exception e)
            {
                WriteToLog("EXCEPTION: " + e.Message + ";  " + e.Data + ";  " + e.StackTrace);
                throw new Exception("OnStart: " + e.Message + "\n" + e.StackTrace);
            }
        }

        private void StartFileWatcher()
        {
            watcher = new FileSystemWatcher(mSource);
            WriteToLog("Synchronizing from " + mSource + " to " + mDestination);
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
        }
        static void WriteToLog(String logText)
        {
            if (String.IsNullOrWhiteSpace(mLogFile))
            {
                return;
            }

            lock (lockLogFile)
            {
                try
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(mLogFile, true))
                    {
                        file.WriteLine(logText);
                    }
                }
                catch (Exception ex)
                {
                    WriteToLog("EXCEPTION: " + ex.Message + ";  " + ex.Data + ";  " + ex.StackTrace);
                }
            }
        }

        static void ReadFromFilesLog()
        {
            if (String.IsNullOrWhiteSpace(sSynchronisedFilesFile))
            {
                return;
            }

            lock (lockFileSync)
            {
                try
                {
                    using (System.IO.StreamReader file = new System.IO.StreamReader(sSynchronisedFilesFile))
                    {
                        while (!file.EndOfStream)
                        {
                            mSyncedFiles.AddLast(file.ReadLine());
                        }
                    }
                }
                catch (Exception ex)
                {
                    WriteToLog("EXCEPTION: " + ex.Message + ";  " + ex.Data + ";  " + ex.StackTrace);
                }
            }
        }

        static void WriteToFilesLog(String fileName)
        {
            if (String.IsNullOrWhiteSpace(sSynchronisedFilesFile) ||
                String.IsNullOrWhiteSpace(fileName))
            {
                return;
            }

            lock (lockFileSync)
            {
                try
                {
                    using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(sSynchronisedFilesFile, true))
                    {
                        file.WriteLine(fileName);
                    }
                }
                catch (Exception ex)
                {
                    WriteToLog("EXCEPTION: " + ex.Message + ";  " + ex.Data + ";  " + ex.StackTrace);
                }
            }
        }

        protected override void OnStop()
        {
            stopped = true;
            watcher.Dispose();
        }
    }
}
