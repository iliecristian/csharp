using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.DataAccessLayer
{
    class UserDAL
    {
        public List<Client> GetUsers()
        {
            var context = new HotelEntities();
            List<Client> result = new List<Client>();
            foreach (var client in context.Client)
            {
                Client newClient = new Client()
                {
                    Id_Client = client.Id_Client,
                    Name = client.Name,
                    Password = client.Password
                };

                if (!client.Deleted)
                    result.Add(client);
            }
            return result;
        }

        public void Add(Client client)
        {
            var context = new HotelEntities();
            context.Client.Add(client);
            context.SaveChanges();
        }

        public void Update(Client newClient)
        {
            var context = new HotelEntities();

            var oldClient = context.Client.First(i => i.Id_Client == newClient.Id_Client);
            oldClient.Name = newClient.Name;
            oldClient.Password = newClient.Password;
            
            context.SaveChanges();
        }
    }
}
