using NLog;
using System;

namespace Redfox
{
    class Program
    {
        static void Main(string[] args)
        {
            var pack = Messages.GlobalMessages.JoinZoneRequest.Generate("chat");
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(pack));
            Core.Boot();
            Console.CancelKeyPress += delegate
            {
                Core.CleanUp();
            };
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
