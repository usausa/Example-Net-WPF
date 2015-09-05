namespace Example.WpfApplication.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    using Example.Models;

    /// <summary>
    ///
    /// </summary>
    [ValueConversion(typeof(LogType), typeof(SolidColorBrush))]
    public class LogColorConverter : IValueConverter
    {
        public SolidColorBrush Debug { get; set; }

        public SolidColorBrush Information { get; set; }

        public SolidColorBrush Warning { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var logType = (LogType)value;
            switch (logType)
            {
                case LogType.Debug:
                    return Debug;
                case LogType.Information:
                    return Information;
                case LogType.Warning:
                    return Warning;
            }

            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
