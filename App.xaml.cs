using Microsoft.UI.Xaml;

namespace WinUI3App
{
    /// <summary>
    /// Proporciona el comportamiento específico de la aplicación para complementar la clase Application predeterminada.
    /// </summary>
    public partial class App : Application
    {
        private Window? m_window;

        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Se invoca cuando la aplicación se inicia normalmente.
        /// </summary>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }
    }
}
