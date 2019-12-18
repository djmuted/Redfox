using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Network.NetworkClients
{
    public interface INetworkClient
    {
        public delegate void UserDisconnectedEventHandler();
        public event UserDisconnectedEventHandler UserDisconnected;

        public delegate void DataReceivedEventHandler(string message);
        public event DataReceivedEventHandler DataReceived;

        public abstract void SendData(string data);
        public abstract void Disconnect();

        public virtual void OnUserConnected()
        {
            Core.UserManager.AddUser(this);
        }

    }
}
