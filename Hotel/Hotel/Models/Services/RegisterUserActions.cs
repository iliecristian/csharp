using Hotel.Helpers;
using Hotel.Models.BusinessLogicLayer;
using Hotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Services
{
    class RegisterUserActions
    {
        /* Variables */
        private RegisterUserVM viewModel;
        private UserBLL userBLL = new UserBLL();

        public RegisterUserActions(RegisterUserVM vm)
        {
            viewModel = vm;
        }

        /* Methods */
        public void Update(object param)
        {
            var tuple = param as Tuple<bool, bool, string, string>;
            string name = tuple.Item3;
            string password = tuple.Item4;


            if (tuple.Item1) /* Register */
            {
                userBLL.Add(name, password);
            }
            else if (tuple.Item2) /* Update */
            {
                if (DataManager.Instance.UpdatePressed)
                {
                    DataManager.Instance.UpdatePressed = false;
                    userBLL.Update(DataManager.Instance.OldId, name, password);
                }
                else
                {
                    DataManager.Instance.UpdatePressed = true;
                    foreach (var user in userBLL.ClientsList)
                    {
                        if (user.Name == name &&
                            user.Password == password)
                        {
                            viewModel.Title = "- Update User -";
                            DataManager.Instance.OldId = user.Id_Client;
                        }
                    }
                }
            }
        }
    }
}
