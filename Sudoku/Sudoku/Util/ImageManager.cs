using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Util
{
    class ImageManager
    {
        /* Singleton Definition */
        private static readonly ImageManager instance = new ImageManager();
        public static ImageManager Instance { get => instance; }
        static ImageManager() { }
        private ImageManager()
        {
            userImages = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                userImages.Add( DataManager.Instance.StartupPath + @"\Images\user" + i + ".jpg");
            }
            usersIndex = 0;
        }

        /* Variables */
        private List<string> userImages;
        private int usersIndex;

        /* Methods */
        public string GetNextUserImage()
        {
            usersIndex = (usersIndex + 1) % userImages.Count;
            return userImages.ElementAt(usersIndex);
        }

        public string GetPreviousUserImage()
        {
            if (usersIndex - 1 >= 0)
                usersIndex = (usersIndex - 1) % userImages.Count;
            else
                usersIndex = userImages.Count - 1;

            return userImages.ElementAt(usersIndex);
        }

        public void ResetUserIndex()
        {
            usersIndex = 0;
        }
    }
}
