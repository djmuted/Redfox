using System;
using System.Collections.Generic;
using System.Text;
using Redfox.Users;

namespace Redfox.Messages.GlobalMessages
{
    class JoinZoneRequest : IGlobalRequestMessage
    {
        public string zoneName { get; private set; }
        public JoinZoneRequest() : base("rfx#jz")
        {
            //this.zoneName = _zoneName;
        }
        public static JoinZoneRequest Generate(string _zoneName)
        {
            JoinZoneRequest ret = new JoinZoneRequest();
            ret.zoneName = _zoneName;
            return ret;
        }
        public override void Handle(User user)
        {
            Core.ZoneManager.GetZone(zoneName)?.Join(user);
        }
    }
}
