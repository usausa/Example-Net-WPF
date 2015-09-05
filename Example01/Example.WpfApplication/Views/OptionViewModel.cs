﻿namespace Example.WpfApplication.Views
{
    using Example.WpfApplication.Models;

    using Smart.ComponentModel;

    /// <summary>
    ///
    /// </summary>
    public class OptionViewModel : NotificationObject
    {
        /// <summary>
        ///
        /// </summary>
        public OptionSettings OptionSettings { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="optionSettings"></param>
        public OptionViewModel(OptionSettings optionSettings)
        {
            OptionSettings = optionSettings;
        }
    }
}
