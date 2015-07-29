using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UWActuallyWorks.Converter
{
    public class UrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Uri address = null;
            if (value != null)
            {
                try
                {
                    address = new Uri(value.ToString());
                }
                catch
                {
                }
            }
            return address;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var address = (Uri) value;
            return (value ?? "").ToString();
        }
    }
}
