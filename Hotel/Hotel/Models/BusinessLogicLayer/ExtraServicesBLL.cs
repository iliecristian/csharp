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
    class ExtraServicesBLL
    {
        /* Variables */
        ExtraServicesDAL extraServicesDAL;

        /* Properties */
        public ObservableCollection<ExtraService> ExtraServicesList;

        /* Constructor */
        public ExtraServicesBLL()
        {
            extraServicesDAL = new ExtraServicesDAL();
            ExtraServicesList = extraServicesDAL.GetExtraServices();
        }

        public void Add(object param)
        {
            var tuple = param as Tuple<string, string>;

            string type = tuple.Item1;
            int price = int.Parse(tuple.Item2);

            if (String.IsNullOrEmpty(type))
            {
                return;
            }

            ExtraService service = new ExtraService()
            {
                Type = type,
                Price = price
            };
            extraServicesDAL.Add(service);
        }

        public void Update(object param)
        {
            var tuple = param as Tuple<string, string>;

            string type = tuple.Item1;
            int price = int.Parse(tuple.Item2);

            if (String.IsNullOrEmpty(type))
            {
                return;
            }

            ExtraService service = new ExtraService()
            {
                Id = DataManager.Instance.SelectedExtraService.Id,
                Type = type,
                Price = price
            };
            extraServicesDAL.Update(service);
        }

        public void Delete(object param)
        {
            var list = (param as ListView);
            if (list != null)
            {
                var index = list.SelectedIndex;
                extraServicesDAL.Delete(ExtraServicesList.ElementAt(index).Id);
                ExtraServicesList.RemoveAt(index);
            }
        }
    }
}
