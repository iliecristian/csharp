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
    class SignInActions
    {
        private SignInVM signInVM;
        public SignInActions(SignInVM signInVM)
        {
            this.signInVM = signInVM;
        }

        public void NewUser(object param)
        {
            /* Do nothing */
            //SignInVM tempVM = param as SignInVM;
            //signInVM.Output = "EMPTY";

            SignIn signInView = param as SignIn;
            if (signInView != null)
            {
                NewUser newUserView = new NewUser(signInView);
                newUserView.ShowDialog();
            }
        }

        public void DeleteUser(object param)
        {
            /* TO DO */
            ListViewItem temp = param as ListViewItem;
            signInVM.Output = temp.Content as string;
        }
    }
}
