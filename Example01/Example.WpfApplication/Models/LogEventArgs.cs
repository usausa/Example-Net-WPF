namespace Example.WpfApplication.Models
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        public DateTime DateTime { get; private set; }

        public LogType LogType { get; private set; }

        public string Message { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        public LogEventArgs(DateTime dateTime, LogType logType, string message)
        {
            DateTime = dateTime;
            LogType = logType;
            Message = message;
        }
    }
}
