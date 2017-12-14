namespace Example.WpfApplication.Views
{
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Text;

    using Example.Models;
    using Example.WpfApplication.Models;

    using Smart.Windows.ViewModels;

    /// <summary>
    ///
    /// </summary>
    public class MainViewModel : DisposableViewModelBase
    {
        private readonly LongProcessWorker worker;

        private readonly OptionSettings optionSettings;

        private bool running;

        /// <summary>
        ///
        /// </summary>
        public IWindowManager WindowManager { get; }

        /// <summary>
        ///
        /// </summary>
        public ObservableCollection<LogEntry> Log { get; }

        /// <summary>
        ///
        /// </summary>
        public IEnumerable SelectedLog { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool Running
        {
            get { return running; }
            set { SetProperty(ref running, value); }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="optionSettings"></param>
        /// <param name="windowManager"></param>
        public MainViewModel(LongProcessWorker worker, OptionSettings optionSettings, IWindowManager windowManager)
        {
            this.worker = worker;
            this.optionSettings = optionSettings;
            Log = new ObservableCollection<LogEntry>();
            WindowManager = windowManager;

            Disposables.Add(Observable
                .FromEvent<EventHandler<LogEventArgs>, LogEventArgs>(h => (s, e) => h(e), h => worker.Logged += h, h => worker.Logged -= h)
                .ObserveOnDispatcher()
                .Subscribe(e => Log.Add(new LogEntry(e.DateTime, e.LogType, e.Message))));
            Disposables.Add(Observable
                .FromEvent<EventHandler<EventArgs>, EventArgs>(h => (s, e) => h(e), h => worker.ExecutingChanged += h, h => worker.ExecutingChanged -= h)
                .ObserveOnDispatcher()
                .Subscribe(e => Running = worker.Executing));
        }

        /// <summary>
        ///
        /// </summary>
        public void Start()
        {
            worker.Start(optionSettings.Level);
        }

        /// <summary>
        ///
        /// </summary>
        public void Cancel()
        {
            worker.Cancel();
        }

        /// <summary>
        ///
        /// </summary>
        public void Clear()
        {
            Log.Clear();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string Copy()
        {
            var text = new StringBuilder();

            foreach (var entry in SelectedLog.Cast<LogEntry>())
            {
                text.Append(entry.DateTime.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture));
                text.Append(" | ");
                text.AppendFormat("{0, -11}", entry.LogType);
                text.Append(" | ");
                text.Append(entry.Message);
                text.Append(Environment.NewLine);
            }

            return text.ToString();
        }
    }
}
