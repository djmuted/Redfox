using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Redfox.Messages.ZoneMessages.Responses;
using Redfox.Users;

namespace Redfox.Messages.ZoneMessages.Requests
{
    class PublicMessageRequest : IZoneRequestMessage
    {
        [JsonProperty]
        public string message;

        public PublicMessageRequest() : base("rfx#pm")
        {
        }

        public override void Handle(User user)
        {
            user.Room?.SendMessage(new PublicMessageResponse(user, message));
        }
    }
}
