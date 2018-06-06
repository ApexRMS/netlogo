
using System;
using SyncroSim.Common;
using System.Globalization;

namespace SyncroSim.NetLogo
{
    internal class InputFileMap
    {
        private SortedKeyMap2<string> m_Map = new SortedKeyMap2<string>(SearchMode.ExactPrev);

        public void AddInputFile(Nullable<int> iteration, Nullable<int> timestep, string fileName)
        {
            string f = this.m_Map.GetItemExact(iteration, timestep);

            if (f != null)
            {
                string s = string.Format(CultureInfo.InvariantCulture,
                    "There is already a file for iteration {0} and timestep {1}", iteration, timestep);

                throw new ArgumentException(s);
            }

            this.m_Map.AddItem(iteration, timestep, fileName);
        }

        public string GetInputFile(int iteration, int timestep)
        {
            return this.m_Map.GetItem(iteration, timestep);
        }
    }
}
