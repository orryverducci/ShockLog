using System;
using System.Collections.Generic;
using System.IO;
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
        private int recordingHandle; // Handle of the recording stream
        private RECORDPROC recordProc = new RECORDPROC(RecordHandler); // Recording Callback
        private DSP_PeakLevelMeter peakLevelMeter; // Peak Level Meter
        private IBaseEncoder encoder = null;
        private Status currentStatus = Status.NOTLOGGING; // Current streaming status
        #endregion

        #region Properties
        public Status CurrentStatus
        {
            get
            {
                return currentStatus;
            }
            private set
            {
                currentStatus = value;
                if (StatusChange != null)
                {
                    StatusChange(null, new EventArgs());
                }
            }
        }
        #endregion

        #region Events
        public class LevelEventArgs : EventArgs
        {
            public double LeftLevel { get; set; }
            public double RightLevel { get; set; }
        }
        public delegate void LevelEventHandler(object sender, LevelEventArgs e);
        public event LevelEventHandler PeakLevelMeterUpdate;
        public event EventHandler StatusChange;
        #endregion

        #region Enumerations
        public enum Status
        {
            NOTLOGGING,
            LOGGING
        }
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

        #region Logging
        /// <summary>
        /// Start logging
        /// </summary>
        public void Start()
        {
            EncoderLAME lameEncoder = new EncoderLAME(recordingHandle);
            // Set encoder settings
            lameEncoder.InputFile = null; // Set input to Stdout
            lameEncoder.OutputFile = "C:\\log.mp3"; // Set output file
            lameEncoder.LAME_Bitrate = 128; // Set bitrate
            lameEncoder.LAME_Mode = EncoderLAME.LAMEMode.Default; // Number of channels
            lameEncoder.LAME_TargetSampleRate = (int)EncoderLAME.SAMPLERATE.Hz_44100; // Sample rate
            lameEncoder.LAME_Quality = EncoderLAME.LAMEQuality.Quality; // Encoding quality
            // Set encoder location
            if (IntPtr.Size == 8) // If running in 64 bit
            {
                lameEncoder.EncoderDirectory = "x64";
            }
            else // Else if running in 32 bit
            {
                lameEncoder.EncoderDirectory = "x86";
            }
            if (!lameEncoder.EncoderExists) // If encoder has not been found, throw an exception
            {
                throw new FileNotFoundException("Unable to find encoder");
            }
            // Start encoder
            encoder = lameEncoder;
            encoder.Start(null, IntPtr.Zero, false);
            CurrentStatus = Status.LOGGING;
        }

        /// <summary>
        /// Stop logging
        /// </summary>
        public void Stop()
        {
            encoder.Stop();
            encoder = null;
            CurrentStatus = Status.NOTLOGGING;
        }
        #endregion
        #endregion
    }
}
