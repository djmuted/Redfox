using Redfox.Network.NetworkClients;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Users
{
    class UserManager
    {
        internal List<User> users { get; private set; }

        public UserManager()
        {
            this.users = new List<User>();
        }
        internal void AddUser(INetworkClient client)
        {
            User user = new User(client);
            this.users.Add(user);
        }
        internal void RemoveUser(User user)
        {
            if (users.Contains(user))
            {
                users.Remove(user);
            }
        }
    }
}
