using Newtonsoft.Json;
using NLog;
using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redfox.Messages
{
    class ZoneMessageHandler : IMessageHandler
    {
        public ZoneMessageHandler() : base(typeof(IZoneRequestMessage))
        {

        }
    }
}
