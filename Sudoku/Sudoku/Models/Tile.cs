using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Models
{
    class Tile
    {
        /* Properties */
        public int Value { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool CanBeRemoved { get; set; }
        public BigTile BigTile { get; set; }
    }
}
