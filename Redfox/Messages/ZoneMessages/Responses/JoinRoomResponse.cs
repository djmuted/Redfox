using Redfox.Messages.ZoneMessages.Data;
using Redfox.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Messages.ZoneMessages.Responses
{
    class JoinRoomResponse : IZoneResponseMessage
    {
        public RoomData roomData;
        public JoinRoomResponse(Room room) : base("rfx#jr")
        {
            this.roomData = RoomData.FromRoom(room);
        }
    }
}
