using Redfox.Configs;
using Redfox.Messages;
using Redfox.Messages.ZoneMessages.Responses;
using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Rooms
{
    public class Room
    {
        public string name;
        public int maxUsers { get; private set; }
        internal List<User> users;

        public Room(RoomConfig cfg)
        {
            this.maxUsers = cfg.max_users;
            this.name = cfg.room_name;
            this.users = new List<User>();
        }
        public void Join(User user)
        {
            if (users.Contains(user))
            {
                throw new Exception("User already is in this room");
            }
            else if (users.Count >= maxUsers)
            {
                throw new Exception("This room is full");
            }
            else
            {
                user.LeaveRoom();
                users.Add(user);
                user.Room = this;
                user.Room.SendMessage(new UserJoinedRoomResponse(user), user);
                user.Zone.extensionEventManager.OnRoomJoin(user, this);
            }
        }
        public void Leave(User user)
        {
            if (users.Contains(user))
            {
                user.Zone.extensionEventManager.OnRoomLeave(user, this);
                SendMessage(new UserLeftRoomResponse(user), user);
                users.Remove(user);
                user.Room = null;
            }
            else
            {
                throw new Exception("User is not in this room");
            }
        }
        public void SendMessage(IResponseMessage message, User skipUser = null)
        {
            foreach (User user in users)
            {
                if (user != skipUser)
                {
                    user.SendMessage(message);
                }
            }
        }
        public void SetMaxUsers(int maxUsers)
        {
            if (maxUsers >= 0)
            {
                this.maxUsers = maxUsers;
            }
        }
    }
}
