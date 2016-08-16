namespace Lib.Startup.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Parametry do procesu logownia
    /// </summary>
    public class LoginParams
    {
        /// <summary>
        /// Nazwa użytkownika
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Hasło użytkownika
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Nowe hasło użytkownika
        /// </summary>
        public string NewPassword { get; set; }
    }
}
