
using System;
using SyncroSim.Common;
using System.Collections.Generic;

namespace SyncroSim.NetLogo
{
    internal class InputFileMap
    {
        private SortedKeyMap2<List<InputFileRecord>> m_Map = new SortedKeyMap2<List<InputFileRecord>>(SearchMode.ExactPrev);

        public void AddInputFileRecord(Nullable<int> iteration, Nullable<int> timestep, string symbol, string fileName)
        {
            List <InputFileRecord> l = this.m_Map.GetItemExact(iteration, timestep);

            if (l == null)
            {
                l = new List<InputFileRecord>();
                this.m_Map.AddItem(iteration, timestep, l);
            }

            l.Add(new InputFileRecord(symbol, fileName));
        }

        public List<InputFileRecord> GetInputFileRecords(int iteration, int timestep)
        {
            return this.m_Map.GetItem(iteration, timestep);
        }
    }
}
