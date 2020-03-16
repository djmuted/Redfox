using Fleck;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Network.NetworkClients
{
    class WebSocketNetworkClient : INetworkClient
    {
        private IWebSocketConnection socket;

        public event INetworkClient.DataReceivedEventHandler DataReceived;
        public event INetworkClient.UserDisconnectedEventHandler UserDisconnected;

        public WebSocketNetworkClient(IWebSocketConnection _socket)
        {
            this.socket = _socket;
            this.socket.OnMessage += message => DataReceived?.Invoke(Encoding.UTF8.GetBytes(message));
            this.socket.OnBinary += message => DataReceived?.Invoke(message);
            this.socket.OnClose += () => UserDisconnected?.Invoke();
            ((INetworkClient)this).OnUserConnected();
        }
        public void SendData(string data)
        {
            LogManager.GetCurrentClassLogger().Debug($"Sending data to client: {data}");
            this.socket.Send(data);
        }

        public void Disconnect()
        {
            this.socket.Close();
        }
    }
}
