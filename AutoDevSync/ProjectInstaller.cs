using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Diagnostics;
using Microsoft.Win32;

namespace AutoDevSync
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

            Process process = new Process();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.FileName = "sc";
            process.StartInfo.Arguments = "failure \"" + serviceInstaller1.ServiceName + "\" reset= 0 actions= restart/5000";
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException();
            }

            //process.StartInfo.Arguments = "start \"" + serviceInstaller1.ServiceName + "\"";
            //process.Start();
            //process.WaitForExit();
            //if (process.ExitCode != 0)
            //{
            //    throw new InvalidOperationException();
            //}
        }

        private void serviceProcessInstaller1_BeforeInstall(object sender, InstallEventArgs e)
        {
            //try
            //{
            //    Debugger.Launch();
            //    RegistryKey key = Registry.LocalMachine;
            //    try
            //    {
            //        key.CreateSubKey("Software\\AutoDevSync");
            //    }
            //    catch (Exception)
            //    {
            //    }
            //    RegistryKey k = key.OpenSubKey("Software\\AutoDevSync", true);
            //    if (k == null)
            //    {
            //        return;
            //    }
            //    ///USERNAME=[USERNAME] /PASSWORD=[PASSWORD] /SOURCEDIR=[SOURCEDIR1] /DESTDIR=[DESTDIR1] 
            //    ////EXTENTIONS=[EXTENTIONS] /EXCLUDE=[EXCLUDE]
            //    String srcDir = Context.Parameters["SOURCEDIR1"];
            //    String destDir = Context.Parameters["DESTDIR1"];
            //    String extentions = Context.Parameters["EXTENTIONS1"];
            //    String exclude = Context.Parameters["EXCLUDE1"];

            //    if (!String.IsNullOrEmpty(srcDir) &&
            //        !String.IsNullOrEmpty(destDir))
            //    {
            //        try
            //        {
            //            k.SetValue("source", srcDir, RegistryValueKind.String);
            //            k.SetValue("destination", destDir, RegistryValueKind.String);

            //            if (extentions != null)
            //            {
            //                k.SetValue("filter", extentions, RegistryValueKind.String);
            //            }

            //            if (exclude != null)
            //            {
            //                k.SetValue("exclude", exclude, RegistryValueKind.String);
            //            }

            //            k.SetValue("logfile", "c:\\", RegistryValueKind.String);

            //            if (!destDir.Equals(k.GetValue("destination", "")))
            //            {
            //                throw new IOException(
            //                    String.Format("serviceProcessInstaller1_BeforeInstall: dest dir is not {0}: {1} ", 
            //                                    srcDir, k.GetValue("source", "")));
            //            }
            //        }
            //        catch (Exception)
            //        {
            //        }
            //    }
            //    try
            //    {
            //        k.Close();
            //    }
            //    catch (Exception)
            //    {
            //    }
            //    try
            //    {
            //        key.Close();
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}
            //catch (Exception)
            //{
            //}
        }

    }
}
//asd
//das
//abstractda
//dassd
//asad
//
//adas
//