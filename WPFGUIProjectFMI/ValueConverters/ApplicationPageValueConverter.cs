using System;
using System.Diagnostics;
using System.Globalization;

namespace WPFGUIProjectFMI
{
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        /// <summary>
        /// Converts aplication page enum into actual page
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Find the appropriate page
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.Login:
                    return new LoginPage();
                case ApplicationPage.Register:
                    return new RegisterPage();
                case ApplicationPage.StoragePage:
                    return new StoragePage();
                case ApplicationPage.BuyPage:
                    return new BuyPage();
                case ApplicationPage.SellPage:
                    return new SellPage();
                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}