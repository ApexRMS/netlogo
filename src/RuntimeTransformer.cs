//netlogo: SyncroSim Base Package for running the NetLogo agent-based modeling environment.
//Copyright © 2007-2021 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Data;
using SyncroSim.Core;
using SyncroSim.StochasticTime;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace SyncroSim.NetLogo
{
    class RuntimeTransformer : StochasticTimeTransformer
    {
        private string m_ExeName;
        private string m_JarFileName;
        private string m_ExtensionDir;
        private DataSheet m_SymbolDataSheet;
        private DataSheet m_RunControlDataSheet;
        private DataSheet m_ScriptDataSheet;
        private DataSheet m_InputDataSheet;
        private DataSheet m_InputFileDataSheet;
        private Dictionary<int, string> m_SymbolMap;
        private InputSymbolMap m_InputMap;
        private InputSymbolMap m_InputFileMap;
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
            this.InitializeScript();
            this.InitializeExeName();
            this.InitializeJarFileName();
            this.InitializeSymbolMap();
            this.InitializeInputMap();
            this.InitializeInputFileMap();
        }

        private void InitializeDataSheets()
        {
            this.m_SymbolDataSheet = this.Project.GetDataSheet("netlogo_Symbol");
            this.m_RunControlDataSheet = this.ResultScenario.GetDataSheet("netlogo_RunControl");
            this.m_ScriptDataSheet = this.ResultScenario.GetDataSheet("netlogo_Script");
            this.m_InputDataSheet = this.ResultScenario.GetDataSheet("netlogo_Input");
            this.m_InputFileDataSheet = this.ResultScenario.GetDataSheet("netlogo_InputFile");
        }

        private void InitializeRunControl()
        {
            this.m_MinimumTimestep = Convert.ToInt32(this.GetRunControlValue("MinimumTimestep"), CultureInfo.InvariantCulture);
            this.m_MaximumTimestep = Convert.ToInt32(this.GetRunControlValue("MaximumTimestep"), CultureInfo.InvariantCulture);
        }

        private void InitializeScript()
        {
            this.m_TemplateFileName = Convert.ToString(this.GetScriptValue("TemplateFile"), CultureInfo.InvariantCulture);
            this.m_ExperimentName = Convert.ToString(this.GetScriptValue("Experiment"), CultureInfo.InvariantCulture);
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
            string ExtDir = Path.Combine(AppDir, "extensions");

            if (Files.Length == 1)
            {
                this.m_JarFileName = Files[0];
                this.m_ExtensionDir = ExtDir;
            }
            else
            {
                throw new InvalidOperationException("Cannot find NetLogo Jar file in: " + AppDir);
            }
        }

        void InitializeSymbolMap()
        {
            this.m_SymbolMap = new Dictionary<int, string>();

            foreach (DataRow dr in this.m_SymbolDataSheet.GetData().Rows)
            {
                this.m_SymbolMap.Add((int)(long)dr["SymbolID"], (string)dr["Name"]);
            }
        }

        void InitializeInputMap()
        {
            this.m_InputMap = new InputSymbolMap();           
            DataTable dt = this.m_InputDataSheet.GetData();

            foreach (DataRow dr in dt.Rows)
            {
                Nullable<int> Iteration = GetNullableInt(dr, "Iteration");
                string Symbol = "%" + this.m_SymbolMap[(int)(long)dr["SymbolID"]] + "%";
                string Value = (string)dr["Value"];

                if (Symbol.Contains(" "))
                {
                    throw new InvalidOperationException("The symbol cannot contain spaces: " + Symbol);
                }

                this.m_InputMap.AddSymbol(Iteration, Symbol, Value);
            }
        }

        void InitializeInputFileMap()
        {
            this.m_InputFileMap = new InputSymbolMap();           
            DataTable dt = this.m_InputFileDataSheet.GetData();

            foreach (DataRow dr in dt.Rows)
            {
                Nullable<int> Iteration = GetNullableInt(dr, "Iteration");
                string Symbol = "%" + this.m_SymbolMap[(int)(long)dr["SymbolID"]] + "%";
                string Filename = (string)dr["Filename"];

                if (Symbol.Contains(" "))
                {
                    throw new InvalidOperationException("The symbol cannot contain spaces: " + Symbol);
                }

                if (Filename.Contains(" "))
                {
                    throw new InvalidOperationException("The file name cannot contain spaces: " + Filename);
                }

                this.m_InputFileMap.AddSymbol(Iteration, Symbol, Filename);
            }
        }

        protected override void OnIteration(int iteration)
        {
            base.OnIteration(iteration);

            string NetLogoFolderName = this.Library.CreateTempFolder("NetLogo", false);
            string DataFolderName = Path.Combine(this.Library.GetFolderName(LibraryFolderType.Temporary), "Data");
            string TemplateFileName = this.CreateNetLogoTemplateFile(iteration, NetLogoFolderName, DataFolderName);

            if (this.IsUserInteractive())
            {
                string Enquoted = "\"" + TemplateFileName + "\"";
                base.ExternalTransform(this.m_ExeName, null, Enquoted, false, null);
            }
            else
            {
                string args = string.Format(CultureInfo.InvariantCulture,
                    "-Xmx1024m -Dfile.encoding=UTF-8 -Dnetlogo.extensions.dir=\"{0}\" -Dcom.sun.media.jai.disableMediaLib=true -cp \"{1}\" org.nlogo.headless.Main --model \"{2}\" --experiment {3}",
                    this.m_ExtensionDir, this.m_JarFileName, TemplateFileName, this.m_ExperimentName);

                base.ExternalTransform("java", null, args, false, null);
            }
        }

        protected override void ExecuteProcess(string programName, string arguments, bool reportsProgress, StringDictionary environment)
        {
            // This module is unusual in that it changes the external executable name when running in headless mode.
            // This, however, does not work if the SSIM_WINDOWS_EXECUTABLE_LOCATION environment variable has already been
            // configured to point to NetLogo.exe.  So, in this case, we remove the key.

            if (programName == "java")
            {
                if (environment.ContainsKey("SSIM_WINDOWS_EXECUTABLE_LOCATION"))
                {          
                     environment.Remove("SSIM_WINDOWS_EXECUTABLE_LOCATION");
                }
            }

            base.ExecuteProcess(programName, arguments, false, environment);
        }

        protected override void ProcessExternalData()
        {
            string NetLogoFolderName = this.Library.CreateTempFolder("NetLogo", false);
            ConvertAllASCFilesToTIF(NetLogoFolderName);

            base.ProcessExternalData();
        }

        private static void ConvertAllASCFilesToTIF(string tempFolderName)
        {
            string[] Files = Directory.GetFiles(tempFolderName);

            foreach (string SourceFileName in Files)
            {
                if (Path.GetExtension(SourceFileName).ToUpperInvariant() == ".ASC")
                {
                    string n = Path.GetFileNameWithoutExtension(SourceFileName);
                    string TifName = Path.Combine(tempFolderName, n + ".tif");

                    Spatial.ConvertFromAAIGridFormat(SourceFileName, TifName, StochasticTime.GeoTiffCompressionType.DEFLATE);
                }
            }
        }

        private string CreateNetLogoTemplateFile(int iteration, string tempFolderName, string dataFolderName)
        {
            List<InputSymbolRecord> InputSymbols = this.m_InputMap.GetSymbols(iteration);
            List<InputSymbolRecord> InputFileSymbols = this.m_InputFileMap.GetSymbols(iteration);
            string SourceTemplateFile = this.GetScriptFileName(this.m_TemplateFileName);
            string TargetTemplateFile = Path.Combine(tempFolderName, this.m_TemplateFileName);
            string IterationString = iteration.ToString(CultureInfo.InvariantCulture);
            string TickString = (this.m_MaximumTimestep - this.m_MinimumTimestep).ToString(CultureInfo.InvariantCulture);
            string VariableFileName = Path.Combine(dataFolderName, "SSIM_APPEND-netlogo_Output.csv");
            string VariableRasterFileName = Path.Combine(dataFolderName, "SSIM_APPEND-netlogo_OutputRaster.csv");
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
                        line = ProcessSystemSymbols(line, IterationString, TickString, vf, vrf, tfn);
                        line = ProcessInputSymbols(line, InputSymbols);
                        line = this.ProcessInputFileSymbols(line, tempFolderName, InputFileSymbols);

                        t.WriteLine(line);
                    }
                }
            }

            return TargetTemplateFile;
        }

        private static string ProcessSystemSymbols(
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

        private static string ProcessInputSymbols(string line, List<InputSymbolRecord> symbols)
        {
            string l = line;

            if (symbols != null)
            {
                foreach (InputSymbolRecord r in symbols)
                {
                    if (l.Contains(r.Symbol))
                    {
                        l = l.Replace(r.Symbol, r.Value);
                    }
                }
            }

            return (l);
        }

        private string ProcessInputFileSymbols(string line, string tempFolderName, List<InputSymbolRecord> symbols)
        {
            string l = line;

            if (symbols != null)
            {
                foreach (InputSymbolRecord r in symbols)
                {
                    if (l.Contains(r.Symbol))
                    {
                        string f1 = this.GetInputFileName(r.Value);
                        string f2 = Path.Combine(tempFolderName, r.Value);
                        string val = "\"" + f2.Replace(@"\", @"\\") + "\"";

                        l = l.Replace(r.Symbol, val);

                        if (!File.Exists(f2))
                        {
                            File.Copy(f1, f2);
                        }
                    }
                }
            }

            return (l);
        }

        private object GetRunControlValue(string columnName)
        {
            DataRow dr = this.m_RunControlDataSheet.GetDataRow();

            if (dr == null || dr[columnName] == DBNull.Value)
            {
                string DispName = this.m_RunControlDataSheet.Columns[columnName].DisplayName;
                throw new ArgumentException("The run control data is missing for: " + DispName);
            }

            return dr[columnName];
        }

        private object GetScriptValue(string columnName)
        {
            DataRow dr = this.m_ScriptDataSheet.GetDataRow();

            if (dr == null || dr[columnName] == DBNull.Value)
            {
                string DispName = this.m_ScriptDataSheet.Columns[columnName].DisplayName;
                throw new ArgumentException("The script data is missing for: " + DispName);
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

        private string GetScriptFileName(string fileName)
        {
            string f = this.Library.GetFolderName(LibraryFolderType.Input, this.m_ScriptDataSheet, false);
            return (Path.Combine(f, fileName));
        }

        private string GetInputFileName(string fileName)
        {
            string f = this.Library.GetFolderName(LibraryFolderType.Input, this.m_InputFileDataSheet, false);
            return (Path.Combine(f, fileName));
        }
    }
}
