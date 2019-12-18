using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Redfox.Configs
{
    public class ServerConfig
    {
        public bool webpanel_enabled = false;
        public string webpanel_url = "http://localhost:8081/";
        public bool tcp_enabled = true;
        public int tcp_port = 6112;
        public bool websocket_enabled = false;
        public string websocket_url = "ws://0.0.0.0:81/ws";
        public List<ZoneConfig> zones = new List<ZoneConfig>();

        public static ServerConfig FromFile(string path)
        {
            ServerConfig config;
            if (File.Exists(path))
            {
                try
                {
                    config = JsonConvert.DeserializeObject<ServerConfig>(File.ReadAllText(path));
                    LogManager.GetCurrentClassLogger().Info("Server configuration loaded");
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error("Could not read server config: " + ex);
                    Environment.Exit(1);
                    return null;
                }
            }
            else
            {
                LogManager.GetCurrentClassLogger().Warn("Initializing new server configuration");
                config = new ServerConfig();
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(config, Formatting.Indented));
            LogManager.GetCurrentClassLogger().Debug("Server configuration initialized");
            return config;
        }
    }
}
