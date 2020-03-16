using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Redfox.Messages.ZoneMessages.Responses;
using Redfox.Users;

namespace Redfox.Messages.ZoneMessages.Requests
{
    class JoinRoomRequest : IZoneRequestMessage
    {
        [JsonProperty]
        public string roomName;
        public JoinRoomRequest() : base("rfx#jr") { }
        public static JoinRoomRequest Generate(string _roomName)
        {
            JoinRoomRequest msg = new JoinRoomRequest();
            msg.roomName = _roomName;
            return msg;
        }

        public override void Handle(User user)
        {
            user.Zone.RoomManager.JoinRoom(roomName, user);
            user.SendMessage(new JoinRoomResponse(user.Room));
        }
    }
}
