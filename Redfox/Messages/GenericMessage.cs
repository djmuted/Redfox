using System;
using System.Collections.Generic;
using System.Text;
using Redfox.Users;

namespace Redfox.Messages
{
    class GenericMessage : IRequestMessage
    {
        public GenericMessage() : base("", "")
        {

        }
        public override void Handle(User user)
        {

        }
    }
}
