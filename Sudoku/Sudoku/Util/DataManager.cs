using Sudoku.Models;
using Sudoku.ViewModels;
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
        private DataManager()
        {
            Users = LoadAllUsers();
            if (Users == null)
                Users = new List<User>();
        }

        /* Properties */
        public string StartupPath { get => System.IO.Directory.GetCurrentDirectory(); }
        public List<User> Users { get; set; }
        public User CurrentUser { get; set; }

        /* Variables */
        private readonly string usersPath = "users.dat";

        /* Methods */
        public void AddUser(User user)
        {
            Users.Add(user);
            SaveAllUsers();
        }

        public void RemoveUser(int index)
        {
            Users.RemoveAt(index);
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

        public List<Tile> LoadLevel(PlayVM.TableSize tableSize)
        {
            switch (tableSize)
            {
                case PlayVM.TableSize._4x4:
                    {
                        return DataManager.Instance.Load<List<Tile>>(DataManager.Instance.StartupPath + @"\Levels\4x4_1.dat");
                    }
                case PlayVM.TableSize._6x6:
                    {
                        return DataManager.Instance.Load<List<Tile>>(DataManager.Instance.StartupPath + @"\Levels\6x6_1.dat");
                    }
                case PlayVM.TableSize._9x9:
                    {
                        return DataManager.Instance.Load<List<Tile>>(DataManager.Instance.StartupPath + @"\Levels\9x9_1.dat");
                    }

                default:
                    {
                        return DataManager.Instance.Load<List<Tile>>(DataManager.Instance.StartupPath + @"\Levels\4x4_1.dat");
                    }
            }
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
