using Hotel.Models.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.BusinessLogicLayer
{
    class ImagesBLL
    {
        /* Variables */
        private ImagesDAL imagesDAL;

        /* Constructor */
        public ImagesBLL()
        {
            imagesDAL = new ImagesDAL();
        }
    }
}
