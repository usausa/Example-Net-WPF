namespace Example.WpfApplication.Models
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        public LogType LogType { get; private set; }

        public string Message { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        public LogEventArgs(LogType logType, string message)
        {
            LogType = logType;
            Message = message;
        }
    }
}
