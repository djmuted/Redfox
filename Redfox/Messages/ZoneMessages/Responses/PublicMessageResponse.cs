using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Messages.ZoneMessages.Responses
{
    class PublicMessageResponse : IZoneResponseMessage
    {
        public string guid;
        public string message;
        public PublicMessageResponse(User user, string _message) : base("rfx#pm")
        {
            this.message = _message;
            this.guid = user.guid;
        }
    }
}
