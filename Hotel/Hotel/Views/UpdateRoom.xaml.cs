using Hotel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hotel.Views
{
    /// <summary>
    /// Interaction logic for UpdateRoom.xaml
    /// </summary>
    public partial class UpdateRoom : Window
    {
        public UpdateRoom()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataManager.Instance.SelectedRoomFeatures.Clear();
            DataManager.Instance.SelectedRoomImages.Clear();

            this.Hide();

            var window = new AdminWindow();
            window.ShowDialog();
        }
    }
}
