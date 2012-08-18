using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Enc;

namespace ShockLog
{
    class Logger
    {
        #region Private Fields
        private bool successfulInit; // Represents if BASS was successfully initialised
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
        #endregion
    }
}
