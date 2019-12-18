using Redfox.Messages.ZoneMessages.Data;
using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Messages.ZoneMessages.Responses
{
    class UserJoinedRoomResponse : IZoneResponseMessage
    {
        public UserData userData;
        public UserJoinedRoomResponse(User user) : base("rfx#ujr")
        {
            this.userData = UserData.FromUser(user);
        }
    }
}
