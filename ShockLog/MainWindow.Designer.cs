namespace ShockLog
{
    partial class MainWindow
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
            this.statusLabel = new System.Windows.Forms.Label();
            this.stopStartButton = new System.Windows.Forms.Button();
            this.expanderLabel = new System.Windows.Forms.Label();
            this.bitrateUpDown = new System.Windows.Forms.NumericUpDown();
            this.bitrateLabel = new System.Windows.Forms.Label();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.lengthUpDown = new System.Windows.Forms.NumericUpDown();
            this.browseButton = new System.Windows.Forms.Button();
            this.folderLabel = new System.Windows.Forms.Label();
            this.organiseCheckBox = new System.Windows.Forms.CheckBox();
            this.clearCheckBox = new System.Windows.Forms.CheckBox();
            this.clearUpDown = new System.Windows.Forms.NumericUpDown();
            this.clearLabel = new System.Windows.Forms.Label();
            this.expanderCheckBox = new System.Windows.Forms.CheckBox();
            this.folderSeperator = new ShockCast.Seperator();
            this.fileSeperator = new ShockCast.Seperator();
            this.statusSeperator = new ShockCast.Seperator();
            this.leftVolumeMeter = new ShockCast.VolumeMeter();
            this.rightVolumeMeter = new ShockCast.VolumeMeter();
            ((System.ComponentModel.ISupportInitialize)(this.bitrateUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lengthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clearUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // statusLabel
            // 
            this.statusLabel.AutoEllipsis = true;
            this.statusLabel.CausesValidation = false;
            this.statusLabel.Location = new System.Drawing.Point(12, 33);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(208, 21);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "Not Logging";
            // 
            // stopStartButton
            // 
            this.stopStartButton.CausesValidation = false;
            this.stopStartButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.stopStartButton.Location = new System.Drawing.Point(15, 57);
            this.stopStartButton.Name = "stopStartButton";
            this.stopStartButton.Size = new System.Drawing.Size(100, 25);
            this.stopStartButton.TabIndex = 2;
            this.stopStartButton.Text = "Start Logging";
            this.stopStartButton.UseVisualStyleBackColor = true;
            this.stopStartButton.Click += new System.EventHandler(this.stopStartButton_Click);
            // 
            // expanderLabel
            // 
            this.expanderLabel.AutoSize = true;
            this.expanderLabel.CausesValidation = false;
            this.expanderLabel.Location = new System.Drawing.Point(46, 94);
            this.expanderLabel.Name = "expanderLabel";
            this.expanderLabel.Size = new System.Drawing.Size(73, 13);
            this.expanderLabel.TabIndex = 4;
            this.expanderLabel.Text = "Show Options";
            // 
            // bitrateUpDown
            // 
            this.bitrateUpDown.CausesValidation = false;
            this.bitrateUpDown.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.bitrateUpDown.Location = new System.Drawing.Point(55, 140);
            this.bitrateUpDown.Maximum = new decimal(new int[] {
            320,
            0,
            0,
            0});
            this.bitrateUpDown.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.bitrateUpDown.Name = "bitrateUpDown";
            this.bitrateUpDown.Size = new System.Drawing.Size(60, 20);
            this.bitrateUpDown.TabIndex = 9;
            this.bitrateUpDown.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.bitrateUpDown.ValueChanged += new System.EventHandler(this.bitrateUpDown_ValueChanged);
            // 
            // bitrateLabel
            // 
            this.bitrateLabel.AutoSize = true;
            this.bitrateLabel.CausesValidation = false;
            this.bitrateLabel.Location = new System.Drawing.Point(12, 142);
            this.bitrateLabel.Name = "bitrateLabel";
            this.bitrateLabel.Size = new System.Drawing.Size(37, 13);
            this.bitrateLabel.TabIndex = 8;
            this.bitrateLabel.Text = "Bitrate";
            // 
            // lengthLabel
            // 
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.CausesValidation = false;
            this.lengthLabel.Location = new System.Drawing.Point(127, 142);
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size(75, 13);
            this.lengthLabel.TabIndex = 10;
            this.lengthLabel.Text = "Length (hours)";
            // 
            // lengthUpDown
            // 
            this.lengthUpDown.CausesValidation = false;
            this.lengthUpDown.Location = new System.Drawing.Point(212, 140);
            this.lengthUpDown.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.lengthUpDown.Name = "lengthUpDown";
            this.lengthUpDown.Size = new System.Drawing.Size(60, 20);
            this.lengthUpDown.TabIndex = 11;
            // 
            // browseButton
            // 
            this.browseButton.CausesValidation = false;
            this.browseButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.browseButton.Location = new System.Drawing.Point(202, 187);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(70, 25);
            this.browseButton.TabIndex = 14;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            // 
            // folderLabel
            // 
            this.folderLabel.AutoEllipsis = true;
            this.folderLabel.CausesValidation = false;
            this.folderLabel.Location = new System.Drawing.Point(12, 192);
            this.folderLabel.Name = "folderLabel";
            this.folderLabel.Size = new System.Drawing.Size(184, 21);
            this.folderLabel.TabIndex = 13;
            // 
            // organiseCheckBox
            // 
            this.organiseCheckBox.AutoSize = true;
            this.organiseCheckBox.CausesValidation = false;
            this.organiseCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.organiseCheckBox.Location = new System.Drawing.Point(15, 218);
            this.organiseCheckBox.Name = "organiseCheckBox";
            this.organiseCheckBox.Size = new System.Drawing.Size(193, 18);
            this.organiseCheckBox.TabIndex = 15;
            this.organiseCheckBox.Text = "Organise folder into weekly folders";
            this.organiseCheckBox.UseVisualStyleBackColor = true;
            // 
            // clearCheckBox
            // 
            this.clearCheckBox.AutoSize = true;
            this.clearCheckBox.CausesValidation = false;
            this.clearCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.clearCheckBox.Location = new System.Drawing.Point(15, 242);
            this.clearCheckBox.Name = "clearCheckBox";
            this.clearCheckBox.Size = new System.Drawing.Size(168, 18);
            this.clearCheckBox.TabIndex = 16;
            this.clearCheckBox.Text = "Delete recordings older than:";
            this.clearCheckBox.UseVisualStyleBackColor = true;
            // 
            // clearUpDown
            // 
            this.clearUpDown.CausesValidation = false;
            this.clearUpDown.Location = new System.Drawing.Point(32, 266);
            this.clearUpDown.Maximum = new decimal(new int[] {
            730,
            0,
            0,
            0});
            this.clearUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.clearUpDown.Name = "clearUpDown";
            this.clearUpDown.Size = new System.Drawing.Size(60, 20);
            this.clearUpDown.TabIndex = 17;
            this.clearUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // clearLabel
            // 
            this.clearLabel.AutoSize = true;
            this.clearLabel.CausesValidation = false;
            this.clearLabel.Location = new System.Drawing.Point(98, 268);
            this.clearLabel.Name = "clearLabel";
            this.clearLabel.Size = new System.Drawing.Size(46, 13);
            this.clearLabel.TabIndex = 18;
            this.clearLabel.Text = "days old";
            // 
            // expanderCheckBox
            // 
            this.expanderCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.expanderCheckBox.CausesValidation = false;
            this.expanderCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.expanderCheckBox.Location = new System.Drawing.Point(15, 88);
            this.expanderCheckBox.Name = "expanderCheckBox";
            this.expanderCheckBox.Size = new System.Drawing.Size(25, 25);
            this.expanderCheckBox.TabIndex = 3;
            this.expanderCheckBox.Text = "⏬";
            this.expanderCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.expanderCheckBox.UseVisualStyleBackColor = true;
            this.expanderCheckBox.CheckedChanged += new System.EventHandler(this.expanderCheckBox_CheckedChanged);
            // 
            // folderSeperator
            // 
            this.folderSeperator.Label = "Folder Options";
            this.folderSeperator.Location = new System.Drawing.Point(12, 166);
            this.folderSeperator.Name = "folderSeperator";
            this.folderSeperator.Size = new System.Drawing.Size(260, 15);
            this.folderSeperator.TabIndex = 12;
            // 
            // fileSeperator
            // 
            this.fileSeperator.Label = "File Options";
            this.fileSeperator.Location = new System.Drawing.Point(12, 119);
            this.fileSeperator.Name = "fileSeperator";
            this.fileSeperator.Size = new System.Drawing.Size(260, 15);
            this.fileSeperator.TabIndex = 7;
            // 
            // statusSeperator
            // 
            this.statusSeperator.Label = "Status";
            this.statusSeperator.Location = new System.Drawing.Point(12, 12);
            this.statusSeperator.Name = "statusSeperator";
            this.statusSeperator.Size = new System.Drawing.Size(208, 15);
            this.statusSeperator.TabIndex = 0;
            // 
            // leftVolumeMeter
            // 
            this.leftVolumeMeter.Amplitude = -60F;
            this.leftVolumeMeter.CausesValidation = false;
            this.leftVolumeMeter.ForeColor = System.Drawing.Color.YellowGreen;
            this.leftVolumeMeter.Location = new System.Drawing.Point(234, 12);
            this.leftVolumeMeter.MaxDb = 0F;
            this.leftVolumeMeter.MinDb = -60F;
            this.leftVolumeMeter.Name = "leftVolumeMeter";
            this.leftVolumeMeter.Size = new System.Drawing.Size(16, 101);
            this.leftVolumeMeter.TabIndex = 5;
            // 
            // rightVolumeMeter
            // 
            this.rightVolumeMeter.Amplitude = -60F;
            this.rightVolumeMeter.CausesValidation = false;
            this.rightVolumeMeter.ForeColor = System.Drawing.Color.YellowGreen;
            this.rightVolumeMeter.Location = new System.Drawing.Point(256, 12);
            this.rightVolumeMeter.MaxDb = 0F;
            this.rightVolumeMeter.MinDb = -60F;
            this.rightVolumeMeter.Name = "rightVolumeMeter";
            this.rightVolumeMeter.Size = new System.Drawing.Size(16, 101);
            this.rightVolumeMeter.TabIndex = 6;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(284, 300);
            this.Controls.Add(this.expanderCheckBox);
            this.Controls.Add(this.clearLabel);
            this.Controls.Add(this.clearUpDown);
            this.Controls.Add(this.clearCheckBox);
            this.Controls.Add(this.organiseCheckBox);
            this.Controls.Add(this.folderLabel);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.folderSeperator);
            this.Controls.Add(this.lengthLabel);
            this.Controls.Add(this.lengthUpDown);
            this.Controls.Add(this.bitrateLabel);
            this.Controls.Add(this.bitrateUpDown);
            this.Controls.Add(this.fileSeperator);
            this.Controls.Add(this.expanderLabel);
            this.Controls.Add(this.stopStartButton);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.statusSeperator);
            this.Controls.Add(this.leftVolumeMeter);
            this.Controls.Add(this.rightVolumeMeter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.Text = "ShockLog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bitrateUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lengthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clearUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ShockCast.VolumeMeter rightVolumeMeter;
        private ShockCast.VolumeMeter leftVolumeMeter;
        private ShockCast.Seperator statusSeperator;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button stopStartButton;
        private System.Windows.Forms.Label expanderLabel;
        private ShockCast.Seperator fileSeperator;
        private System.Windows.Forms.NumericUpDown bitrateUpDown;
        private System.Windows.Forms.Label bitrateLabel;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.NumericUpDown lengthUpDown;
        private ShockCast.Seperator folderSeperator;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Label folderLabel;
        private System.Windows.Forms.CheckBox organiseCheckBox;
        private System.Windows.Forms.CheckBox clearCheckBox;
        private System.Windows.Forms.NumericUpDown clearUpDown;
        private System.Windows.Forms.Label clearLabel;
        private System.Windows.Forms.CheckBox expanderCheckBox;
    }
}

