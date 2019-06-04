using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Hotel.Converters
{
    class LoginUserConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var password = values[1] as PasswordBox;
            return new Tuple<string, PasswordBox>(values[0].ToString(), values[1] as PasswordBox);
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            Tuple<string, PasswordBox> tuple = value as Tuple<string, PasswordBox>;
            object[] result = new object[2] { tuple.Item1, tuple.Item2 };
            return result;
        }
    }
}
