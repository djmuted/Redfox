using NLog;
using Redfox.Configs;
using System;
using System.Collections.Generic;
using System.Text;
using Redfox.Network.NetworkServers;
using Redfox.Administration;
using Redfox.Zones;
using Redfox.Extensions;
using Redfox.Rooms;
using Redfox.Users;
using Redfox.Messages;

namespace Redfox
{
    static class Core
    {
        public static ServerConfig serverConfig { get; private set; }

        private static List<INetworkServer> networkServers = new List<INetworkServer>();
        private static WebPanel webPanel;

        internal static ExtensionManager ExtensionManager;

        public static UserManager UserManager { get; private set; }
        public static ZoneManager ZoneManager { get; private set; }
        internal static MessageHandlerManager messageHandler { get; private set; }
        public static void Boot()
        {
            LogManager.GetCurrentClassLogger().Info("Booting Redfox server...\r\n\r\n  ██████╗ ███████╗██████╗ ███████╗ ██████╗ ██╗  ██╗\r\n  ██╔══██╗██╔════╝██╔══██╗██╔════╝██╔═══██╗╚██╗██╔╝\r\n  ██████╔╝█████╗  ██║  ██║█████╗  ██║   ██║ ╚███╔╝ \r\n  ██╔══██╗██╔══╝  ██║  ██║██╔══╝  ██║   ██║ ██╔██╗ \r\n  ██║  ██║███████╗██████╔╝██║     ╚██████╔╝██╔╝ ██╗\r\n  ╚═╝  ╚═╝╚══════╝╚═════╝ ╚═╝      ╚═════╝ ╚═╝  ╚═╝\r\n                           ___ ___ _ ___ _____ _ _ \r\n                          (_-</ -_) '_\\ V / -_) '_|\r\n                          /__/\\___|_|  \\_/\\___|_|  \r\n");
            Core.serverConfig = ServerConfig.FromFile("Config/Server.json");
            try
            {
                InitializeExtensions();
            }
            catch (Exception ex) when (!Env.Debugging)
            {
                LogManager.GetCurrentClassLogger().Error("There was an error while initializing extensions: " + ex.ToString());
                CleanUp();
            }
            messageHandler = new MessageHandlerManager();
            UserManager = new UserManager();
            try
            {
                InitializeZones();
            }
            catch (Exception ex) when (!Env.Debugging)
            {
                LogManager.GetCurrentClassLogger().Error("There was an error while initializing zones: " + ex.ToString());
                CleanUp();
            }
            try
            {
                InitializeNetwork();
            }
            catch (Exception ex) when (!Env.Debugging)
            {
                LogManager.GetCurrentClassLogger().Error("There was an error while initializing network: " + ex.ToString());
                CleanUp();
            }
            LogManager.GetCurrentClassLogger().Info("Redfox server is ready");
        }
        private static void InitializeExtensions()
        {
            ExtensionManager = new ExtensionManager();
            ExtensionManager.Rescan();
        }
        private static void InitializeZones()
        {
            ZoneManager = new ZoneManager();
            foreach (ZoneConfig zonecfg in serverConfig.zones)
            {
                List<Type> extensions = new List<Type>();
                foreach (string extensionName in zonecfg.zone_extensions)
                {
                    if (ExtensionManager.extensions.ContainsKey(extensionName))
                    {
                        extensions.Add(ExtensionManager.extensions[extensionName]);
                    }
                    else
                    {
                        LogManager.GetCurrentClassLogger().Warn($"Zone '{zonecfg.zone_name}' requires an extension '{extensionName}', which is not installed!");
                    }
                }
                Zone zone = new Zone(zonecfg.zone_name, extensions);
                foreach (RoomConfig roomcfg in zonecfg.zone_rooms)
                {
                    Room room = new Room(roomcfg);
                    zone.RoomManager.AddRoom(room);
                }
                ZoneManager.AddZone(zone);
            }
            ZoneManager.RaiseZonesReady();
        }
        private static void InitializeNetwork()
        {
            if (serverConfig.tcp_enabled)
            {
                networkServers.Add(new TcpNetworkServer(serverConfig.tcp_port));
            }
            if (serverConfig.websocket_enabled)
            {
                networkServers.Add(new WebSocketNetworkServer(serverConfig.websocket_url));
            }
            if (serverConfig.webpanel_enabled)
            {
                if (!serverConfig.websocket_enabled)
                {
                    throw new Exception("WebSocketNetworkServer has to be enabled in order to use the WebPanel");
                }
                webPanel = new WebPanel(serverConfig.webpanel_url);
            }
        }
        public static void CleanUp()
        {
            LogManager.GetCurrentClassLogger().Info("Shutting down...");

            Environment.Exit(0);
        }
    }
}
