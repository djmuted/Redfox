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
            int id = new Random().Next(99999);
            string name = "Guest" + id;
            this.UpdateUserData(user, id, name, true);
            return true;
        }

        public override bool Login(User user, string login, string password)
        {
            if (Core.UserManager.GetUser(login) != null)
            {
                return false; //user already logged in
            }
            else
            {
                int id = new Random().Next(99999);
                this.UpdateUserData(user, id, login, true); //is still a guest
            }
            return true;
        }
    }
}
