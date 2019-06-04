using Hotel.Models.BusinessLogicLayer;
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
    class LoginUserVM
    {
        /* Variables */
        LoginUserActions loginUserActions;
        UserBLL userBLL;


        /* Constructor */
        public LoginUserVM()
        {
            userBLL = new UserBLL();

            loginUserActions = new LoginUserActions(this, userBLL);
        }
        
        /* Commands */
        private ICommand okCommnad;
        public ICommand OkCommnad
        {
            get
            {
                if (okCommnad == null)
                {
                    okCommnad = new RelayCommand(loginUserActions.OkPressed, param => true);
                }
                return okCommnad;
            }
        }
    }
}
