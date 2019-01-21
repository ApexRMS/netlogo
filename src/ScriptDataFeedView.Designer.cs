namespace SyncroSim.NetLogo
{
    partial class ScriptDataFeedView
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
            this.ButtonChooseTemplate = new System.Windows.Forms.Button();
            this.TextBoxExperiment = new System.Windows.Forms.TextBox();
            this.LabelExperiment = new System.Windows.Forms.Label();
            this.TextBoxTemplateFile = new System.Windows.Forms.TextBox();
            this.LabelInputFile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ButtonChooseTemplate
            // 
            this.ButtonChooseTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonChooseTemplate.Location = new System.Drawing.Point(497, 18);
            this.ButtonChooseTemplate.Name = "ButtonChooseTemplate";
            this.ButtonChooseTemplate.Size = new System.Drawing.Size(75, 23);
            this.ButtonChooseTemplate.TabIndex = 11;
            this.ButtonChooseTemplate.Text = "Browse...";
            this.ButtonChooseTemplate.UseVisualStyleBackColor = true;
            this.ButtonChooseTemplate.Click += new System.EventHandler(this.ButtonChooseTemplate_Click);
            // 
            // TextBoxExperiment
            // 
            this.TextBoxExperiment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxExperiment.Location = new System.Drawing.Point(146, 51);
            this.TextBoxExperiment.Name = "TextBoxExperiment";
            this.TextBoxExperiment.Size = new System.Drawing.Size(343, 20);
            this.TextBoxExperiment.TabIndex = 13;
            // 
            // LabelExperiment
            // 
            this.LabelExperiment.AutoSize = true;
            this.LabelExperiment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelExperiment.Location = new System.Drawing.Point(37, 54);
            this.LabelExperiment.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.LabelExperiment.Name = "LabelExperiment";
            this.LabelExperiment.Size = new System.Drawing.Size(91, 13);
            this.LabelExperiment.TabIndex = 12;
            this.LabelExperiment.Text = "Experiment name:";
            this.LabelExperiment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TextBoxTemplateFile
            // 
            this.TextBoxTemplateFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxTemplateFile.Location = new System.Drawing.Point(146, 20);
            this.TextBoxTemplateFile.Name = "TextBoxTemplateFile";
            this.TextBoxTemplateFile.ReadOnly = true;
            this.TextBoxTemplateFile.Size = new System.Drawing.Size(343, 20);
            this.TextBoxTemplateFile.TabIndex = 10;
            // 
            // LabelInputFile
            // 
            this.LabelInputFile.AutoSize = true;
            this.LabelInputFile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelInputFile.Location = new System.Drawing.Point(18, 24);
            this.LabelInputFile.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.LabelInputFile.Name = "LabelInputFile";
            this.LabelInputFile.Size = new System.Drawing.Size(110, 13);
            this.LabelInputFile.TabIndex = 9;
            this.LabelInputFile.Text = "NetLogo template file:";
            this.LabelInputFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ScriptDataFeedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ButtonChooseTemplate);
            this.Controls.Add(this.TextBoxExperiment);
            this.Controls.Add(this.LabelExperiment);
            this.Controls.Add(this.TextBoxTemplateFile);
            this.Controls.Add(this.LabelInputFile);
            this.Name = "ScriptDataFeedView";
            this.Size = new System.Drawing.Size(592, 83);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonChooseTemplate;
        internal System.Windows.Forms.TextBox TextBoxExperiment;
        internal System.Windows.Forms.Label LabelExperiment;
        internal System.Windows.Forms.TextBox TextBoxTemplateFile;
        internal System.Windows.Forms.Label LabelInputFile;
    }
}
