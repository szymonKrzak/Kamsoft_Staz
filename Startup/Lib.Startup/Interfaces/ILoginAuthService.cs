namespace Lib.Startup.Interfaces
{
    using Lib.Startup.BaseClasses;
    using Lib.Startup.Data;
    using Lib.Startup.Enums;

    public interface ILoginAuthService
    {
        /// <summary>
        /// Metoda ustawia context uruchomieniowy (środowisko w którym zostanie uruchomiona aplikacja)
        /// Poprawne ustawienie kontekstu powinno zwrócić Result = LoginResultEnum.Success
        /// METODA JEST WYWOŁYWANA PRZED KAŻDYM WYWOŁANIEM METOD Login i ChangePassword
        /// </summary>
        /// <param name="param">środowisko, jeśli parametr jest nullem to kontekst należy ustawiać ręcznie!</param>
        /// <returns>Wynik operacji</returns>
        LoginOperationResult SetContext(LoginSelectedContextParams param);

        /// <summary>
        /// Metoda zmieniająca hasło dla użytkownika
        /// </summary>
        /// <param name="param">Informacje o użytkowniku i nowyn haśle</param>
        /// <returns>Wynik operacji</returns>
        LoginOperationResult ChangePassword(LoginParams param);

        /// <summary>
        /// Metoda odpowiedzialna za logoawnie
        /// </summary>
        /// <param name="param">Informacje o użytkowniku i haśle</param>
        /// <returns>Wynik operacji</returns>
        LoginOperationResult Login(LoginParams param);

        /// <summary>
        /// Metoda sprawdza siłę nowego hasła
        /// </summary>
        /// <param name="param">Nowe hasło do sprawdzenia</param>
        /// <returns>Wartość NewPasswordStrengthEnum.NotDefined ukryje obsługę mocy hasła</returns>
        NewPasswordStrengthEnum GetPasswordStrength(LoginParams param);
    }
}
