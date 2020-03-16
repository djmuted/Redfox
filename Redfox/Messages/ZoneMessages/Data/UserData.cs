using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Messages.ZoneMessages.Data
{
    class UserData
    {
        public int id;
        public string name;

        public static UserData FromUser(User user)
        {
            UserData userData = new UserData();
            //TODO
            userData.name = user.name;
            userData.id = user.id;
            return userData;
        }
    }
}
