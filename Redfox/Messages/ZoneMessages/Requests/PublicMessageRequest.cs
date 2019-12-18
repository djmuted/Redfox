using System;
using System.Collections.Generic;
using System.Text;
using Redfox.Messages.ZoneMessages.Responses;
using Redfox.Users;

namespace Redfox.Messages.ZoneMessages.Requests
{
    class PublicMessageRequest : IZoneRequestMessage
    {
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
