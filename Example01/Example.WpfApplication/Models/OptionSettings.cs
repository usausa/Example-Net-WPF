namespace Example.WpfApplication.Models
{
    using Smart.ComponentModel;

    /// <summary>
    ///
    /// </summary>
    public class OptionSettings : NotificationObject
    {
        private readonly int max;

        private int counter;

        /// <summary>
        ///
        /// </summary>
        public int Counter
        {
            get { return counter; }
            private set
            {
                if (counter == value)
                {
                    return;
                }

                counter = value;
                RaisePropertyChanged(nameof(Counter));
                RaisePropertyChanged(nameof(IncrementEnable));
                RaisePropertyChanged(nameof(DecrementEnable));
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool IncrementEnable => counter < max;

        /// <summary>
        ///
        /// </summary>
        public bool DecrementEnable => counter > 1;

        /// <summary>
        ///
        /// </summary>
        /// <param name="counter"></param>
        /// <param name="max"></param>
        public OptionSettings(int counter, int max)
        {
            this.max = max;
            this.counter = counter;
        }

        /// <summary>
        ///
        /// </summary>
        public void Increment()
        {
            if (!IncrementEnable)
            {
                return;
            }

            Counter++;
        }

        /// <summary>
        ///
        /// </summary>
        public void Decrement()
        {
            if (!DecrementEnable)
            {
                return;
            }

            Counter--;
        }
    }
}
