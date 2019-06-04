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
    class UpdateDiscountVM
    {
        /* Variables */
        DiscountBLL discountBLL;

        /* Properties */
        public UpdateDiscountVM()
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
                    okCommand = new RelayCommand(discountBLL.Update, param => true);
                }
                return okCommand;
            }
        }
    }
}
