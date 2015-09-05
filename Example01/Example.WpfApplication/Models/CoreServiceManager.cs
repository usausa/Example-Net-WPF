namespace Example.WpfApplication.Models
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Example.Models;

    /// <summary>
    ///
    /// </summary>
    public sealed class CoreServiceManager : IDisposable
    {
        public event EventHandler<LogEventArgs> Logged;

        public event EventHandler<EventArgs> ExecutingChanged;

        private readonly CoreService coreService;

        private readonly ManualResetEvent executing = new ManualResetEvent(false);

        private readonly ManualResetEvent cancel = new ManualResetEvent(false);

        /// <summary>
        ///
        /// </summary>
        public bool Executing => executing.WaitOne(0);

        /// <summary>
        ///
        /// </summary>
        /// <param name="coreService"></param>
        public CoreServiceManager(CoreService coreService)
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
            Logged?.Invoke(this, new LogEventArgs(logType, message));
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
            executing.Set();
            ExecutingChanged?.Invoke(this, EventArgs.Empty);

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
                executing.Reset();
                ExecutingChanged?.Invoke(this, EventArgs.Empty);

                NotifyLog(LogType.Information, "Stop.");
            }
        }
    }
}
