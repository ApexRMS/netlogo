//netlogo: SyncroSim Base Package for running the NetLogo agent-based modeling environment.
//Copyright © 2007-2021 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;

namespace SyncroSim.NetLogo
{
    [ObfuscationAttribute(Exclude = true, ApplyToMembers = false)]
    internal class DBUpdate : UpdateProvider
    {
        public override void PerformUpdate(DataStore store, int currentSchemaVersion)
        {
            PerformUpdateInternal(store, currentSchemaVersion);

#if DEBUG
            //Verify that all expected indexes exist after the update because it is easy to forget to recreate them after 
            //adding a column to an existing table (which requires the table to be recreated if you want to preserve column order.)

            ASSERT_INDEX_EXISTS(store, "netlogo_Output");
            ASSERT_INDEX_EXISTS(store, "netlogo_OutputRaster");
#endif

        }

#if DEBUG

        private static void ASSERT_INDEX_EXISTS(DataStore store, string tableName)
        {
            if (store.TableExists(tableName))
            {
                string IndexName = tableName + "_Index";
                string Query = string.Format(CultureInfo.InvariantCulture, "SELECT COUNT(name) FROM sqlite_master WHERE type = 'index' AND name = '{0}'", IndexName);
                Debug.Assert((long)store.ExecuteScalar(Query) == 1);
            }
        }

#endif

        private static void PerformUpdateInternal(DataStore store, int currentSchemaVersion)
        {
            if (currentSchemaVersion < 1)
            {
                NL000001(store);
            }
        }

        private static void NL000001(DataStore store)
        {
            UpdateProvider.RenameTablesWithPrefix(store, "NetLogo_", "netlogo_");
            UpdateProvider.RenameInputFoldersWithPrefix(store, "NetLogo_", "netlogo_");
            UpdateProvider.RenameOutputFoldersWithPrefix(store, "NetLogo_", "netlogo_");

            store.ExecuteNonQuery("DROP INDEX IF EXISTS NetLogo_Output_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS NetLogo_OutputRaster_Index");

            UpdateProvider.CreateIndex(store, "netlogo_Output", new[] { "ScenarioID", "Iteration", "Timestep" });
            UpdateProvider.CreateIndex(store, "netlogo_OutputRaster", new[] { "ScenarioID", "Iteration", "Timestep" });

            if (store.TableExists("corestime_Charts"))
            {
                store.ExecuteNonQuery("UPDATE corestime_Charts SET Criteria = REPLACE(Criteria, 'Variables', 'netlogo_Variables')");
                store.ExecuteNonQuery("UPDATE corestime_Charts SET Criteria = REPLACE(Criteria, 'NetLogo_', 'netlogo_')");
            }

            if (store.TableExists("corestime_Maps"))
            {
                store.ExecuteNonQuery("UPDATE corestime_Maps SET Criteria = REPLACE(Criteria, 'Variables', 'netlogo_Variables')");
                store.ExecuteNonQuery("UPDATE corestime_Maps SET Criteria = REPLACE(Criteria, 'NetLogo_', 'netlogo_')");
            }
        }
    }
}

