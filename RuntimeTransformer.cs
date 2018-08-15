
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
        private DataSheet m_InputFileSymbols;
        private DataSheet m_OtherSymbols;
        private DataSheet m_OutputVariable;
        private DataSheet m_OutputVariableRaster;
        private InputFileMap m_InputFileMap;
        private int m_MinimumIteration;
        private int m_MaximumIteration;
        private int m_MinimumTimestep;
        private int m_MaximumTimestep;
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
            this.m_InputFileSymbols = this.ResultScenario.GetDataSheet("NetLogo_InputFileSymbol");
            this.m_OtherSymbols = this.ResultScenario.GetDataSheet("NetLogo_OtherSymbol");
            this.m_OutputVariable = this.ResultScenario.GetDataSheet("NetLogo_OutputVariable");
            this.m_OutputVariableRaster = this.ResultScenario.GetDataSheet("NetLogo_OutputVariableRaster");
        }

        private void InitializeRunControl()
        {
            this.m_MinimumIteration = Convert.ToInt32(this.GetRunControlValue("MinimumIteration"), CultureInfo.InvariantCulture);
            this.m_MaximumIteration = Convert.ToInt32(this.GetRunControlValue("MaximumIteration"), CultureInfo.InvariantCulture);
            this.m_MinimumTimestep= Convert.ToInt32(this.GetRunControlValue("MinimumTimestep"), CultureInfo.InvariantCulture);
            this.m_MaximumTimestep = Convert.ToInt32(this.GetRunControlValue("MaximumTimestep"), CultureInfo.InvariantCulture);
            this.m_TemplateFileName = Convert.ToString(this.GetRunControlValue("TemplateFile"), CultureInfo.InvariantCulture);
            this.m_ExperimentName = Convert.ToString(this.GetRunControlValue("Experiment"), CultureInfo.InvariantCulture);
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
            DataTable dt = this.m_InputFileSymbols.GetData();

            foreach (DataRow dr in dt.Rows)
            {
                Nullable<int> Iteration = GetNullableInt(dr, "Iteration");
                string Symbol = (string)dr["Symbol"];
                string Filename = (string)dr["Filename"];

                this.m_InputFileMap.AddInputFileRecord(Iteration, Symbol, Filename);
            }
        }

        protected override void OnIteration(int iteration)
        {
            base.OnIteration(iteration);

            string NetLogoFolderName = this.Library.CreateTempFolder("NetLogo", true);
            string DataFolderName = Path.Combine(this.Library.GetFolderName(LibraryFolderType.Temporary), "Data");
            string TemplateFileName = this.CreateNetLogoTemplateFile(iteration, NetLogoFolderName, DataFolderName);

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

        protected override void ProcessExternalData()
        {
            string NetLogoFolderName = this.Library.CreateTempFolder("NetLogo", false);
            this.ConvertAllASCFilesToTIF(NetLogoFolderName);

            base.ProcessExternalData();
        }

        private void ConvertAllASCFilesToTIF(string tempFolderName)
        {
            string[] Files = Directory.GetFiles(tempFolderName);

            foreach (string SourceFileName in Files)
            {
                if (Path.GetExtension(SourceFileName).ToUpperInvariant() == ".ASC")
                {
                    string n = Path.GetFileNameWithoutExtension(SourceFileName);
                    string TifName = Path.Combine(tempFolderName, n + ".tif");

                    if (!Translate.GdalTranslate(SourceFileName, TifName, GdalOutputFormat.GTiff, GdalOutputType.Float64, GeoTiffCompressionType.None, null))
                    {
                        throw new InvalidOperationException("Cannot translate from Raster ASCII format: " + SourceFileName);
                    }
                }
            }
        }

        private string CreateNetLogoTemplateFile(int iteration, string tempFolderName, string dataFolderName)
        {       
            string SourceTemplateFile = this.GetRunControlFileName(this.m_TemplateFileName);
            string TargetTemplateFile = Path.Combine(tempFolderName, this.m_TemplateFileName);
            string IterationString = iteration.ToString(CultureInfo.InvariantCulture);
            string TickString = (this.m_MaximumTimestep - this.m_MinimumTimestep).ToString(CultureInfo.InvariantCulture);
            string VariableFileName = Path.Combine(dataFolderName, "NetLogo_OutputVariable.csv");
            string VariableRasterFileName = Path.Combine(dataFolderName, "NetLogo_OutputVariableRaster.csv");
            string vf = "\"" + VariableFileName.Replace(@"\", @"\\") + "\"";
            string vrf = "\"" + VariableRasterFileName.Replace(@"\", @"\\") + "\"";
            string tfn = "\"" + tempFolderName.Replace(@"\", @"\\") + "\"";

            if (!File.Exists(SourceTemplateFile))
            {
                throw new InvalidOperationException("The NetLogo template file was not found: " + SourceTemplateFile);
            }

            using (StreamReader s = new StreamReader(SourceTemplateFile))
            {
                string line;

                using (StreamWriter t = new StreamWriter(TargetTemplateFile))
                {
                    while ((line = s.ReadLine()) != null)
                    {
                        line = this.ProcessSystemSymbols(line, IterationString, TickString, vf, vrf, tfn);
                        line = this.ProcessInputFileSymbols(line, iteration, tempFolderName);
                        line = this.ProcessOtherSymbols(line);

                        t.WriteLine(line);
                    }
                }
            }

            return TargetTemplateFile;
        }

        private string ProcessSystemSymbols(
            string line, 
            string iterationString, 
            string tickString, 
            string variableFileName, 
            string variableRasterFileName,
            string tempFolderName)
        {
            string l = line;
          
            l = l.Replace("%SSIM_ITERATION%", iterationString);
            l = l.Replace("%SSIM_TICKS%", tickString);
            l = l.Replace("%SSIM_VARIABLE_FILENAME%", variableFileName);
            l = l.Replace("%SSIM_VARIABLE_RASTER_FILENAME%", variableRasterFileName);
            l = l.Replace("%SSIM_NETLOGO_TEMP_FOLDER%", tempFolderName);  

            return (l);
        }

        private string ProcessInputFileSymbols(string line, int iteration, string tempFolderName)
        {
            string l = line;
            List<InputFileRecord> recs = this.m_InputFileMap.GetInputFileRecords(iteration);

            if (recs != null)
            {
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
            }

            return (l);
        }

        private string ProcessOtherSymbols(string line)
        {
            string l = line;

            foreach (DataRow dr in this.m_OtherSymbols.GetData().Rows)
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

        private static int? GetNullableInt(DataRow dr, string columnName)
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
            string f = this.Library.GetFolderName(LibraryFolderType.Input, this.m_InputFileSymbols, false);
            return (Path.Combine(f, fileName));
        }
    }
}
