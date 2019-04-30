using Sudoku.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Sudoku.Util
{
    class DataManager
    {
        /* Singleton Definition */
        private static readonly DataManager instance = new DataManager();
        public static DataManager Instance { get => instance; }
        static DataManager() { }
        private DataManager() { Users = LoadAllUsers(); }

        /* Properties */
        public string StartupPath { get => System.IO.Directory.GetCurrentDirectory(); }
        //private List<User> users; // Not needed now!
        public List<User> Users { get; set; }

        /* Variables */
        private readonly string usersPath = "users.dat";

        /* Methods */
        public void AddUser(User user)
        {
            Users.Add(user);
            SaveAllUsers();
        }

        public void SaveAllUsers()
        {
            Save<List<User>>(Users, usersPath);
        }

        public List<User> LoadAllUsers()
        {
            return Load<List<User>>(usersPath);
        }

        /* Main Methods */
        public void Save<T>(T saveObject, string path)
        {
            File.WriteAllText(path, new JavaScriptSerializer().Serialize(saveObject));
        }

        public T Load<T>(string path)
        {
            if (File.Exists(path))
                return new JavaScriptSerializer().Deserialize<T>(File.ReadAllText(path));
            return default(T);
        }
    }
}
