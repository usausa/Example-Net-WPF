namespace Example.WpfApplication.Views
{
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Text;

    using Example.WpfApplication.Models;

    using Smart.Windows.ViewModels;

    /// <summary>
    ///
    /// </summary>
    public class MainViewModel : DisposableViewModelBase
    {
        private readonly CountWorker countWorker;

        private readonly OptionSettings optionSettings;

        private bool startable;

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
        public bool Startable
        {
            get { return startable; }
            set
            {
                if (startable == value)
                {
                    return;
                }

                startable = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="countWorker"></param>
        /// <param name="optionSettings"></param>
        public MainViewModel(CountWorker countWorker, OptionSettings optionSettings)
        {
            this.countWorker = countWorker;
            this.optionSettings = optionSettings;
            Log = new ObservableCollection<LogEntry>();

            Disposables.Add(Observable
                .FromEvent<EventHandler<LogEventArgs>, LogEventArgs>(h => (s, e) => h(e), h => countWorker.Logged += h, h => countWorker.Logged -= h)
                .ObserveOnDispatcher()
                .Subscribe(e => Log.Add(new LogEntry(e.DateTime, e.LogType, e.Message))));
            Disposables.Add(Observable
                .FromEvent<EventHandler<EventArgs>, EventArgs>(h => (s, e) => h(e), h => countWorker.ExecutingChanged += h, h => countWorker.ExecutingChanged -= h)
                .ObserveOnDispatcher()
                .Subscribe(e => Startable = countWorker.Executing));
        }

        /// <summary>
        ///
        /// </summary>
        public void Start()
        {
            countWorker.Start(optionSettings.Counter);
        }

        /// <summary>
        /// 選択ログのコピー
        /// </summary>
        public void Cancel()
        {
            countWorker.Cancel();
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
