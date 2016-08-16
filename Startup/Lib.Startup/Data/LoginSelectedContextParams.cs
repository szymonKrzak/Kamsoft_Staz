namespace Lib.Startup.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Parametr z zebranymi informacjami potrzebnymi do ustawienia kontekstu uruchomienia podczas operacji logowania
    /// </summary>
    public class LoginSelectedContextParams
    {
        /// <summary>
        /// Wybrany contekst (środowisko do którego ma nastąpić logowanie)
        /// </summary>
        public string SelectedContext { get; set; }

        /// <summary>
        /// Informacja o konieczności sprawdzenia istnienia kontekstu ( przypadek gdy pozwalamy na jego ręczne wprowadzenie)
        /// </summary>
        public bool ValidateSelectedContext { get; set; }
    }
}
