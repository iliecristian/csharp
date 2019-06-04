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
    class UpdateFeatureVM
    {
        /* Variables */
        FeaturesBLL featuresBLL;

        /* Properties */
        public UpdateFeatureVM()
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
                    okCommand = new RelayCommand(featuresBLL.Update, param => true);
                }
                return okCommand;
            }
        }
    }
}
