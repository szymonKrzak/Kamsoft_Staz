using System.Linq;
using AppFramework.Interfaces.ApplicationServices;
using AppFramework.Interfaces.ApplicationServices.Security;
using AppFramework.Interfaces.Startup;
using AppFramework.Interfaces.Utilities;
using Lib.Startup.Data;
using Lib.Startup.Enums;
using Lib.Startup.Interfaces;
using Lib.NLog;

namespace AppFramework.InfrastructureServices
{
    public class LoginAuthService : ILoginAuthService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IAppService _appService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEnvironmentContextService _startupContext;

        public LoginAuthService(IAuthenticationService authService, IAppService currentApplicationService,IEnvironmentContextService startupContext)
        {
            _appService = currentApplicationService;
            _authenticationService = authService;
            _startupContext = startupContext;
        }

        public IStartupContext Context
        {
            get
            {
                // Kontekst jest zainicjalizowany w bootstraperze
                return _startupContext.Context;
            }
        }

        #region ILoginAuthService

        public LoginOperationResult ChangePassword(LoginParams param)
        {
            // Przed zmianą hasła musi nastąpić logowanie
            LoginOperationResult res = Login(param);

            if (res.Result == LoginResultEnum.Success || res.Result == LoginResultEnum.PasswordExpirationWarning)
            {
                SecurityOperationResult result = _authenticationService.ChangePassword(param.NewPassword);
                res = new LoginOperationResult
                {
                    Header = result.ErrorHeader,
                    Message = result.ErrorMessage == string.Empty ? result.WarningMessage : result.ErrorMessage,
                    LoggedUserId = result.LoggedUserId,
                    LoggedUserName = result.LoggedUserName
                };

                switch (result.Result)
                {
                    case AppFramework.Interfaces.Enums.AuthenticationResultEnum.UnknownError:
                        res.Result = LoginResultEnum.UnknownError;
                        break;
                    case AppFramework.Interfaces.Enums.AuthenticationResultEnum.Success:
                        res.Result = LoginResultEnum.Success;
                        break;
                    case AppFramework.Interfaces.Enums.AuthenticationResultEnum.InvalidCredentials:
                        res.Result = LoginResultEnum.InvalidCredentials;
                        break;
                    case AppFramework.Interfaces.Enums.AuthenticationResultEnum.PasswordChangeRequired:
                    case AppFramework.Interfaces.Enums.AuthenticationResultEnum.PasswordExpiredLoginAllowed:
                        res.Result = LoginResultEnum.PasswordChangeRequired;
                        break;
                    case AppFramework.Interfaces.Enums.AuthenticationResultEnum.AccountLocked:
                        res.Result = LoginResultEnum.AccountLocked;
                        break;
                    case AppFramework.Interfaces.Enums.AuthenticationResultEnum.PasswordExpired:
                        res.Result = LoginResultEnum.PasswordExpired;
                        break;
                    case AppFramework.Interfaces.Enums.AuthenticationResultEnum.PasswordExpirationWarning:
                        res.Result = LoginResultEnum.PasswordExpirationWarning;
                        break;
                    case AppFramework.Interfaces.Enums.AuthenticationResultEnum.ProviderNotSupported:
                        res.Result = LoginResultEnum.ContextNotSupported;
                        break;
                }
            }

            return res;
        }

        /// <summary>
        /// Metoda ustawia context uruchomieniowy (środowisko w którym zostanie uruchomiona aplikacja)
        /// Pamiętaj o ustawieniu właściwej odpowiedzi
        /// </summary>
        /// <param name="environment">środowisko, jeśli parametr jest nullem to kontekst należy ustawiać ręcznie!</param>
        public LoginOperationResult SetContext(LoginSelectedContextParams param)
        {
            LoginOperationResult res = new LoginOperationResult { Result = LoginResultEnum.UnknownError };
            
            if (param == null)
            { 
                // Jeśli parametr jest nullem to kontekst należy ustawiać ręcznie!
                // Robi się to za pomocą ConfigureLoginViewParams{ ShowEnvironments= true, UseProvidedEnvironments = false }; w miejscu podpięcia okna logowania
                _startupContext.SetContextDetails(Context.Contexts.FirstOrDefault(i => i.Name == "K2PLAN"));
                res.Result = LoginResultEnum.Success;
            }
            else
            {
                // UWAGA w parametrze param.ValidateSelectedEnvironment przychodzi informacja o konieczności walidacji conteksu pod kątem jego istnienia

                // Próba ustawienia wybanego contekstu i pobranie dla niego szczegółów
                IStartupContextData d = Context.Contexts.FirstOrDefault(i => i.Name == param.SelectedContext);
                if (d != null)
                {
                    _startupContext.SetContextDetails(d);
                    res = new LoginOperationResult { Result = LoginResultEnum.Success };
                }
                else
                {
                    res = new LoginOperationResult { Result = LoginResultEnum.ContextNotSupported,
                                                     Message = "Podane środowisko jest niepoprawne, proszę wybrać poprawne." };
                }
            }
            return res;
        }

        public LoginOperationResult Login(LoginParams param)
        {            
            SecurityOperationResult result = _authenticationService.Login(Context, param.UserName, param.Password, _appService.ApplicationData.Code, _appService.ApplicationData.IsPrototypeMode);
            var res = new LoginOperationResult
            {
                Header = result.ErrorHeader,
                Message = result.ErrorMessage == null ? result.WarningMessage : result.ErrorMessage,
                LoggedUserId = result.LoggedUserId,
                LoggedUserName = result.LoggedUserName
            };

            switch (result.Result)
            {
                case AppFramework.Interfaces.Enums.AuthenticationResultEnum.UnknownError:
                    res.Result = LoginResultEnum.UnknownError;
                    break;
                case AppFramework.Interfaces.Enums.AuthenticationResultEnum.Success:
                    res.Result = LoginResultEnum.Success;
                    break;
                case AppFramework.Interfaces.Enums.AuthenticationResultEnum.InvalidCredentials:
                    res.Result = LoginResultEnum.InvalidCredentials;
                    break;
                case AppFramework.Interfaces.Enums.AuthenticationResultEnum.PasswordChangeRequired:
                case AppFramework.Interfaces.Enums.AuthenticationResultEnum.PasswordExpiredLoginAllowed:
                    res.Result = LoginResultEnum.PasswordChangeRequired;
                    break;
                case AppFramework.Interfaces.Enums.AuthenticationResultEnum.AccountLocked:
                    res.Result = LoginResultEnum.AccountLocked;
                    break;
                case AppFramework.Interfaces.Enums.AuthenticationResultEnum.PasswordExpired:
                    res.Result = LoginResultEnum.PasswordExpired;
                    break;
                case AppFramework.Interfaces.Enums.AuthenticationResultEnum.PasswordExpirationWarning:
                    res.Result = LoginResultEnum.PasswordExpirationWarning;
                    break;
                case AppFramework.Interfaces.Enums.AuthenticationResultEnum.ProviderNotSupported:
                    res.Result = LoginResultEnum.ContextNotSupported;
                    break;
            }

            return res;
        }

        public NewPasswordStrengthEnum GetPasswordStrength(LoginParams param)
        {
            NewPasswordStrengthEnum result = NewPasswordStrengthEnum.NotDefined;

            if (param != null)
            {
                if (param.Password.Length < 3)
                    result = NewPasswordStrengthEnum.Weak;
                else if (param.Password.Length < 5)
                    result = NewPasswordStrengthEnum.Medium;
                else 
                    result = NewPasswordStrengthEnum.Strong;
            }

            return result;
        }

        #endregion ILoginAuthService        
    }
}
