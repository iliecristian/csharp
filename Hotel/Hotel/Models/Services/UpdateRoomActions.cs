using Hotel.Helpers;
using Hotel.Models.EntityLayer;
using Hotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Services
{
    class UpdateRoomActions
    {
        /* Variables */
        UpdateRoomVM viewModel;

        /* Constructor */
        public UpdateRoomActions(UpdateRoomVM vm)
        {
            viewModel = vm;
        }

        public void SelectImage(object param)
        {
            if (param != null)
            {
                var image = param as string;
                viewModel.ImagesList.Remove(image);
                DataManager.Instance.SelectedRoomImages.Add(image);
            }
        }

        public void SelectFeature(object param)
        {
            if (param != null)
            {
                var feature = param as Feature;
                viewModel.FeaturesList.Remove(feature);
                DataManager.Instance.SelectedRoomFeatures.Add(feature);
            }
        }
    }
}
