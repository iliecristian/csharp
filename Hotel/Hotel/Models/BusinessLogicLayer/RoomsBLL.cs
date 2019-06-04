using Hotel.Helpers;
using Hotel.Models.DataAccessLayer;
using Hotel.Models.EntityLayer;
using Hotel.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hotel.Models.BusinessLogicLayer
{
    class RoomsBLL
    {
        /* Variables */
        RoomsDAL roomsDAL;
        ImagesDAL imagesDAL;
        FeaturesDAL featuresDAL;

        /* Constructor */
        public RoomsBLL()
        {
            roomsDAL = new RoomsDAL();
            imagesDAL = new ImagesDAL();
            featuresDAL = new FeaturesDAL();

            RoomList = roomsDAL.GetRooms();
        }

        /* Properties */
        public ObservableCollection<Room> RoomList { get; set; }

        public void Create(object param)
        {
            var tuple = param as Tuple<string, string, string, string>;
            if (String.IsNullOrEmpty(tuple.Item1) ||
                String.IsNullOrEmpty(tuple.Item2) ||
                String.IsNullOrEmpty(tuple.Item3))
            {
                return;
            }

            roomsDAL.AddRoom(int.Parse(tuple.Item1), tuple.Item2, int.Parse(tuple.Item3));

            foreach (var image in DataManager.Instance.SelectedRoomImages)
            {
                var temp = image.Substring(image.LastIndexOf("\\") + 1);
                imagesDAL.Add(DataManager.Instance.LastRoom.Id_Camera, temp);
            }

            foreach (var feature in DataManager.Instance.SelectedRoomFeatures)
            {
                feature.IdRoom = DataManager.Instance.LastRoom.Id_Camera;
                featuresDAL.Add(feature);
            }

            DataManager.Instance.SelectedRoomFeatures.Clear();
            DataManager.Instance.SelectedRoomImages.Clear();
        }

        public void Update(object param)
        {
            var tuple = param as Tuple<string, string, string, string>;
            if (String.IsNullOrEmpty(tuple.Item1) ||
                String.IsNullOrEmpty(tuple.Item2) ||
                String.IsNullOrEmpty(tuple.Item3) ||
                String.IsNullOrEmpty(tuple.Item4))
            {
                return;
            }
            int id = int.Parse(tuple.Item4);
            roomsDAL.Update(id, int.Parse(tuple.Item1), tuple.Item2, int.Parse(tuple.Item3));

            imagesDAL.DeleteRoomImage(id);
            foreach (var image in DataManager.Instance.SelectedRoomImages)
            {
                var temp = image.Substring(image.LastIndexOf("\\") + 1);
                imagesDAL.Add(id, temp);
            }

            featuresDAL.DeleteRoomFeature(id);
            foreach (var feature in DataManager.Instance.SelectedRoomFeatures)
            {
                feature.IdRoom = id;
                featuresDAL.Add(feature);
            }

            DataManager.Instance.SelectedRoomFeatures.Clear();
            DataManager.Instance.SelectedRoomImages.Clear();
        }

        public void Delete(object param)
        {
            var list = (param as ListView);             
            if (list != null)
            {
                var index = list.SelectedIndex;
                roomsDAL.Delete(RoomList.ElementAt(index).Id);
                RoomList.RemoveAt(index);
            }
        }
    }
}
