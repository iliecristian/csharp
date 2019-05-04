using Sudoku.Models;
using Sudoku.Util;
using Sudoku.ViewModels;
using Sudoku.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            SignIn signInView = param as SignIn;
            if (signInView != null)
            {
                /* Create the New User Window */
                NewUser newUserView = new NewUser(signInView);

                /* Hide the Window that hold up the SignIn UserControl */
                var grandParent = (signInView.Parent as Grid).Parent as MainWindow;
                grandParent.Hide();

                /* Showe the New User Window */
                newUserView.ShowDialog();
            }
        }

        public void DeleteUser(object param)
        {
            SignIn temp = param as SignIn;
            User selectedUser = signInVM.SelectedUser as User;

            /* Remove User from the GUI list */
            int index = signInVM.Users.IndexOf(selectedUser);
            signInVM.Users.RemoveAt(index);

            /* Remove User from the *.dat file as well */
            DataManager.Instance.RemoveUser(index);

            /* Notify the change of the property */
            signInVM.OnPropertyChanged("Users");
        }

        public void Play(object param)
        {
            SignIn signInView = param as SignIn;
            if (signInView != null)
            {
                /* Save the current User so the next Window will know about it */
                DataManager.Instance.CurrentUser = signInVM.SelectedUser as User;

                /* Hide the Window that hold up the SignIn UserControl */
                var grandParent = (signInView.Parent as Grid).Parent as MainWindow;
                grandParent.Hide();

                /* Show the Play Window */
                Views.Play playWindow = new Views.Play();
                playWindow.ShowDialog();
            }
        }

        public void Exit(object param)
        {
            SignIn signInView = param as SignIn;
            if (signInView != null)
            {
                /* Hide the Window that hold up the SignIn UserControl */
                var grandParent = (signInView.Parent as Grid).Parent as MainWindow;
                grandParent.Close();
            }
        }
    }
}
