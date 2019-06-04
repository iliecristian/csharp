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
    class AdminVM : BasePropertyChanged
    {
        /* Business Logic Layer */
        private RoomsBLL roomBLL = new RoomsBLL();
        private FeaturesBLL featureBLL = new FeaturesBLL();
        private ExtraServicesBLL extraServicesBLL = new ExtraServicesBLL();
        private DiscountBLL discountBLL = new DiscountBLL();

        /* Actions */
        private AdminActions adminActions;

        public AdminVM()
        {
            RoomList = roomBLL.RoomList;
            Image = DataManager.Instance.DefaultIcon;
            DescriptionText = "";
            PriceText = "";

            adminActions = new AdminActions(this, roomBLL, featureBLL, extraServicesBLL, discountBLL);
        }

        /* Poperties */
        #region Properties
        private string image;
        public string Image
        {
            get { return image; }
            set
            {
                image = value;
                NotifyPropertyChanged("Image");
            }
        }

        private string descriptionText;
        public string DescriptionText
        {
            get { return descriptionText; }
            set
            {
                descriptionText = value;
                NotifyPropertyChanged("DescriptionText");
            }
        }

        private string priceText;
        public string PriceText
        {
            get { return priceText; }
            set
            {
                priceText = value;
                NotifyPropertyChanged("PriceText");
            }
        }

        private Room selectedRoom;
        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;

                if (value == null)
                {
                    DescriptionText = "";
                    PriceText = "";
                    Image = DataManager.Instance.DefaultIcon;
                }
                else
                {
                    DescriptionText = value.Type;
                    PriceText = value.Price.ToString();
                    adminActions.CurrentRoom = value;
                    Image = adminActions.ImageManager.GetNextImage();
                }
                NotifyPropertyChanged("SelectedRoom");
            }
        }

        private Feature selectedFeature;
        public Feature SelectedFeature
        {
            get { return selectedFeature; }
            set
            {
                selectedFeature = value;
                DataManager.Instance.SelectedFeature = value;
                NotifyPropertyChanged("SelectedFeature");
            }
        }

        private ExtraService selectedExtraService;
        public ExtraService SelectedExtraService
        {
            get { return selectedExtraService; }
            set
            {
                selectedExtraService = value;
                DataManager.Instance.SelectedExtraService = value;
                NotifyPropertyChanged("SelectedExtraService");
            }
        }

        private Discount selectedDiscount;
        public Discount SelectedDiscount
        {
            get { return selectedDiscount; }
            set
            {
                selectedDiscount = value;
                DataManager.Instance.SelectedDiscount = value;
                NotifyPropertyChanged("SelectedDiscount");
            }
        }
        #endregion

        /* Lists */
        #region Lists
        public ObservableCollection<Room> RoomList
        {
            get => roomBLL.RoomList;
            set => roomBLL.RoomList = value;
        }

        public ObservableCollection<Feature> FeatureList
        {
            get => featureBLL.FeatureList;
            set => featureBLL.FeatureList = value;
        }

        public ObservableCollection<ExtraService> ExtraServicesList
        {
            get => extraServicesBLL.ExtraServicesList;
            set => extraServicesBLL.ExtraServicesList = value;
        }

        public ObservableCollection<Discount> DiscountList
        {
            get => discountBLL.DiscountList;
            set => discountBLL.DiscountList = value;
        }
        #endregion

        /* Commands */
        #region Commands
        private ICommand selectedTabChangedCommand;
        public ICommand SelectedTabChangedCommand
        {
            get
            {
                if (selectedTabChangedCommand == null)
                {
                    selectedTabChangedCommand = new RelayCommand(adminActions.SelectedTabChanged, param => true);
                }
                return selectedTabChangedCommand;
            }
        }

        private ICommand nextCommand;
        public ICommand NextCommand
        {
            get
            {
                if (nextCommand == null)
                {
                    nextCommand = new RelayCommand(adminActions.NextImage, param => true);
                }
                return nextCommand;
            }
        }

        private ICommand previousCommand;
        public ICommand PreviousCommand
        {
            get
            {
                if (previousCommand == null)
                {
                    previousCommand = new RelayCommand(adminActions.PreviousImage, param => true);
                }
                return previousCommand;
            }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(adminActions.Delete, param => true);
                }
                return deleteCommand;
            }
        }
        #endregion
    }
}
