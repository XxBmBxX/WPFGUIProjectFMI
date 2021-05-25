using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPFGUIProjectFMI
{

    class ValueToColorReceipt : BaseValueConverter<ValueToColorReceipt>
    {
        /// <summary>
        /// Displays different color depending on current receipt and players money
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((float)value > CombinedViewModel.Money)
            {
                return Brushes.Red;
            }
            else if ((float)value > CombinedViewModel.Money-(CombinedViewModel.Money/5))
            {
                return Brushes.DarkOrange;
            }
            else
            {
                return Brushes.Green;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
