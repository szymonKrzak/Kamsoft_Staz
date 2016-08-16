using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lib.Startup.Data;
using Lib.Startup;
using Lib.Startup.Interfaces;
using System.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ConfigureLoginViewParams param = new ConfigureLoginViewParams { ShowContexts = true, UseProvidedContexts = true, CanChangePassword = true };
            // Wstrzyknięcie kontekstów/środowisk
            param.Contexts.Add("K2PLAN");
            // Ustawienie SelectedContext spowoduje wybranie tego context-u z dropDown-a
            param.SelectedContext = param.Contexts.FirstOrDefault();
            // Ustawienie kodu, nazwy i wersji aplikacji
            param.AppCode = "CLO_US";
            param.AppName = "Moduł Obsługi umów ze Świadczeniodawcami";
            param.AppVersion = "1.0.12.2";
            // Przekazanie ścieżki do logo
            //param.LogoUri = new Uri("pack://application:,,,/AppFramework.InfrastructureServices;component/eSODLogo.png", UriKind.Absolute);


            // Stworzenie okna (może użyć swojego ponieważ logika siedzi w widoku)
            var loginWindow = new LoginWindow(new sss(), param);

            SplashWindow.ShowSplashScreen("aaaa", "bbbb");
            Thread.Sleep(2000);
            SplashWindow.CloseSplashWindow();
            // Możliwość wykonania logowania z pominięciem wyświetlenia okna
            //loginWindow.LoginBroker.Login();
            // Wyświetlenie okna
            loginWindow.ShowDialog();
        }
    }

    public class sss : ILoginAuthService
    {

        public LoginOperationResult SetContext(LoginSelectedContextParams param)
        {
            return new LoginOperationResult() { Result = Lib.Startup.Enums.LoginResultEnum.Success };
        }

        public LoginOperationResult ChangePassword(LoginParams param)
        {
            return new LoginOperationResult() { Result = Lib.Startup.Enums.LoginResultEnum.Success };
        }

        public LoginOperationResult Login(LoginParams param)
        {
            return new LoginOperationResult() { Result = Lib.Startup.Enums.LoginResultEnum.Success };
        }

        public Lib.Startup.Enums.NewPasswordStrengthEnum GetPasswordStrength(LoginParams param)
        {
            return Lib.Startup.Enums.NewPasswordStrengthEnum.NotDefined;
        }
    }
}
