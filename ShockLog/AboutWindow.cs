using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShockLog
{
    public partial class AboutWindow : Form
    {
        public AboutWindow()
        {
            // Set UI font to system font
            this.Font = SystemFonts.MessageBoxFont;
            // Initialise UI
            InitializeComponent();
        }

        private void AboutWindow_Load(object sender, EventArgs e)
        {
            // Display version details on window
            versionLabel.Text = versionLabel.Text + " " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            #if DEBUG
            versionLabel.Text = versionLabel.Text + " (Debug Build)";
            #endif
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
