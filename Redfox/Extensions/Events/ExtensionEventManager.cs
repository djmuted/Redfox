using Redfox.Rooms;
using Redfox.Users;
using Redfox.Zones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Extensions.Events
{
    public class ExtensionEventManager
    {
        public delegate void ZoneReadyEventHandler();
        public delegate void RoomJoinEventHandler(User user, Room room);
        public delegate void RoomLeaveEventHandler(User user, Room room);
        public delegate void ZoneJoinEventHandler(User user, Zone zone);
        public delegate void ZoneLeaveEventHandler(User user, Zone zone);

        public event ZoneReadyEventHandler ZoneReady;
        public event RoomJoinEventHandler RoomJoin;
        public event RoomLeaveEventHandler RoomLeave;
        public event ZoneJoinEventHandler ZoneJoin;
        public event ZoneLeaveEventHandler ZoneLeave;

        public ExtensionEventManager()
        {

        }
        internal virtual void OnZoneReady()
        {
            ZoneReady?.Invoke();
        }
        internal virtual void OnRoomJoin(User user, Room room)
        {
            RoomJoin?.Invoke(user, room);
        }
        internal virtual void OnRoomLeave(User user, Room room)
        {
            RoomLeave?.Invoke(user, room);
        }
        internal virtual void OnZoneJoin(User user, Zone zone)
        {
            ZoneJoin?.Invoke(user, zone);
        }
        internal virtual void OnZoneLeave(User user, Zone zone)
        {
            ZoneLeave?.Invoke(user, zone);
        }
    }
}
