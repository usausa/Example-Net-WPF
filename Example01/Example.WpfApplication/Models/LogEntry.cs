namespace Example.WpfApplication.Models
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class LogEntry
    {
        public DateTime DateTime { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public LogType LogType { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        public LogEntry(DateTime dateTime, LogType logType, string message)
        {
            DateTime = dateTime;
            LogType = logType;
            Message = message;
        }
    }
}
