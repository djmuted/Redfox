using Newtonsoft.Json;
using NLog;
using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redfox.Messages
{
    public abstract class IMessageHandler
    {
        protected Dictionary<string, Type> handlers;
        private Type messageType;
        public string target { get; private set; }
        public IMessageHandler(Type _messageType)
        {
            this.messageType = _messageType;
            var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => messageType.IsAssignableFrom(p) && !p.IsAbstract);

            this.handlers = new Dictionary<string, Type>();
            foreach (Type type in types)
            {
                IRequestMessage message = Activator.CreateInstance(type) as IRequestMessage;
                this.handlers.Add(message.type, type);
                LogManager.GetCurrentClassLogger().Debug($"Added a new message handler {message.type} handled by class {type.Name}");
            }
        }
        public virtual void HandleMessage(User user, string message_str)
        {
            IRequestMessage message = JsonConvert.DeserializeObject<GenericMessage>(message_str);
            if (this.handlers.ContainsKey(message.type))
            {
                Type type = this.handlers[message.type];
                IRequestMessage typed_message = JsonConvert.DeserializeObject(message_str, type) as IRequestMessage;
                typed_message.Handle(user);
            }
            else
            {
                LogManager.GetCurrentClassLogger().Warn($"Unknown message type {message.type} for target {message.target}");
            }
        }
    }
}
