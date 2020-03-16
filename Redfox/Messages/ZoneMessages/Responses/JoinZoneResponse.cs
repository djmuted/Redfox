using Redfox.Messages.ZoneMessages.Data;
using Redfox.Rooms;
using System;
using System.Collections.Generic;
using Redfox.Zones;
using System.Text;
using Redfox.Users;

namespace Redfox.Messages.ZoneMessages.Responses
{
    class JoinZoneResponse : IZoneResponseMessage
    {
        public string zoneName;
        public UserData userData;
        public JoinZoneResponse(Zone zone, User user) : base("rfx#jz")
        {
            this.zoneName = zone.name;
            this.userData = UserData.FromUser(user);
        }
    }
}
