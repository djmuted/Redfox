using Redfox.Extensions.Events;
using Redfox.Zones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Extensions
{
    public abstract class RedfoxExtension
    {
        public ExtensionEventManager extensionEventManager { get; internal set; }
        public string ExtensionName { get; private set; }
        public Zone Zone { get; internal set; }
        public RedfoxExtension(string extensionName)
        {
            this.ExtensionName = extensionName;
        }
        public abstract void Initialize();
    }
}
