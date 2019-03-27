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
using TotalCommanderNew.Views;

namespace TotalCommanderNew.Windows
{
    ///<summary>
    /// Interaction logic for NewArhiveWindoe.xaml
    ///</summary>
    public partial class NewArchiveWindow : Window
    {
        /* Main Window Reference */
        ListViewView listView = null;

        public NewArchiveWindow(ListViewView window)
        {
            InitializeComponent();
            listView = window;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            listView.NewArchiveName = (InputBox.Text.Replace("\n", string.Empty)).Replace("\r", string.Empty) + ".rar";
            this.Hide();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
