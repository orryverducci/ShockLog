using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShockLog
{
    public partial class MainWindow : Form
    {
        #region DllImport
        // Import Win32 API system menu functions
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        private static extern bool InsertMenu(IntPtr hMenu, Int32 wPosition, Int32 wFlags, Int32 wIDNewItem, string lpNewItem);
        public const Int32 WM_SYSCOMMAND = 0x112;
        public const Int32 MF_SEPARATOR = 0x800;
        public const Int32 MF_BYPOSITION = 0x400;
        public const Int32 MF_STRING = 0x0;
        public const Int32 IDM_ABOUT = 1000;
        #endregion

        #region Private Fields
        private Logger logger = new Logger();
        #endregion

        #region Form Load and Close
        public MainWindow()
        {
            // Set UI font to system font
            this.Font = SystemFonts.MessageBoxFont;
            // Initialise UI
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            // Add 'About' to System Menu
            IntPtr sysMenuHandle = GetSystemMenu(this.Handle, false);
            InsertMenu(sysMenuHandle, 5, MF_BYPOSITION | MF_SEPARATOR, 0, string.Empty);
            InsertMenu(sysMenuHandle, 6, MF_BYPOSITION, IDM_ABOUT, "About ShockLog...");
            // Add event handlers
            logger.PeakLevelMeterUpdate += new Logger.LevelEventHandler(LevelUpdate);
            logger.StatusChange += new EventHandler(StatusUpdate);
            // Set initial NumericUpDown values in logger
            logger.Bitrate = 32;
            logger.Length = 1;
            logger.ClearAge = 1;
            // Load settings
            bitrateUpDown.Value = Properties.Settings.Default.Bitrate;
            lengthUpDown.Value = Properties.Settings.Default.Length;
            if (Properties.Settings.Default.Folder == String.Empty) // If a folder has not been set, set to My Music
            {
                folderLabel.Text = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic);
            }
            else // Else if folder has been set, use it
            {
                folderLabel.Text = Properties.Settings.Default.Folder;
            }
            organiseCheckBox.Checked = Properties.Settings.Default.OrganiseFolder;
            clearCheckBox.Checked = Properties.Settings.Default.DeleteOld;
            clearUpDown.Value = Properties.Settings.Default.DeleteTime;
            autoCheckBox.Checked = Properties.Settings.Default.AutoLog;
            playingCheckBox.Checked = Properties.Settings.Default.LogPlaylist;
            playingLabel.Text = Properties.Settings.Default.PlaylistFile;
            if (autoCheckBox.Checked)
            {
                logger.Start();
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save settings
            Properties.Settings.Default.Bitrate = (int)bitrateUpDown.Value;
            Properties.Settings.Default.Length = (int)lengthUpDown.Value;
            Properties.Settings.Default.Folder = folderLabel.Text;
            Properties.Settings.Default.OrganiseFolder = organiseCheckBox.Checked;
            Properties.Settings.Default.DeleteOld = clearCheckBox.Checked;
            Properties.Settings.Default.DeleteTime = (int)clearUpDown.Value;
            Properties.Settings.Default.AutoLog = autoCheckBox.Checked;
            Properties.Settings.Default.LogPlaylist = playingCheckBox.Checked;
            Properties.Settings.Default.PlaylistFile = playingLabel.Text;
            Properties.Settings.Default.Save();
        }
        #endregion

        /// <summary>
        /// WndProc override to respond to About window click on System Menu
        /// </summary>
        /// <param name="m">WndProc message</param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND) // If WndProc message is system menu item click
            {
                switch (m.WParam.ToInt32()) // For item clicked
                {
                    case IDM_ABOUT: // If about item
                        // Open about window
                        AboutWindow aboutWindow = new AboutWindow();
                        aboutWindow.ShowDialog();
                        return;
                    default:
                        break;
                }
            }
            // Run base function
            base.WndProc(ref m);
        }

        #region Buttons
        /// <summary>
        /// Starts or stops the logger depending on the logger state
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        private void stopStartButton_Click(object sender, EventArgs e)
        {
            if (logger.CurrentStatus == Logger.Status.NOTLOGGING) // If not logging
            {
                logger.Start(); // Start the logger
            }
            else if (logger.CurrentStatus == Logger.Status.LOGGING) // If currently logging
            {
                logger.Stop(); // Stop the logger
            }
        }

        /// <summary>
        /// Changes window size based on whether check box is checked, revealing or hiding options
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        private void expanderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked) // If checked to show more options
            {
                this.Height = 438;
                ((CheckBox)sender).Image = Properties.Resources.up;
            }
            else // Else if not checked
            {
                this.Height = 160;
                ((CheckBox)sender).Image = Properties.Resources.down;
            }
        }

        /// <summary>
        /// Opens browse dialog and uses result to set logging location
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event argument</param>
        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) // If OK is chosen on folder browse dialog
            {
                folderLabel.Text = folderDialog.SelectedPath; // Set location to selected path
            }
        }

        /// <summary>
        /// Opens browse dialog and uses result to set playlist file
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event argument</param>
        private void playingBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*";
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) // If OK is chosen on file browse dialog
            {
                playingLabel.Text = fileDialog.FileName;
            }

        }
        #endregion

        #region Settings Changes
        /// <summary>
        /// Updates logger bitrate with sender value
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        private void bitrateUpDown_ValueChanged(object sender, EventArgs e)
        {
            logger.Bitrate = (int)((NumericUpDown)sender).Value;
        }

        /// <summary>
        /// Updates length to be used by logger with sender value
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        private void lengthUpDown_ValueChanged(object sender, EventArgs e)
        {
            logger.Length = (int)((NumericUpDown)sender).Value;
        }

        /// <summary>
        /// Updates logging location with sender text
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        private void folderLabel_TextChanged(object sender, EventArgs e)
        {
            logger.Folder = ((Label)sender).Text;
        }

        /// <summary>
        /// Updates weekly folders setting depending on if selected checkbox is checked
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        private void organiseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            logger.WeeklyFolders = ((CheckBox)sender).Checked;
        }

        /// <summary>
        /// Updates if old files should be deleted depending on if selected checkbox is checked
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        private void clearCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            logger.ClearOldLogs = ((CheckBox)sender).Checked;
        }

        /// <summary>
        /// Updates number of days until files are deleted according to sender value
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        private void clearUpDown_ValueChanged(object sender, EventArgs e)
        {
            logger.ClearAge = (int)((NumericUpDown)sender).Value;
        }

        /// <summary>
        /// Updates if the playing tracks should be logged depending on if selected checkbox is checked
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        private void playingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            logger.LogTracks = ((CheckBox)sender).Checked;
        }

        /// <summary>
        /// Updates track logging file location with sender text
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        private void playingLabel_TextChanged(object sender, EventArgs e)
        {
            logger.TracksFile = ((Label)sender).Text;
        }
        #endregion

        /// <summary>
        /// Update levels on the volume meters
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        private void LevelUpdate(object sender, Logger.LevelEventArgs e)
        {
            if (IsHandleCreated) // If the window has a handle
            {
                // Invoke on form thread
                BeginInvoke((MethodInvoker)delegate
                {
                    leftVolumeMeter.Amplitude = (float)e.LeftLevel;
                    rightVolumeMeter.Amplitude = (float)e.RightLevel;
                });
            }
        }

        /// <summary>
        /// Update the window to reflect encoder status
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        private void StatusUpdate(object sender, EventArgs e)
        {
            // Invokes required as timer events run on seperate threads
            if (logger.CurrentStatus == Logger.Status.LOGGING) // If logging
            {
                // Change text to logging
                statusLabel.Invoke(new MethodInvoker(delegate { statusLabel.Text = "Logging"; }));
                stopStartButton.Invoke(new MethodInvoker(delegate { stopStartButton.Text = "Stop Logging"; }));
                // Disable options
                fileSeperator.Invoke(new MethodInvoker(delegate { fileSeperator.Enabled = false; }));
                bitrateLabel.Invoke(new MethodInvoker(delegate { bitrateLabel.Enabled = false; }));
                bitrateUpDown.Invoke(new MethodInvoker(delegate { bitrateUpDown.Enabled = false; }));
                lengthLabel.Invoke(new MethodInvoker(delegate { lengthLabel.Enabled = false; }));
                lengthUpDown.Invoke(new MethodInvoker(delegate { lengthUpDown.Enabled = false; }));
                folderSeperator.Invoke(new MethodInvoker(delegate { folderSeperator.Enabled = false; }));
                folderLabel.Invoke(new MethodInvoker(delegate { folderLabel.Enabled = false; }));
                browseButton.Invoke(new MethodInvoker(delegate { browseButton.Enabled = false; }));
                organiseCheckBox.Invoke(new MethodInvoker(delegate { organiseCheckBox.Enabled = false; }));
                clearCheckBox.Invoke(new MethodInvoker(delegate { clearCheckBox.Enabled = false; }));
                clearUpDown.Invoke(new MethodInvoker(delegate { clearUpDown.Enabled = false; }));
                clearLabel.Invoke(new MethodInvoker(delegate { clearLabel.Enabled = false; }));
                logSeperator.Invoke(new MethodInvoker(delegate { logSeperator.Enabled = false; }));
                autoCheckBox.Invoke(new MethodInvoker(delegate { autoCheckBox.Enabled = false; }));
                playingCheckBox.Invoke(new MethodInvoker(delegate { playingCheckBox.Enabled = false; }));
                playingLabel.Invoke(new MethodInvoker(delegate { playingLabel.Enabled = false; }));
                playingBrowseButton.Invoke(new MethodInvoker(delegate { playingBrowseButton.Enabled = false; }));
            }
            else if (logger.CurrentStatus == Logger.Status.NOTLOGGING) // If not logging
            {
                // Change text to not logging
                statusLabel.Invoke(new MethodInvoker(delegate { statusLabel.Text = "Not Logging"; }));
                stopStartButton.Invoke(new MethodInvoker(delegate { stopStartButton.Text = "Start Logging"; }));
                // Enable options
                fileSeperator.Invoke(new MethodInvoker(delegate { fileSeperator.Enabled = true; }));
                bitrateLabel.Invoke(new MethodInvoker(delegate { bitrateLabel.Enabled = true; }));
                bitrateUpDown.Invoke(new MethodInvoker(delegate { bitrateUpDown.Enabled = true; }));
                lengthLabel.Invoke(new MethodInvoker(delegate { lengthLabel.Enabled = true; }));
                lengthUpDown.Invoke(new MethodInvoker(delegate { lengthUpDown.Enabled = true; }));
                folderSeperator.Invoke(new MethodInvoker(delegate { folderSeperator.Enabled = true; }));
                folderLabel.Invoke(new MethodInvoker(delegate { folderLabel.Enabled = true; }));
                browseButton.Invoke(new MethodInvoker(delegate { browseButton.Enabled = true; }));
                organiseCheckBox.Invoke(new MethodInvoker(delegate { organiseCheckBox.Enabled = true; }));
                clearCheckBox.Invoke(new MethodInvoker(delegate { clearCheckBox.Enabled = true; }));
                clearUpDown.Invoke(new MethodInvoker(delegate { clearUpDown.Enabled = true; }));
                clearLabel.Invoke(new MethodInvoker(delegate { clearLabel.Enabled = true; }));
                logSeperator.Invoke(new MethodInvoker(delegate { logSeperator.Enabled = true; }));
                autoCheckBox.Invoke(new MethodInvoker(delegate { autoCheckBox.Enabled = true; }));
                playingCheckBox.Invoke(new MethodInvoker(delegate { playingCheckBox.Enabled = true; }));
                playingLabel.Invoke(new MethodInvoker(delegate { playingLabel.Enabled = true; }));
                playingBrowseButton.Invoke(new MethodInvoker(delegate { playingBrowseButton.Enabled = true; }));
            }
        }
    }
}
