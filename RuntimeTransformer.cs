
using System;
using System.IO;
using System.Data;
using SyncroSim.Core;
using SyncroSim.StochasticTime;
using System.Globalization;
using System.Collections.Generic;

namespace SyncroSim.NetLogo
{
    class RuntimeTransformer : StochasticTimeTransformer
    {
        private string m_ExeName;
        private string m_JarFileName;
        private DataSheet m_RunControl;
        private DataSheet m_InputFiles;
        private DataSheet m_OutputFiles;
        private DataSheet m_Symbols;
        private InputFileMap m_InputFileMap;
        private string m_MinimumIteration;
        private string m_MaximumIteration;
        private string m_MinimumTimestep;
        private string m_MaximumTimestep;
        private string m_TemplateFileName;
        private string m_ExperimentName;
        private const string DEFAULT_NETLOGO_FOLDER = "NetLogo 6.0";
        private const string NETLOGO_EXE_NAME = "NetLogo.exe";

        public override void Initialize()
        {
            base.Initialize();

            this.InitializeDataSheets();
            this.InitializeRunControl();
            this.InitializeExeName();
            this.InitializeJarFileName();
            this.InitializeInputFiles();
        }

        private void InitializeDataSheets()
        {
            this.m_RunControl = this.ResultScenario.GetDataSheet("NetLogo_RunControl");
            this.m_InputFiles = this.ResultScenario.GetDataSheet("NetLogo_InputFile");
            this.m_OutputFiles = this.ResultScenario.GetDataSheet("NetLogo_OutputFile");
            this.m_Symbols = this.ResultScenario.GetDataSheet("NetLogo_Symbol");
        }

        private void InitializeRunControl()
        {
            this.m_MinimumIteration = Convert.ToString(this.GetRunControlValue("MinimumIteration"));
            this.m_MaximumIteration = Convert.ToString(this.GetRunControlValue("MaximumIteration"));
            this.m_MinimumTimestep= Convert.ToString(this.GetRunControlValue("MinimumTimestep"));
            this.m_MaximumTimestep = Convert.ToString(this.GetRunControlValue("MaximumTimestep"));
            this.m_TemplateFileName = Convert.ToString(this.GetRunControlValue("TemplateFile"));
            this.m_ExperimentName = Convert.ToString(this.GetRunControlValue("Experiment"));
        }

        private void InitializeExeName()
        {
            this.m_ExeName = this.GetExternalExecutableName(NETLOGO_EXE_NAME, DEFAULT_NETLOGO_FOLDER);

            if (this.m_ExeName == null || !Path.IsPathRooted(this.m_ExeName))
            {
                throw new InvalidOperationException("Cannot find NetLogo.exe - please configure your library properties.");
            }
        }

        private void InitializeJarFileName()
        {
            string AppDir = Path.Combine(Path.GetDirectoryName(this.m_ExeName), "app");
            string[] Files = Directory.GetFiles(AppDir, "netlogo-*.jar");

            if (Files.Length == 1)
            {
                this.m_JarFileName = Files[0];
            }
            else
            {
                throw new InvalidOperationException("Cannot find NetLogo Jar file in: " + AppDir);
            }
        }

        void InitializeInputFiles()
        {
            this.m_InputFileMap = new InputFileMap();           
            DataTable dt = this.m_InputFiles.GetData();

            foreach (DataRow dr in dt.Rows)
            {
                Nullable<int> Iteration = GetNullableInt(dr, "Iteration");
                Nullable<int> Timestep = GetNullableInt(dr, "Timestep");
                string Symbol = (string)dr["Symbol"];
                string Filename = (string)dr["Filename"];

                this.m_InputFileMap.AddInputFileRecord(Iteration, Timestep, Symbol, Filename);
            }
        }

        protected override void OnTimestep(int iteration, int timestep)
        {
            base.OnTimestep(iteration, timestep);

            string TemplateFileName = this.CreateNetLogoTemplateFile(iteration, timestep);

            if (this.IsUserInteractive())
            {
                base.ExternalTransform(this.m_ExeName, null, TemplateFileName, null);
            }
            else
            {
                string args = string.Format(CultureInfo.InvariantCulture,
                    "-Xmx1024m -Dfile.encoding=UTF-8 -cp \"{0}\" org.nlogo.headless.Main --model \"{1}\" --experiment {2}",
                    this.m_JarFileName, TemplateFileName, this.m_ExperimentName);

                base.ExternalTransform("java", null, args, null);
            }
        }

