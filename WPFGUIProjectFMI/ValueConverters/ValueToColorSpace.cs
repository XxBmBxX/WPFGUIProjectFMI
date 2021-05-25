using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPFGUIProjectFMI
{
    class ValueToColorSpace : BaseValueConverter<ValueToColorSpace>
    {
        /// <summary>
        /// Displays different color depending on current free space
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value >= 0 && (int)value <= 25000 ? Brushes.Green : Brushes.Red;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
