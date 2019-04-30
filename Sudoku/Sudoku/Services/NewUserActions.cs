using Sudoku.Models;
using Sudoku.Util;
using Sudoku.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Services
{
    class NewUserActions
    {
        private NewUserVM newUserVM;
        public NewUserActions(NewUserVM newUserVM)
        {
            this.newUserVM = newUserVM;
        }

        public void Next(object param)
        {

        }

        public void Previous(object param)
        {

        }

        public void Done(object param)
        {
            NewUserVM temp = param as NewUserVM;
            if (temp != null)
            {
                User user = new User
                {
                    Name = temp.Name,
                    Image = temp.Image
                };

                DataManager.Instance.AddUser(user);
                temp.CurrentWindow.Hide();
            }
        }
    }
}
