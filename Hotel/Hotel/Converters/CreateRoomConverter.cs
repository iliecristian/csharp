using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Hotel.Converters
{
    class CreateRoomConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Count() == 3)
                return new Tuple<string, string, string>(values[0].ToString(), values[1].ToString(), values[2].ToString());
            else
                return new Tuple<string, string, string, string>(values[0].ToString(), values[1].ToString(), values[2].ToString(), values[3].ToString());
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            Tuple<string, string, string, string> tuple = value as Tuple<string, string, string, string>;
            object[] result = new object[4] { tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4 };
            return result;
        }
    }
}
