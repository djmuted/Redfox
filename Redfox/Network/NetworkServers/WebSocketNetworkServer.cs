using Fleck;
using NLog;
using Redfox.Network.NetworkClients;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Network.NetworkServers
{
    class WebSocketNetworkServer : INetworkServer
    {
        private WebSocketServer ws;
        public WebSocketNetworkServer(string url)
        {
            ws = new WebSocketServer(url);
            var logger = LogManager.GetCurrentClassLogger();
            FleckLog.LogAction = (level, message, ex) =>
            {
                switch (level)
                {
                    case Fleck.LogLevel.Debug:
                        //logger.Debug(ex, message);
                        break;
                    case Fleck.LogLevel.Error:
                        logger.Error(ex, message);
                        break;
                    case Fleck.LogLevel.Warn:
                        logger.Warn(ex, message);
                        break;
                    default:
                        logger.Info(ex, message);
                        break;
                }
            };
            ws.Start(socket =>
            {
                socket.OnOpen += () =>
                {
                    WebSocketNetworkClient client = new WebSocketNetworkClient(socket);
                };
            });
        }
    }
}
