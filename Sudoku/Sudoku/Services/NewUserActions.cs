using Sudoku.Models;
using Sudoku.Util;
using Sudoku.ViewModels;
using Sudoku.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sudoku.Services
{
    class NewUserActions
    {
        /* Variables */
        private NewUserVM newUserVM;

        /* Constructors */
        public NewUserActions(NewUserVM newUserVM)
        {
            this.newUserVM = newUserVM;
        }

        /* Methods */
        public void Next(object param)
        {
            newUserVM.Image = ImageManager.Instance.GetNextUserImage();
        }

        public void Previous(object param)
        {
            newUserVM.Image = ImageManager.Instance.GetPreviousUserImage();
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

                MainWindow window = new MainWindow();
                window.ShowDialog();
            }
        }
    }
}
