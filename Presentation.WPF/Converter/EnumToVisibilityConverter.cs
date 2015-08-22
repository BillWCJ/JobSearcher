using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Presentation.WPF.Converter
{
    public class EnumToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals(parameter))
                return Visibility.Visible;
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(Visibility.Visible) ? parameter : Binding.DoNothing;
        }
    }
}