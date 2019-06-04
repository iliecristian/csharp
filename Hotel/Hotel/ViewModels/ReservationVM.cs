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
    class ReservationVM : BasePropertyChanged
    {
        /* Business Logic Layer */
        private RoomsBLL roomBLL = new RoomsBLL();
        private FeaturesBLL featureBLL = new FeaturesBLL();
        private ExtraServicesBLL extraServicesBLL = new ExtraServicesBLL();
        private DiscountBLL discountBLL = new DiscountBLL();

        private ReservationActions actions;

        /* Constructor */
        public ReservationVM()
        {
            actions = new ReservationActions(this, roomBLL, featureBLL, extraServicesBLL, discountBLL);

            RoomList = new ObservableCollection<Room>();
            ExtraServicesList = new ObservableCollection<ExtraService>();
            FeatureList = new ObservableCollection<Feature>();
            DiscountList = new ObservableCollection<Discount>();
        }

        /* Properties */
        private string startDate;
        public DateTime startDateTime;
        public string StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                startDateTime = DateTime.ParseExact(startDate, "dd-MM-yyyy", null);
                NotifyPropertyChanged("StartDate");
            }
        }

        private string endDate;
        public DateTime endDateTime;
        public string EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                endDateTime = DateTime.ParseExact(startDate, "dd-MM-yyyy", null);
                NotifyPropertyChanged("EndDate");
            }
        }

        private string persons;
        public string Persons
        {
            get { return persons; }
            set
            {
                persons = value;
                NotifyPropertyChanged("Persons");
            }
        }

        private int totalInt = 0;
        public int TotalInt
        {
            get { return totalInt; }
            set
            {
                totalInt = value;
                Total = value.ToString();
            }
        }

        private string total;
        public string Total
        {
            get { return total; }
            set
            {
                total  = value;
                NotifyPropertyChanged("Total");
            }
        }

        private bool CanExecute()
        {
            if (DataManager.Instance.CurrentUserType == DataManager.UserType.Guest)
                return false;
            return true;
        }

        /* Lists */
        #region Lists
        public ObservableCollection<Room> RoomList
        {
            get;// => roomBLL.RoomList;
            set;// => roomBLL.RoomList = value;
        }

        public ObservableCollection<Feature> FeatureList
        {
            get;// => featureBLL.FeatureList;
            set;// => featureBLL.FeatureList = value;
        }

        public ObservableCollection<ExtraService> ExtraServicesList
        {
            get;// => extraServicesBLL.ExtraServicesList;
            set;// => extraServicesBLL.ExtraServicesList = value;
        }

        public ObservableCollection<Discount> DiscountList
        {
            get;// => discountBLL.DiscountList;
            set;// => discountBLL.DiscountList = value;
        }

        public ObservableCollection<Rezervare> ReservationList
        {
            get;// => discountBLL.DiscountList;
            set;// => discountBLL.DiscountList = value;
        }
        #endregion


        /* Commands */
        private ICommand search;
        public ICommand Search
        {
            get
            {
                if (search == null)
                {
                    search = new RelayCommand(actions.Search, param => true);
                }
                return search;
            }
        }

        private ICommand selectRoom;
        public ICommand SelectRoom
        {
            get
            {
                if (selectRoom == null)
                {
                    selectRoom = new RelayCommand(actions.SelectRoom, param => CanExecute());
                }
                return selectRoom;
            }
        }

        private ICommand selectService;
        public ICommand SelectService
        {
            get
            {
                if (selectService == null)
                {
                    selectService = new RelayCommand(actions.SelectService, param => CanExecute());
                }
                return selectService;
            }
        }

        private ICommand selectDiscount;
        public ICommand SelectDiscount
        {
            get
            {
                if (selectDiscount == null)
                {
                    selectDiscount = new RelayCommand(actions.SelectDiscount, param => CanExecute());
                }
                return selectDiscount;
            }
        }

        private ICommand addReservation;
        public ICommand AddReservation
        {
            get
            {
                if (addReservation == null)
                {
                    addReservation = new RelayCommand(actions.AddReservation, param => CanExecute());
                }
                return addReservation;
            }
        }

        private ICommand cancelReservation;
        public ICommand CancelReservation
        {
            get
            {
                if (cancelReservation == null)
                {
                    cancelReservation = new RelayCommand(actions.CancelReservation, param => CanExecute());
                }
                return cancelReservation;
            }
        }

        private ICommand payReservation;
        public ICommand PayReservation
        {
            get
            {
                if (payReservation == null)
                {
                    payReservation = new RelayCommand(actions.PayReservation, param => CanExecute());
                }
                return payReservation;
            }
        }
    }
}
