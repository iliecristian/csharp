using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Helpers
{
    class ImageManager
    {
        /* Singleton Definition */
        //private static readonly ImageManager instance = new ImageManager();
        //public static ImageManager Instance { get => instance; }
        //static ImageManager() { }
        //private ImageManager()
        //{
        //    roomImages = new List<string>();
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        roomImages.Add(System.IO.Directory.GetCurrentDirectory() + @"\Images\room" + i + ".jpg");
        //    }
        //    roomIndex = 0;
        //}

        /* Variables */
        public List<string> Images { get; set; }
        private int roomIndex;

        /* Methods */
        public void InitAllImages()
        {
            Images = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                Images.Add(System.IO.Directory.GetCurrentDirectory() + @"\Images\room" + i + ".jpg");
            }
            roomIndex = 0;
        }

        public ObservableCollection<string> ToObservableCollection()
        {
            var result = new ObservableCollection<string>();
            foreach(var image in Images)
            {
                result.Add(image);
            }
            return result;
        }

        public string GetNextImage()
        {
            if (Images == null)
                return DataManager.Instance.DefaultIcon;

            if (Images.Count != 0)
                roomIndex = (roomIndex + 1) % Images.Count;
            return Images.ElementAt(roomIndex);
        }

        public string GetPreviousImage()
        {
            if (Images == null)
                return DataManager.Instance.DefaultIcon;

            if (roomIndex - 1 >= 0)
                roomIndex = (roomIndex - 1) % Images.Count;
            else
                roomIndex = Images.Count - 1;

            return Images.ElementAt(roomIndex);
        }

        public void ResetIndex()
        {
            roomIndex = 0;
        }
    }
}
