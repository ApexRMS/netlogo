//NetLogo: A SyncroSim Module for running NetLogo simulations.
//Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.IO;
using SyncroSim.Core;
using SyncroSim.Core.Forms;
using System.Windows.Forms;

namespace SyncroSim.NetLogo
{
    public partial class ScriptDataFeedView : DataFeedView
    {
        public ScriptDataFeedView()
        {
            InitializeComponent();
        }

        public override void LoadDataFeed(DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);

            this.SetTextBoxBinding(this.TextBoxTemplateFile, "TemplateFile");
            this.SetTextBoxBinding(this.TextBoxExperiment, "Experiment");

            this.RefreshBoundControls();
            this.AddStandardCommands();
        }

        public override void EnableView(bool enable)
        {
            base.EnableView(enable);
            this.TextBoxTemplateFile.Enabled = false;
        }

        private void ButtonChooseTemplate_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Choose File";
            dlg.Filter = "NetLogo Files|*.nlogo";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataSheet ds = this.DataFeed.GetDataSheet("NetLogo_Script");

                ds.AddExternalInputFile(dlg.FileName);
                ds.SetSingleRowData("TemplateFile", Path.GetFileName(dlg.FileName));
                this.RefreshBoundControls();
            }
        }
    }
}
