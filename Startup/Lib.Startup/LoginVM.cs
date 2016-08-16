using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Lib.Startup.BaseClasses;
using Lib.Startup.Interfaces;
using System.Windows.Input;
using Lib.NLog;
using Lib.Startup.Data;
using Lib.Startup.Enums;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Lib.Startup
{
    public class LoginVM : BaseVM, IDataErrorInfo
    {
        #region Private fields

        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IView _view;
        private readonly ILoginAuthService _authService;

        #endregion Private fields

        public LoginVM(IView view, ILoginAuthService authService, ConfigureLoginViewParams param)
        {
            // Wstrzyknięcie widoku aby móc go zamknąć z poziomu viewModelu 
            _view = view;
            _authService = authService;
            PasswordExpires = false;
            //Warning = "Jakiś text aby pokazać ostrzeżenie Jakiś text aby pokazać ostrzeżenie Jakiś text aby pokazać ostrzeżenie Jakiś text aby pokazać ostrzeżenie Jakiś text aby pokazać ostrzeżenie Jakiś text aby pokazać ostrzeżenie Jakiś text aby pokazać ostrzeżenie Jakiś text aby pokazać ostrzeżenie Jakiś text aby pokazać ostrzeżenie Jakiś text aby pokazać ostrzeżenie";
            LoginResult = new LoginOperationResult { Result = Enums.LoginResultEnum.UnknownError };
            Contexts = new List<string>();

            // Dekodowanie parametrów
            AppCode = param.AppCode;
            AppName = param.AppName;
            AppVersion = param.AppVersion;
            Contexts = param.Contexts;            
            SelectedContext = param.SelectedContext;
            ShowContexts = param.ShowContexts;            
            UserId = param.UserId;
            UserName = param.UserName;
            Password = param.Password;
            UseProvidedContexts = param.UseProvidedContexts;
            CanChangePassword = param.CanChangePassword;
            if (param.LogoUri != null)
                Image = new BitmapImage(param.LogoUri);
            else if (param.LogoImage != null)
                Image = param.LogoImage;
        }      

        #region Properties


        /// <summary>
        /// Logo aplikacji wyświetlane na oknie logowania
        /// </summary>
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        /// <summary>
        /// Logo aplikacji wyświetlane na oknie logowania
        /// </summary>
        public static readonly DependencyProperty ImageProperty =
           DependencyProperty.Register("Image", typeof(ImageSource), typeof(LoginVM), new UIPropertyMetadata(null));

        private bool _canChangePassword = true;
        /// <summary>
        /// Sterowanie możliwością zmiany hasła
        /// </summary>
        public bool CanChangePassword
        {
            get { return _canChangePassword; }
            set
            {
                _canChangePassword = value;
                OnPropertyChanged("CanChangePassword");
            }
        }

        private bool _showContexts = true;
        /// <summary>
        /// Pokaż kontrolki do obsługi środowiska
        /// </summary>
        public bool ShowContexts
        {
            get { return _showContexts; }
            set
            {
                _showContexts = value;
                OnPropertyChanged("ShowContexts");
            }
        }

        private bool _useProvidedContexts = true;
        /// <summary>
        /// Użyj dostarczonych środowisk za pomocą ComboBox-a/nie pozwól na ręczną edycję środowiska
        /// </summary>
        public bool UseProvidedContexts
        {
            get { return _useProvidedContexts; }
            set
            {
                _useProvidedContexts = value;
                OnPropertyChanged("UseProvidedContexts");
                OnPropertyChanged("IsContextNotSet");
            }
        }

        /// <summary>
        /// Flaga sterująca wyświetleniem informacji na temat środowiska (pojawi się ona gdy nie będzie wybranego context-u)
        /// </summary>
        public bool IsContextNotSet
        {
            get { return string.IsNullOrWhiteSpace(SelectedContext) && UseProvidedContexts; }
        }

        private List<string> _contexts;
        /// <summary>
        /// Lista dostępnych środowisk
        /// </summary>
        public List<string> Contexts
        {
            get
            {
                return _contexts;
            }
            set
            {
                _contexts = value;
                OnPropertyChanged("Contexts");
                OnPropertyChanged("IsContextNotSet");
            }
        }

        private string _selectedContext;
        /// <summary>
        /// Kod wybranego contekstu
        /// </summary>
        public string SelectedContext
        {
            get { return _selectedContext; }
            set
            {
                ResumeState();
                _selectedContext = value;
                OnPropertyChanged("SelectedContext");
                OnPropertyChanged("IsContextNotSet");
                LoginCommand.RaiseCanExecuteChanged();
                //SelectedContext = Contexts.Contexts.FirstOrDefault(i => i.Name == value);
            }
        }

        /// <summary>
        /// Kod uruchamianej aplikacji
        /// </summary>
        public string AppCode { get; set; }

        /// <summary>
        /// Nazwa uruchamianej aplikacji
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// Wersja uruchamianej aplikacji
        /// </summary>
        public string AppVersion { get; set; }

        private string _userId = string.Empty;
        /// <summary>
        /// Identyfikator zalogowanego użytkownika
        /// </summary>
        public string UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChanged("UserId");
            }
        }               

        private string _userName = string.Empty;
        /// <summary>
        /// Nazwa użytkownika
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set
            {
                ResumeState();
                if (_userName != value)
                {
                    Password = String.Empty;
                    NewPassword = string.Empty;
                    NewPasswordRepeat = string.Empty;
                }
                _userName = value;
                OnPropertyChanged("UserName");
                // Jeśli jest widoczny panel do zmiany hasła to w momęcie zmiany loginu należy go ukryć
                if(IsChangingPasswordPanelVisible)
                    ShowPanelForChangingPasswordCommand.Execute(string.Empty);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password = string.Empty;
        /// <summary>
        /// Hasło użytkownika
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                ResumeState();
                _password = value;
                OnPropertyChanged("Password");
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private string _newPassword = string.Empty;
        /// <summary>
        /// Nowe hasło uzytkownika
        /// </summary>
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged("NewPassword");
                PasswordStrength = _authService.GetPasswordStrength(new LoginParams { Password = value });
                ChangePasswordCommand.RaiseCanExecuteChanged();
            }
        }

        private string _newPasswordRepeat = string.Empty;
        /// <summary>
        /// Powtórka nowego hasła użytkownika
        /// </summary>
        public string NewPasswordRepeat
        {
            get { return _newPasswordRepeat; }
            set
            {
                _newPasswordRepeat = value;
                OnPropertyChanged("RepeatNewPassword");                
                ChangePasswordCommand.RaiseCanExecuteChanged();
            }
        }

        private LoginOperationResult _loginResult;
        /// <summary>
        /// Wynik logowania
        /// </summary>
        public LoginOperationResult LoginResult
        {
            get { return _loginResult; }
            set
            {
                _loginResult = value;
            }
        }

        private bool _passwordExpires;
        /// <summary>
        /// Flaga sterująca komunikatem o zmianie hasła
        /// </summary>
        public bool PasswordExpires
        {
            get { return _passwordExpires; }
            set
            {
                _passwordExpires = value;
                OnPropertyChanged("PasswordExpires");
            }
        }
        
        private bool _isChangingPasswordPanelVisible = false;
        /// <summary>
        /// Flaga sterująca widocznością panelu zmiany hasła
        /// </summary>
        public bool IsChangingPasswordPanelVisible
        {
            get { return _isChangingPasswordPanelVisible; }
            set
            {
                if (_isChangingPasswordPanelVisible == value) return;
                _isChangingPasswordPanelVisible = value;
                OnPropertyChanged("IsChangingPasswordPanelVisible");
            }
        }

        /// <summary>
        /// Flaga informująca, że jest dostępny komunikat ostrzeżenia
        /// </summary>
        public bool HasWarning
        {
            get { return !string.IsNullOrEmpty(Warning); }
        }

        private string _warning;
        /// <summary>
        /// Informacja o ostrzeżeniach wyświetlanych podczas logowania
        /// </summary>
        public string Warning
        {
            get { return _warning; }

            set
            {
                _warning = value;
                OnPropertyChanged("Warning");
                OnPropertyChanged("HasWarning");
            }
        }

        /// <summary>
        /// Flaga informująca, że jest dostępny komunikat błędu
        /// </summary>
        public bool HasError
        {
            get { return !string.IsNullOrEmpty(Error); }
        }

        private string _error;
        /// <summary>
        /// Informacja o błędach wyświetlanych podczas logowania
        /// </summary>
        public string Error
        {
            get { return _error; }

            set
            {
                _error = value;
                OnPropertyChanged("Error");
                OnPropertyChanged("HasError");
            }
        }

        private NewPasswordStrengthEnum _passwordStrength = NewPasswordStrengthEnum.NotDefined;

        /// <summary>
        /// Enum z informacją na temat mocy nowego hasła
        /// </summary>
        public NewPasswordStrengthEnum PasswordStrength
        {
            get { return _passwordStrength; }
            set
            {
                _passwordStrength = value;
                OnPropertyChanged("PasswordStrength");
            }       
        }

        #endregion Properties

        #region LoginCommand

        private DelegateCommand _loginCommand;
        /// <summary>
        /// Logowanie
        /// </summary>
        public DelegateCommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                    _loginCommand = new DelegateCommand(Login, CanLogin);
                return _loginCommand;
            }
        }    

        private bool CanLogin(object obj)
        {
            if (string.IsNullOrEmpty(this["UserName"]) && string.IsNullOrEmpty(this["Password"]) && (!string.IsNullOrWhiteSpace(SelectedContext) || !ShowContexts))
                return true;
            return false;
        }

        private void Login(object obj)
        {
            Error = null;

            _logger.Debug("Dokonano próby logowania do środowiska {0} na użytkownika {1} ", SelectedContext, UserName);
            // Próba ustawienia kontekstu (wybranego środowiska)
            LoginSelectedContextParams contextParam = ShowContexts ? new LoginSelectedContextParams { SelectedContext = SelectedContext, ValidateSelectedContext = !UseProvidedContexts } : null;
            LoginOperationResult contextResult = _authService.SetContext(contextParam);
            _logger.DebugObject("Wynik ustawienia środowiska", contextResult);

            switch (contextResult.Result)
            {
                case Lib.Startup.Enums.LoginResultEnum.Success:
                    // OK
                    break;
                case Lib.Startup.Enums.LoginResultEnum.ContextNotSupported:
                    // Nie znaleziono contextu, najprawdopodobniej wpisane z palca nie istnieje, bo lista możliwych do wyboru jest wstrzykiwana z zewnątrz               
                case Lib.Startup.Enums.LoginResultEnum.UnknownError:
                case Lib.Startup.Enums.LoginResultEnum.InvalidCredentials:
                case Lib.Startup.Enums.LoginResultEnum.PasswordChangeRequired:
                case Lib.Startup.Enums.LoginResultEnum.AccountLocked:
                case Lib.Startup.Enums.LoginResultEnum.PasswordExpired:
                case Lib.Startup.Enums.LoginResultEnum.PasswordExpirationWarning:
                default:
                    if (string.IsNullOrWhiteSpace(contextResult.Message))
                        throw new InvalidOperationException("Błąd wpięcia okna logowania! Ustawienie kontekstu zwróciło błąd z pustym opisem dotyczącym szczegółów błędu. Sprawdz czy w Result zwracasz poprawny status.");
                    Error =  contextResult.Message;
                    return;
            }

            // Próba logowania
            LoginParams param = new LoginParams { Password = Password, UserName = UserName };
            LoginResult = _authService.Login(param);
            _logger.DebugObject("Wynik logowania", LoginResult);

            // Jeśli wyświetlony został komunikat z ostrzeżeniem o zmianie hasła i zalogowano pomimo to
            if (PasswordExpires && LoginResult.Result == Enums.LoginResultEnum.PasswordExpirationWarning)
                LoginResult.Result = Enums.LoginResultEnum.Success;

            // Obsługa rezultatu
            switch (LoginResult.Result)
            {
                case Lib.Startup.Enums.LoginResultEnum.UnknownError:
                    Error = LoginResult.Message;
                    break;
                case Lib.Startup.Enums.LoginResultEnum.Success:
                    // Ustawienie id zalogowanego użytkownika jeśli przyszła jakaś wartość
                    string id = LoginResult.LoggedUserId;
                    if (!string.IsNullOrWhiteSpace(id))
                        UserId = id;
                    _view.Close();
                    break;
                case Lib.Startup.Enums.LoginResultEnum.InvalidCredentials:
                    // Nieprawidłowe dane logowania
                    Error = LoginResult.Message;
                    break;
                case Lib.Startup.Enums.LoginResultEnum.PasswordChangeRequired:
                    // Wymaganie zmiany hasła
                    ShowPanelForChangingPasswordCommand.Execute(string.Empty);
                    Warning = LoginResult.Message;                    
                    break;
                case Lib.Startup.Enums.LoginResultEnum.AccountLocked:
                    // Zablokowane konto
                    Error = LoginResult.Message;
                    break;
                case Lib.Startup.Enums.LoginResultEnum.PasswordExpired:
                    // Hasło wygasło
                    ShowPanelForChangingPasswordCommand.Execute(string.Empty);
                    Error = LoginResult.Message;
                    break;
                case Lib.Startup.Enums.LoginResultEnum.PasswordExpirationWarning:
                    // Ostrzeżenie o konieczności zmiany hasła
                    PasswordExpires = true;
                    Warning = LoginResult.Message;
                    break;
                case Lib.Startup.Enums.LoginResultEnum.ContextNotSupported:
                    Error = LoginResult.Message;
                    break;
                default:
                    break;
            }
        }

        #endregion LoginCommand

        #region ShowPanelForChangingPasswordCommand

        private DelegateCommand _showPanelForChangingPasswordCommand;
        /// <summary>
        /// Komenda pokazująca/ukrywająca panel do zmiany hasła
        /// </summary>
        public DelegateCommand ShowPanelForChangingPasswordCommand
        {
            get
            {
                if (_showPanelForChangingPasswordCommand == null)
                    _showPanelForChangingPasswordCommand = new DelegateCommand(ShowPanelForChangingPasswordCommandExecute, ShowPanelForChangingPasswordCommandCanExecute);
                return _showPanelForChangingPasswordCommand;
            }
        }

        /// <summary>
        /// Komenda pokazująca/ukrywająca panel do zmiany hasła
        /// </summary>
        public void ShowPanelForChangingPasswordCommandExecute(object parameter)
        {
            ResumeState();
            // Ukrycie/pokazanie panelu
            IsChangingPasswordPanelVisible = !_isChangingPasswordPanelVisible;
            // wyzerowanie błędów
            Warning = string.Empty;
            Error = string.Empty;
            // czyszczenie haseł
            NewPassword = string.Empty;
            NewPasswordRepeat = string.Empty;
            PasswordStrength = NewPasswordStrengthEnum.NotDefined;
        }

        private bool ShowPanelForChangingPasswordCommandCanExecute(object parameter)
        {
            // Sterowanie możliwością zmiany hasła
            return true;
        }

        #endregion ShowPanelForChangingPasswordCommand

        #region ChangePasswordCommand

        private DelegateCommand _changePassowrdCommand;
        /// <summary>
        /// Komenda do obsługi zmiany hasła
        /// </summary>
        public DelegateCommand ChangePasswordCommand
        {
            get
            {
                if (_changePassowrdCommand == null)
                    _changePassowrdCommand = new DelegateCommand(ChangePasswordCommandExecute, ChangePasswordCommandCanExecute);
                return _changePassowrdCommand;
            }
        }

        private bool ChangePasswordCommandCanExecute(object parameter)
        {
            if (string.IsNullOrEmpty(this["NewPassword"]) && string.IsNullOrEmpty(this["NewPasswordRepeat"]) && NewPasswordRepeat == NewPassword)
                return true;
            return false;
        }

        /// <summary>
        /// Komenda do obsługi zmiany hasła
        /// </summary>
        public void ChangePasswordCommandExecute(object parameter)
        {
            _logger.Debug("Dokonano próby logowania do środowiska {0} na użytkownika {1} ", SelectedContext, UserName);
            // Próba ustawienia kontekstu (wybranego środowiska)
            LoginSelectedContextParams contextParam = ShowContexts ? new LoginSelectedContextParams { SelectedContext = SelectedContext, ValidateSelectedContext = !UseProvidedContexts } : null;
            LoginOperationResult contextResult = _authService.SetContext(contextParam);
            _logger.DebugObject("Wynik ustawienia środowiska", contextResult);

            switch (contextResult.Result)
            {
                case Lib.Startup.Enums.LoginResultEnum.Success:
                    // OK
                    break;
                case Lib.Startup.Enums.LoginResultEnum.ContextNotSupported:
                // Nie znaleziono contextu, najprawdopodobniej wpisane z palca nie istnieje, bo lista możliwych do wyboru jest wstrzykiwana z zewnątrz               
                case Lib.Startup.Enums.LoginResultEnum.UnknownError:
                case Lib.Startup.Enums.LoginResultEnum.InvalidCredentials:
                case Lib.Startup.Enums.LoginResultEnum.PasswordChangeRequired:
                case Lib.Startup.Enums.LoginResultEnum.AccountLocked:
                case Lib.Startup.Enums.LoginResultEnum.PasswordExpired:
                case Lib.Startup.Enums.LoginResultEnum.PasswordExpirationWarning:
                default:
                    if (string.IsNullOrWhiteSpace(contextResult.Message))
                        throw new InvalidOperationException("Błąd wpięcia okna logowania! Ustawienie kontekstu zwróciło błąd z pustym opisem dotyczącym szczegółów błędu. Sprawdz czy w Result zwracasz poprawny status.");
                    Error = contextResult.Message;
                    return;
            }

            // Próba logowania
            LoginParams paramChange = new LoginParams { Password = Password, UserName = UserName, NewPassword = NewPassword };
            LoginResult = _authService.ChangePassword(paramChange);
            _logger.DebugObject("Wynik zmiany hasła", LoginResult);

            // Obsługa rezultatu
            switch (LoginResult.Result)
            {
                case Lib.Startup.Enums.LoginResultEnum.UnknownError:
                    Error = LoginResult.Message;
                    break;
                case Lib.Startup.Enums.LoginResultEnum.Success:
                    // Ustawienie id zalogowanego użytkownika jeśli przyszła jakaś wartość
                    string id = LoginResult.LoggedUserId;
                    if(!string.IsNullOrWhiteSpace(id))
                        UserId = id;
                    _view.Close();
                    break;
                case Lib.Startup.Enums.LoginResultEnum.InvalidCredentials:
                    // Nieprawidłowe dane logowania
                    Error = LoginResult.Message;
                    break;
                case Lib.Startup.Enums.LoginResultEnum.PasswordChangeRequired:
                    // Wymaganie zmiany hasła
                    ShowPanelForChangingPasswordCommand.Execute(string.Empty);
                    Warning = LoginResult.Message;
                    break;
                case Lib.Startup.Enums.LoginResultEnum.AccountLocked:
                    // Zablokowane konto
                    Error = LoginResult.Message;
                    break;
                case Lib.Startup.Enums.LoginResultEnum.PasswordExpired:
                    // Hasło wygasło
                    ShowPanelForChangingPasswordCommand.Execute(string.Empty);
                    Error = LoginResult.Message;
                    break;
                case Lib.Startup.Enums.LoginResultEnum.PasswordExpirationWarning:
                    // Ostrzeżenie o konieczności zmiany hasła
                    PasswordExpires = true;
                    Warning = LoginResult.Message;
                    break;
                case Lib.Startup.Enums.LoginResultEnum.ContextNotSupported:
                    Error = LoginResult.Message;
                    break;
                default:
                    break;
            }
        }

        #endregion ChangePasswordCommand

        #region EnterCommand

        private DelegateCommand _enterCommand;
        /// <summary>
        /// Komenda do przechwycenia entera i przekierowania go do odpowiedniej metody
        /// </summary>
        public DelegateCommand EnterCommand
        {
            get
            {
                if (_enterCommand == null)
                    _enterCommand = new DelegateCommand(EnterCommandExecute, (_ => true));
                return _enterCommand;
            }
        }

        /// <summary>
        /// Komenda do przechwycenia entera i przekierowania go do odpowiedniej metody
        /// </summary>
        public void EnterCommandExecute(object parameter)
        {
            if (IsChangingPasswordPanelVisible)
                ChangePasswordCommand.Execute(string.Empty);
            else
                LoginCommand.Execute(string.Empty);
        }

        #endregion EnterCommand

        /// <summary>
        /// Wyczyszczenie komunikatów i flag po zmianie loginu i hasłą
        /// </summary>
        private void ResumeState()
        {
            PasswordExpires = false;
            Warning = string.Empty;
            Error = string.Empty;
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "UserName":
                        if (string.IsNullOrEmpty(UserName))
                            return "Nazwa użytkownika nie może być pusta";
                        return null;
                    case "Password":
                        if (string.IsNullOrEmpty(Password))
                            return "Hasło nie może być puste";
                        return null;
                    case "NewPassword":
                        if (string.IsNullOrEmpty(NewPassword))
                            return "Nowe hasło nie może być puste";
                        return null;
                    case "NewPasswordRepeat":
                        if (NewPasswordRepeat.Length > 0 && NewPasswordRepeat != NewPassword)
                            return "Powtórka nowego hasła musi być identyczna z proponowanym nowym hasłem";
                        return null;
                    default:
                        return null;

                }
            }
        }
    }
}
