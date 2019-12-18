using System;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;

namespace Redfox.Administration.NancySettings
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private readonly IAppConfiguration appConfig;

        public Bootstrapper()
        {
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("swagger-ui", @"swagger-ui")
            );
        }

        public Bootstrapper(IAppConfiguration appConfig)
        {
            this.appConfig = appConfig;
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<IAppConfiguration>(appConfig);
        }


    }
}
