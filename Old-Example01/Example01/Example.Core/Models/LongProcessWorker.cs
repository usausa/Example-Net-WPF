namespace Example.Models
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///
    /// </summary>
    public sealed class LongProcessWorker : IDisposable
    {
        public event EventHandler<LogEventArgs> Logged;

        public event EventHandler<EventArgs> ExecutingChanged;

        private readonly ManualResetEvent executing = new ManualResetEvent(false);

        private readonly ManualResetEvent cancel = new ManualResetEvent(false);

        /// <summary>
        ///
        /// </summary>
        public bool Executing
        {
            get { return executing.WaitOne(0); }
            set
            {
                if (value)
                {
                    executing.Set();
                }
                else
                {
                    executing.Reset();
                }

                ExecutingChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            cancel.Dispose();
            executing.Dispose();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        private void NotifyLog(LogType logType, string message)
        {
            Logged?.Invoke(this, new LogEventArgs(DateTime.Now, logType, message));
        }

        /// <summary>
        ///
        /// </summary>
        public void Cancel()
        {
            cancel.Set();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="level"></param>
        public void Start(int level)
        {
            if (Executing)
            {
                return;
            }

            cancel.Reset();
            Executing = true;

            NotifyLog(LogType.Information, "Start.");

            Task.Factory.StartNew(state => Worker((int)state), level);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="level"></param>
        private void Worker(int level)
        {
            try
            {
                while (level > 0)
                {
                    if (cancel.WaitOne(0))
                    {
                        NotifyLog(LogType.Warning, "Canceled.");
                        break;
                    }

                    Thread.Sleep(1000);

                    NotifyLog(LogType.Debug, $"Step. remain=[{level}]");

                    level--;
                }
            }
            finally
            {
                Executing = false;

                NotifyLog(LogType.Information, "Stop.");
            }
        }
    }
}
