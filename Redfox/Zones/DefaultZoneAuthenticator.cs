using System;
using System.Collections.Generic;
using System.Text;
using Redfox.Users;

namespace Redfox.Zones
{
    class DefaultZoneAuthenticator : ZoneAuthenticator
    {
        public override bool GuestLogin(User user)
        {
            string guid = Guid.NewGuid().ToString("N");
            string name = "Guest" + new Random().Next(99999);
            this.UpdateUserData(user, guid, name, true);
            return true;
        }

        public override bool Login(User user, string login, string password)
        {
            return false; //no account login
        }
    }
}
