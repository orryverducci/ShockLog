using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Enc;
using Un4seen.Bass.Misc;

namespace ShockLog
{
    class Logger
    {
        #region Private Fields
        private bool successfulInit; // Represents if BASS was successfully initialised
        private static int recordingHandle; // Handle of the recording stream
        private static RECORDPROC recordProc = new RECORDPROC(RecordHandler); // Recording Callback
        private static DSP_PeakLevelMeter peakLevelMeter; // Peak Level Meter
        #endregion

        #region Events
        public class LevelEventArgs : EventArgs
        {
            public double LeftLevel { get; set; }
            public double RightLevel { get; set; }
        }
        public delegate void LevelEventHandler(object sender, LevelEventArgs e);
        public event LevelEventHandler PeakLevelMeterUpdate;
        #endregion

        #region Methods
        #region Constructor and destructor
        /// <summary>
        /// Starts up BASS, ready for audio to be logged
        /// </summary>
        public Logger()
        {
            // Registration code for Bass.Net
            // Provided value should be used only for ShockLog, not derived products
            //BassNet.Registration("your mail", "your registration key");
            // Set dll locations
            if (IntPtr.Size == 8) // If running in 64 bit
            {
                Bass.LoadMe("x64");
                BassEnc.LoadMe("x64");
            }
            else // Else if running in 32 bit
            {
                Bass.LoadMe("x86");
                BassEnc.LoadMe("x86");
            }
            // Initialise BASS
            if (!Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_DEFAULT, System.IntPtr.Zero)) // If unable to initialise
            {
                // Throw exception
                successfulInit = false;
                throw new ApplicationException();
            }
            successfulInit = true;
            // Initialise audio input
            if (!Bass.BASS_RecordInit(-1)) // If unable to initialise default input device
            {
                // Throw exception
                throw new ApplicationException();
            }
            // Start recording
            recordingHandle = Bass.BASS_RecordStart(44100, 2, BASSFlag.BASS_SAMPLE_FLOAT, recordProc, IntPtr.Zero);
            // Add Peak Level Meter DSP and Event Handler
            peakLevelMeter = new DSP_PeakLevelMeter(recordingHandle, 1);
            peakLevelMeter.Notification += new EventHandler(PeakLevelMeterNotification);
        }

        /// <summary>
        /// Stops logs and shuts down BASS
        /// </summary>
        ~Logger()
        {
            if (successfulInit) // If BASS has been initialised
            {
                BassEnc.FreeMe(); // Free BASSenc
                Bass.FreeMe(); // Free BASS
            }
        }
        #endregion

        #region Audio input
        /// <summary>
        /// Stub recording callback function
        /// </summary>
        /// <param name="handle">Handle of the recording stream</param>
        /// <param name="buffer">Recording buffer</param>
        /// <param name="length">Length of buffer</param>
        /// <param name="user">User parameters</param>
        /// <returns></returns>
        private static bool RecordHandler(int handle, IntPtr buffer, int length, IntPtr user)
        {
            return true;
        }

        /// <summary>
        /// Triggers event sending peak level meter values
        /// </summary>
        /// <param name="sender">Sending Object</param>
        /// <param name="e">Event argument</param>
        public void PeakLevelMeterNotification(object sender, EventArgs e)
        {
            if (PeakLevelMeterUpdate != null)
            {
                // Create event args containing levels
                LevelEventArgs levelEvent = new LevelEventArgs();
                levelEvent.LeftLevel = peakLevelMeter.LevelL_dBV;
                levelEvent.RightLevel = peakLevelMeter.LevelR_dBV;
                // Trigger event
                PeakLevelMeterUpdate(null, levelEvent);
            }
        }
        #endregion
        #endregion
    }
}
