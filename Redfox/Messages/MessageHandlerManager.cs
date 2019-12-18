using Newtonsoft.Json;
using NLog;
using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redfox.Messages
{
    class MessageHandlerManager
    {
        GlobalMessageHandler globalMessageHandler;
        public MessageHandlerManager()
        {
            this.globalMessageHandler = new GlobalMessageHandler();
        }
        public void HandleMessage(User user, string message_str)
        {
            if (message_str.StartsWith("{") && message_str.EndsWith("}"))
            {
                IRequestMessage message = JsonConvert.DeserializeObject<GenericMessage>(message_str);
                switch (message.target)
                {
                    case "global":
                        {
                            globalMessageHandler.HandleMessage(user, message_str);
                            break;
                        }
                    case "zone":
                        {
                            if (user.Zone != null)
                            {
                                user.Zone.messageHandler.HandleMessage(user, message_str);
                            }
                            else
                            {
                                throw new Exception("User requested to handle a zone message but is not in a zone");
                            }
                            break;
                        }
                    default:
                        {
                            LogManager.GetCurrentClassLogger().Warn($"Unknown message target {message.target}");
                            break;
                        }
                }
            }
            else
            {
                LogManager.GetCurrentClassLogger().Warn($"Malformed message received {message_str}");
            }
        }
    }
}
