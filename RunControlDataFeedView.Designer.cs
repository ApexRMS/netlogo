namespace SyncroSim.NetLogo
{
    partial class RunControlDataFeedView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LabelInputFile = new System.Windows.Forms.Label();
            this.ButtonClearAll = new System.Windows.Forms.Button();
            this.LabelTimesteps = new System.Windows.Forms.Label();
            this.TextBoxTimesteps = new System.Windows.Forms.TextBox();
            this.LabelIterations = new System.Windows.Forms.Label();
            this.TextBoxIterations = new System.Windows.Forms.TextBox();
            this.TextBoxTemplateFile = new System.Windows.Forms.TextBox();
            this.LabelExperiment = new System.Windows.Forms.Label();
            this.TextBoxExperiment = new System.Windows.Forms.TextBox();
            this.ButtonChooseTemplate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelInputFile
            // 
            this.LabelInputFile.AutoSize = true;
            this.LabelInputFile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelInputFile.Location = new System.Drawing.Point(20, 84);
            this.LabelInputFile.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.LabelInputFile.Name = "LabelInputFile";
            this.LabelInputFile.Size = new System.Drawing.Size(110, 13);
            this.LabelInputFile.TabIndex = 4;
            this.LabelInputFile.Text = "NetLogo template file:";
            this.LabelInputFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ButtonClearAll
            // 
            this.ButtonClearAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ButtonClearAll.Location = new System.Drawing.Point(148, 147);
            this.ButtonClearAll.Name = "ButtonClearAll";
            this.ButtonClearAll.Size = new System.Drawing.Size(193, 23);
            this.ButtonClearAll.TabIndex = 9;
            this.ButtonClearAll.Text = "Clear All";
            this.ButtonClearAll.UseVisualStyleBackColor = true;
            this.ButtonClearAll.Click += new System.EventHandler(this.ButtonClearAll_Click);
            // 
            // LabelTimesteps
            // 
            this.LabelTimesteps.AutoSize = true;
            this.LabelTimesteps.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelTimesteps.Location = new System.Drawing.Point(49, 21);
            this.LabelTimesteps.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.LabelTimesteps.Name = "LabelTimesteps";
            this.LabelTimesteps.Size = new System.Drawing.Size(81, 13);
            this.LabelTimesteps.TabIndex = 0;
            this.LabelTimesteps.Text = "Total timesteps:";
            this.LabelTimesteps.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TextBoxTimesteps
            // 
            this.TextBoxTimesteps.Location = new System.Drawing.Point(148, 18);
            this.TextBoxTimesteps.Name = "TextBoxTimesteps";
            this.TextBoxTimesteps.Size = new System.Drawing.Size(193, 20);
            this.TextBoxTimesteps.TabIndex = 1;
            // 
            // LabelIterations
            // 
            this.LabelIterations.AutoSize = true;
            this.LabelIterations.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelIterations.Location = new System.Drawing.Point(51, 53);
            this.LabelIterations.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.LabelIterations.Name = "LabelIterations";
            this.LabelIterations.Size = new System.Drawing.Size(79, 13);
            this.LabelIterations.TabIndex = 2;
            this.LabelIterations.Text = "Total iterations:";
            this.LabelIterations.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TextBoxIterations
            // 
            this.TextBoxIterations.Location = new System.Drawing.Point(148, 49);
            this.TextBoxIterations.Name = "TextBoxIterations";
            this.TextBoxIterations.Size = new System.Drawing.Size(193, 20);
            this.TextBoxIterations.TabIndex = 3;
            // 
            // TextBoxTemplateFile
            // 
            this.TextBoxTemplateFile.Location = new System.Drawing.Point(148, 80);
            this.TextBoxTemplateFile.Name = "TextBoxTemplateFile";
            this.TextBoxTemplateFile.ReadOnly = true;
            this.TextBoxTemplateFile.Size = new System.Drawing.Size(193, 20);
            this.TextBoxTemplateFile.TabIndex = 5;
            // 
            // LabelExperiment
            // 
            this.LabelExperiment.AutoSize = true;
            this.LabelExperiment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelExperiment.Location = new System.Drawing.Point(39, 114);
            this.LabelExperiment.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.LabelExperiment.Name = "LabelExperiment";
            this.LabelExperiment.Size = new System.Drawing.Size(91, 13);
            this.LabelExperiment.TabIndex = 7;
            this.LabelExperiment.Text = "Experiment name:";
            this.LabelExperiment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TextBoxExperiment
            // 
            this.TextBoxExperiment.Location = new System.Drawing.Point(148, 111);
            this.TextBoxExperiment.Name = "TextBoxExperiment";
            this.TextBoxExperiment.Size = new System.Drawing.Size(193, 20);
            this.TextBoxExperiment.TabIndex = 8;
            // 
            // ButtonChooseTemplate
            // 
            this.ButtonChooseTemplate.Location = new System.Drawing.Point(347, 78);
            this.ButtonChooseTemplate.Name = "ButtonChooseTemplate";
            this.ButtonChooseTemplate.Size = new System.Drawing.Size(75, 23);
            this.ButtonChooseTemplate.TabIndex = 6;
            this.ButtonChooseTemplate.Text = "Browse...";
            this.ButtonChooseTemplate.UseVisualStyleBackColor = true;
            this.ButtonChooseTemplate.Click += new System.EventHandler(this.ButtonChooseTemplate_Click);
            // 
            // RunControlDataFeedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ButtonChooseTemplate);
            this.Controls.Add(this.ButtonClearAll);
            this.Controls.Add(this.TextBoxExperiment);
            this.Controls.Add(this.LabelExperiment);
            this.Controls.Add(this.TextBoxTemplateFile);
            this.Controls.Add(this.LabelInputFile);
            this.Controls.Add(this.TextBoxIterations);
            this.Controls.Add(this.LabelIterations);
            this.Controls.Add(this.TextBoxTimesteps);
            this.Controls.Add(this.LabelTimesteps);
            this.Name = "RunControlDataFeedView";
            this.Size = new System.Drawing.Size(439, 190);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Label LabelInputFile;
        internal System.Windows.Forms.Button ButtonClearAll;
        internal System.Windows.Forms.Label LabelTimesteps;
        internal System.Windows.Forms.TextBox TextBoxTimesteps;
        internal System.Windows.Forms.Label LabelIterations;
        internal System.Windows.Forms.TextBox TextBoxIterations;
        internal System.Windows.Forms.TextBox TextBoxTemplateFile;
        internal System.Windows.Forms.Label LabelExperiment;
        internal System.Windows.Forms.TextBox TextBoxExperiment;
        private System.Windows.Forms.Button ButtonChooseTemplate;
    }
}
