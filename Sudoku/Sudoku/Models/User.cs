using Sudoku.ViewModels;
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
        public List<Tile> SaveGame { get; set; }
        public PlayVM.TableSize LevelSize { get; set; }
        public int SecondsRemaining { get; set; }

        /* Constructors */
        public User() {}

        public override bool Equals(object obj)
        {
            User user = obj as User;

            if (user != null)
                return user.Name == Name;

            return false;
        }
    }
}
