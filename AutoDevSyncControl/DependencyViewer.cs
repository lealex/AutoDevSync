using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using Microsoft.Win32;

namespace AutoDevSync
{
    public partial class DependencyViewer : Form
    {
        protected const String SRegistryPath = @"Software\AutoDevSync";
        protected const String SRegistryProjects = @"ProjectsLocation";
        private List<DSLibrary> mLibraries;

        public String InitialDirectory { set; get; }

        public DependencyViewer()
        {
            InitializeComponent();
            InitialDirectory = "";
            mLibraries = new List<DSLibrary>();
        }

        private void btnChooseProject_Click(object sender, EventArgs e)
        {
            if (InitialDirectory.Length > 0)
            {
                ofdProjectSelector.InitialDirectory = InitialDirectory;
            }
            if (ofdProjectSelector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ScanDependencies(ofdProjectSelector.FileName);
            }
        }

        private void ScanDependencies(string path)
        {
            if (mLibraries.Count == 0)
            {
                RegistryKey reg = Registry.LocalMachine.OpenSubKey(SRegistryPath, false);
                if (reg != null)
                {
                    try
                    {
                        foreach (String item in reg.GetValue(SRegistryProjects, "").ToString().Split(';'))
                        {
                            if (
                                (item.Length > 0) &&
                                (!item.StartsWith(InitialDirectory))
                                )
                            {
                                if (MessageBox.Show("The path directory in the registry" +
                                    " is not the same as in the main dialog. " +
                                    "Whoul'd you like to use the old path's libraries? " +
                                    "Press \"No\", to refresh the libraries with the new path. (It may take several minutes)", "Dependency scan",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning,
                                    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                                {
                                    break;
                                }
                            }
                            if (item.Length > 0)
                            {
                                mLibraries.Add(new DSLibrary(item, IsGeneratingLibrary(item), GetProjectName(item), GetOutputName(item)));
                            }
                        }
                    }
                    catch (Exception)
                    {
                        //fkladjf sdlfjksdf sdkl klsdklsdflss
                    }
                    reg.Close();
                }
            }
            if (mLibraries.Count == 0)
            {
                ScanRootDirectory(InitialDirectory);
                RegistryKey reg = Registry.LocalMachine.OpenSubKey(SRegistryPath, true);
                if (reg != null)
                {
                    String libraries = "";
                    foreach (DSLibrary item in mLibraries)
                    {
                        if (libraries.Length == 0)
                        {
                            libraries = item.FullFileName;
                        }
                        else
                        {
                            libraries = libraries + ";" + item.FullFileName;
                        }
                    }
                    try
                    {
                        reg.SetValue(SRegistryProjects, libraries);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    reg.Close();
                }
            }
            var fileIo = new System.IO.StreamReader(path);
            string xmlData = fileIo.ReadToEnd();
            fileIo.Close();
            XmlReader reader = XmlReader.Create(new StringReader(xmlData));
            if (reader != null)
            {
                if (reader.ReadToFollowing("AdditionalDependencies"))
                {
                    reader.MoveToFirstAttribute();
                    string p = "";
                    foreach (string item in reader.ReadElementContentAsString().Split(';'))
                    {
                        p = p + "\n" + item;
                    }
                    MessageBox.Show(p);
                }//fkladjf sdlfjksdf sdkl klsdklsdflss
                reader.Close();
            }

        }

        private void ScanRootDirectory(String root)
        {
            DirectoryInfo dir = new DirectoryInfo(root);
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                if (!subDir.Name.ToLower().Contains("debug") &&
                    !subDir.Name.ToLower().Contains("release") &&
                    !subDir.Name.ToLower().Contains("relnpdb")
                    )
                {
                    ScanRootDirectory(subDir.FullName);
                }
            }

            foreach (FileInfo file in dir.GetFiles())
            {
                if (
                    (file.Extension.ToLower().EndsWith("vcproj") ||
                    file.Extension.ToLower().EndsWith("vcxproj")
                    ) &&
                    IsGeneratingLibrary(file.FullName))
                {
                    int ind = 0;
                    if ((ind = IndexInLibraryList(file.Name)) == -1)
                    {
                        mLibraries.Add(new DSLibrary(file.FullName, true, GetProjectName(file.FullName), GetOutputName(file.FullName)));
                    }
                    else
                    {
                        if (!mLibraries[ind].FullFileName.EndsWith(file.Extension) &&
                            file.Extension.ToLower().Equals("vcxproj"))
                        {
                            mLibraries[ind].FullFileName = file.FullName;
                        }
                    }
                }
            }
        }

        private string GetProjectName(string file)
        {
            return file.Substring(file.LastIndexOf('\\') + 1);
        }

        private String GetOutputName(string file)
        {
            String output = "";
            var fileIo = new System.IO.StreamReader(file);
            string xmlData = fileIo.ReadToEnd();
            fileIo.Close();
            try
            {
                XmlReader reader = XmlReader.Create(new StringReader(xmlData));
                if (reader != null)
                {
                    if (reader.ReadToFollowing("OutputFile"))
                    {
                        reader.MoveToFirstAttribute();
                        output = reader.ReadElementContentAsString();
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
            }
            if (
                (output.Length > 0) &&
                output.ToLower().Contains("$(projectname)")
                )
            {
                output = output.ToLower().Replace("$(projectname)", GetProjectName(file));
            }
            return output;
        }

        private int IndexInLibraryList(string file)
        {
            int index = 0;
            foreach (DSLibrary item in mLibraries)
            {
                FileInfo fileInfo = new FileInfo(item.FullFileName);
                if (fileInfo.Name.StartsWith(file.Substring(0, file.LastIndexOf('.'))))
                {
                    return index;//fkladjf sdlfjksdf sdkl klsdklsdflss
                }
                index++;
            }
            return -1;
        }

        private bool IsGeneratingLibrary(string path)
        {
            bool isLib = false;
            var fileIo = new System.IO.StreamReader(path);
            string xmlData = fileIo.ReadToEnd();
            fileIo.Close();
            try
            {
                XmlReader reader = XmlReader.Create(new StringReader(xmlData));
                if (reader.ReadToFollowing("ConfigurationType"))
                {
                    reader.MoveToFirstAttribute();
                    if (reader.ReadElementContentAsString().Equals("StaticLibrary"))
                    {
                        isLib = true;
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
            }
            return isLib;
        }
    }
}
//fkladjf sdlfjksdf sdkl klsdklsdflss