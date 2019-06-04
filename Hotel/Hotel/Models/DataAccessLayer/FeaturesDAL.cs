using Hotel.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.DataAccessLayer
{
    class FeaturesDAL
    {
        public void Add(Feature feature)
        {
            var newFeature = new Dotare();

            newFeature.Id_Camera = feature.IdRoom;
            newFeature.Price = feature.Price;
            newFeature.Type = feature.Type;

            var context = new HotelEntities();
            context.Dotare.Add(newFeature);
            context.SaveChanges();
        }

        public void Update(Feature feature)
        {
            var context = new HotelEntities();

            var oldFeature = context.Dotare.First(i => i.Id_Dotare == feature.Id);
            oldFeature.Price = feature.Price;
            oldFeature.Type = feature.Type;

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var context = new HotelEntities();

            var feature = context.Dotare.First(i => i.Id_Dotare == id);
            feature.Deleted = true;

            context.SaveChanges();
        }

        public void DeleteRoomFeature(int id)
        {
            var context = new HotelEntities();

            var camera = context.Camera.First(i => i.Id_Camera == id);
            var features = camera.Dotare.Where(i => i.Id_Camera == id);

            var indexes = new List<int>();
            foreach (var feature in features)
                indexes.Add(feature.Id_Dotare);

            foreach (var index in indexes)
            {
                var toDelete = context.Dotare.First(i => i.Id_Dotare == index);
                context.Dotare.Remove(toDelete);
            }

            context.SaveChanges();
        }


        public ObservableCollection<Feature> GetFeatures()
        {
            ObservableCollection<Feature> result = new ObservableCollection<Feature>();
            var context = new HotelEntities();
            foreach (var feature in context.Dotare)
            {
                var newFeature = new Feature()
                {
                    Id = feature.Id_Dotare,
                    Price = feature.Price,
                    Type = feature.Type,
                    IdRoom = feature.Id_Camera
                };

                if (!feature.Deleted)
                    result.Add(newFeature);
            }
            return result;
        }

    }
}
