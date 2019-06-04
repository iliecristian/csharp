using Hotel.Models.BusinessLogicLayer;
using Hotel.Models.EntityLayer;
using Hotel.Models.Services;
using Hotel.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.ViewModels
{
    class RegisterUserVM : BasePropertyChanged
    {
        /* Variables */
        RegisterUserActions registerUserActions;

        public RegisterUserVM()
        {
            registerUserActions = new RegisterUserActions(this);
            Title = "- Register User -";
        }

        /* Properties */
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                NotifyPropertyChanged("Title");
            }
        }
        
        
        /* Commands */
        private ICommand okCommnad;
        public ICommand OkCommnad
        {
            get
            {
                if (okCommnad == null)
                {
                    okCommnad = new RelayCommand(registerUserActions.Update, param => true);
                }
                return okCommnad;
            }
        }
    }
}
