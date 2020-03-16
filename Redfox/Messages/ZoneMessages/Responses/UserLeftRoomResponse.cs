using Newtonsoft.Json;
using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Messages.ZoneMessages.Responses
{
    class UserLeftRoomResponse : IZoneResponseMessage
    {
        [JsonProperty]
        int id;
        public UserLeftRoomResponse(User user) : base("rfx#ulr")
        {
            this.id = user.id;
        }
    }
}
