namespace Lib.Startup.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Parametry podawane do widoku/okna logowania
    /// </summary>
    public class ConfigureLoginViewParams
    {
        public ConfigureLoginViewParams()
        {
            Contexts = new List<string>();
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

        /// <summary>
        /// Identyfikator zalogowanego użytkownika
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Nazwa użytkownika
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Hasło użytkownika
        /// </summary>
        public string Password { get; set; }

         /// <summary>
        /// Lista dostępnych środowisk
        /// </summary>
        public List<string> Contexts { get; set; }

        /// <summary>
        /// Kod wybranego contekstu
        /// </summary>
        public string SelectedContext { get; set; }
        
        /// <summary>
        /// Pokaż kontrolki do obsługi środowiska
        /// </summary>
        public bool ShowContexts { get; set; }

        /// <summary>
        /// Użyj dostarczonych środowisk za pomocą ComboBox-a/nie pozwól na ręczną edycję środowiska
        /// </summary>
        public bool UseProvidedContexts { get; set; }

        /// <summary>
        /// Sterowanie możliwością zmiany hasła
        /// </summary>
        public bool CanChangePassword { get; set; }

        /// <summary>
        /// Ścieżka do logo aplikacji
        /// </summary>
        public Uri LogoUri { get; set; }

        /// <summary>
        /// Logo aplikacji
        /// </summary>
        public BitmapImage LogoImage { get; set; }
    }
}
