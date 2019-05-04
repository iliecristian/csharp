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
            List<Tile> Tiles = new List<Tile>();

            Tiles.Add(new Tile() { Value = 3, Row = 0, Column = 0, CanBeRemoved = false });
            Tiles.Add(new Tile() { Value = 4, Row = 0, Column = 1, CanBeRemoved = false });
            Tiles.Add(new Tile() { Value = 1, Row = 0, Column = 2, CanBeRemoved = false });
            Tiles.Add(new Tile() { Value = 0, Row = 0, Column = 3, CanBeRemoved = false });

            Tiles.Add(new Tile() { Value = 0, Row = 1, Column = 1, CanBeRemoved = false });
            Tiles.Add(new Tile() { Value = 2, Row = 1, Column = 2, CanBeRemoved = false });
            Tiles.Add(new Tile() { Value = 0, Row = 1, Column = 3, CanBeRemoved = false });
            Tiles.Add(new Tile() { Value = 0, Row = 1, Column = 4, CanBeRemoved = false });

            Tiles.Add(new Tile() { Value = 0, Row = 2, Column = 0, CanBeRemoved = false });
            Tiles.Add(new Tile() { Value = 0, Row = 2, Column = 1, CanBeRemoved = false });
            Tiles.Add(new Tile() { Value = 2, Row = 2, Column = 2, CanBeRemoved = false });
            Tiles.Add(new Tile() { Value = 0, Row = 2, Column = 3, CanBeRemoved = false });

            Tiles.Add(new Tile() { Value = 0, Row = 3, Column = 0, CanBeRemoved = false });
            Tiles.Add(new Tile() { Value = 1, Row = 3, Column = 1, CanBeRemoved = false });
            Tiles.Add(new Tile() { Value = 4, Row = 3, Column = 2, CanBeRemoved = false });
            Tiles.Add(new Tile() { Value = 3, Row = 3, Column = 3, CanBeRemoved = false });
            
            //DataManager.Instance.Save<List<Tile>>(Tiles, DataManager.Instance.StartupPath + @"\Levels\3x3_1.dat");
        }
    }
}
