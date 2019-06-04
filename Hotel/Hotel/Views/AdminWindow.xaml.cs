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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            DataManager.Instance.CurrentTable = DataManager.Table.Rooms;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            switch (DataManager.Instance.CurrentTable)
            {
                case DataManager.Table.Rooms:
                    {
                        CreateRoom window = new CreateRoom();
                        window.ShowDialog();
                        break;
                    }
                case DataManager.Table.Features:
                    {
                        var window = new CreateFeature();
                        window.ShowDialog();
                        break;
                    }
                case DataManager.Table.ExtraServices:
                    {
                        var window = new CreateExtraServices();
                        window.ShowDialog();
                        break;
                    }
                case DataManager.Table.Discount:
                    {
                        var window = new CreateDiscount();
                        window.ShowDialog();
                        break;
                    }

                default:
                    break;
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            switch (DataManager.Instance.CurrentTable)
            {
                case DataManager.Table.Rooms:
                    {
                        var window = new UpdateRoom();
                        window.ShowDialog();
                        break;
                    }
                case DataManager.Table.Features:
                    {
                        var window = new UpdateFeature();
                        window.ShowDialog();
                        break;
                    }
                case DataManager.Table.ExtraServices:
                    {
                        var window = new UpdateExtraService();
                        window.ShowDialog();
                        break;
                    }
                case DataManager.Table.Discount:
                    {
                        var window = new UpdateDiscount();
                        window.ShowDialog();
                        break;
                    }

                default:
                    break;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            MainWindow window = new MainWindow();
            window.ShowDialog();
        }
    }
}
