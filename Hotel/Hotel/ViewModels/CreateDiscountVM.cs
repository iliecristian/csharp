using Hotel.Models.BusinessLogicLayer;
using Hotel.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.ViewModels
{
    class CreateDiscountVM
    {
        /* Variables */
        DiscountBLL discountBLL;

        /* Properties */
        public CreateDiscountVM()
        {
            discountBLL = new DiscountBLL();
        }

        /* Commands */
        private ICommand okCommand;
        public ICommand OkCommand
        {
            get
            {
                if (okCommand == null)
                {
                    okCommand = new RelayCommand(discountBLL.Add, param => true);
                }
                return okCommand;
            }
        }
    }
}
