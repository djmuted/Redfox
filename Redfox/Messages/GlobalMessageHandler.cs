using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Redfox.Users;

namespace Redfox.Messages
{
    class GlobalMessageHandler : IMessageHandler
    {
        public GlobalMessageHandler() : base(typeof(IGlobalRequestMessage))
        {

        }
    }
}
