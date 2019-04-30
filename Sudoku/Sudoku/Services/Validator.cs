using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Services
{
    class Validator
    {
        public static bool CanExecuteAction(string username)
        {
            if (username != "")
                return true;
            return false;
        }
    }
}
