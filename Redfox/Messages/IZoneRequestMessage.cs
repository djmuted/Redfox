using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Messages
{
    public abstract class IZoneRequestMessage : IRequestMessage
    {
        public IZoneRequestMessage(string _type) : base(_type, "zone")
        {

        }
    }
}
