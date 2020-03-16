using NetCoreServer;
using NLog;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Linq;

namespace Redfox.Network.NetworkClients
{
    class TcpNetworkClient : TcpSession, INetworkClient
    {
        public TcpNetworkClient(TcpServer server) : base(server) { }

        public event INetworkClient.DataReceivedEventHandler DataReceived;
        public event INetworkClient.UserDisconnectedEventHandler UserDisconnected;

        protected override void OnConnected()
        {
            ((INetworkClient)this).OnUserConnected();
        }

        protected override void OnDisconnected()
        {
            UserDisconnected?.Invoke();
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            DataReceived?.Invoke(buffer.Take((int)size).ToArray());
        }

        protected override void OnError(SocketError error)
        {
            LogManager.GetCurrentClassLogger().Error($"Caught an error with code {error}");
        }

        public void SendData(string data)
        {
            LogManager.GetCurrentClassLogger().Debug($"Sending data to client {Id}: {data}");
            SendAsync(data + "\0");
        }

        void INetworkClient.Disconnect()
        {
            Disconnect();
        }
    }
}
