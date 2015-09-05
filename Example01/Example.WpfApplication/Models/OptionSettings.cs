namespace Example.WpfApplication.Models
{
    using Smart.ComponentModel;

    /// <summary>
    ///
    /// </summary>
    public class OptionSettings : NotificationObject
    {
        private readonly int max;

        private int level;

        /// <summary>
        ///
        /// </summary>
        public int Level
        {
            get { return level; }
            private set
            {
                if (level == value)
                {
                    return;
                }

                level = value;
                RaisePropertyChanged(nameof(Level));
                RaisePropertyChanged(nameof(IncrementEnable));
                RaisePropertyChanged(nameof(DecrementEnable));
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool IncrementEnable => level < max;

        /// <summary>
        ///
        /// </summary>
        public bool DecrementEnable => level > 1;

        /// <summary>
        ///
        /// </summary>
        /// <param name="level"></param>
        /// <param name="max"></param>
        public OptionSettings(int level, int max)
        {
            this.max = max;
            this.level = level;
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

            Level++;
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

            Level--;
        }
    }
}
