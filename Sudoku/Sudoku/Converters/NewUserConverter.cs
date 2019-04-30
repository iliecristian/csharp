using Sudoku.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Sudoku.Converters
{
    class NewUserConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new NewUserVM()
            {
                Name = values[0].ToString(),
                Image = values[1].ToString(),
                CurrentWindow = values[2] as Window
            };
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            NewUserVM newUserVM = value as NewUserVM;
            object[] result = new object[3] { newUserVM.Name, newUserVM.Image, newUserVM.CurrentWindow };
            return result;
        }
    }
}
