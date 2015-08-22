using System;
using System.Globalization;
using System.Windows.Data;
using Common.Utility;

namespace Presentation.WPF.Converter
{
    public class EnumDescriptionConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var myEnum = (Enum) value;
            var description = myEnum.GetDescription();
            return description;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}