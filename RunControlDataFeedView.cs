
using System.IO;
using System.Windows.Forms;
using SyncroSim.Core;
using SyncroSim.Core.Forms;

namespace SyncroSim.NetLogo
{
    public partial class RunControlDataFeedView : DataFeedView
    {
        public RunControlDataFeedView()
        {
            InitializeComponent();
        }

        public override void LoadDataFeed(Core.DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);

            this.SetTextBoxBinding(this.TextBoxTimesteps, "MaximumTimestep");
            this.SetTextBoxBinding(this.TextBoxIterations, "MaximumIteration");
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

        private void ButtonClearAll_Click(System.Object sender, System.EventArgs e)
        {
            this.ResetBoundControls();
            this.ClearDataSheets();
        }

        private void ButtonChooseTemplate_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Choose File";
            dlg.Filter = "NetLogo Files|*.nlogo";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataSheet ds = this.DataFeed.GetDataSheet("NetLogo_RunControl");

                ds.AddExternalInputFile(dlg.FileName);
                ds.SetSingleRowData("TemplateFile", Path.GetFileName(dlg.FileName));
                this.RefreshBoundControls();
            }
        }
    }
}
