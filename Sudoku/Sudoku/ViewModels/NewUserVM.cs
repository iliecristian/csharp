using Sudoku.Commands;
using Sudoku.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Sudoku.ViewModels
{
    class NewUserVM : BaseVM
    {
        /* Variables */
        private NewUserActions actions;

        /* Properties */
        private bool canExecuteCommnad = true;
        public bool CanExecuteCommand
        {
            get
            {
                return canExecuteCommnad;
            }
            set
            {
                if (canExecuteCommnad == value)
                    return;
                canExecuteCommnad = value;
            }
        }

        public string Name { get; set; }
        public string Image { get; set; }
        public Window CurrentWindow { get; set; }

        /* Constructors */
        public NewUserVM()
        {
            this.actions = new NewUserActions(this);
        }

        /* Commands */
        private ICommand onNext;
        public ICommand OnNext
        {
            get
            {
                if (onNext == null)
                    onNext = new RelayCommand(actions.Next, param => CanExecuteCommand);
                return onNext;
            }
        }

        private ICommand onPrevious;
        public ICommand OnPrevious
        {
            get
            {
                if (onPrevious == null)
                    onPrevious = new RelayCommand(actions.Previous, param => CanExecuteCommand);
                return onPrevious;
            }
        }

        private ICommand onDone;
        public ICommand OnDone
        {
            get
            {
                if (onDone == null)
                    onDone = new RelayCommand(actions.Done, param => CanExecuteCommand);
                return onDone;
            }
        }
    }
}
