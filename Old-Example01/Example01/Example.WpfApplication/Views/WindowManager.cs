namespace Example.WpfApplication.Views
{
    using Example.WpfApplication.Infrastructure;

    using Smart.ComponentModel;

    /// <summary>
    ///
    /// </summary>
    public interface IWindowManager
    {
        /// <summary>
        ///
        /// </summary>
        bool OptionVisible { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class WindowManager : NotificationObject, IWindowManager
    {
        private readonly IDependencyResolver resolver;

        private OptionWindow optionWindow;

        /// <summary>
        ///
        /// </summary>
        public bool OptionVisible
        {
            get { return optionWindow != null && optionWindow.IsVisible; }
            set
            {
                if (optionWindow == null)
                {
                    optionWindow = resolver.Resolve<OptionWindow>();
                    optionWindow.Closing += (s, e) =>
                    {
                        e.Cancel = true;
                        OptionVisible = false;
                    };
                }

                if (value)
                {
                    optionWindow.Show();
                }
                else
                {
                    optionWindow.Hide();
                }

                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="resolver"></param>
        public WindowManager(IDependencyResolver resolver)
        {
            this.resolver = resolver;
        }
    }
}
