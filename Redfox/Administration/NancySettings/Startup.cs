using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Owin;

namespace Redfox.Administration.NancySettings
{
    class Startup
    {
        private readonly IConfiguration config;

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(env.ContentRootPath);
            config = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {

        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            var appConfig = new AppConfiguration();
            ConfigurationBinder.Bind(config, appConfig);
            app.UseOwin(x => x.UseNancy(opt => opt.Bootstrapper = new Bootstrapper(appConfig)));
        }
    }
}
