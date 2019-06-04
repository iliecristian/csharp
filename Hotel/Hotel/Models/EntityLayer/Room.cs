using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.EntityLayer
{
    class Room// : BasePropertyChanged
    {
        public int Id { get; set; }

        private string type;
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                //NotifyPropertyChanged("Type");
            }
        }

        private int price;
        public int Price
        {
            get { return price; }
            set
            {
                price = value;
                //NotifyPropertyChanged("Price");
            }
        }
    }
}
