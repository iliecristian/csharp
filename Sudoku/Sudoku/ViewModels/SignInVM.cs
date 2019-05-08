    using Sudoku.Commands;
using Sudoku.Models;
using Sudoku.Services;
using Sudoku.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<User> Users { get; set; }

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

                CanExecuteCommand = Validator.CanExecuteAction(newValue);
                
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

            Users = new ObservableCollection<User>();
            SelectedUser = null;
            List<User> usersList = DataManager.Instance.LoadAllUsers();
            foreach (var item in usersList)
                Users.Add(item);
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

        private ICommand newUserCommand;
        public ICommand NewUserCommand
        {
            get
            {
                if (newUserCommand == null)
                    newUserCommand = new RelayCommand(actions.NewUser, param => true);
                return newUserCommand;
            }
        }

        private ICommand deleteUserCommand;
        public ICommand DeleteUserCommand
        {
            get
            {
                if (deleteUserCommand == null)
                    deleteUserCommand = new RelayCommand(actions.DeleteUser, param => CanExecuteCommand);
                return deleteUserCommand;
            }
        }

        private ICommand playCommand;
        public ICommand PlayCommand
        {
            get
            {
                if (playCommand == null)
                    playCommand = new RelayCommand(actions.Play, param => CanExecuteCommand);
                return playCommand;
            }
        }

        private ICommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                    exitCommand = new RelayCommand(actions.Exit, param => true);
                return exitCommand;
            }
        }
    }
}
