using Hotel.Helpers;
using Hotel.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //var context = new HotelEntities();
            //context.GetAllClients();

            //var context = new HotelEntities();
            //var camera = new Camera()
            //{
            //    Image = "room1.png",
            //    Type = "Camera dubla"
            //};
            //context.Camera.Add(camera);
            //context.SaveChanges();
        }

        private void LoginGuest_Click(object sender, RoutedEventArgs e)
        {
            DataManager.Instance.CurrentUserType = DataManager.UserType.Guest;

            this.Hide();

            var window = new UserWindow();
            window.ShowDialog();
        }

        private void LoginUser_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            var window = new LoginUser();
            window.ShowDialog();
        }

        private void LoginEmployee_Click(object sender, RoutedEventArgs e)
        {
        }

        private void LoginAdmin_Click(object sender, RoutedEventArgs e)
        {
            DataManager.Instance.CurrentUserType = DataManager.UserType.Admin;

            this.Hide();

            var window = new AdminLogin();
            window.ShowDialog();
        }

        private void RegisterUser_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            var window = new RegisterUser();
            window.ShowDialog();
        }

        private void RegisterEmployee_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
