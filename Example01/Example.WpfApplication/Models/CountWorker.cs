namespace Example.WpfApplication.Models
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Example.Models;

    /// <summary>
    ///
    /// </summary>
    public sealed class CountWorker : IDisposable
    {
        public event EventHandler<LogEventArgs> Logged;

        public event EventHandler<EventArgs> ExecutingChanged;

        private readonly CoreService coreService;

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
        /// <param name="coreService"></param>
        public CountWorker(CoreService coreService)
        {
            this.coreService = coreService;
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
        /// <param name="counter"></param>
        public void Start(int counter)
        {
            if (Executing)
            {
                return;
            }

            cancel.Reset();
            Executing = true;

            NotifyLog(LogType.Information, "Start.");

            Task.Factory.StartNew(state => Worker((int)state), counter);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="counter"></param>
        private void Worker(int counter)
        {
            try
            {
                while (counter > 0)
                {
                    // キャンセル判定
                    if (cancel.WaitOne(0))
                    {
                        NotifyLog(LogType.Warning, "Canceled.");
                        break;
                    }

                    // コアサービス呼び出し
                    coreService.Something();

                    NotifyLog(LogType.Debug, $"Step. counter=[{counter}]");

                    counter--;
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
