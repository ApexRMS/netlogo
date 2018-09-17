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
            this.LabelTimesteps = new System.Windows.Forms.Label();
            this.TextBoxTimesteps = new System.Windows.Forms.TextBox();
            this.LabelIterations = new System.Windows.Forms.Label();
            this.TextBoxIterations = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // LabelTimesteps
            // 
            this.LabelTimesteps.AutoSize = true;
            this.LabelTimesteps.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelTimesteps.Location = new System.Drawing.Point(13, 19);
            this.LabelTimesteps.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.LabelTimesteps.Name = "LabelTimesteps";
            this.LabelTimesteps.Size = new System.Drawing.Size(81, 13);
            this.LabelTimesteps.TabIndex = 0;
            this.LabelTimesteps.Text = "Total timesteps:";
            this.LabelTimesteps.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TextBoxTimesteps
            // 
            this.TextBoxTimesteps.Location = new System.Drawing.Point(112, 16);
            this.TextBoxTimesteps.Name = "TextBoxTimesteps";
            this.TextBoxTimesteps.Size = new System.Drawing.Size(81, 20);
            this.TextBoxTimesteps.TabIndex = 1;
            // 
            // LabelIterations
            // 
            this.LabelIterations.AutoSize = true;
            this.LabelIterations.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelIterations.Location = new System.Drawing.Point(15, 51);
            this.LabelIterations.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.LabelIterations.Name = "LabelIterations";
            this.LabelIterations.Size = new System.Drawing.Size(79, 13);
            this.LabelIterations.TabIndex = 2;
            this.LabelIterations.Text = "Total iterations:";
            this.LabelIterations.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TextBoxIterations
            // 
            this.TextBoxIterations.Location = new System.Drawing.Point(112, 47);
            this.TextBoxIterations.Name = "TextBoxIterations";
            this.TextBoxIterations.Size = new System.Drawing.Size(81, 20);
            this.TextBoxIterations.TabIndex = 3;
            // 
            // RunControlDataFeedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TextBoxIterations);
            this.Controls.Add(this.LabelIterations);
            this.Controls.Add(this.TextBoxTimesteps);
            this.Controls.Add(this.LabelTimesteps);
            this.Name = "RunControlDataFeedView";
            this.Size = new System.Drawing.Size(214, 80);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Label LabelTimesteps;
        internal System.Windows.Forms.TextBox TextBoxTimesteps;
        internal System.Windows.Forms.Label LabelIterations;
        internal System.Windows.Forms.TextBox TextBoxIterations;
    }
}
