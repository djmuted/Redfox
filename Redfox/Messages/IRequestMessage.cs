using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Messages
{
    public abstract class IRequestMessage
    {
        public string type;
        public string target;

        public IRequestMessage(string _type, string _target)
        {
            this.type = _type;
            this.target = _target;
        }
        public abstract void Handle(User user);
    }
}
