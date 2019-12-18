using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Zones
{
    public abstract class ZoneAuthenticator
    {
        public abstract bool Login(User user, string login, string password);
        public abstract bool GuestLogin(User user);

        public void UpdateUserData(User user, string guid, string name, bool isGuest)
        {
            user.guid = guid;
            user.name = name;
            user.IsGuest = isGuest;
        }
    }
}
