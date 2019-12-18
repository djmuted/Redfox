using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Redfox.Administration.NancySettings;
using NLog;

namespace Redfox.Administration
{
    class WebPanel
    {
        IWebHost webHost;
        public WebPanel(string url)
        {
            this.webHost = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel(options =>
                {
                    options.AllowSynchronousIO = true;
                })
                .UseUrls(url)
                .UseStartup<Startup>()
                .Build();
            this.webHost.Start();
            LogManager.GetCurrentClassLogger().Info($"WebPanel started at {url}");
        }
        public void Shutdown()
        {
            this.webHost.StopAsync();
        }
    }
}
