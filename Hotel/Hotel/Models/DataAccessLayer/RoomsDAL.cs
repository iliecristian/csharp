using Hotel.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.DataAccessLayer
{
    class RoomsDAL
    {
        public void AddRoom(int roomsNr, string type, int price)
        {
            var newRoom = new Camera();

            newRoom.NrCamere = roomsNr;
            newRoom.Type = type;
            newRoom.Price = price;

            var context = new HotelEntities();
            context.Camera.Add(newRoom);
            context.SaveChanges();
        }

        public void Update(int id, int roomsNr, string type, int price)
        {
            var context = new HotelEntities();

            foreach(var camera in context.Camera)
            {
                if (camera.Id_Camera == id)
                {
                    camera.NrCamere = roomsNr;
                    camera.Type = type;
                    camera.Price = price;
                    break;
                }
            }
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var context = new HotelEntities();

            var room = context.Camera.First(i => i.Id_Camera == id);
            room.Deleted = true;

            var features = room.Dotare.Where(i => i.Id_Camera == id);
            foreach(var feature in features)
            {
                feature.Deleted = true;
            }

            var images = room.Imagine.Where(i => i.Id_Camera == id);
            foreach(var image in images)
            {
                image.Deleted = true;
            }

            context.SaveChanges();
        }

        public ObservableCollection<Room> GetRooms()
        {
            ObservableCollection<Room> result = new ObservableCollection<Room>();
            var context = new HotelEntities();
            foreach (var room in context.Camera)
            {
                var newRoom = new Room()
                {
                    Id = room.Id_Camera,
                    Price = room.Price,
                    Type = room.Type
                };

                if (!room.Deleted)
                    result.Add(newRoom);
            }
            return result;
        }

        public List<string> GetImages(int id)
        {
            var context = new HotelEntities();
            List<string> result = new List<string>();
            foreach (var room in context.Camera)
            {
                if (room.Id_Camera == id)
                {
                    foreach(var image in room.Imagine)
                    {
                        if (!image.Deleted)
                            result.Add(Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"Images\" + image.Image));
                    }
                    return result;
                }
            }
            return null;
        }
    }
}
