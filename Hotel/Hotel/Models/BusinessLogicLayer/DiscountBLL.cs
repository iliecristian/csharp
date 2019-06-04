using Hotel.Helpers;
using Hotel.Models.DataAccessLayer;
using Hotel.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hotel.Models.BusinessLogicLayer
{
    class DiscountBLL
    {
        /* Variables */
        public DiscountDAL discountDAL;

        /* Properties */
        public ObservableCollection<Discount> DiscountList;

        /* Constructor */
        public DiscountBLL()
        {
            discountDAL = new DiscountDAL();
            DiscountList = discountDAL.GetDiscounts();
        }

        public void Add(object param)
        {
            var tuple = param as Tuple<string, string, string, string>;

            string type = tuple.Item1;
            double value = double.Parse(tuple.Item2);
            DateTime stardDate = DateTime.ParseExact(tuple.Item3, "dd-MM-yyyy", null);
            DateTime endDate = DateTime.ParseExact(tuple.Item4, "dd-MM-yyyy", null);

            if (String.IsNullOrEmpty(type))
            {
                return;
            }

            Discount discount = new Discount()
            {
                Type = type,
                Value = value,
                StartDate = stardDate,
                EndDate = endDate
            };
            discountDAL.Add(discount);
        }

        public void Update(object param)
        {
            var tuple = param as Tuple<string, string, string, string>;

            string type = tuple.Item1;
            double value = double.Parse(tuple.Item2);
            DateTime stardDate = DateTime.ParseExact(tuple.Item3, "dd-MM-yyyy", null);
            DateTime endDate = DateTime.ParseExact(tuple.Item4, "dd-MM-yyyy", null);

            if (String.IsNullOrEmpty(type))
            {
                return;
            }

            Discount discount = new Discount()
            {
                Id = DataManager.Instance.SelectedDiscount.Id,
                Type = type,
                Value = value,
                StartDate = stardDate,
                EndDate = endDate
            };
            discountDAL.Update(discount);
        }

        public void Delete(object param)
        {
            var list = (param as ListView);
            if (list != null)
            {
                var index = list.SelectedIndex;
                discountDAL.Delete(DiscountList.ElementAt(index).Id);
                DiscountList.RemoveAt(index);
            }
        }
    }
}
