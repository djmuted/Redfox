using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Messages.ZoneMessages.Responses
{
    class UserLeftRoomResponse : IZoneResponseMessage
    {
        string guid;
        public UserLeftRoomResponse(User user) : base("rfx#ulr")
        {
            this.guid = user.guid;
        }
    }
}
