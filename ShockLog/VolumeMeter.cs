using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ShockCast
{
    public partial class VolumeMeter : Control
    {
        #region Private variables
        private Brush foregroundBrush; // Brush for bar colour
        private float amplitude; // Amplitude of audio
        #endregion

        #region Properties
        /// <summary>
        /// Current Value
        /// </summary>
        [DefaultValue(-3.0)]
        public float Amplitude
        {
            get
            {
                return amplitude;
            }
            set
            {
                amplitude = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Minimum decibels
        /// </summary>
        [DefaultValue(-60.0)]
        public float MinDb { get; set; }

        /// <summary>
        /// Maximum decibels
        /// </summary>
        [DefaultValue(0.0)]
        public float MaxDb { get; set; }

        /// <summary>
        /// Meter orientation
        /// </summary>
        [DefaultValue(Orientation.Vertical)]
        public Orientation Orientation { get; set; }
        #endregion

        /// <summary>
        /// Basic volume meter
        /// </summary>
        public VolumeMeter()
        {
            // Set painting options
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            // Make unselectable
            this.SetStyle(ControlStyles.Selectable, false);
            // Set default values
            MinDb = -60;
            MaxDb = 0;
            Amplitude = 0;
            Orientation = Orientation.Vertical;
            // Initialise UI
            InitializeComponent();
            OnForeColorChanged(EventArgs.Empty);
        }

        /// <summary>
        /// On Fore Color Changed
        /// </summary>
        protected override void OnForeColorChanged(EventArgs e)
        {
            foregroundBrush = new SolidBrush(ForeColor);
            base.OnForeColorChanged(e);
        }

        /// <summary>
        /// Paints the volume meter
        /// </summary>
        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.DrawRectangle(Pens.Black, 0, 0, this.Width - 1, this.Height - 1);

            double db;
            if (Amplitude >= MinDb && Amplitude <= MaxDb)
            {
                db = Amplitude;
            }
            else if (Amplitude < MinDb)
            {
                db = MinDb;
            }
            else
            {
                db = MaxDb;
            }
            double multiple = Math.Pow(10, (db / 20));

            int width = this.Width - 2;
            int height = this.Height - 2;
            if (Orientation == Orientation.Horizontal)
            {
                width = (int)(width * multiple);
                pe.Graphics.FillRectangle(foregroundBrush, 1, 1, width, height);
            }
            else
            {
                height = (int)(height * multiple);
                pe.Graphics.FillRectangle(foregroundBrush, 1, this.Height - 1 - height, width, height);
            }
        }
    }
}
