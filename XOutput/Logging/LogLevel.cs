
namespace XOutput.Logging
{
    /// <summary>
    /// Log levels.
    /// </summary>
    public class LogLevel
    {
        /// <summary>
        /// Trace logger level.
        /// </summary>
        public static readonly LogLevel Trace = new LogLevel("TRACE", 20);
        /// <summary>
        /// Debug logger level.
        /// </summary>
        public static readonly LogLevel Debug = new LogLevel("DEBUG", 40);
        /// <summary>
        /// Info logger level.
        /// </summary>
        public static readonly LogLevel Info = new LogLevel("INFO ", 60);
        /// <summary>
        /// Warning logger level.
        /// </summary>
        public static readonly LogLevel Warning = new LogLevel("WARN ", 80);
        /// <summary>
        /// Error logger level.
        /// </summary>
        public static readonly LogLevel Error = new LogLevel("ERROR", 100);

        public string Text { get; }
        public int Level { get; }

        private LogLevel(string text, int level)
        {
            Text = text;
            Level = level;
        }
    }
}
