using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using UtilitiesLib;

namespace Customer_Support_Ticketing_System_PL.Converters
{
    internal class StringToIntConverter : IValueConverter
    {
        private readonly ConverterUtils _converterUtils = new ConverterUtils();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert int back to string for display in TextBox
            return value?.ToString() ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_converterUtils.StringToIntConverter(value as string, out int number, 0, 100))
            {
                return number;
            }
            return 0; // or handle it as a fallback value
        }
    }
}
