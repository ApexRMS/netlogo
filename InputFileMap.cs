
using System;
using SyncroSim.Common;
using System.Collections.Generic;

namespace SyncroSim.NetLogo
{
    internal class InputFileMap
    {
        private SortedKeyMap1<List<InputFileRecord>> m_Map = new SortedKeyMap1<List<InputFileRecord>>(SearchMode.ExactPrev);

        public void AddInputFileRecord(Nullable<int> iteration, string symbol, string fileName)
        {
            List <InputFileRecord> l = this.m_Map.GetItemExact(iteration);

            if (l == null)
            {
                l = new List<InputFileRecord>();
                this.m_Map.AddItem(iteration, l);
            }

            l.Add(new InputFileRecord(symbol, fileName));
        }

        public List<InputFileRecord> GetInputFileRecords(int iteration)
        {
            return this.m_Map.GetItem(iteration);
        }
    }
}
