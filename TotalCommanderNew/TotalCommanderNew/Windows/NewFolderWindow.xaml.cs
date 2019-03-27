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
using TotalCommanderNew.Views;

namespace TotalCommanderNew.Windows
{
    /// <summary>
    /// Interaction logic for NewFolderWindow.xaml
    /// </summary>
    public partial class NewFolderWindow : Window
    {
        /* Main Window Reference */
        ListViewView listView = null;
        private string NewFolderName { get; set; }

        public NewFolderWindow()
        {
            InitializeComponent();
        }

        public NewFolderWindow(ListViewView window)
        {
            InitializeComponent();
            listView = window;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            listView.NewFolderName = InputBox.Text.Replace("\r\n", string.Empty);
            NewFolderName = InputBox.Text.Replace("\r\n", string.Empty);
            this.Hide();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
