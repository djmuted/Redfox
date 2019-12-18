using System;
using Nancy;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;
using Nancy.ModelBinding;

namespace Redfox.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule() : base ("/")
        {
            Get("/", args => "keks");

        }
    }
}
