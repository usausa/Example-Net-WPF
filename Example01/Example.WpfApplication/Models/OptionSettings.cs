namespace Example.WpfApplication.Models
{
    using Smart.ComponentModel;

    /// <summary>
    ///
    /// </summary>
    public class OptionSettings : NotificationObject
    {
        private int counter;

        /// <summary>
        ///
        /// </summary>
        public int Counter
        {
            get { return counter; }
            set
            {
                counter = value;
                RaisePropertyChanged();
            }
        }
    }
}
