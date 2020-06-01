using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Messages.ZoneMessages.Responses
{
    class PublicMessageResponse : IZoneResponseMessage
    {
        public int uid;
        public string message;
        public PublicMessageResponse(User user, string _message) : base("rfx#pm")
        {
            this.message = _message;
            this.uid = user.id;
        }
    }
}
