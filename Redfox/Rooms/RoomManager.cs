using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Rooms
{
    public class RoomManager
    {
        private Dictionary<string, Room> rooms;
        public RoomManager()
        {
            this.rooms = new Dictionary<string, Room>();
        }
        public void AddRoom(Room room)
        {
            if (!rooms.ContainsKey(room.name))
            {
                this.rooms.Add(room.name, room);
            }
            else
            {
                throw new Exception($"A room called '{room.name}' already exists in this zone!");
            }
        }
        public void RemoveRoom(Room room)
        {
            if (rooms.ContainsKey(room.name))
            {
                this.rooms.Remove(room.name);
            }
            else
            {
                throw new Exception($"A room called '{room.name}' does not exist in this zone!");
            }
        }
        public Room GetRoom(string roomName)
        {
            if (rooms.ContainsKey(roomName))
            {
                return rooms[roomName];
            }
            else
            {
                throw new Exception("Such room does not exist");
            }
        }
        internal void LeaveRoom(User user)
        {
            if (user.Room != null)
            {
                Room room = user.Room;
                user.Room.Leave(user);
            }
        }
        public void JoinRoom(string roomName, User user)
        {
            if (rooms.ContainsKey(roomName))
            {
                Room room = rooms[roomName];
                this.JoinRoom(room, user);
            }
            else
            {
                throw new Exception("Such room does not exist");
            }
        }
        public void JoinRoom(Room room, User user)
        {
            this.LeaveRoom(user);
            room.Join(user);
        }
    }
}
