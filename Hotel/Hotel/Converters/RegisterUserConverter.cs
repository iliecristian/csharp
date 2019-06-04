using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Hotel.Converters
{
    class RegisterUserConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var password = values[3] as PasswordBox;
            return new Tuple<bool, bool, string, string>((bool)values[0], (bool)values[1], values[2].ToString(), password.Password);
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            Tuple<bool, bool, string, string> tuple = value as Tuple<bool, bool, string, string>;
            object[] result = new object[4] { tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4 };
            return result;
        }
    }
}
