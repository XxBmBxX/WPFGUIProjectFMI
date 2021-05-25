using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGUIProjectFMI
{
    class IntegerToString : BaseValueConverter<IntegerToString>
    {
        /// <summary>
        /// Converts integers into strings
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value == 0 ? string.Empty : value.ToString();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int retVal;
            return int.TryParse(value.ToString(), out retVal) ? retVal : 0;
        }
    }
}
