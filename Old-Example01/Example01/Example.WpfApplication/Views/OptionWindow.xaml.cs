namespace Example.WpfApplication.Views
{
    /// <summary>
    /// OptionWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class OptionWindow
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="vm"></param>
        public OptionWindow(OptionViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
