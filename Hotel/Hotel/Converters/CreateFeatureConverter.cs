using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Hotel.Converters
{
    class CreateFeatureConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new Tuple<string, string>(values[0].ToString(), values[1].ToString());
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            Tuple<string, string> tuple = value as Tuple<string, string>;
            object[] result = new object[2] { tuple.Item1, tuple.Item2};
            return result;
        }
    }
}
