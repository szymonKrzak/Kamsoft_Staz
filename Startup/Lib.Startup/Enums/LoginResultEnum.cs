using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Startup.Enums
{
    /// <summary>
    /// Wartości obsługiwane przez okienko logowania
    /// </summary>
    public enum LoginResultEnum
    {
        /// <summary>
        /// Nieznany błąd, szczegóły błędu zostaną zalogowane i wyświetlone użytkownikowi na ekranie
        /// </summary>
        UnknownError = -1,
        /// <summary>
        /// Sukces wszystko poszło pomyślnie
        /// </summary>
        Success = 0,
        /// <summary>
        /// Niepoprawne dane użytkownika przekazane do logowania 
        /// </summary>
        InvalidCredentials = 1,
        /// <summary>
        /// Zmiana hasła jest wymagana w celu zalogowania, dotyczy przypadku gdy mamy nowego użytkownika z wymuszeniem zmiany hasła przy następnym logowaniu
        /// </summary>
        PasswordChangeRequired = 5,
        /// <summary>
        /// Nieaktywne konto użytkownika
        /// </summary> 
        AccountLocked = 6,
        /// <summary>
        /// Wygasło hasło użytkownika
        /// </summary>
        PasswordExpired = 7,
        /// <summary>
        /// Zbliża się termin zmiany hasła. Pozostało {0} dni do jego zmiany.
        /// </summary>
        PasswordExpirationWarning = 9,
        /// <summary>
        /// Nie znane jest środowisko do którego nastąpi logowanie
        /// </summary>
        ContextNotSupported = 10,
    }
}
