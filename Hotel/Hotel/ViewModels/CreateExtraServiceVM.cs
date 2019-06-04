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
    class CreateExtraServiceVM
    {
        /* Variables */
        ExtraServicesBLL extraServicesBLL;

        /* Properties */
        public CreateExtraServiceVM()
        {
            extraServicesBLL = new ExtraServicesBLL();
        }

        /* Commands */
        private ICommand okCommand;
        public ICommand OkCommand
        {
            get
            {
                if (okCommand == null)
                {
                    okCommand = new RelayCommand(extraServicesBLL.Add, param => true);
                }
                return okCommand;
            }
        }
    }
}
