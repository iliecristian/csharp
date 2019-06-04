using Hotel.Helpers;
using Hotel.Models.BusinessLogicLayer;
using Hotel.ViewModels;
using Hotel.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hotel.Models.Services
{
    class LoginUserActions
    {
        /* Variables */
        UserBLL userBLL;
        LoginUserVM viewModel;

        /* Constructor */
        public LoginUserActions(LoginUserVM vm, UserBLL uBLL)
        {
            viewModel = vm;
            userBLL = uBLL;
        }

        public void OkPressed(object param)
        {
            var tuple = param as Tuple<string, PasswordBox>;
            DataManager.Instance.CurrentUserType = DataManager.UserType.Client;
            foreach(var user in userBLL.ClientsList)
            {
                if (user.Name == tuple.Item1 &&
                    user.Password == tuple.Item2.Password)
                {
                    DataManager.Instance.CurrentUser = new Client()
                    {
                        Id_Client = user.Id_Client,
                        Name = user.Name,
                        Password = user.Password
                    };

                    var window = new UserWindow();
                    window.ShowDialog();
                }
            }
        }
    }
}
