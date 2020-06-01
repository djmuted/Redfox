using Newtonsoft.Json;
using NLog;
using Redfox.Extensions;
using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redfox.Messages
{
    class ZoneMessageHandler : IMessageHandler
    {
        public ZoneMessageHandler(List<RedfoxExtension> extensions) : base(typeof(IZoneRequestMessage))
        {
            foreach (RedfoxExtension extension in extensions)
            {
                foreach (Type type in Core.ExtensionManager.extensionHandlers[extension.ExtensionName])
                {
                    IRequestMessage message = Activator.CreateInstance(type) as IRequestMessage;
                    this.handlers.Add(message.type, type);
                    LogManager.GetCurrentClassLogger().Debug($"Added a new message handler {message.type} handled by class {type.Name}");
                }
            }
        }
    }
}
