//netlogo: SyncroSim Base Package for running the NetLogo agent-based modeling environment.
//Copyright © 2007-2021 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.NetLogo
{
    class InputSymbolRecord
    {
        private string m_Symbol;
        private string m_Value;

        public InputSymbolRecord(string symbol, string value)
        {
            this.m_Symbol = symbol;
            this.m_Value = value;
        }

        public string Symbol
        {
            get
            {
                return m_Symbol;
            }
        }

        public string Value
        {
            get
            {
                return m_Value;
            }
        }
    }
}
