using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Models
{
    [Serializable]
    class User
    {
        /* Properties */
        public string Name { get; set; }
        public string Image { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }

        /* Constructors */
        public User() {}
    }
}
