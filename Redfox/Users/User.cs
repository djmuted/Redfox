using Newtonsoft.Json;
using NLog;
using Redfox.Messages;
using Redfox.Network.NetworkClients;
using Redfox.Rooms;
using Redfox.Users.UserVariables;
using Redfox.Zones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Users
{
    public class User
    {
        private INetworkClient client;

        public int id;
        public string name { get; internal set; }
        public bool IsGuest { get; internal set; }
        public Zone Zone { get; internal set; }
        public Room Room { get; internal set; }
        public Dictionary<string, IUserVariable> UserVariables { get; internal set; }

        public User(INetworkClient _client)
        {
            this.client = _client;
            this.UserVariables = new Dictionary<string, IUserVariable>();
            client.DataReceived += OnDataReceived;
            client.UserDisconnected += OnDisconnected;
            LogManager.GetCurrentClassLogger().Debug($"User connected!");
        }
        private void OnDataReceived(byte[] bytes)
        {
            string message = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            LogManager.GetCurrentClassLogger().Debug($"Data received: " + message);
            if (message.StartsWith("{"))
            {
                try
                {
                    if (!string.IsNullOrEmpty(message))
                        Core.messageHandler.HandleMessage(this, message);
                }
                catch (Exception ex) when (!Env.Debugging)
                {
                    LogManager.GetCurrentClassLogger().Error($"There was an error while handling a message: " + ex.ToString());
                }
            } else
            {
                try
                {
                    this.Zone?.extensionEventManager.OnBinaryData(this, bytes);
                }
                catch when (!Env.Debugging)
                {
                    LogManager.GetCurrentClassLogger().Error($"There was an error while handling binary data. ");
                }
            }
        }
        private void OnDisconnected()
        {
            lock (Core.messageHandler.globalMessageHandler)
            {
                LeaveZone();
                Core.UserManager.RemoveUser(this);
                LogManager.GetCurrentClassLogger().Debug($"User disconnected!");
            }
        }
        public void SendMessage(IResponseMessage message)
        {
            client.SendData(JsonConvert.SerializeObject(message));
        }
        public void JoinZone(Zone zone)
        {
            Core.ZoneManager.JoinZone(this, zone);
        }
        public void JoinRoom(Room room)
        {
            if (this.Zone == null)
            {
                throw new Exception("User is not in any zone!");
            }
            Zone.RoomManager.JoinRoom(room, this);
        }
        public void LeaveRoom()
        {
            this.Room?.Leave(this);
        }
        public void LeaveZone()
        {
            this.Zone?.Leave(this);
        }
    }
}
