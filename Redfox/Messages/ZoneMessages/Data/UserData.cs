using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Messages.ZoneMessages.Data
{
    class UserData
    {
        public string guid;
        public string name;

        public static UserData FromUser(User user)
        {
            UserData userData = new UserData();
            //TODO
            userData.guid = user.guid;
            return userData;
        }
    }
}
