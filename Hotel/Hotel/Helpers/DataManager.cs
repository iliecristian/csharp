using Hotel.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Helpers
{
    class DataManager
    {
        /* Singleton Definition */
        private static readonly DataManager instance = new DataManager();
        public static DataManager Instance { get => instance; }
        static DataManager() { }
        private DataManager()
        {
            CurrentDiscounts = new List<Discount>();
            CurrentRooms = new List<Room>();
            CurrentServices = new List<ExtraService>();
        }

        public enum UserType
        {
            Guest,
            Client,
            Employee,
            Admin
        }

        public enum Table
        {
            Rooms,
            Features,
            ExtraServices,
            Discount
        }

        /* Properties */
        public UserType CurrentUserType { get; set; }
        public Table CurrentTable { get; set; }
        public Camera LastRoom
        {
            get
            {
                var context = new HotelEntities();

                Camera room = new Camera();
                foreach (var camera in context.Camera)
                {
                    room = camera;
                }
                return room;
            }
        }

        public bool UpdatePressed { get; set; }
        public int OldId { get; set; }

        public Feature SelectedFeature { get; set; }
        public ExtraService SelectedExtraService { get; set; }
        public Discount SelectedDiscount { get; set; }

        public Client CurrentUser { get; set; }

        public string DefaultImagePath { get => "Images"; }
        public string DefaultIcon
        {
            get => Path.Combine(System.IO.Directory.GetCurrentDirectory(), 
                                Path.Combine(DefaultImagePath, "default.png"));
        }
        public string AdminPassword { get => "123"; }

        private List<string> selectedRoomImages = new List<string>();
        public List<string> SelectedRoomImages
        {
            get => selectedRoomImages;
            set => selectedRoomImages = value;
        }

        private List<Feature> selectedRoomFeatures = new List<Feature>();
        public List<Feature> SelectedRoomFeatures
        {
            get => selectedRoomFeatures;
            set => selectedRoomFeatures = value;
        }

        /* Reservation */
        public List<Room> CurrentRooms { get; set; }
        public List<Discount> CurrentDiscounts { get; set; }
        public List<ExtraService> CurrentServices { get; set; }
        
        /* Methods */
        public ObservableCollection<Feature> SortFeatures(ObservableCollection<Feature> features)
        {
            ObservableCollection<Feature> result = new ObservableCollection<Feature>();
            foreach (var feature in features)
            {
                if (feature.IdRoom == null)
                {
                    result.Add(feature);
                }
            }
            return result;
        }
    }
}
