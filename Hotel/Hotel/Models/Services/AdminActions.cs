using Hotel.Helpers;
using Hotel.Models.BusinessLogicLayer;
using Hotel.Models.DataAccessLayer;
using Hotel.Models.EntityLayer;
using Hotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Hotel.Models.Services
{
    class AdminActions
    {
        /* Variables */
        private AdminVM viewModel;
        private RoomsDAL roomsDAL;

        private RoomsBLL roomBLL;
        private FeaturesBLL featureBLL;
        private ExtraServicesBLL extraServicesBLL;
        private DiscountBLL discountBLL;

        /* Constructor */
        public AdminActions(AdminVM adminVM, 
                            RoomsBLL roomBLL, 
                            FeaturesBLL featureBLL, 
                            ExtraServicesBLL extraServicesBLL,
                            DiscountBLL discountBLL)
        {
            viewModel = adminVM;

            this.roomBLL = roomBLL;
            this.featureBLL = featureBLL;
            this.extraServicesBLL = extraServicesBLL;
            this.discountBLL = discountBLL;

            roomsDAL = new RoomsDAL();
            ImageManager = new ImageManager();
        }

        /* Properties */
        #region Properties
        public ImageManager ImageManager { get; set; }
        private Room currentRoom;
        public Room CurrentRoom
        {
            get { return currentRoom; }
            set
            {
                currentRoom = value;
                ImageManager.Images = roomsDAL.GetImages(value.Id);
            }
        }
        #endregion

        /* Methods */
        #region Methods
        public void SelectedTabChanged(object param)
        {
            Tuple<Image, TextBlock, TextBlock, string> tuple = param as Tuple<Image, TextBlock, TextBlock, string>;

            if (tuple.Item4 != DataManager.Instance.CurrentTable.ToString())
            {
                //tuple.Item1.Source = new BitmapImage(new Uri(DataManager.Instance.DefaultIcon));
                //tuple.Item2.Text = "";
                //tuple.Item3.Text = "";
            }

            switch (tuple.Item4)
            {
                case "Rooms":
                    DataManager.Instance.CurrentTable = DataManager.Table.Rooms;
                    break;
                case "Features":
                    DataManager.Instance.CurrentTable = DataManager.Table.Features;
                    break;
                case "Extra Services":
                    DataManager.Instance.CurrentTable = DataManager.Table.ExtraServices;
                    break;
                case "Discounts":
                    DataManager.Instance.CurrentTable = DataManager.Table.Discount;
                    break;

                default:
                    break;
            }
        }

        public void NextImage(object param)
        {
            viewModel.Image = ImageManager.GetNextImage();
        }

        public void PreviousImage(object param)
        {
            viewModel.Image = ImageManager.GetPreviousImage();
        }
        public void Delete(object param)
        {
            switch(DataManager.Instance.CurrentTable)
            {
                case DataManager.Table.Rooms:
                    roomBLL.Delete(param);
                    break;
                case DataManager.Table.Features:
                    featureBLL.Delete(param);
                    break;
                case DataManager.Table.ExtraServices:
                    extraServicesBLL.Delete(param);
                    break;
                case DataManager.Table.Discount:
                    discountBLL.Delete(param);
                    break;

                default:
                    break;
            }
        }

        #endregion
    }
}
