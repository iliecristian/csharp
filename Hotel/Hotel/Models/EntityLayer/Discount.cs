using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.EntityLayer
{
    class Discount : BasePropertyChanged
    {
        public int Id { get; set; }

        private double valueDiscount;
        public double Value
        {
            get { return valueDiscount; }
            set
            {
                valueDiscount = value;
                NotifyPropertyChanged("Value");
            }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                NotifyPropertyChanged("Type");
            }
        }

        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
    }
}