        private string CreateNetLogoTemplateFile(int iteration, int timestep)
        {       
            string TempFolderName = this.Library.CreateTempFolder("NetLogo", true);
            string f1 = this.GetRunControlFileName(this.m_TemplateFileName);
            string f2 = Path.Combine(TempFolderName, this.m_TemplateFileName);

            if (!File.Exists(f1))
            {
                throw new InvalidOperationException("The NetLogo template file was not found.");
            }

            this.WriteNetLogoTemplate(f1, f2, iteration, timestep, TempFolderName);
            return f2;
        }

        private void WriteNetLogoTemplate(string source, string target, int iteration, int timestep, string tempFolderName)
        {
            using (StreamReader s = new StreamReader(source))
            {
                string line;

                using (StreamWriter t = new StreamWriter(target))
                {
                    while ((line = s.ReadLine()) != null)
                    {
                        line = this.ProcessSystemSymbols(line);
                        line = this.ProcessInputFileSymbols(line, iteration, timestep, tempFolderName);
                        line = this.ProcessOutputFileSymbols(line, tempFolderName);
                        line = this.ProcessOtherSymbols(line);

                        t.WriteLine(line);
                    }
                }
            }
        }

        private string ProcessSystemSymbols(string line)
        {
            string l = line;

            l = l.Replace("%SSIM_MIN_ITERATION%", this.m_MinimumIteration);
            l = l.Replace("%SSIM_MAX_ITERATION%", this.m_MaximumIteration);
            l = l.Replace("%SSIM_MIN_TIMESTEP%", this.m_MinimumTimestep);
            l = l.Replace("%SSIM_MAX_TIMESTEP%", this.m_MaximumTimestep);

            return (l);
        }

        private string ProcessInputFileSymbols(string line, int iteration, int timestep, string tempFolderName)
        {
            string l = line;
            List<InputFileRecord> recs = this.m_InputFileMap.GetInputFileRecords(iteration, timestep);

            foreach (InputFileRecord r in recs)
            {          
                string sym = "%" + r.Symbol + "%";

                if (l.Contains(sym))
                {
                    string f1 = this.GetInputFileName(r.Filename);
                    string f2 = Path.Combine(tempFolderName, r.Filename);
                    string val = "\"" + f2.Replace(@"\", @"\\") + "\"";

                    l = l.Replace(sym, val);

                    if (!File.Exists(f2))
                    {
                        File.Copy(f1, f2);
                    }
                }
            }       

            return (l);
        }

        private string ProcessOutputFileSymbols(string line, string tempFolderName)
        {
            string l = line;

            foreach (DataRow dr in this.m_OutputFiles.GetData().Rows)
            {
                string sym = "%" + (string)dr["Symbol"] + "%";

                if (l.Contains(sym))
                {
                    string f1 = Path.Combine(tempFolderName, (string)dr["Filename"]);
                    string val = "\"" + f1.Replace(@"\", @"\\") + "\"";

                    l = l.Replace(sym, val);
                }
            }

            return (l);
        }

        private string ProcessOtherSymbols(string line)
        {
            string l = line;

            foreach (DataRow dr in this.m_Symbols.GetData().Rows)
            {
                string sym = "%" + (string)dr["Symbol"] + "%";
                string val = (string)dr["Value"];

                l = l.Replace(sym, val);
            }             

            return (l);
        }

        private object GetRunControlValue(string columnName)
        {
            DataRow dr = this.m_RunControl.GetDataRow();

            if (dr == null || dr[columnName] == DBNull.Value)
            {
                throw new ArgumentException("The run control data is missing for: " + columnName);
            }

            return dr[columnName];
        }

        private int? GetNullableInt(DataRow dr, string columnName)
        {
            object value = dr[columnName];

            if (object.ReferenceEquals(value, DBNull.Value) || object.ReferenceEquals(value, null))
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(value, CultureInfo.InvariantCulture);
            }
        }

        private string GetRunControlFileName(string fileName)
        {
            string f = this.Library.GetFolderName(LibraryFolderType.Input, this.m_RunControl, false);
            return (Path.Combine(f, fileName));
        }

        private string GetInputFileName(string fileName)
        {
            string f = this.Library.GetFolderName(LibraryFolderType.Input, this.m_InputFiles, false);
            return (Path.Combine(f, fileName));
        }
    }
}
