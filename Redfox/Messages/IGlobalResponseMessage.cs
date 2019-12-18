using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Messages
{
    class IGlobalResponseMessage : IResponseMessage
    {
        public IGlobalResponseMessage(string _type) : base(_type, "global")
        {
        }
    }
}
