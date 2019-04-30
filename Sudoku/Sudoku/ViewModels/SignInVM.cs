using Sudoku.Commands;
using Sudoku.Models;
using Sudoku.Services;
using Sudoku.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Input;

namespace Sudoku.ViewModels
{
    class SignInVM : BaseVM
    {
        /* Properties */
        public string Username { get; set; }
        public List<User> Users { get; set; }

        private User selectedUser;
        public object SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                User newValue = value as User;
                if (selectedUser == newValue)
                    return;

                selectedUser = newValue;
                OnPropertyChanged("SelectedUser");
            }

        }

        /* Variables */
        private SignInActions actions;

        /* Constructors */
        public SignInVM()
        {
            actions = new SignInActions(this);
            Users = DataManager.Instance.LoadAllUsers();
        }

        /* Output */
        private string output;
        public string Output
        {
            get { return output; }
            set
            {
                output = value;
                OnPropertyChanged("Output");
            } 
        }

        private bool canExecuteCommand = true;
        public bool CanExecuteCommand
        {
            get
            {
                return canExecuteCommand;
            }
            set
            {
                if (canExecuteCommand == value)
                    return;
                canExecuteCommand = value;
            }
        }

        public ICommand newUserCommand;
        public ICommand NewUserCommand
        {
            get
            {
                if (newUserCommand == null)
                    newUserCommand = new RelayCommand(actions.NewUser, param => CanExecuteCommand);
                return newUserCommand;
            }
        }
    }
}
