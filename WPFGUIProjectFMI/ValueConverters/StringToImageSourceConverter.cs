using System;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;

namespace WPFGUIProjectFMI
{
    class StringToImageSourceConverter : BaseValueConverter<StringToImageSourceConverter>
    {
        /// <summary>
        /// Takes a string and converts it into bitmap image
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            if (File.Exists(@"ProfileImages/"+(string)value+""))
            {
                bi.UriSource = new Uri(Path.Combine(Environment.CurrentDirectory, "ProfileImages", ""+(string)value+""), UriKind.RelativeOrAbsolute);
                bi.DecodePixelWidth = 400;
                bi.DecodePixelHeight = 400;
                bi.EndInit();
                return bi;
            }
            else
            {
                bi.UriSource = new Uri("/Images/profilePicture_blank.png", UriKind.RelativeOrAbsolute);
                bi.DecodePixelWidth = 400;
                bi.DecodePixelHeight = 400;
                bi.EndInit();
                return bi;
            }
            
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
