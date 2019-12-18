using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Messages
{
    class IZoneResponseMessage : IResponseMessage
    {
        public IZoneResponseMessage(string _type) : base(_type, "zone")
        {
        }
    }
}
