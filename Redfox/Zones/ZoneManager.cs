using NLog;
using Redfox.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redfox.Zones
{
    class ZoneManager
    {
        private Dictionary<string, Zone> zones;

        public ZoneManager()
        {
            this.zones = new Dictionary<string, Zone>();
            LogManager.GetCurrentClassLogger().Info("ZoneManager initialized");
        }
        public void AddZone(Zone zone)
        {
            if (this.zones.ContainsKey(zone.name))
            {
                throw new Exception("A zone with specified name already exists");
            }
            else
            {
                this.zones.Add(zone.name, zone);
                LogManager.GetCurrentClassLogger().Info($"New zone {zone.name} added");
            }
        }
        public Zone GetZone(string name)
        {
            if (!this.zones.ContainsKey(name))
            {
                throw new Exception("A zone with specified name does not exist");
            }
            else
            {
                return zones[name];
            }
        }
        public void JoinZone(User user, Zone zone)
        {
            user.Zone?.Leave(user);
            zone.Join(user);
        }
        internal void RaiseZonesReady()
        {
            foreach (Zone zone in zones.Values)
            {
                zone.extensionEventManager.OnZoneReady();
            }
        }
    }
}
