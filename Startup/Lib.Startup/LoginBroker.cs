namespace Lib.Startup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Lib.Startup.Interfaces;
    using Lib.Startup.Data;
    using Lib.Startup.Enums;

    /// <summary>
    /// Pośrednik w procesie logowania, klasa wstrzykuje ViewModel, udostępnia informacje o zalogowanym użytkowniku, pozwala na logowanie
    /// Klasa wprowadzona w celu uniemożliwienia użycia okienka logowania w niechciany sposób
    /// </summary>
    public class LoginBroker
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="view">Referencja do okna wyświetlającego widok logowania</param>
        /// <param name="authService">Serwis obsługujący logowanie</param>
        /// <param name="param">Parametry konfigurujące widok logowania</param>
        public LoginBroker(IView view, ILoginAuthService authService, ConfigureLoginViewParams param)
        {
            _loginVM = new LoginVM(view, authService, param);
        }

        private LoginVM _loginVM;

        /// <summary>
        /// Data contekst do ustawienia na okienku logowania
        /// </summary>
        public LoginVM LoginDataContext 
        {
            get { return _loginVM; } 
        }

        /// <summary>
        /// Programowe wykonanie logowania. Wywołana zostanie metoda podpięta pod guzik logowania
        /// </summary>
        public void Login()
        {
            _loginVM.LoginCommand.Execute(string.Empty);
        }

        /// <summary>
        /// Wynik operacji logowania
        /// </summary>
        public LoginResultEnum Result 
        {
            get
            {
                return _loginVM.LoginResult.Result;
            }
        }

        /// <summary>
        /// Identyfikator zalogowanego użytkownika
        /// </summary>
        public string UserId
        {
            get
            {
                return _loginVM.UserId;
            }
        }

        /// <summary>
        /// Login zalogowanego użytkownika
        /// </summary>
        public string UserName
        {
            get
            {
                return _loginVM.UserName;
            }
        }

    }
}
