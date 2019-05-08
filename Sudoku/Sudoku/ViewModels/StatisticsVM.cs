using Sudoku.Models;
using Sudoku.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.ViewModels
{
    class StatisticsVM
    {
        public StatisticsVM()
        {
            Users = new ObservableCollection<User>();
            List<User> usersList = DataManager.Instance.LoadAllUsers();
            foreach (var item in usersList)
                Users.Add(item);
        }
        
        public ObservableCollection<User> Users { get; set; }
    }
}
