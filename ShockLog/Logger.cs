using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
        private IBaseEncoder encoder = null; // Audio encoder
        private Status currentStatus = Status.NOTLOGGING; // Current streaming status
        private Timer timer = new Timer(1000); // 1 second timer for file management
        private Timer trackTimer = new Timer(30000); // 30 second timer for track logging
        private int currentHour; // Time of hour as last noted by time based functionality in 24 hour format
        private int hourRecordingStarted; // The hour the recording was started in 24 hour format
        private string trackLog; // Track log
        private string currentArtist;
        private string currentTrack;
        #endregion

        #region Properties
        public int Bitrate; // Bitrate of the recordings
        public int Length; // Length of the recordings in hours
        public string Folder; // Location of folder to save recordings in
        public bool WeeklyFolders; // Save recordings in folders specific to each week
        public bool ClearOldLogs; // Clear logs older than a specified age
        public int ClearAge; // Number of days before logs should be cleared, if enabled
        public bool LogTracks; // Log the tracks played from an XML file
        public string TracksFile; // Location of the file to log tracks from
        public Status CurrentStatus // The current status of the logger
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
            BassNet.Registration("orry@orryverducci.co.uk", "2X24373423243720");
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
            // Add timer event handler
            timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            trackTimer.Elapsed += new ElapsedEventHandler(TrackTimerElapsed);
        }

        /// <summary>
        /// Stops logs and shuts down BASS
        /// </summary>
        ~Logger()
        {
            if (currentStatus == Status.LOGGING) // If currently logging
            {
                // Stop logging
                timer.Stop();
                encoder.Stop();
            }
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
            string location;
            // Determine location to save recording to
            if (WeeklyFolders == true) // If option to save into weekly folders is enabled
            {
                CultureInfo cultureInfo = CultureInfo.CurrentCulture; // Get system localisation information
                DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek; // Determine first day of the week
                DateTime firstDayInWeek = DateTime.Now.Date; // Set to current date
                // Keep reducing date until the day is the same as the first day of the week
                while (firstDayInWeek.DayOfWeek != firstDay)
                {
                    firstDayInWeek = firstDayInWeek.AddDays(-1);
                }
                DayOfWeek lastDay = firstDayInWeek.AddDays(-1).DayOfWeek; // Determine last day of the week
                DateTime lastDayInWeek = DateTime.Now.Date; // Set to current date
                // Keep reducing date until the day is the same as the first day of the week
                while (lastDayInWeek.DayOfWeek != lastDay)
                {
                    lastDayInWeek = lastDayInWeek.AddDays(1);
                }
                // Set location
                location = Folder + "\\" + firstDayInWeek.ToString("dd-MM-yy") + " to " + lastDayInWeek.ToString("dd-MM-yy");
            }
            else // If option to save into weekly folders is disabled
            {
                location = Folder;
            }
            // Create location if it does not exist
            if (!Directory.Exists(location))
            {
                Directory.CreateDirectory(location);
            }
            // Set encoder settings
            lameEncoder.InputFile = null; // Set input to Stdout
            lameEncoder.OutputFile = location + "\\Log " + DateTime.Now.ToString("dd-MM-yy HH.mm") + ".mp3"; // Set output file
            lameEncoder.LAME_Bitrate = Bitrate; // Set bitrate
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
            // Start timer
            currentHour = DateTime.Now.Hour;
            hourRecordingStarted = currentHour;
            timer.Start();
            // Start logging tracks
            if (LogTracks)
            {
                trackLog = location + "\\Log " + DateTime.Now.ToString("dd-MM-yy HH.mm") + ".txt";
                if (System.IO.File.Exists(TracksFile))
                {
                    string[] trackFile = System.IO.File.ReadAllLines(TracksFile);
                    currentArtist = trackFile[1];
                    currentTrack = trackFile[2];
                }
                trackTimer.Start();
            }
        }

        /// <summary>
        /// Stop logging
        /// </summary>
        public void Stop()
        {
            // Stop timer
            timer.Stop();
            // Stop track logging
            if (LogTracks)
            {
                trackTimer.Stop();
            }
            // Stop encoder
            encoder.Stop();
            encoder = null;
            CurrentStatus = Status.NOTLOGGING;
        }
        #endregion

        #region Time based functionality
        /// <summary>
        /// Determines if it is the top of a new hour to run file management functionality
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (currentHour != DateTime.Now.Hour) // If hour has changed since last second
            {
                int newFileTime;
                // Update current hour
                currentHour = DateTime.Now.Hour;
                // Determine time new file should be started
                newFileTime = hourRecordingStarted + Length;
                if (newFileTime > 23) // If calculated value flows past the 24 hour clock
                {
                    newFileTime = newFileTime - 24; // Correct overflow past 24 hours
                }
                // Start new file if neccessary
                if (currentHour == newFileTime) // If hour matches the time to start a new file
                {
                    NewFile();
                }
                // Clear old recordings if enabled
                if (ClearOldLogs)
                {
                    ClearLogs(ClearAge);
                }
            }
        }

        /// <summary>
        /// Start recording to a new file
        /// </summary>
        private void NewFile()
        {
            Stop();
            Start();
        }

        /// <summary>
        /// Clears all recordings older than a certain age
        /// </summary>
        /// <param name="age">Age in days that files should be deleted if older than value</param>
        private void ClearLogs(int age)
        {
            // Create background worker to run tasks
            BackgroundWorker worker = new BackgroundWorker();
            // Tasks to run in the background worker
            worker.DoWork += delegate(object s, DoWorkEventArgs args)
            {
                DirectoryInfo folder = new DirectoryInfo(Folder);
                ClearLogs(age, folder);
            };
            // Run background worker
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// Clears all recordings older than a certain age
        /// </summary>
        /// <param name="age">Age in days that files should be deleted if older than value</param>
        /// <param name="folder">Folder in which files to be deleted are contained</param>
        private void ClearLogs(int age, DirectoryInfo folder)
        {
            TimeSpan ageTimeSpan = new TimeSpan(age, 0, 0, 0);
            // Scan all files in the current folder
            foreach (FileInfo file in folder.GetFiles())
            {
                // If current file was created by ShockLog and is older than the specified age
                if (file.Name.StartsWith("Log ") && (file.Name.EndsWith(".mp3") || file.Name.EndsWith(".txt")) && file.CreationTime < DateTime.Now.Subtract(ageTimeSpan))
                {
                    File.Delete(file.FullName);
                }
            }
            // Get subdirectories in the current folder
            DirectoryInfo[] subDirectories = folder.GetDirectories();
            // Scan the folders in the current folders and call this method 
            // again to go one level into the directory tree
            foreach (DirectoryInfo subDirectory in subDirectories)
            {
                ClearLogs(age, subDirectory);
            }
            // If folder is empty after clearing old files, delete folder
            try
            {
                folder.Delete();
            }
            catch (IOException)
            {
                // Folder isn't empty or in use if exception is thrown, do nothing
            }
        }

        /// <summary>
        /// Updates track log if enabled
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        private void TrackTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (System.IO.File.Exists(TracksFile))
            {
                string[] trackFile = System.IO.File.ReadAllLines(TracksFile, Encoding.GetEncoding("Windows-1252"));
                if (trackFile.Length == 3 && (trackFile[0] == "Song" || trackFile[0] == "Cart") && (trackFile[1] != currentArtist || trackFile[2] != currentTrack))
                {
                    currentArtist = trackFile[1];
                    currentTrack = trackFile[2];
                    using (StreamWriter sw = File.AppendText(trackLog))
                    {
                        sw.WriteLine(currentArtist + " - " + currentTrack);
                    }
                }
            }
        }
        #endregion
        #endregion
    }
}
