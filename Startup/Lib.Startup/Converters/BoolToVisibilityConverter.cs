using System;
using System.Windows;
using System.Windows.Data;

namespace Lib.Startup.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool invert = parameter != null && parameter.ToString().ToUpper().Contains("INVERT");
            bool isVisible = System.Convert.ToBoolean(value) ^ invert;
            if (parameter is bool && (bool)parameter)
                return isVisible ? Visibility.Visible : Visibility.Hidden;
            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
         
        private static BoolToVisibilityConverter _instance;
        /// <summary>
        /// 
        /// </summary>
        public static BoolToVisibilityConverter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BoolToVisibilityConverter();

                }
                return _instance;
            }
        }
    }
}
