namespace ShockLog
{
    partial class AboutWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutWindow));
            this.okButton = new System.Windows.Forms.Button();
            this.backPanel = new System.Windows.Forms.Panel();
            this.disclaimerLabel = new System.Windows.Forms.Label();
            this.copyrightLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.backPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.CausesValidation = false;
            this.okButton.Location = new System.Drawing.Point(297, 277);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 25);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // backPanel
            // 
            this.backPanel.BackColor = System.Drawing.SystemColors.Window;
            this.backPanel.CausesValidation = false;
            this.backPanel.Controls.Add(this.disclaimerLabel);
            this.backPanel.Controls.Add(this.copyrightLabel);
            this.backPanel.Controls.Add(this.versionLabel);
            this.backPanel.Location = new System.Drawing.Point(0, 0);
            this.backPanel.Name = "backPanel";
            this.backPanel.Size = new System.Drawing.Size(384, 271);
            this.backPanel.TabIndex = 1;
            // 
            // disclaimerLabel
            // 
            this.disclaimerLabel.CausesValidation = false;
            this.disclaimerLabel.Location = new System.Drawing.Point(12, 47);
            this.disclaimerLabel.Name = "disclaimerLabel";
            this.disclaimerLabel.Size = new System.Drawing.Size(360, 224);
            this.disclaimerLabel.TabIndex = 2;
            this.disclaimerLabel.Text = resources.GetString("disclaimerLabel.Text");
            // 
            // copyrightLabel
            // 
            this.copyrightLabel.AutoSize = true;
            this.copyrightLabel.CausesValidation = false;
            this.copyrightLabel.Location = new System.Drawing.Point(12, 22);
            this.copyrightLabel.Name = "copyrightLabel";
            this.copyrightLabel.Size = new System.Drawing.Size(182, 13);
            this.copyrightLabel.TabIndex = 1;
            this.copyrightLabel.Text = "Copyright © Shock Radio 2012-2013";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.CausesValidation = false;
            this.versionLabel.Location = new System.Drawing.Point(12, 9);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(42, 13);
            this.versionLabel.TabIndex = 0;
            this.versionLabel.Text = "Version";
            // 
            // AboutWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(384, 314);
            this.Controls.Add(this.backPanel);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About ShockLog";
            this.Load += new System.EventHandler(this.AboutWindow_Load);
            this.backPanel.ResumeLayout(false);
            this.backPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Panel backPanel;
        private System.Windows.Forms.Label copyrightLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label disclaimerLabel;
    }
}