//NetLogo: A SyncroSim Module for running NetLogo simulations.
//Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Common;
using System.Collections.Generic;

namespace SyncroSim.NetLogo
{
    internal class InputSymbolMap
    {
        private SortedKeyMap1<List<InputSymbolRecord>> m_Map = new SortedKeyMap1<List<InputSymbolRecord>>(SearchMode.ExactPrev);

        public void AddSymbol(Nullable<int> iteration, string symbol, string value)
        {
            List <InputSymbolRecord> l = this.m_Map.GetItemExact(iteration);

            if (l == null)
            {
                l = new List<InputSymbolRecord>();
                this.m_Map.AddItem(iteration, l);
            }

            l.Add(new InputSymbolRecord(symbol, value));
        }

        public List<InputSymbolRecord> GetSymbols(int iteration)
        {
            return this.m_Map.GetItem(iteration);
        }
    }
}
