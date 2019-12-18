using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Messages
{
    public abstract class IResponseMessage
    {
        public string type;
        public string target;
        public IResponseMessage(string _type, string _target)
        {
            this.type = _type;
            this.target = _target;
        }
    }
}
