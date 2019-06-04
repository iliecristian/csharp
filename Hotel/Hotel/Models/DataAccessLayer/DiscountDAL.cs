using Hotel.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.DataAccessLayer
{
    class DiscountDAL
    {
        public ObservableCollection<Discount> GetDiscounts()
        {
            ObservableCollection<Discount> result = new ObservableCollection<Discount>();
            var context = new HotelEntities();
            foreach (var discount in context.Oferta)
            {
                var newDiscount = new Discount()
                {
                    Id = discount.Id_Oferta,
                    Value = discount.Value,
                    Type = discount.Type,
                    StartDate = discount.StartDate,
                    EndDate = discount.EndDate
                };

                if (!discount.Deleted)
                    result.Add(newDiscount);
            }
            return result;
        }

        public void Add(Discount discount)
        {
            var newDiscount = new Oferta();

            newDiscount.Value = discount.Value;
            newDiscount.Type = discount.Type;
            newDiscount.StartDate = discount.StartDate;
            newDiscount.EndDate = discount.EndDate;

            var context = new HotelEntities();
            context.Oferta.Add(newDiscount);
            context.SaveChanges();
        }

        public void Update(Discount service)
        {
            var context = new HotelEntities();

            var oldService = context.Oferta.First(i => i.Id_Oferta == service.Id);
            oldService.Value = service.Value;
            oldService.Type = service.Type;
            oldService.StartDate = service.StartDate;
            oldService.EndDate = service.EndDate;

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var context = new HotelEntities();

            var discount = context.Oferta.First(i => i.Id_Oferta == id);
            discount.Deleted = true;

            context.SaveChanges();
        }
    }
}
