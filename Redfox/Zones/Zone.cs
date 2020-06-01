using Redfox.Extensions;
using Redfox.Extensions.Events;
using Redfox.Messages;
using Redfox.Rooms;
using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Zones
{
    public class Zone
    {
        public string name;

        private List<User> users;
        public RoomManager RoomManager { get; private set; }
        public ZoneAuthenticator Authenticator { get; private set; }

        internal ZoneMessageHandler messageHandler { get; private set; }

        internal List<RedfoxExtension> extensions;
        internal ExtensionEventManager extensionEventManager;
        public Zone(string _name, List<Type> _extensions)
        {
            this.name = _name;
            this.users = new List<User>();
            this.Authenticator = new DefaultZoneAuthenticator();
            this.RoomManager = new RoomManager();
            this.PrepareExtensions(_extensions);
            this.messageHandler = new ZoneMessageHandler(this.extensions);
        }
        internal void PrepareExtensions(List<Type> _extensions)
        {
            this.extensions = new List<RedfoxExtension>();
            this.extensionEventManager = new ExtensionEventManager();
            if (_extensions != null)
            {
                foreach (Type extensionType in _extensions)
                {
                    Console.WriteLine("Preparing "+extensionType.ToString());
                    RedfoxExtension extension = Activator.CreateInstance(extensionType) as RedfoxExtension;
                    extension.Zone = this;
                    extension.extensionEventManager = extensionEventManager;
                    this.extensions.Add(extension);
                    extension.Initialize();
                }
            }
        }
        internal void Join(User user, string username, string password)
        {
            if (this.users.Contains(user))
            {
                throw new Exception("User already is in this zone");
            }
            else
            {
                bool loginSuccessful = false;
                if (string.IsNullOrEmpty(username))
                {
                    loginSuccessful = this.Authenticator.GuestLogin(user);
                }
                else
                {
                    loginSuccessful = this.Authenticator.Login(user, username, password);
                }
                if (loginSuccessful)
                {
                    Join(user);
                }
            }
        }
        internal void Join(User user)
        {
            if (this.users.Contains(user))
            {
                throw new Exception("User already is in this zone");
            }
            else
            {
                users.Add(user);
                user.Zone = this;
                extensionEventManager.OnZoneJoin(user, this);
            }
        }
        internal void Leave(User user)
        {
            if (!this.users.Contains(user))
            {
                throw new Exception("User is not in this zone");
            }
            else
            {
                extensionEventManager.OnZoneLeave(user, this);
                RoomManager.LeaveRoom(user);
                users.Remove(user);
                user.Zone = null;
            }
        }
        public void SetAuthenticator(ZoneAuthenticator _zoneAuthenticator)
        {
            this.Authenticator = _zoneAuthenticator;
        }
    }
}
