using Hotel.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Hotel.Converters
{
    class AdminListConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Image img = values[0] as Image;
            TextBlock descText = values[1] as TextBlock;
            TextBlock priceText = values[2] as TextBlock;
            string headerName = "";
            if (values[3] != null)
                headerName = (values[3] as TabItem).Header.ToString();

            return new Tuple<Image, TextBlock, TextBlock, string>(img, descText, priceText, headerName);
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            Tuple < Image, TextBlock, TextBlock, string > tuple = value as Tuple<Image, TextBlock, TextBlock, string>;
            object[] result = new object[4] { tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4 };
            return result;
        }
    }
}
