//NetLogo: A SyncroSim Module for running NetLogo simulations.
//Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.NetLogo
{
    class InputFileRecord
    {
        private string m_Symbol;
        private string m_Filename;

        public InputFileRecord(string symbol, string fileName)
        {
            this.m_Symbol = symbol;
            this.m_Filename = fileName;
        }

        public string Symbol
        {
            get
            {
                return m_Symbol;
            }
        }

        public string Filename
        {
            get
            {
                return m_Filename;
            }
        }
    }
}
