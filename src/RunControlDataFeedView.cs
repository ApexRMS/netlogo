//netlogo: SyncroSim Base Package for running the NetLogo agent-based modeling environment.
//Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

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

            this.RefreshBoundControls();
            this.AddStandardCommands();
        }
    }
}
