using Hotel.Models.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.BusinessLogicLayer
{
    class UserBLL
    {
        /* Variables */
        UserDAL userDAL;

        /* Constructor */
        public UserBLL()
        {
            userDAL = new UserDAL();
            ClientsList = userDAL.GetUsers();
        }

        public List<Client> ClientsList { get; set; }

        public void Add(string name, string password)
        {
            if (String.IsNullOrEmpty(name) ||
                String.IsNullOrEmpty(password))
            {
                return;
            }

            var client = new Client();
            client.Name = name;
            client.Password = password;

            userDAL.Add(client);
        }

        public void Update(int id, string name, string password)
        {
            if (String.IsNullOrEmpty(name) ||
                String.IsNullOrEmpty(password))
            {
                return;
            }

            userDAL.Update(new Client() { Id_Client = id, Name = name, Password = password });
        }
    }
}
