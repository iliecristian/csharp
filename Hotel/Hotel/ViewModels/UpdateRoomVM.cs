using Hotel.Helpers;
using Hotel.Models.BusinessLogicLayer;
using Hotel.Models.EntityLayer;
using Hotel.Models.Services;
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
    class UpdateRoomVM
    {
        /* Variables */
        private FeaturesBLL featuresBLL = new FeaturesBLL();
        private RoomsBLL roomsBLL = new RoomsBLL();

        private UpdateRoomActions updateRoomActions;
        private ImageManager imageManager;

        /* Constructor */
        public UpdateRoomVM()
        {
            imageManager = new ImageManager();
            imageManager.InitAllImages();
            ImagesList = imageManager.ToObservableCollection();
            updateRoomActions = new UpdateRoomActions(this);
            featuresBLL.FeatureList = DataManager.Instance.SortFeatures(featuresBLL.FeatureList);
        }

        /* Properties */
        public ObservableCollection<string> ImagesList { get; set; }
        public ObservableCollection<Feature> FeaturesList
        {
            get => featuresBLL.FeatureList;
            set => featuresBLL.FeatureList = value;
        }

        /* Commands */
        private ICommand selectImageCommand;
        public ICommand SelectImageCommand
        {
            get
            {
                if (selectImageCommand == null)
                {
                    selectImageCommand = new RelayCommand(updateRoomActions.SelectImage, param => true);
                }
                return selectImageCommand;
            }
        }

        private ICommand selectFeatureCommand;
        public ICommand SelectFeatureCommand
        {
            get
            {
                if (selectFeatureCommand == null)
                {
                    selectFeatureCommand = new RelayCommand(updateRoomActions.SelectFeature, param => true);
                }
                return selectFeatureCommand;
            }
        }

        private ICommand createCommand;
        public ICommand CreateCommand
        {
            get
            {
                if (createCommand == null)
                {
                    createCommand = new RelayCommand(roomsBLL.Update, param => true);
                }
                return createCommand;
            }
        }
    }
}
