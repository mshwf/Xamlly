using System;
using System.Globalization;
using Xamarin.Forms;

namespace Xamlly.Converters
{
    public class WidthConverter : IValueConverter
    {
        public double Ratio { get; set; } = 1.5d;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var newWidth = ((double)value) * Ratio;
            return newWidth;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
