using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using NetCoreServer;
using Redfox.Network.NetworkClients;
using System.Net.Sockets;
using NLog;

namespace Redfox.Network.NetworkServers
{
    class TcpNetworkServer : TcpServer, INetworkServer
    {
        public TcpNetworkServer(int port) : base(IPAddress.Any, port)
        {
            this.Start();
            LogManager.GetCurrentClassLogger().Info("Listening on port " + port);
        }

        protected override TcpSession CreateSession() { return new TcpNetworkClient(this); }

        protected override void OnError(SocketError error)
        {
            LogManager.GetCurrentClassLogger().Error($"Caught an error with code {error}");
        }
    }
}
