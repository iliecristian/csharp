using Hotel.Models.BusinessLogicLayer;
using Hotel.Models.EntityLayer;
using Hotel.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.ViewModels
{
    class CreateFeatureVM
    {
        /* Variables */
        FeaturesBLL featuresBLL;

        /* Properties */
        public CreateFeatureVM()
        {
            featuresBLL = new FeaturesBLL();
        }

        /* Commands */
        private ICommand okCommand;
        public ICommand OkCommand
        {
            get
            {
                if (okCommand == null)
                {
                    okCommand = new RelayCommand(featuresBLL.Add, param => true);
                }
                return okCommand;
            }
        }
    }
}
