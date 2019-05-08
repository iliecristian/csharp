using Sudoku.Models;
using Sudoku.Util;
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

namespace Sudoku.Views
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : UserControl
    {
        public SignIn()
        {
            InitializeComponent();

            /* Used to create new Sudoku Levels */
            /*
            List<Tile> Tiles = new List<Tile>();
            List<int> list = new List<int>()
            {
                0,0,0,3,
                3,2,4,0,
                0,4,3,2,
                2,0,0,0
            };
            foreach (int elem in list)
            {
                Tiles.Add(new Tile() { Value = elem });
            }
            DataManager.Instance.Save<List<Tile>>(Tiles, DataManager.Instance.StartupPath + @"\Levels\3x3_3.dat");
            */
        }
    }
}
