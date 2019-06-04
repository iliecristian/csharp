using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.DataAccessLayer
{
    class ImagesDAL
    {
        public void Add(int roomId, string image)
        {
            var newImage = new Imagine();

            newImage.Id_Camera = roomId;
            newImage.Image = image;

            var context = new HotelEntities();
            context.Imagine.Add(newImage);
            context.SaveChanges();
        }

        public void DeleteRoomImage(int id)
        {
            var context = new HotelEntities();

            var camera = context.Camera.First(i => i.Id_Camera == id);
            var images = camera.Imagine.Where(i => i.Id_Camera == id);

            var indexes = new List<int>();
            foreach(var image in images)
                indexes.Add(image.Id_Imagine);

            foreach(var index in indexes)
            {
                var toDelete = context.Imagine.First(i => i.Id_Imagine == index);
                context.Imagine.Remove(toDelete);
            }

            context.SaveChanges();
        }
    }
}
