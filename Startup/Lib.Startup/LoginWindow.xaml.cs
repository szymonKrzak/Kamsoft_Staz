using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Lib.Startup.Interfaces;
using Lib.Startup.Data;

namespace Lib.Startup
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, IView
    {
        public LoginWindow(ILoginAuthService authService, ConfigureLoginViewParams param)
        {
            InitializeComponent();
            _loginBroker = new LoginBroker(this, authService, param);
            this.DataContext = _loginBroker.LoginDataContext;
        }

        private LoginBroker _loginBroker;

        public LoginBroker LoginBroker
        {
            get { return _loginBroker; }
        }

        /// <summary>
        /// Obsługa zamknięcia okna logowania
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">argumenty</param>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
