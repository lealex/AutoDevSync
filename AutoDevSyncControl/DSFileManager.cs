using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace AutoDevSync
{
    public class DsFileManager
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Backup { get; set; }
        public string Filters { get; set; }
        public string Excludes { get; set; }
        public string RegistryFilePath { get; set; }

        public DateTime ScanFrom { get; set; }
        public DateTime ScanTo { get; set; }

        public List<String> FilesModified { get; private set; }

        public delegate void ThreadCallBack();

        public DsFileManager(String src, string dest)
        {
            Source = src;
            Destination = dest;
        }

        public DsFileManager(String src)
        {
            Source = src;
        }

        public void listModifiedFiles()
        {
            if (FilesModified != null)
            {
                FilesModified.Clear();
            }
            else
            {
                FilesModified = new List<string>();
            }

            if (
                (Source != null) &&
                (Destination != null)
                )
            {
                listModifiedFiles(Source, Destination, ScanFrom, ScanTo);
            }
        }

        private void listModifiedFiles(string origPath, string dest, DateTime from, DateTime to)
        {

            DirectoryInfo dir = new DirectoryInfo(origPath);
            string temp = dest.EndsWith("\\") ? dest : dest + "\\";
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                listModifiedFiles(subDir.FullName, temp + subDir.Name, from, to);
            }

            foreach (FileInfo file in dir.GetFiles())
            {
                bool moveFile = false;
                bool excludeFile = false;
                foreach (string item in Excludes.Split(';'))
                {
                    if (file.FullName.ToLower().Contains(item.ToLower()))
                    {
                        excludeFile = true;
                        break;
                    }
                }
                if (!excludeFile)
                {
                    foreach (string item in Filters.Split(';'))
                    {
                        if (file.Name.ToLower().EndsWith(item.ToLower()))
                        {
                            moveFile = true;
                            break;
                        }
                    }
                }

                if (
                    (moveFile) &&
                    (file.LastWriteTime.CompareTo(from) >= 0) &&
                    (file.LastWriteTime.CompareTo(to) <= 0)
                    )
                {
                    if (ScanReadOnly ||
                        (file.Attributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
                    {
                        FilesModified.Add(file.FullName);
                    }
                }
            }
        }

        private bool IsarCompnent(string str, string compFilters, StreamWriter s)
        {
            String[] collection = compFilters.Split(';');
            foreach (String item in collection)
            {
                if (str.IndexOf(item) >= 0)
                {
                    s.WriteLine(str);
                    return true;
                }
            }
            return false;
        }
        private bool ToDelRegKey(RegistryKey key, StreamWriter stream)
        {
            bool s = false;
            if (key.SubKeyCount > 0)
            {
                String[] list = key.GetSubKeyNames();
                foreach (String item in list)
                {
                    try
                    {
                        if (item.Length > 0)
                        {
                            s = ToDelRegKey(key.OpenSubKey(item), stream);
                            if (s)
                            {
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            if (key.ValueCount > 0)
            {
                foreach (String val in key.GetValueNames())
                {
                    try
                    {
                        String str = key.GetValue(val).ToString().ToLower();
                        if ((IsarCompnent(str, Filters, stream)) && 
                            ((str.IndexOf(".dll") >= 0) || (str.IndexOf(".ocx") >= 0)))
                        {
                            s = true;
                            break;
                        }
                    }
                    catch (Exception)
                    { }
                }
            }

            return s;
        }

        public void ScanClientRegistry()
        {
            String rootKey = @"CLSID";
            String record = @"Record";
            String rootKey64 = "Wow6432Node\\CLSID";

            StreamWriter s = new StreamWriter(RegistryFilePath, false);
            //MessageBox.Show(key.SubKeyCount.ToString());
            s.WriteLine("--------------------------------------------" + rootKey + "-----------------------------------------------------------");
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(rootKey, false);
            foreach (String item in key.GetSubKeyNames())
            {
                if (ToDelRegKey(key.OpenSubKey(item), s))
                {
                    s.WriteLine(item + " - True");
                }
                //if (item.Equals("{075DBD26-FEED-31E6-9363-C3A186205966}"))
                //{
                //    s.WriteLine(item + " - " + ToDelRegKey(key.OpenSubKey(item)));
                //}
            }
            s.WriteLine("--------------------------------------------" + rootKey64 + "------------------------------------------------------------");
            key = Registry.ClassesRoot.OpenSubKey(rootKey64, false);
            foreach (String item in key.GetSubKeyNames())
            {
                if (ToDelRegKey(key.OpenSubKey(item), s))
                {
                    s.WriteLine(item + " - True");
                }
            }
            s.WriteLine("--------------------------------------------" + record + "------------------------------------------------------------");
            key = Registry.ClassesRoot.OpenSubKey(record, false);
            foreach (String item in key.GetSubKeyNames())
            {
                if (ToDelRegKey(key.OpenSubKey(item), s))
                {
                    s.WriteLine(item + " - True");
                }
            }
            s.Close();
            key.Close();
        }

        public bool ScanReadOnly { get; set; }
    }
}
