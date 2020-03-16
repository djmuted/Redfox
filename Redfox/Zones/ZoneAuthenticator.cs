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

        public void UpdateUserData(User user, int _id, string _name, bool _isGuest)
        {
            user.id = _id;
            user.name = _name;
            user.IsGuest = _isGuest;
        }
    }
}
