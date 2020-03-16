using Redfox.Rooms;
using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Messages.ZoneMessages.Data
{
    class RoomData
    {
        public int id;
        public string name;
        public List<UserData> users;

        public static RoomData FromRoom(Room room)
        {
            RoomData roomData = new RoomData();
            roomData.id = room.id;
            roomData.name = room.name;
            roomData.users = new List<UserData>();
            foreach (User user in room.users)
            {
                roomData.users.Add(UserData.FromUser(user));
            }
            return roomData;
        }
    }
}
