using Hotel.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.DataAccessLayer
{
    class ExtraServicesDAL
    {
        public ObservableCollection<ExtraService> GetExtraServices()
        {
            ObservableCollection<ExtraService> result = new ObservableCollection<ExtraService>();
            var context = new HotelEntities();
            foreach (var extraService in context.ServiciiSuplimentare)
            {
                var newExtraService = new ExtraService()
                {
                    Id = extraService.Id_ServiciiSuplimentare,
                    Price = extraService.Price,
                    Type = extraService.Type
                };

                if (!extraService.Deleted)
                    result.Add(newExtraService);
            }
            return result;
        }

        public void Add(ExtraService service)
        {
            var newService = new ServiciiSuplimentare();
            
            newService.Price = service.Price;
            newService.Type = service.Type;

            var context = new HotelEntities();
            context.ServiciiSuplimentare.Add(newService);
            context.SaveChanges();
        }

        public void Update(ExtraService service)
        {
            var context = new HotelEntities();

            var oldService = context.ServiciiSuplimentare.First(i => i.Id_ServiciiSuplimentare == service.Id);
            oldService.Price = service.Price;
            oldService.Type = service.Type;

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var context = new HotelEntities();

            var services = context.ServiciiSuplimentare.First(i => i.Id_ServiciiSuplimentare == id);
            services.Deleted = true;

            context.SaveChanges();
        }
    }
}
