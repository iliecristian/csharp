using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.ViewModels.Commands
{
    class RelayCommand : ICommand
    {
        private Action<object> commandTask;
        private Predicate<object> canExecuteTask;

        public RelayCommand(Action<object> workToDo)
        {
            commandTask = workToDo;
        }

        public RelayCommand(Action<object> workToDo, Predicate<object> canExecute)
        {
            commandTask = workToDo;
            canExecuteTask = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteTask != null && canExecuteTask(parameter);
        }

        public void Execute(object parameter)
        {
            commandTask(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}
