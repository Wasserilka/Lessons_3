using System;
using System.Windows.Data;
using System.Globalization;

namespace MetricsManagerClient.Converters
{
    public class GetMetricMultiBinding : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
