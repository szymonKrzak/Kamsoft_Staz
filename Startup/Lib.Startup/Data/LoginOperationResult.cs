namespace Lib.Startup.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Lib.Startup.Enums;

    /// <summary>
    /// Odpowiedz zwrotna z logowania
    /// </summary>
    public class LoginOperationResult
    {
        /// <summary>
        /// Tekst nagłówka wiadomości wyświetlanej użytkownikowi
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Tekst wiadomości wyświetlanej użytkownikowi
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Rodzaj odpowiedzi
        /// </summary>
        public LoginResultEnum Result { get; set; }

        /// <summary>
        /// Nazwa zalogowanego użytkownika
        /// </summary>
        public string LoggedUserName { get; set; }

        /// <summary>
        /// Identyfikator zalogowanego użytkownika
        /// </summary>
        public string LoggedUserId { get; set; }
    }
}
