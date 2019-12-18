using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Messages
{
    public abstract class IGlobalRequestMessage : IRequestMessage
    {
        public IGlobalRequestMessage(string _type) : base(_type, "global")
        {

        }
    }
}
