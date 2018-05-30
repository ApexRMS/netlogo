
using System;
using System.IO;
using System.Data;
using SyncroSim.Core;
using System.Globalization;

namespace SyncroSim.NetLogo
{
    class RuntimeTransformer : Transformer
    {
        public override void Transform()
        {
            try
            {
                this.SetStatusMessage("Running NetLogo");
                this.InternalTransform();
            }
            finally
            {
                this.SetStatusMessage(null);
            }
        }

        private void InternalTransform()
        {
            string TemplateFileName = this.GetNetLogoTemplateFile();

            if (this.IsHeadlessInvocation())
            {
                string JarFileName = this.GetNetLogoJarFile();
                string Experiment = this.GetNetLogoExperiment();

                string args = string.Format(CultureInfo.InvariantCulture,
                    "-Xmx1024m -Dfile.encoding=UTF-8 -cp \"{0}\" org.nlogo.headless.Main --model \"{1}\" --experiment {2}",
                    JarFileName, TemplateFileName, Experiment);

                base.ExternalTransform("java", null, args, null);
            }
            else
            {
                string Exe = this.GetNetLogoExeFile();
                base.ExternalTransform(Exe, null, TemplateFileName, null);
            }
        }

        private bool IsHeadlessInvocation()
        {
            DataRow dr = this.GetEnvironmentRow("SSIM_USER_INTERACTIVE");

            if (dr == null)
            {
                return true;
            }

            return ((string)dr["Value"] != "True");
        }

        private string GetNetLogoJarFile()
        {
            string f = this.GetJarFileNameFromConfig();

            if (f == null)
            {
                f = this.GetDefaultJarFileName();
            }

            if (f == null)
            {
                throw new InvalidOperationException("Cannot find the NetLogo Jar file!");
            }

            if (!File.Exists(f))
            {
                string s = string.Format(CultureInfo.InvariantCulture, "The NetLogo jar file does not exist: {0}", f);
                throw new InvalidOperationException(s);
            }

            return f;
        }

        private string GetNetLogoExeFile()
        {
            string f = this.GetExeNameFromConfig();

            if (f == null)
            {
                f = this.GetDefaultExeName();
            }

            if (f == null)
            {
                throw new InvalidOperationException("Cannot find the NetLogo EXE file!");
            }

            if (!File.Exists(f))
            {
                string s = string.Format(CultureInfo.InvariantCulture, "The NetLogo EXE file does not exist: {0}", f);
                throw new InvalidOperationException(s);
            }

            return f;
        }

        private string GetNetLogoTemplateFile()
        {
            string TemplateFileName = (string)this.GetRunControlValue("TemplateFile");
            string TempFolderName = this.Library.GetTempFolderName("NetLogo");
            string f1 = this.GetRunControlFileName(TemplateFileName);
            string f2 = Path.Combine(TempFolderName, TemplateFileName);

            if (!File.Exists(f1))
            {
                throw new InvalidOperationException("The NetLogo template file was not found.");
            }

            this.WriteNetLogoTemplate(f1, f2);
            return f2;
        }

        private void WriteNetLogoTemplate(string source, string target)
        {
            if (File.Exists(target))
            {
                File.Delete(target);
            }

            using (StreamReader s = new StreamReader(source))
            {
                string line;

                using (StreamWriter t = new StreamWriter(target))
                {
                    while ((line = s.ReadLine()) != null)
                    {
                        line = this.ReplaceSymbols(line);
                        t.WriteLine(line);
                    }
                }
            }
        }

        private string ReplaceSymbols(string line)
        {
            string l = line.Replace("%SSIM_MIN_TIMESTEP%", Convert.ToString(this.GetRunControlValue("MinimumTimestep")));
            l = line.Replace("%SSIM_MAX_TIMESTEP%", Convert.ToString(this.GetRunControlValue("MaximumTimestep")));

            return (l);
        }

        private string GetNetLogoExperiment()
        {
            return (string)this.GetRunControlValue("Experiment");
        }

        private object GetRunControlValue(string columnName)
        {
            DataRow dr = this.ResultScenario.GetDataSheet("NetLogo_RunControl").GetDataRow();

            if (dr[columnName] == DBNull.Value)
            {
                throw new ArgumentException("The run control data is missing for: " + columnName);
            }

            return dr[columnName];
        }

        private string GetExeNameFromConfig()
        {
            DataRow dr = this.GetEnvironmentRow("SSIM_WINDOWS_EXECUTABLE_LOCATION");

            if (dr == null)
            {
                return null;
            }

            string v = (string)dr["Value"];
            return v;
        }

        private string GetJarFileNameFromConfig()
        {
            string f = this.GetExeNameFromConfig();

            if (f == null)
            {
                return null;
            }

            f = Path.Combine(Path.GetDirectoryName(f), "app");
            f = Path.Combine(f, "netlogo-6.0.0.jar");

            return f;
        }

        private string GetDefaultExeName()
        {
            string f = this.GetDefaultNetLogoFolder();
            return Path.Combine(f, "NetLogo.exe");
        }

        private string GetDefaultJarFileName()
        {
            string f = this.GetDefaultNetLogoFolder();

            f = Path.Combine(f, "app");
            f = Path.Combine(f, "netlogo-6.0.0.jar");

            return f;
        }

        private string GetDefaultNetLogoFolder()
        {
            string f = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "NetLogo 6.0");

            if (Directory.Exists(f))
            {
                return f;
            }

            f = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "NetLogo 6.0");

            if (Directory.Exists(f))
            {
                return f;
            }

            //We only ship x64 builds, but it is nice to have this work on an x86
            //development machine, so:

            f = Path.Combine("C:\\Program Files", "NetLogo 6.0");

            if (Directory.Exists(f))
            {
                return f;
            }

            f = Path.Combine("C:\\Program Files (x86)", "NetLogo 6.0");

            if (Directory.Exists(f))
            {
                return f;
            }

            return null;
        }

        private DataRow GetEnvironmentRow(string keyName)
        {
            DataSheet ds = this.Library.GetDataSheet("NetLogo_Environment");

            foreach (DataRow dr in ds.GetData().Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                if (dr["Name"] != DBNull.Value)
                {
                    string n = (string)dr["Name"];

                    if (n == keyName)
                    {
                        return dr;
                    }
                }
            }

            return null;
        }

        private string GetRunControlFileName(string fileName)
        {
            DataSheet ds = this.ResultScenario.GetDataSheet("NetLogo_RunControl");
            string f = this.Library.GetFolderName(LibraryFolderType.Input, ds, false);

            return (Path.Combine(f, fileName));
        }

        private string GetInputFileName(string fileName)
        {
            DataSheet ds = this.ResultScenario.GetDataSheet("NetLogo_InputFiles");
            string f = this.Library.GetFolderName(LibraryFolderType.Input, ds, false);

            return (Path.Combine(f, fileName));
        }

        private string GetOutputFileName(string fileName)
        {
            DataSheet ds = this.ResultScenario.GetDataSheet("NetLogo_OutputFiles");
            string f = this.Library.GetFolderName(LibraryFolderType.Output, ds, true);

            return (Path.Combine(f, fileName));
        }
    }
}
