namespace Example.WpfApplication
{
    using System;
    using System.Windows;
    using Example.Models;

    using Example.WpfApplication.Infrastructure;
    using Example.WpfApplication.Models;
    using Example.WpfApplication.Views;

    using Ninject;

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public sealed partial class App : IDisposable
    {
        private readonly StandardKernel kernel = new StandardKernel();

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            kernel.Dispose();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            RegisterComponents();

            MainWindow = kernel.Get<MainWindow>();
            MainWindow.Show();
        }

        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:スコープを失う前にオブジェクトを破棄", Justification = "Factory")]
        private void RegisterComponents()
        {
            // Infrastructure
            kernel.Bind<IDependencyResolver>().To<NinjectDependencyResolver>().InSingletonScope();

            // Core domain model
            kernel.Bind<LongProcessWorker>().ToSelf().InSingletonScope(); // Singleton

            // Application model
            kernel.Bind<OptionSettings>().ToConstant(new OptionSettings(5, 10));    // Configured object

            // View & ViewModel
            kernel.Bind<OptionViewModel>().ToSelf();
            kernel.Bind<OptionWindow>().ToSelf();

            kernel.Bind<MainViewModel>().ToSelf();
            kernel.Bind<MainWindow>().ToSelf();

            // View service
            kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
        }
    }
}
